# pylint: disable=bad-indentation
import json
import os

from PIL import Image
from transformers import AutoProcessor, AutoModelForCausalLM, AutoTokenizer
from ..pyvips_dll_handler import handle_pyvips_dll_error

from .. import settings
from .. import devices as devices
from .. import utilities, paths
from unittest.mock import patch


class Moondream2Captioning:

    def __init__(self, model_name, revision):
        handle_pyvips_dll_error(download_dir=os.path.join("."))
        self.MODEL_REPO = model_name
        self.MODEL_REVISION = revision
        self.model = None
        self.cmd = None
        self.prompt = None
        self.split = False

    def load(self, cmd, prompt, split, skip_online: bool = False):
        self.cmd = cmd
        self.prompt = prompt
        self.split = split
        if self.model is None:
            self.model = AutoModelForCausalLM.from_pretrained(self.MODEL_REPO,
                                                              revision=self.MODEL_REVISION,
                                                              trust_remote_code=True,
                                                              cache_dir=paths.setting_model_path,
                                                              local_files_only=skip_online
                                                              ).to(devices.device)
            # self.model = AutoModelForCausalLM.from_pretrained(self.MODEL_REPO,
            #                                                   revision=self.MODEL_REVISION,
            #                                                   torch_dtype=devices.get_torch_dtype(),
            #                                                   trust_remote_code=True,
            #                                                   cache_dir=paths.setting_model_path,
            #                                                   local_files_only=skip_online
            #                                                   ).to(devices.device)

    def unload(self):
        if not settings.current.interrogator_keep_in_memory:
            self.model = None
            devices.torch_gc()

    def apply(self, image: Image.Image):
        if self.model is None:
            return ""
        enc_image = self.model.encode_image(image)
        if self.cmd == 'Short_caption':
            result = self.model.caption(enc_image, length="short")["caption"]
            if self.split:
                return [x.strip() for x in result.split(',')]
            else:
                return [result]
        elif self.cmd == 'Normal_caption':
            result = self.model.caption(enc_image, length="normal")["caption"]
            if self.split:
                return [x.strip() for x in result.split(',')]
            else:
                return [result]
        elif self.cmd == 'Visual_query':
            result = self.model.query(enc_image, self.prompt)["answer"]
            if self.split:
                return [x.strip() for x in result.split(',')]
            else:
                return [result]
        elif self.cmd == 'Object_detection':
            return [json.dumps(self.model.detect(enc_image, self.prompt)["objects"])]
        elif self.cmd == 'Pointing':
            return [json.dumps(self.model.point(enc_image, self.prompt)["points"])]
        else:
            return None
