import pathlib

from flask import Flask, request
from flask_restful import Resource, Api

import logging
import threading
import tqdm
import traceback
import time
import io
from PIL import Image
from concurrent import futures
from modules import settings

from modules.translators.seed_x_translator import LANGUAGES

import collections

import models
from modules import utilities
from modules.server_dataclasses import *

INTERROGATOR_LOCK = threading.Lock()

ACTIVE_INTERROGATOR = None
ONE_INTERROGATOR_IN_VRAM_AT_A_TIME = False

app = Flask(__name__)
api = Api(app)
app.logger.setLevel(logging.ERROR)


def interrogate_image(network_name, data_object, data_type, net_params, skip_online):
    global ACTIVE_INTERROGATOR
    global ONE_INTERROGATOR_IN_VRAM_AT_A_TIME

    with INTERROGATOR_LOCK:
        if ONE_INTERROGATOR_IN_VRAM_AT_A_TIME and ACTIVE_INTERROGATOR and ACTIVE_INTERROGATOR != network_name:
            print("Unloading interrogator %s" % (ACTIVE_INTERROGATOR,))

            for interrogator_name, interg in models.INTERROGATOR_MAP.items():
                if interrogator_name != network_name:
                    interg.stop()

        if ONE_INTERROGATOR_IN_VRAM_AT_A_TIME and ACTIVE_INTERROGATOR != network_name:
            print("Need to load network", network_name)

        ACTIVE_INTERROGATOR = network_name
        intg = models.INTERROGATOR_MAP[network_name]
        intg.start(net_params, skip_online=skip_online)

        tags = intg.predict(data_object, data_type)

        return tags


def edit_image(network_name, image_obj, net_params, skip_online):
    global ACTIVE_INTERROGATOR
    global ONE_INTERROGATOR_IN_VRAM_AT_A_TIME

    with INTERROGATOR_LOCK:
        if ONE_INTERROGATOR_IN_VRAM_AT_A_TIME and ACTIVE_INTERROGATOR and ACTIVE_INTERROGATOR != network_name:
            print("Unloading editor %s" % (ACTIVE_INTERROGATOR,))

            for interrogator_name, interg in models.EDITOR_MAP.items():
                if interrogator_name != network_name:
                    interg.stop()

        if ONE_INTERROGATOR_IN_VRAM_AT_A_TIME and ACTIVE_INTERROGATOR != network_name:
            print("Need to load network", network_name)

        ACTIVE_INTERROGATOR = network_name
        editor = models.EDITOR_MAP[network_name]
        editor.start(net_params, skip_online=skip_online)

        res_image = editor.predict(image_obj)

        return res_image


def translate_text(network_name, text, from_lang, to_lang, net_params, skip_online):
    global ACTIVE_INTERROGATOR
    global ONE_INTERROGATOR_IN_VRAM_AT_A_TIME

    with INTERROGATOR_LOCK:
        if ONE_INTERROGATOR_IN_VRAM_AT_A_TIME and ACTIVE_INTERROGATOR and ACTIVE_INTERROGATOR != network_name:
            print("Unloading editor %s" % (ACTIVE_INTERROGATOR,))

            for translator_name, interg in models.TRANSLATOR_MAP.items():
                if translator_name != network_name:
                    interg.stop()

        if ONE_INTERROGATOR_IN_VRAM_AT_A_TIME and ACTIVE_INTERROGATOR != network_name:
            print("Need to load network", network_name)

        ACTIVE_INTERROGATOR = network_name
        translator = models.TRANSLATOR_MAP[network_name]
        translator.start(net_params, skip_online=skip_online)

        translated_text = translator.translate(text, from_lang, to_lang)

        return translated_text


def extract_tag_ret(tags_in, threshold):
    ret = {}

    if isinstance(tags_in, dict):
        for tag, probability in tags_in.items():
            if probability > threshold:
                ret[tag] = round(float(probability), 2)

    elif isinstance(tags_in, (list, tuple)):
        for tag in tags_in:
            ret[tag] = 1

    else:
        raise RuntimeError("Tags must either be a list or a dict")

    return ret


def create_interrogator_parameter(key, value, type, comment):
    return ModelAdditionalParameters(Key=key, Value=value, Type=type, Comment=comment)


def create_dict_from_additional_parameters(param_list):
    result = {}
    for param in param_list:
        if param.Type == "float1":
            result[param.Key] = float(param.Value)
        elif param.Type == "int":
            result[param.Key] = int(param.Value)
        elif param.Type == "string":
            result[param.Key] = param.Value
        elif param.Type == "list":
            result[param.Key] = param.Value
        elif param.Type == "bool":
            result[param.Key] = param.Value.lower()
    return result


def get_model_base_info_list(model_map: dict, type_name: str = None):
    result = []
    for key, value in model_map.items():
        if type_name is None or value.type == type_name:
            mbi = ModelBaseInfo()
            mbi.ModelName = key
            mbi.SupportedVideo = value.video_supported
            if value.repo_name != "":
                mbi.RepositoryLink = "https://huggingface.co/" + value.repo_name
            result.append(mbi)
    return result


# Get all model list
class GetConfig(Resource):
    def get(self):
        result = ConfigResponse()
        result.Interrogators = get_model_base_info_list(models.INTERROGATOR_MAP)
        result.Editors = get_model_base_info_list(models.EDITOR_MAP)
        result.Translators = get_model_base_info_list(models.TRANSLATOR_MAP)
        # result.Interrogators = list(models.INTERROGATOR_MAP.keys())
        # result.Editors = list(models.EDITOR_MAP.keys())
        # result.Translators = list(models.TRANSLATOR_MAP.keys())
        return result.to_dict(), 200


# Get model list by type
class ListModelsByType(Resource):
    def get(self):
        result = ConfigResponse()
        model_type = request.args.get("name")

        result.Interrogators = get_model_base_info_list(models.INTERROGATOR_MAP, model_type)
        result.Editors = get_model_base_info_list(models.EDITOR_MAP, model_type)
        result.Translators = get_model_base_info_list(models.TRANSLATOR_MAP, model_type)
        return result.to_dict(), 200


def taggers_params(model_name):
    result = ModelParamResponse()
    if model_name not in models.INTERROGATOR_MAP:
        result.ErrorMessage = "model not found!"
        # print(result.err_mes)
        return result
    result.Success = True
    int_instance = models.INTERROGATOR_MAP[model_name]
    if int_instance.type == "wd":
        result.Type = "wd"
        result.Parameters.append(
            create_interrogator_parameter("threshold", str(int_instance.threshold), "float1", ""))
    elif int_instance.type == "florence2":
        result.Type = "florence2"
        result.Parameters.append(
            create_interrogator_parameter("cmd", (",").join(int_instance.commands), "list", ""))
        result.Parameters.append(create_interrogator_parameter("prompt", int_instance.defaultPrompt, "string", ""))
        result.Parameters.append(
            create_interrogator_parameter("split", "false", "bool", "Split lines with commas"))
        result.Parameters.append(create_interrogator_parameter("Comment",
                                                               "The \"prompt\" field is only used with the "
                                                               "<CAPTION_TO_PHRASE_GROUNDING> command.",
                                                               "label", ""))
    elif int_instance.type == "moondream2":
        result.Type = "moondream2"
        result.Parameters.append(
            create_interrogator_parameter("cmd", (",").join(int_instance.commands), "list", ""))
        result.Parameters.append(create_interrogator_parameter("query", int_instance.defaultPrompt, "string", ""))
        result.Parameters.append(
            create_interrogator_parameter("split", "false", "bool", "Split lines with commas"))
        result.Parameters.append(create_interrogator_parameter("Comment",
                                                               "Only \"Short_caption\", \"Normal_caption\", \"Visual_query\"\n"
                                                               "are used to create captions.\nThe \"query\" field is "
                                                               "only used with \"Visual_query\".",
                                                               "label", ""))
    elif int_instance.type == "joycaption":
        result.Type = "joycaption"
        # result.Parameters.append(create_interrogator_parameter("cmd", (",").join(intInstance.commands), "list", ""))
        result.Parameters.append(create_interrogator_parameter("query", int_instance.defaultPrompt, "string", ""))
        result.Parameters.append(
            create_interrogator_parameter("split", "false", "bool", "Split lines with commas"))
        result.Parameters.append(create_interrogator_parameter("Comment",
                                                               "You can see examples of requests on the project page "
                                                               "https://github.com/fpgaminer/joycaption",
                                                               "label", ""))
    elif int_instance.type == "qwen25" or int_instance.type == "keye":
        result.Type = int_instance.type
        # result.Parameters.append(create_interrogator_parameter("cmd", (",").join(intInstance.commands), "list", ""))
        result.Parameters.append(create_interrogator_parameter("query", int_instance.defaultPrompt, "string", ""))
        result.Parameters.append(
            create_interrogator_parameter("fps", int_instance.fps, "int", "Fps limit for video reading"))
        result.Parameters.append(create_interrogator_parameter("max_frames", int_instance.max_frames, "int",
                                                               "Maximum number of frames to analyze. -1 for default value."))
        result.Parameters.append(create_interrogator_parameter("min_pixels", int_instance.min_pixels, "int",
                                                               "Image Resolution for performance boost. -1 for default value. For example 256 * 28 * 28 = 200704."))
        result.Parameters.append(create_interrogator_parameter("max_pixels", int_instance.max_pixels, "int",
                                                               "Image Resolution for performance boost. -1 for default value. For example 1280 * 28 * 28 = 1003520."))
        result.Parameters.append(
            create_interrogator_parameter("split", "false", "bool", "Split lines with commas"))
    elif int_instance.type == "qwen3":
        result.Type = int_instance.type
        result.Parameters.append(create_interrogator_parameter("query", int_instance.defaultPrompt, "string", ""))
        result.Parameters.append(
            create_interrogator_parameter("fps", int_instance.fps, "int", "Fps limit for video reading"))
        result.Parameters.append(create_interrogator_parameter("max_frames", int_instance.max_frames, "int",
                                                               "Maximum number of frames to analyze. -1 for default value."))
        result.Parameters.append(create_interrogator_parameter("min_pixels", int_instance.min_pixels, "int",
                                                               "Image Resolution for performance boost. -1 for default value. For example 256 * 28 * 28 = 200704."))
        result.Parameters.append(create_interrogator_parameter("max_pixels", int_instance.max_pixels, "int",
                                                               "Image Resolution for performance boost. -1 for default value. For example 1280 * 28 * 28 = 1003520."))
        result.Parameters.append(create_interrogator_parameter("max_new_tokens", int_instance.max_new_tokens, "int",
                                                               "Maximum new tokens in generation (Don't know what it affects.). 128 for default value."))
        result.Parameters.append(
            create_interrogator_parameter("split", "false", "bool", "Split lines with commas"))
    elif int_instance.type == "blip":
        result.Type = "blip"
    elif int_instance.type == "blip2":
        result.Type = "blip2"
    elif int_instance.type == "gitlarge":
        result.Type = "gitlarge"
    elif int_instance.type == "dd":
        result.Type = "dd"
        result.Parameters.append(
            create_interrogator_parameter("threshold", str(int_instance.threshold), "float1", ""))
    return result


def editors_params(model_name):
    result = ModelParamResponse()
    if model_name not in models.EDITOR_MAP:
        result.ErrorMessage = "model not found!"
        # print(result.err_mes)
        return result
    result.Success = True
    int_instance = models.EDITOR_MAP[model_name]
    if int_instance.type == "rmbg2":
        result.Type = "rmbg2"
    return result


def translators_params(model_name):
    result = ModelParamResponse()
    if model_name not in models.TRANSLATOR_MAP:
        result.ErrorMessage = "model not found!"
        # print(result.err_mes)
        return result
    result.Success = True
    int_instance = models.TRANSLATOR_MAP[model_name]
    if int_instance.type == "seedx":
        result.Type = "seedx"
        result.Parameters.append(
            create_interrogator_parameter("languages", (",").join(LANGUAGES.keys()), "list", ""))
    return result


class GetModelParams(Resource):
    def post(self):
        data = request.get_json()
        model_name = data["Name"]
        tg_params = taggers_params(model_name)
        ed_params = editors_params(model_name)
        tr_params = translators_params(model_name)
        if tg_params.Success:
            return tg_params.to_dict(), 200
        elif ed_params.Success:
            return ed_params.to_dict(), 200
        elif tr_params.Success:
            return tr_params.to_dict(), 200
        else:
            return tg_params.to_dict(), 200


class SetCustomSystemPrompt(Resource):
    def post(self):
        data = request.get_json()
        prompt = data["Prompt"]
        settings.current = settings.current._replace(custom_system_prompt=prompt)
        return 200


class InterrogateImage(Resource):
    def post(self):
        data = request.get_json()
        int_request = InterrogateImageRequest.from_dict(data)
        return self.interrogate_image(int_request).to_dict(), 200

    def interrogate_image(self, int_request: InterrogateImageRequest):
        ret = InterrogateImageResponse()
        print("Interrogate Request!")
        print("File name: ", int_request.FileName)
        if int_request.DataType == ObjectDataType.IMAGE_BYTE_ARRAY:
            print("File size: ", len(int_request.DataObject))
        print("File type: ", int_request.DataType.name)
        for network_conf in int_request.Models:
            if network_conf.ModelName not in models.INTERROGATOR_MAP:
                ret.Success = False
                ret.ErrorMessage = "Image Interrogator named '%s' is not a valid interrogator name. Known interrogators: '%s'" % (
                    network_conf.interrogator_network, list(models.INTERROGATOR_MAP.keys())
                )
                print(ret.ErrorMessage)
                return ret

        if not int_request.DataObject:
            ret.Success = False
            ret.ErrorMessage = "Interrogate Failed - Received no image!"
            print(ret.ErrorMessage)
            return ret

        global ONE_INTERROGATOR_IN_VRAM_AT_A_TIME
        ONE_INTERROGATOR_IN_VRAM_AT_A_TIME = int_request.SerializeVramUsage

        try:

            # image_bytes = int_request.Image

            # image_obj = Image.open(io.BytesIO(image_bytes))

            for network_conf in int_request.Models:
                param_dict = create_dict_from_additional_parameters(network_conf.AdditionalParameters)
                threshold = 1.0
                if 'threshold' in param_dict:
                    threshold = param_dict['threshold']
                tag_ret = interrogate_image(network_conf.ModelName, int_request.DataObject, int_request.DataType,
                                            param_dict,
                                            skip_online=int_request.SkipInternetRequests)
                network_tags = extract_tag_ret(tag_ret, threshold)

                net_resp = InterrogateImageResult()
                net_resp.ModelName = network_conf.ModelName

                for tag, probability in network_tags.items():
                    tag_obj = TagEntry(
                        Tag=tag,
                        Probability=probability,
                    )
                    net_resp.Tags.append(tag_obj)

                ret.Result.append(net_resp)

        except Exception as e:
            global ACTIVE_INTERROGATOR
            if "out of memory" in str(e).lower() and len(
                    int_request.Models) > 1 and not ONE_INTERROGATOR_IN_VRAM_AT_A_TIME:
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
                for int_tmp in models.INTERROGATOR_MAP.values():
                    for _ in range(3):
                        try:
                            int_tmp.stop()
                        except:
                            pass

                ACTIVE_INTERROGATOR = None

                return self.interrogate_image(int_request)

            ret.Success = False
            ret.ErrorMessage = str(e)
            print("Exception processing item!")
            print("Exception string: '%s'" % (str(e),))
            traceback.print_exc()
            return ret

        ret.Success = True
        ret.ErrorMessage = "Image successfully processed."

        print(ret.ErrorMessage)

        return ret


class EditImage(Resource):
    def post(self):
        data = request.get_json()
        ed_request = EditImageRequest.from_dict(data)
        return self.edit_image(ed_request).to_dict(), 200

    def edit_image(self, int_request: EditImageRequest):
        ret = EditImageResponse()
        print("Editor Request!")
        print("File name: ", int_request.FileName)
        print("File size: ", len(int_request.Image))
        if int_request.Model.ModelName not in models.EDITOR_MAP:
            ret.Success = False
            ret.ErrorMessage = "Image Interrogator named '%s' is not a valid interrogator name. Known interrogators: '%s'" % (
                int_request.Model.ModelName, list(models.EDITOR_MAP.keys())
            )
            print(ret.ErrorMessage)
            return ret

        if not int_request.Image:
            ret.Success = False
            ret.ErrorMessage = "Interrogate Failed - Received no image!"
            print(ret.ErrorMessage)
            return ret

        global ONE_INTERROGATOR_IN_VRAM_AT_A_TIME
        ONE_INTERROGATOR_IN_VRAM_AT_A_TIME = int_request.SerializeVramUsage

        try:

            image_bytes = int_request.Image

            image_obj = Image.open(io.BytesIO(image_bytes))

            param_dict = create_dict_from_additional_parameters(int_request.Model.AdditionalParameters)
            img_ret = edit_image(int_request.Model.ModelName, image_obj, param_dict,
                                 skip_online=int_request.SkipInternetRequests)
            format = img_ret.format
            mode = img_ret.mode
            if format is None:
                file_extension = pathlib.Path(int_request.FileName).suffix.lower()
                if file_extension == '.png':
                    format = 'PNG'
                elif file_extension in ('.jpg', '.jpeg'):
                    format = 'JPEG'
                elif file_extension == '.bmp':
                    format = 'BMP'
                elif file_extension == '.webp':
                    format = 'WEBP'
                else:
                    format = file_extension[1:].upper()
            # Removing alpha channel for bmp and jpg formats
            if format in ('JPEG', 'BMP') and mode == 'RGBA':
                img_ret = utilities.remove_transparency(img_ret).convert("RGB")
            img_byte_arr = io.BytesIO()
            img_ret.save(img_byte_arr, format=format)
            img_byte_arr = img_byte_arr.getvalue()
            ret.Image = img_byte_arr
            img_ret.close()

        except Exception as e:
            global ACTIVE_INTERROGATOR
            if "out of memory" in str(e).lower() and not ONE_INTERROGATOR_IN_VRAM_AT_A_TIME:
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
                for int_tmp in models.EDITOR_MAP.values():
                    for _ in range(3):
                        try:
                            int_tmp.stop()
                        except:
                            pass

                ACTIVE_INTERROGATOR = None

                return self.edit_image(int_request)

            ret.Success = False
            ret.ErrorMessage = str(e)
            print("Exception processing item!")
            print("Exception string: '%s'" % (str(e),))
            traceback.print_exc()
            return ret

        ret.Success = True
        ret.ErrorMessage = "Image successfully processed."

        print(ret.ErrorMessage)

        return ret


class TranslateText(Resource):
    def post(self):
        data = request.get_json()
        tr_request = TranslateRequest.from_dict(data)
        return self.translate_text(tr_request).to_dict(), 200

    def translate_text(self, tr_request: TranslateRequest):
        ret = TranslateTextResponse()
        print("Editor Request!")
        print("Original text: ", tr_request.Text)
        print("From lang: ", tr_request.FromLang)
        print("To lang: ", tr_request.ToLang)
        if tr_request.Model.ModelName not in models.TRANSLATOR_MAP:
            ret.Success = False
            ret.ErrorMessage = "Image Interrogator named '%s' is not a valid interrogator name. Known interrogators: '%s'" % (
                tr_request.Model.ModelName, list(models.TRANSLATOR_MAP.keys())
            )
            print(ret.ErrorMessage)
            return ret

        if tr_request.FromLang not in LANGUAGES.keys() or tr_request.ToLang not in LANGUAGES.keys():
            ret.Success = False
            ret.ErrorMessage = "Unsupported language"
            print(ret.ErrorMessage)
            return ret

        global ONE_INTERROGATOR_IN_VRAM_AT_A_TIME
        ONE_INTERROGATOR_IN_VRAM_AT_A_TIME = tr_request.SerializeVramUsage

        try:
            param_dict = create_dict_from_additional_parameters(tr_request.Model.AdditionalParameters)
            translated_text = translate_text(tr_request.Model.ModelName,
                                             tr_request.Text,
                                             tr_request.FromLang,
                                             tr_request.ToLang,
                                             param_dict,
                                             skip_online=tr_request.SkipInternetRequests)
            ret.TranslatedText = translated_text

        except Exception as e:
            global ACTIVE_INTERROGATOR
            if "out of memory" in str(e).lower() and not ONE_INTERROGATOR_IN_VRAM_AT_A_TIME:
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
                for int_tmp in models.TRANSLATOR_MAP.values():
                    for _ in range(3):
                        try:
                            int_tmp.stop()
                        except:
                            pass

                ACTIVE_INTERROGATOR = None

                return self.translate_text(tr_request)

            ret.Success = False
            ret.ErrorMessage = str(e)
            print("Exception processing item!")
            print("Exception string: '%s'" % (str(e),))
            traceback.print_exc()
            return ret

        ret.Success = True
        ret.ErrorMessage = "Text successfully translated."

        print(ret.ErrorMessage)
        print(ret.TranslatedText)

        return ret


api.add_resource(GetConfig, '/getconfig')
api.add_resource(ListModelsByType, '/listmodelsbytype')
api.add_resource(GetModelParams, '/getmodelparams')
api.add_resource(InterrogateImage, '/interrogateimage')
api.add_resource(EditImage, '/editimage')
api.add_resource(TranslateText, '/translate')
api.add_resource(SetCustomSystemPrompt, '/setcustomsustemprompt')

if __name__ == '__main__':
    models.init()
    app.run(debug=False, host='0.0.0.0', port=50051)
