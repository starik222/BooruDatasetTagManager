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
from concurrent import futures

import grpc

import interrogator

import rpc_proto.services_pb2
import rpc_proto.services_pb2_grpc

INTERROGATOR_LOCK = threading.Lock()


class InterrogatorServicer(rpc_proto.services_pb2_grpc.ImageInterrogatorServicer):
	"""Provides methods that implement functionality of route guide server."""

	def __init__(self):
		pass

	def ListInterrogators(self, request, context):
		return rpc_proto.services_pb2.InterrogatorListing(
				interrogator_names = interrogator.INTERROGATOR_NAMES
			)


	def InterrogateImage(self, request, context):
		"""Missing associated documentation comment in .proto file."""
		context.set_code(grpc.StatusCode.UNIMPLEMENTED)
		context.set_details('Method not implemented!')

		tag_listing = []
		try:
			pass
		except Exception as e:
			ret = rpc_proto.services_pb2.InterrogatorListing.ImageTagResults(
					interrogate_ok = False,
					error_msg      = str(e),
				)
			return ret


		ret = rpc_proto.services_pb2.InterrogatorListing.ImageTagResults()

		ret.interrogate_ok = True

		if isinstance(tag_listing, dict):
			for tag, probability in tag_listing.items():
				tag_obj = rpc_proto.services_pb2.InterrogatorListing.TagEntry(
						tag         = tag,
						probability = probability,
					)
				ret.tags.append(tag_obj)
			pass
		elif isinstance(tag_listing, (list, tuple)):
			for tag in tag_listing:
				tag_obj = rpc_proto.services_pb2.InterrogatorListing.TagEntry(
						tag         = tag,
						probability = 1,
					)
				ret.tags.append(tag_obj)
		else:

			ret.interrogate_ok = False
			ret.error_msg = "Invalid type for tag return: %s" % (type(tag_listing), )


		return ret



def serve():
	server = grpc.server(futures.ThreadPoolExecutor(max_workers=10))
	rpc_proto.services_pb2_grpc.add_ImageInterrogatorServicer_to_server(
		InterrogatorServicer(), server
	)
	server.add_insecure_port("[::]:50051")
	server.start()
	server.wait_for_termination()


if __name__ == "__main__":
	logging.basicConfig()
	serve()
