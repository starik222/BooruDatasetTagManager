# pylint: disable=bad-indentation
import os

from PIL import Image
from transformers import AutoProcessor, AutoModelForCausalLM
from transformers.dynamic_module_utils import get_imports

from .. import settings
from .. import devices as devices
from .. import utilities, paths
from unittest.mock import patch


def fixed_get_imports(filename: str | os.PathLike) -> list[str]:
    if not str(filename).endswith("modeling_florence2.py"):
        return get_imports(filename)
    imports = get_imports(filename)
    if "flash_attn" in imports:
        imports.remove("flash_attn")
    return imports


class Florence2Captioning:
    def __init__(self, model_name):
        self.MODEL_REPO = model_name
        self.model = None
        self.processor = None
        self.cmd = None
        self.prompt = None
        self.split = False

    def load(self, cmd, prompt, split, skip_online: bool = False):
        self.cmd = cmd
        self.prompt = prompt
        self.split = split
        if self.model is None or self.processor is None:
            attention = 'sdpa'
            with patch("transformers.dynamic_module_utils.get_imports",
                       fixed_get_imports):  # workaround for unnecessary flash_attn requirement
                self.model = AutoModelForCausalLM.from_pretrained(self.MODEL_REPO,
                                                                  attn_implementation=attention,
                                                                  torch_dtype=devices.get_torch_dtype(),
                                                                  trust_remote_code=True,
                                                                  cache_dir=paths.setting_model_path,
                                                                  local_files_only=skip_online
                                                                  ).to(devices.device)
            self.processor = AutoProcessor.from_pretrained(self.MODEL_REPO,
                                                           trust_remote_code=True,
                                                           cache_dir=paths.setting_model_path,
                                                           local_files_only=skip_online)

    def unload(self):
        if not settings.current.interrogator_keep_in_memory:
            self.model = None
            self.processor = None
            devices.torch_gc()

    def apply(self, image: Image.Image):
        if self.model is None or self.processor is None:
            return ""

        prompt = self.cmd + self.prompt
        if image.mode != "RGB":
            image = image.convert("RGB")
        inputs = self.processor(text=prompt, images=image, return_tensors="pt").to(devices.device,
                                                                                   devices.get_torch_dtype())
        generated_ids = self.model.generate(
            input_ids=inputs["input_ids"],
            pixel_values=inputs["pixel_values"],
            max_new_tokens=1024,
            do_sample=False,
            num_beams=3,
        )
        generated_text = self.processor.batch_decode(generated_ids, skip_special_tokens=False)[0]
        parsed_answer = self.processor.post_process_generation(generated_text, task=self.cmd,
                                                               image_size=(image.width, image.height))
        if self.cmd == '<CAPTION_TO_PHRASE_GROUNDING>' or self.cmd == '<OD>':
            return parsed_answer[self.cmd]['labels']
        else:
            result = parsed_answer[self.cmd]
            if self.split:
                return [x.strip() for x in result.split(',')]
            else:
                return [result]
        # if self.cmd == '<CAPTION>' or self.cmd == '<DETAILED_CAPTION>' or self.cmd == '<MORE_DETAILED_CAPTION>':
        #     result = parsed_answer[self.cmd]
        #     if self.split:
        #         return [x.strip() for x in result.split(',')]
        #     else:
        #         return [result]
        # elif self.cmd == '<CAPTION_TO_PHRASE_GROUNDING>' or self.cmd == '<OD>':
        #     return parsed_answer[self.cmd]['labels']
        # else:
        #     return None
