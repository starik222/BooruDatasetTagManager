# pylint: disable=bad-indentation
import os

import torch
from PIL import Image
from transformers import AutoModel, AutoTokenizer, AutoProcessor
from keye_vl_utils import process_vision_info
from transformers.video_utils import load_video

from .. import settings
from .. import devices as devices
from .. import utilities, paths
from ..server_dataclasses import ObjectDataType


class KeyeCaptionCaptioning:
    def __init__(self, model_name):
        self.MODEL_REPO = model_name
        self.model = None
        self.processor = None
        # self.cmd = None
        self.prompt = None
        self.split = False
        self.fps = 1
        self.max_frames = -1
        self.min_pixels = -1
        self.max_pixels = -1

    def load(self, prompt, split, fps, max_frames, min_pixels, max_pixels, skip_online: bool = False):
        # self.cmd = cmd
        self.prompt = prompt
        self.split = split
        self.fps = fps
        self.max_frames = max_frames
        self.min_pixels = min_pixels
        self.max_pixels = max_pixels
        if self.model is None or self.processor is None:
            self.model = AutoModel.from_pretrained(self.MODEL_REPO,
                                                   trust_remote_code=True,
                                                   torch_dtype=devices.get_torch_dtype(),
                                                   cache_dir=paths.setting_model_path,
                                                   local_files_only=skip_online,
                                                   attn_implementation="flash_attention_2",
                                                   device_map="auto")  # .to(devices.device)

            self.processor = AutoProcessor.from_pretrained(self.MODEL_REPO,
                                                           trust_remote_code=True,
                                                           cache_dir=paths.setting_model_path,
                                                           local_files_only=skip_online)

    def unload(self):
        if not settings.current.interrogator_keep_in_memory:
            self.model = None
            self.processor = None
            devices.torch_gc()

    def apply(self, data_obj, data_type: ObjectDataType):
        if self.model is None or self.processor is None:
            return ""
        messages = None
        if data_type == ObjectDataType.IMAGE_BYTE_ARRAY:
            image = utilities.byte_array_to_image(data_obj)
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
                },
            ]
            if self.min_pixels != -1:
                messages[0]["content"][0]["min_pixels"] = self.min_pixels
            if self.max_pixels != -1:
                messages[0]["content"][0]["max_pixels"] = self.max_pixels
        elif data_type == ObjectDataType.VIDEO_PATH:
            video_path = data_obj.decode("utf-8")
            messages = [
                {
                    "role": "user",
                    "content": [
                        {
                            "type": "video",
                            "video": video_path,
                            "fps": self.fps,
                            # "min_pixels": 32 * 28 * 28,
                            # "max_pixels": 1280 * 28 * 28
                        },
                        {"type": "text", "text": self.prompt},
                    ],
                }
            ]
            if self.max_frames != -1:
                messages[0]["content"][0]["max_frames"] = self.max_frames
            if self.min_pixels != -1:
                messages[0]["content"][0]["min_pixels"] = self.min_pixels
            if self.max_pixels != -1:
                messages[0]["content"][0]["max_pixels"] = self.max_pixels

        if settings.current.custom_system_prompt != "":
            messages.insert(0, {"role": "system", "content": settings.current.custom_system_prompt})
        # Preparation for inference
        text = self.processor.apply_chat_template(
            messages, tokenize=False, add_generation_prompt=True
        )
        image_inputs, video_inputs, mm_processor_kwargs = process_vision_info(messages)
        inputs = self.processor(
            text=[text],
            images=image_inputs,
            videos=video_inputs,
            padding=True,
            return_tensors="pt",
            **mm_processor_kwargs
        )
        inputs = inputs.to(devices.device)

        # Inference: Generation of the output
        generated_ids = self.model.generate(**inputs, max_new_tokens=1024)
        generated_ids_trimmed = [
            out_ids[len(in_ids):] for in_ids, out_ids in zip(inputs.input_ids, generated_ids)
        ]
        output_text = self.processor.batch_decode(
            generated_ids_trimmed, skip_special_tokens=True, clean_up_tokenization_spaces=False
        )
        output_text = [utilities.remove_tags_with_content(output_text[0])]
        if self.split:
            return [x.strip() for x in output_text[0].split(',')]
        return output_text
