# pylint: disable=bad-indentation
import os

import torch
from PIL import Image
from transformers import Qwen2_5_VLForConditionalGeneration, AutoTokenizer, AutoProcessor
from qwen_vl_utils import process_vision_info

from .. import settings
from .. import devices as devices
from .. import launch, utilities, paths



class Qwen25CaptionCaptioning:
    def __init__(self, model_name):
        self.MODEL_REPO = model_name
        self.model = None
        self.processor = None
        #self.cmd = None
        self.prompt = None
        self.split = False

    def load(self, prompt, split, skip_online: bool = False):
        # self.cmd = cmd
        self.prompt = prompt
        self.split = split
        if self.model is None or self.processor is None:
            self.model = Qwen2_5_VLForConditionalGeneration.from_pretrained(self.MODEL_REPO,
                                                                       torch_dtype=devices.get_torch_dtype(),
                                                                       cache_dir=paths.setting_model_path,
                                                                       local_files_only=skip_online,
                                                                       device_map="auto")#.to(devices.device)

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

        messages = [
            {
                "role": "user",
                "content": [
                    {
                        "type": "image",
                        "image": image,
                    },
                    {"type": "text", "text": self.prompt},
                ],
            }
        ]

        # Preparation for inference
        text = self.processor.apply_chat_template(
            messages, tokenize=False, add_generation_prompt=True
        )
        image_inputs, video_inputs = process_vision_info(messages)
        inputs = self.processor(
            text=[text],
            images=image_inputs,
            videos=video_inputs,
            padding=True,
            return_tensors="pt",
        )
        inputs = inputs.to(devices.device)

        # Inference: Generation of the output
        generated_ids = self.model.generate(**inputs, max_new_tokens=128)
        generated_ids_trimmed = [
            out_ids[len(in_ids):] for in_ids, out_ids in zip(inputs.input_ids, generated_ids)
        ]
        output_text = self.processor.batch_decode(
            generated_ids_trimmed, skip_special_tokens=True, clean_up_tokenization_spaces=False
        )
        if self.split:
            return [x.strip() for x in output_text[0].split(',')]
        return output_text
