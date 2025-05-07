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

import collections

import grpc

import interrogator
from ext_kohya import utilities

import rpc_proto.services_pb2
import rpc_proto.services_pb2_grpc

INTERROGATOR_LOCK = threading.Lock()

ACTIVE_INTERROGATOR = None
ONE_INTERROGATOR_IN_VRAM_AT_A_TIME = False


# Whooooo, there's a bunch of gross global state here.
# I blame finite GPUs and gRPC being kind of crap.
def interrogate_image(network_name, image_obj, net_params, skip_online):
    global ACTIVE_INTERROGATOR
    global ONE_INTERROGATOR_IN_VRAM_AT_A_TIME

    # This is /maybe/ not needed, I'm not sure how multiple simultaneous usage would work. If multiple
    # threads can use the same network loaded into VRAM, this could potentially be removed. Something
    # to look into.
    with INTERROGATOR_LOCK:
        if ONE_INTERROGATOR_IN_VRAM_AT_A_TIME and ACTIVE_INTERROGATOR and ACTIVE_INTERROGATOR != network_name:
            print("Unloading interrogator %s" % (ACTIVE_INTERROGATOR,))

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
        intg.start(net_params, skip_online=skip_online)

        tags = intg.predict(image_obj)

        return tags

def edit_image(network_name, image_obj, net_params, skip_online):
    global ACTIVE_INTERROGATOR
    global ONE_INTERROGATOR_IN_VRAM_AT_A_TIME

    # This is /maybe/ not needed, I'm not sure how multiple simultaneous usage would work. If multiple
    # threads can use the same network loaded into VRAM, this could potentially be removed. Something
    # to look into.
    with INTERROGATOR_LOCK:
        if ONE_INTERROGATOR_IN_VRAM_AT_A_TIME and ACTIVE_INTERROGATOR and ACTIVE_INTERROGATOR != network_name:
            print("Unloading editor %s" % (ACTIVE_INTERROGATOR,))

            # So one concern here is that if there are multiple other networks loaded, and we then
            # get a call which requests serialization (e.g. for a single very large network),
            # only the last used network will be unloaded. To fix this, we iterate over every network potentially
            # in ram and just speculatively unload it (except the one we want, in case it is loaded).
            for interrogator_name, interg in interrogator.EDITOR_MAP.items():
                if interrogator_name != network_name:
                    interg.stop()

        if ONE_INTERROGATOR_IN_VRAM_AT_A_TIME and ACTIVE_INTERROGATOR != network_name:
            print("Need to load network", network_name)

        ACTIVE_INTERROGATOR = network_name
        intg = interrogator.EDITOR_MAP[network_name]
        intg.start(net_params, skip_online=skip_online)

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


def createInterrogatorParameter(key, value, type, comment):
    return rpc_proto.services_pb2.AdditionalNetworkParameter(
        key=key,
        value=value,
        type=type,
        comment=comment
    )


def createDictFromAdditionalParameters(paramList):
    result = {}
    for param in paramList:
        if param.type == "float1":
            result[param.key] = float(param.value)
        elif param.type == "string":
            result[param.key] = param.value
        elif param.type == "list":
            result[param.key] = param.value
        elif param.type == "bool":
            result[param.key] = param.value.lower()
    return result


class InterrogatorServicer(rpc_proto.services_pb2_grpc.ImageInterrogatorServicer):
    """Provides methods that implement functionality of route guide server."""

    def __init__(self):
        pass

    def ListInterrogators(self, request, context):
        return rpc_proto.services_pb2.InterrogatorListing(
            interrogator_names=list(interrogator.INTERROGATOR_MAP.keys())
        )
    def ListEditors(self, request, context):
        return rpc_proto.services_pb2.InterrogatorListing(
            interrogator_names=list(interrogator.EDITOR_MAP.keys())
        )

    def ListModelsByType(self, request, context):
        model_type = request.model_type
        ret = rpc_proto.services_pb2.InterrogatorListing()
        for model in interrogator.INTERROGATORS:
            if model.type == model_type:
                ret.interrogator_names.append(model.name())
        for model in interrogator.EDITORS:
            if model.type == model_type:
                ret.interrogator_names.append(model.name())
        return ret

    def InterrogatorParameters(self, request, context):
        ret = rpc_proto.services_pb2.InterrogatorParamResponse(
            ErrMes="",
            Result=True
        )
        if request.interrogator_network not in interrogator.INTERROGATOR_MAP:
            ret.Result = False
            ret.ErrMes = "Interrogator not found!"
            print(ret.ErrMes)
            return ret
        intInstance = interrogator.INTERROGATOR_MAP[request.interrogator_network]
        if intInstance.type == "wd":
            ret.Type = "wd"
            ret.Parameters.append(createInterrogatorParameter("threshold", str(intInstance.threshold), "float1", ""))
        elif intInstance.type == "florence2":
            ret.Type = "florence2"
            ret.Parameters.append(createInterrogatorParameter("cmd", (",").join(intInstance.commands), "list", ""))
            ret.Parameters.append(createInterrogatorParameter("prompt", intInstance.defaultPrompt, "string", ""))
            ret.Parameters.append(createInterrogatorParameter("split", "false", "bool", "Split lines with commas"))
            ret.Parameters.append(createInterrogatorParameter("Comment",
                                                              "The \"prompt\" field is only used with the "
                                                              "<CAPTION_TO_PHRASE_GROUNDING> command.",
                                                              "label", ""))
        elif intInstance.type == "moondream2":
            ret.Type = "moondream2"
            ret.Parameters.append(createInterrogatorParameter("cmd", (",").join(intInstance.commands), "list", ""))
            ret.Parameters.append(createInterrogatorParameter("query", intInstance.defaultPrompt, "string", ""))
            ret.Parameters.append(createInterrogatorParameter("split", "false", "bool", "Split lines with commas"))
            ret.Parameters.append(createInterrogatorParameter("Comment",
                                                              "Only \"Short_caption\", \"Normal_caption\", \"Visual_query\"\n"
                                                              "are used to create captions.\nThe \"query\" field is "
                                                              "only used with \"Visual_query\".",
                                                              "label", ""))
        elif intInstance.type == "joycaption":
            ret.Type = "joycaption"
            #ret.Parameters.append(createInterrogatorParameter("cmd", (",").join(intInstance.commands), "list", ""))
            ret.Parameters.append(createInterrogatorParameter("query", intInstance.defaultPrompt, "string", ""))
            ret.Parameters.append(createInterrogatorParameter("split", "false", "bool", "Split lines with commas"))
            ret.Parameters.append(createInterrogatorParameter("Comment",
                                                              "You can see examples of requests on the project page "
                                                              "https://github.com/fpgaminer/joycaption",
                                                              "label", ""))
        elif intInstance.type == "qwen25":
            ret.Type = "qwen25"
            #ret.Parameters.append(createInterrogatorParameter("cmd", (",").join(intInstance.commands), "list", ""))
            ret.Parameters.append(createInterrogatorParameter("query", intInstance.defaultPrompt, "string", ""))
            ret.Parameters.append(createInterrogatorParameter("split", "false", "bool", "Split lines with commas"))
            ret.Parameters.append(createInterrogatorParameter("Comment",
                                                              "You can see examples of requests on the project page "
                                                              "https://github.com/fpgaminer/joycaption",
                                                              "label", ""))
        elif intInstance.type == "blip":
            ret.Type = "blip"
        elif intInstance.type == "blip2":
            ret.Type = "blip2"
        elif intInstance.type == "gitlarge":
            ret.Type = "gitlarge"
        elif intInstance.type == "dd":
            ret.Type = "dd"
        return ret

    def EditorParameters(self, request, context):
        ret = rpc_proto.services_pb2.InterrogatorParamResponse(
            ErrMes="",
            Result=True
        )
        if request.interrogator_network not in interrogator.EDITOR_MAP:
            ret.Result = False
            ret.ErrMes = "Interrogator not found!"
            print(ret.ErrMes)
            return ret
        intInstance = interrogator.EDITOR_MAP[request.interrogator_network]
        if intInstance.type == "rmbg2":
            ret.Type = "rmbg2"
        return ret

    def InterrogateImage(self, request, context):

        print("Interrogate Request!")
        print("Network: ", request.params)
        print("File name: ", request.image_name)
        print("File size: ", len(request.interrogate_image))
        for network_conf in request.params:
            if network_conf.interrogator_network not in interrogator.INTERROGATOR_MAP:
                ret = rpc_proto.services_pb2.ImageTagResults(
                    interrogate_ok=False,
                    error_msg="Image Interrogator named '%s' is not a valid interrogator name. Known interrogators: '%s'" % (
                        network_conf.interrogator_network, list(interrogator.INTERROGATOR_MAP.keys())
                    )
                )
                print(ret.error_msg)
                return ret

        if not request.interrogate_image:
            ret = rpc_proto.services_pb2.ImageTagResults(
                interrogate_ok=False,
                error_msg="Interrogate Failed - Received no image!"
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
                paramDict = createDictFromAdditionalParameters(network_conf.AdditionalParameters)
                threshold = 1.0
                if 'threshold' in paramDict:
                    threshold = paramDict['threshold']
                tag_ret = interrogate_image(network_conf.interrogator_network, image_obj, paramDict,
                                            skip_online=request.skip_internet_requests)
                network_tags = extract_tag_ret(tag_ret, threshold)

                net_resp = rpc_proto.services_pb2.InterrogationResponse()
                net_resp.network_name = network_conf.interrogator_network

                for tag, probability in network_tags.items():
                    tag_obj = rpc_proto.services_pb2.TagEntry(
                        tag=tag,
                        probability=probability,
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
                interrogate_ok=False,
                error_msg=str(e),
            )
            print("Exception processing item!")
            print("Exception string: '%s'" % (str(e),))
            traceback.print_exc()
            return ret

        ret.interrogate_ok = True
        ret.error_msg = "Image successfully processed."

        print(ret.error_msg)

        return ret

    def EditImage(self, request, context):

        print("Editor Request!")
        print("Network: ", request.params)
        print("File name: ", request.image_name)
        print("File size: ", len(request.interrogate_image))
        for network_conf in request.params:
            if network_conf.interrogator_network not in interrogator.EDITOR_MAP:
                ret = rpc_proto.services_pb2.ImageEditResult(
                    result=False,
                    error_msg="Image Interrogator named '%s' is not a valid interrogator name. Known interrogators: '%s'" % (
                        network_conf.interrogator_network, list(interrogator.EDITOR_MAP.keys())
                    )
                )
                print(ret.error_msg)
                return ret

        if not request.interrogate_image:
            ret = rpc_proto.services_pb2.ImageEditResult(
                result=False,
                error_msg="Edit Failed - Received no image!"
            )
            print(ret.error_msg)
            return ret

        global ONE_INTERROGATOR_IN_VRAM_AT_A_TIME
        ONE_INTERROGATOR_IN_VRAM_AT_A_TIME = request.serialize_vram_usage

        ret = rpc_proto.services_pb2.ImageEditResult()

        try:

            image_bytes = request.interrogate_image

            image_obj = Image.open(io.BytesIO(image_bytes))

            for network_conf in request.params:
                paramDict = createDictFromAdditionalParameters(network_conf.AdditionalParameters)
                img_ret = edit_image(network_conf.interrogator_network, image_obj, paramDict,
                                            skip_online=request.skip_internet_requests)
                format = img_ret.format
                mode = img_ret.mode
                #Removing alpha channel for bmp and jpg formats
                if format in ('JPEG', 'BMP') and mode == 'RGBA':
                    img_ret = utilities.remove_transparency(img_ret).convert("RGB")
                imgByteArr = io.BytesIO()
                img_ret.save(imgByteArr, format=format)
                imgByteArr = imgByteArr.getvalue()
                ret.edited_image = imgByteArr
                img_ret.close()

        except Exception as e:
            global ACTIVE_INTERROGATOR
            if "out of memory" in str(e).lower() and len(request.params) > 1 and not ONE_INTERROGATOR_IN_VRAM_AT_A_TIME:
                # If we ran out of VRAM, and are trying to load multiple interrogators at once, retry
                # without keeping all interrogators in memory.
                # Yes, this uses globals.
                print("Ran out of VRAM while trying to perform image edition. Retrying without")
                print("keeping all interrogator networks in VRAM simultaneously.")
                ONE_INTERROGATOR_IN_VRAM_AT_A_TIME = True

                # unload all the interrogators.
                # CUDA is annoying and can still be propagating errors from an earlier OOM
                # when we get to this point (funky async magic).
                # Anyways, retry a few times.
                for int_tmp in interrogator.EDITOR_MAP.values():
                    for _ in range(3):
                        try:
                            int_tmp.stop()
                        except:
                            pass

                ACTIVE_INTERROGATOR = None

                return self.EditImage(request, context)

            ret = rpc_proto.services_pb2.ImageEditResult(
                result=False,
                error_msg=str(e),
            )
            print("Exception processing item!")
            print("Exception string: '%s'" % (str(e),))
            traceback.print_exc()
            return ret

        ret.result = True
        ret.error_msg = "Image successfully processed."

        print(ret.error_msg)

        return ret


def serve():
    maxMsgLength = 30 * 1024 * 1024

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
    logging.basicConfig(level=logging.INFO)

    interrogator.init()
    print(rpc_proto.services_pb2.InterrogatorListing)

    serve()
