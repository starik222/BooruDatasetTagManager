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

ACTIVE_INTERROGATOR                = None
ONE_INTERROGATOR_IN_VRAM_AT_A_TIME = False

DEBUG = True
Unprompted = None

# Whooooo, there's a bunch of gross global state here.
# I blame finite GPUs and gRPC being kind of crap.
def interrogate_image(network_name, image_obj, skip_online):
	global ACTIVE_INTERROGATOR
	global ONE_INTERROGATOR_IN_VRAM_AT_A_TIME

	# This is /maybe/ not needed, I'm not sure how multiple simultaneous usage would work. If multiple
	# threads can use the same network loaded into VRAM, this could potentially be removed. Something
	# to look into.
	with INTERROGATOR_LOCK: 
		if ONE_INTERROGATOR_IN_VRAM_AT_A_TIME and ACTIVE_INTERROGATOR and ACTIVE_INTERROGATOR != network_name:
			print("Unloading interrogator %s" % (ACTIVE_INTERROGATOR, ))

			# So one concern here is that if there are multiple other networks loaded, and we then
			# get a call which requests serialization (e.g. for a single very large network),
			# only the last used network will be unloaded. To fix this, we iterate over every network potentially
			# in ram and just speculatively unload it (except the one we want, in case it is loaded).
			for interrogator_name, interg in interrogator.INTERROGATOR_MAP.items():
				if interrogator_name != network_name:
					interg.stop()

		if ONE_INTERROGATOR_IN_VRAM_AT_A_TIME and ACTIVE_INTERROGATOR != network_name:
			print("Need to load network", network_name)

		ACTIVE_INTERROGATOR = network_name
		intg = interrogator.INTERROGATOR_MAP[network_name]
		intg.start(skip_online=skip_online)

		tags = intg.predict(image_obj)

		return tags


def import_file(full_name, path):
	"""Allows importing of modules from full filepath, not sure why Python requires a helper function for this in 2023"""
	from importlib import util

	spec = util.spec_from_file_location(full_name, path)
	mod = util.module_from_spec(spec)

	spec.loader.exec_module(mod)
	return mod

def extract_tag_ret(tags_in, threshold, image_obj):
	global DEBUG, Unprompted

	def do_unprompted(string,context=None):
		if Unprompted:
			if context: Unprompted.shortcode_user_vars["context"] = str(context)
			result = Unprompted.start(str(string),debug=False)
			return result
		else: return string

	# Load middleman.json object for interrogator
	import json, os
	if os.path.isfile("middleman.json"):
		if DEBUG: print("Running Middleman v0.2.0")
		middleman = json.loads(open("middleman.json", "r").read())
		blacklist_tags = middleman["_BLACKLIST_TAGS"] if "_BLACKLIST_TAGS" in middleman else []
		whitelist_tags = middleman["_WHITELIST_TAGS"] if "_WHITELIST_TAGS" in middleman else []
		debug_tags = middleman["_DEBUG_TAGS"] if "_DEBUG_TAGS" in middleman else []

		if "_UNPROMPTED" in middleman and not Unprompted:
			try:
				import sys
				sys.path.insert(0, f"{middleman['_UNPROMPTED']}")
				unprompted_path = f"{middleman['_UNPROMPTED']}/lib_unprompted/shared.py"
				shared = import_file("unprompted", unprompted_path)
				Unprompted = shared.Unprompted(base_dir=f"{middleman['_UNPROMPTED']}")
			except Exception as e:
				print("Failed to load Unprompted:", e)
		if Unprompted:
			Unprompted.shortcode_user_vars = {}
			Unprompted.shortcode_user_vars["threshold"] = str(threshold)
			# Load tags into Unprompted variables
			if isinstance(tags_in, dict):
				for tag, confidence in tags_in.items():
					Unprompted.shortcode_user_vars[tag] = confidence
			
			# Ensure that the image is PIL.Image.Image - necessary for pyiqa library
			# (The library does not handle PIL subtypes correctly)
			image = Image.new(image_obj.mode, image_obj.size)
			image.putdata(list(image_obj.getdata()))
			Unprompted.shortcode_user_vars["default_image"] = image

			if "_INIT" in middleman: do_unprompted(middleman["_INIT"])
	else:
		middleman = {}
		blacklist_tags = []
	
	ret = {}

	if isinstance(tags_in, dict):
		for tag, probability in tags_in.items():
			if tag in debug_tags or (tag in middleman and "debug" in middleman[tag]):
				print(f"(DEBUG) Tag `{tag}` probability: {probability}")
				del debug_tags[debug_tags.index(tag)]
			if tag in blacklist_tags:
				if DEBUG: print(f"Tag `{tag}` blacklisted, skipping.")
				del blacklist_tags[blacklist_tags.index(tag)]
				continue
			if whitelist_tags and tag not in whitelist_tags:
				if DEBUG: print(f"Tag `{tag}` not in whitelist, skipping.")
				del whitelist_tags[whitelist_tags.index(tag)]
				continue
			# Check if this tag is defined in the middleman
			if tag in middleman:
				try:
					# Re-initialize Unprompted vars
					if Unprompted:
						# Pass our starting values into Unprompted
						Unprompted.shortcode_user_vars["tag"] = str(tag)
						Unprompted.shortcode_user_vars["confidence"] = str(probability)
					
					if ("ban" in middleman[tag] and int(do_unprompted(middleman[tag]["ban"],"ban"))):
						if DEBUG: print(f"Tag `{tag}` banned, skipping.")
						continue

					new_tag_name = do_unprompted(middleman[tag]["name"],"name") if "name" in middleman[tag] else tag
					new_tag_confidence = float(do_unprompted(middleman[tag]["pre"],"pre")) if "pre" in middleman[tag] else probability

					new_threshold = float(do_unprompted(middleman[tag]["threshold"],"threshold")) if "threshold" in middleman[tag] else threshold

					if new_tag_confidence > new_threshold:
						if "post" in middleman[tag]:
							new_tag_confidence = float(do_unprompted(middleman[tag]["post"],"confidence"))

						ret[new_tag_name] = new_tag_confidence

						if "aliases" in middleman[tag]:
							all_aliases = middleman[tag]["aliases"]
							alias_multiplier = float(do_unprompted(middleman[tag]["alias_multiplier"],"confidence")) if "alias_multiplier" in middleman[tag] else 0.5
							next_confidence = new_tag_confidence * alias_multiplier
							if isinstance(all_aliases, str): all_aliases = [all_aliases]

							for alias in all_aliases:
								alias = do_unprompted(alias)
								ret[alias] = next_confidence
								next_confidence = next_confidence * alias_multiplier
						if "excludes" in middleman[tag]:
							all_excludes = middleman[tag]["excludes"]
							for exclude in all_excludes:
								exclude = do_unprompted(exclude)
								if exclude in ret: del ret[exclude]
								elif exclude not in blacklist_tags:
									blacklist_tags.append(exclude)

						if DEBUG:
							if tag != new_tag_name: print(f"Tag `{tag}` renamed to `{new_tag_name}`")
							if probability != new_tag_confidence: print(f"Tag `{tag}` confidence changed from {probability} to {new_tag_confidence}")
					
					# Update Unprompted tag variable
					if Unprompted:
						Unprompted.shortcode_user_vars[new_tag_name] = new_tag_confidence
				
				except Exception as e:
					print(f"Exception occurred while processing tag `{tag}`:", e)
					if probability > threshold: ret[tag] = probability
			elif probability > threshold:
				ret[tag] = probability

	elif isinstance(tags_in, (list, tuple)):
		for tag in tags_in:
			ret[tag] = 1

	else:
		raise RuntimeError("Tags must either be a list or a dict")

	if Unprompted:
		# Process extra tags from user variables
		new_tag_prefix = "tag_"
		for key, value in Unprompted.shortcode_user_vars.items():
			if key.startswith(new_tag_prefix):
				tag_name = key[len(new_tag_prefix):].replace("_"," ")
				if not value:
					if tag_name in ret: del ret[tag_name]
				else: ret[tag_name] = float(value)
		
		Unprompted.cleanup()

	if len(debug_tags): print(f"(WARNING) Debug tags {debug_tags} are invalid!")
	if len(blacklist_tags): print(f"(WARNING) Blacklist tags {blacklist_tags} are invalid!")
	if len(whitelist_tags): print(f"(WARNING) Whitelist tags {whitelist_tags} are invalid!")

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


		global ONE_INTERROGATOR_IN_VRAM_AT_A_TIME
		ONE_INTERROGATOR_IN_VRAM_AT_A_TIME = request.serialize_vram_usage

		ret = rpc_proto.services_pb2.ImageTagResults()

		try:

			image_bytes = request.interrogate_image

			image_obj = Image.open(io.BytesIO(image_bytes))

			for network_conf in request.params:

				tag_ret = interrogate_image(network_conf.interrogator_network, image_obj, skip_online=request.skip_internet_requests)
				network_tags = extract_tag_ret(tag_ret, network_conf.interrogator_threshold, image_obj)

				net_resp = rpc_proto.services_pb2.InterrogationResponse()
				net_resp.network_name = network_conf.interrogator_network

				for tag, probability in network_tags.items():

					tag_obj = rpc_proto.services_pb2.TagEntry(
							tag         = tag,
							probability = probability,
						)
					net_resp.tags.append(tag_obj)

				ret.responses.append(net_resp)

		except Exception as e:
			global ACTIVE_INTERROGATOR
			if "out of memory" in str(e).lower() and len(request.params) > 1 and not ONE_INTERROGATOR_IN_VRAM_AT_A_TIME:
				# If we ran out of VRAM, and are trying to load multiple interrogators at once, retry
				# without keeping all interrogators in memory.
				# Yes, this uses globals.
				print("Ran out of VRAM while trying to perform image interrogation. Retrying without")
				print("keeping all interrogator networks in VRAM simultaneously.")
				ONE_INTERROGATOR_IN_VRAM_AT_A_TIME = True

				# unload all the interrogators.
				# CUDA is annoying and can still be propagating errors from an earlier OOM
				# when we get to this point (funky async magic).
				# Anyways, retry a few times.
				for int_tmp in interrogator.INTERROGATOR_MAP.values():
					for _ in range(3):
						try:
							int_tmp.stop()
						except:
							pass

				ACTIVE_INTERROGATOR = None

				return self.InterrogateImage(request, context)

			ret = rpc_proto.services_pb2.ImageTagResults(
					interrogate_ok = False,
					error_msg      = str(e),
				)
			print("Exception processing item!")
			print("Exception string: '%s'" % (str(e), ))
			traceback.print_exc()
			return ret



		ret.interrogate_ok = True
		ret.error_msg = "Image successfully processed."


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
	logging.basicConfig(level = logging.INFO)

	interrogator.init()
	print(rpc_proto.services_pb2.InterrogatorListing)

	serve()
