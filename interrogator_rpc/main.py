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
		if ACTIVE_INTERROGATOR and ACTIVE_INTERROGATOR != network_name:
			interrogator.INTERROGATOR_MAP[ACTIVE_INTERROGATOR].stop()

		intg = interrogator.INTERROGATOR_MAP[network_name]
		intg.start()

		tags = intg.predict(image_obj)

		return tags

class InterrogatorServicer(rpc_proto.services_pb2_grpc.ImageInterrogatorServicer):
	"""Provides methods that implement functionality of route guide server."""

	def __init__(self):
		pass

	def ListInterrogators(self, request, context):
		return rpc_proto.services_pb2.InterrogatorListing(
				interrogator_names = list(interrogator.INTERROGATOR_MAP.keys())
			)


	def InterrogateImage(self, request, context):

		if request.interrogator_network not in interrogator.INTERROGATOR_MAP:

			ret = rpc_proto.services_pb2.ImageTagResults(
					interrogate_ok = False,
					error_msg      = "Image Interrogator named '%s' is not a valid interrogator name. Known interrogators: '%s'" % (
						request.interrogator_network, list(interrogator.INTERROGATOR_MAP.keys())
						)
				)
			return ret

		tag_listing = []
		try:

			image_bytes = request.interrogate_image

			image_obj = Image.open(io.BytesIO(image_bytes))

			tag_listing = interrogate_image(request.interrogator_network, image_obj)

		except Exception as e:
			ret = rpc_proto.services_pb2.ImageTagResults(
					interrogate_ok = False,
					error_msg      = str(e),
				)
			return ret


		ret = rpc_proto.services_pb2.ImageTagResults()

		ret.interrogate_ok = True

		if isinstance(tag_listing, dict):
			for tag, probability in tag_listing.items():
				if probability > request.interrogator_threshold:
					tag_obj = rpc_proto.services_pb2.TagEntry(
							tag         = tag,
							probability = probability,
						)
					ret.tags.append(tag_obj)

		elif isinstance(tag_listing, (list, tuple)):
			for tag in tag_listing:
				tag_obj = rpc_proto.services_pb2.TagEntry(
						tag         = tag,
						probability = 1,
					)
				ret.tags.append(tag_obj)
		else:

			ret.interrogate_ok = False
			ret.error_msg = "Invalid type for tag return: %s" % (type(tag_listing), )


		return ret



def serve():
	maxMsgLength = 1024 * 1024 * 1024

	server = grpc.server(futures.ThreadPoolExecutor(max_workers=10),
					 options=[('grpc.max_message_length', maxMsgLength),
							  ('grpc.max_send_message_length', maxMsgLength),
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
