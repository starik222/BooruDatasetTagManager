
import sys
import os
import os.path
import grpc
import tqdm
from PIL import Image
from google.protobuf import empty_pb2 as google_dot_protobuf_dot_empty__pb2

import interrogator


import rpc_proto.services_pb2
import rpc_proto.services_pb2_grpc




def go(test_images):

	maxMsgLength = 1024 * 1024 * 1024
	channel = grpc.insecure_channel('localhost:50051',
								options=[('grpc.max_message_length', maxMsgLength),
										 ('grpc.max_send_message_length', maxMsgLength),
										 ('grpc.max_receive_message_length', maxMsgLength)])

	stub = rpc_proto.services_pb2_grpc.ImageInterrogatorStub(channel)

	print(stub)
	null_req = rpc_proto.services_pb2.InterrogatorListingRequest()
	ret = stub.ListInterrogators(null_req)


	networks = []
	for name in ret.interrogator_names:

		if "blip" in name.lower():
			continue
		if "GIT" in name:
			continue

		print(name)

		networks.append(name)

	for filename in tqdm.tqdm(test_images):


		with open(filename, "rb") as fp:
			im_bytes = fp.read()

		interrogate_req = rpc_proto.services_pb2.InterrogationRequest(
			params = [
				rpc_proto.services_pb2.NetworkInterrogationParameters(
					interrogator_network   = name,
					interrogator_threshold = 0.1,
				) for name in networks
			],
			interrogate_image      = im_bytes,

			)

		ret = stub.InterrogateImage(interrogate_req)

		print("Result: ", ret)


	# for in_name, intg in interrogator.INTERROGATOR_MAP.items():
	# 	print(in_name)

	# 	# if "danboor" in in_name.lower():
	# 	# 	continue


	# 	intg.start()

	# 	for filename in tqdm.tqdm(test_images):
	# 		im  = Image.open(filename)
	# 		# print(im)

	# 		results = intg.predict(im)
	# 		# print(results)
	# 		# print()


	# 	intg.stop()

	print("Ran all interrogators!")

if __name__ == "__main__":

	if len(sys.argv) < 2:
		print("You need to specify a directory containing images")
		sys.exit(-1)


	fdir = sys.argv[1]

	if not os.path.exists(fdir):
		print("Passed directory ('%s') does not exist!" % (fdir, ))
		sys.exit(-2)

	if not os.path.isdir(fdir):

		print("Passed directory ('%s') is not a directory!" % (fdir, ))
		sys.exit(-2)

	image_exts = [
		".jpg",
		".jpeg",
		".png",
		".bmp",
		".webp",
	]

	images = []
	for filename in os.listdir(fdir):
		if any([filename.lower().endswith(fext) for fext in image_exts]):
			images.append(os.path.join(fdir, filename))

	print("Found %s images to test with." % (len(images), ))
	go(images)



