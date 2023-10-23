# Copyright 2015 gRPC authors.
#
# Licensed under the Apache License, Version 2.0 (the "License");
# you may not use this file except in compliance with the License.
# You may obtain a copy of the License at
#
#     http://www.apache.org/licenses/LICENSE-2.0
#
# Unless required by applicable law or agreed to in writing, software
# distributed under the License is distributed on an "AS IS" BASIS,
# WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
# See the License for the specific language governing permissions and
# limitations under the License.
"""The Python implementation of the gRPC route guide server."""

import logging
import threading
import tqdm
import traceback
import time
import io
from PIL import Image
from concurrent import futures

import grpc

import interrogator

import rpc_proto.services_pb2
import rpc_proto.services_pb2_grpc

INTERROGATOR_LOCK = threading.Lock()

ACTIVE_INTERROGATOR = None

def interrogate_image(network_name, image_obj): 
	global ACTIVE_INTERROGATOR
	with INTERROGATOR_LOCK: 

		intg = interrogator.INTERROGATOR_MAP[network_name]
		intg.start()

		tags = intg.predict(image_obj)

		return tags


def extract_tag_ret(tags_in, threshold):

	ret = {}

	if isinstance(tags_in, dict):
		for tag, probability in tags_in.items():
			if probability > threshold:
				ret[tag] = probability

	elif isinstance(tags_in, (list, tuple)):
		for tag in tags_in:
			ret[tag] = 1

	else:
		raise RuntimeError("Tags must either be a list or a dict")

	return ret


class InterrogatorServicer(rpc_proto.services_pb2_grpc.ImageInterrogatorServicer):
	"""Provides methods that implement functionality of route guide server."""

	def __init__(self):
		pass

	def ListInterrogators(self, request, context):
		return rpc_proto.services_pb2.InterrogatorListing(
				interrogator_names = list(interrogator.INTERROGATOR_MAP.keys())
			)


	def InterrogateImage(self, request, context):

		print("Interrogate Request!")
		print("Network: ", request.params)
		print("File size: ", len(request.interrogate_image))
		for network_conf in request.params:
			if network_conf.interrogator_network not in interrogator.INTERROGATOR_MAP:

				ret = rpc_proto.services_pb2.ImageTagResults(
						interrogate_ok = False,
						error_msg      = "Image Interrogator named '%s' is not a valid interrogator name. Known interrogators: '%s'" % (
							network_conf.interrogator_network, list(interrogator.INTERROGATOR_MAP.keys())
							)
					)
				print(ret.error_msg)
				return ret

		if not request.interrogate_image:
			ret = rpc_proto.services_pb2.ImageTagResults(
					interrogate_ok = False,
					error_msg      = "Interrogate Failed - Received no image!"
				)
			print(ret.error_msg)
			return ret


		tag_listing = {}
		try:

			image_bytes = request.interrogate_image

			image_obj = Image.open(io.BytesIO(image_bytes))

			for network_conf in request.params:
				tag_ret = interrogate_image(network_conf.interrogator_network, image_obj)
				network_tags = extract_tag_ret(tag_ret, network_conf.interrogator_threshold)

				for tag, probability in network_tags.items():
					if probability > network_conf.interrogator_threshold:
						tag_listing[tag] = probability

		except Exception as e:
			ret = rpc_proto.services_pb2.ImageTagResults(
					interrogate_ok = False,
					error_msg      = str(e),
				)
			print("Exception processing item!")
			traceback.print_exc()
			return ret


		ret = rpc_proto.services_pb2.ImageTagResults()

		ret.interrogate_ok = True

		# I want to return the tags in sorted order. This is kind of silly, but w/e
		tag_listing = [(tag, probability) for tag, probability in tag_listing.items()]
		tag_listing.sort()

		for tag, probability in tag_listing:
			tag_obj = rpc_proto.services_pb2.TagEntry(
					tag         = tag,
					probability = probability,
				)
			ret.tags.append(tag_obj)


		ret.error_msg = "Image successfully processed. Deduced %s tags." % (len(ret.tags), )


		print(ret.error_msg)

		return ret



def serve():
	maxMsgLength = 1024 * 1024 * 1024

	server = grpc.server(futures.ThreadPoolExecutor(max_workers=10),
					 options=[('grpc.max_message_length',         maxMsgLength),
							  ('grpc.max_send_message_length',    maxMsgLength),
							  ('grpc.max_receive_message_length', maxMsgLength)]
		)
	rpc_proto.services_pb2_grpc.add_ImageInterrogatorServicer_to_server(
		InterrogatorServicer(), server
	)
	server.add_insecure_port("[::]:50051")
	server.start()
	server.wait_for_termination()


if __name__ == "__main__":
	logging.basicConfig(level = 1)

	print(rpc_proto.services_pb2.InterrogatorListing)

	serve()
