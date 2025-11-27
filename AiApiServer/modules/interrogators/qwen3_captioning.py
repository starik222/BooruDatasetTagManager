# pylint: disable=bad-indentation
import os

import torch
from PIL import Image
from transformers import Qwen3VLForConditionalGeneration, AutoProcessor
from qwen_vl_utils import process_vision_info
from transformers.video_utils import load_video

from .. import settings
from .. import devices as devices
from .. import utilities, paths
from ..server_dataclasses import ObjectDataType


class Qwen3CaptionCaptioning:
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
        self.max_new_tokens = 128

    def load(self, prompt, split, fps, max_frames, min_pixels, max_pixels, max_new_tokens, skip_online: bool = False):
        # self.cmd = cmd
        self.prompt = prompt
        self.split = split
        self.fps = fps
        self.max_frames = max_frames
        self.min_pixels = min_pixels
        self.max_pixels = max_pixels
        self.max_new_tokens = max_new_tokens
        if self.model is None or self.processor is None:
            self.model = Qwen3VLForConditionalGeneration.from_pretrained(self.MODEL_REPO,
                                                                            torch_dtype=devices.get_torch_dtype(),
                                                                            cache_dir=paths.setting_model_path,
                                                                            attn_implementation="flash_attention_2",
                                                                            local_files_only=skip_online,
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
        if data_type == ObjectDataType.VIDEO_PATH:
            image_inputs, video_inputs, video_kwargs = process_vision_info(messages, image_patch_size=16, return_video_kwargs=True, return_video_metadata=True)
            # split the videos and according metadatas
            if video_inputs is not None:
                video_inputs, video_metadatas = zip(*video_inputs)
                video_inputs, video_metadatas = list(video_inputs), list(video_metadatas)
            else:
                video_metadatas = None
            inputs = self.processor(
                text=text,
                images=image_inputs,
                videos=video_inputs,
                video_metadata=video_metadatas,
                do_resize=False,
                return_tensors="pt",
                **video_kwargs
            )
            inputs = inputs.to(devices.device)
        else:
            image_inputs, video_inputs = process_vision_info(messages, image_patch_size=16)
            inputs = self.processor(
                text=text,
                images=image_inputs,
                videos=video_inputs,
                do_resize=False,
                padding=True,
                return_tensors="pt",
            )
            inputs = inputs.to(devices.device)

        # Inference: Generation of the output
        generated_ids = self.model.generate(**inputs, max_new_tokens=self.max_new_tokens)
        generated_ids_trimmed = [
            out_ids[len(in_ids):] for in_ids, out_ids in zip(inputs.input_ids, generated_ids)
        ]
        output_text = self.processor.batch_decode(
            generated_ids_trimmed, skip_special_tokens=True, clean_up_tokenization_spaces=False
        )
        if self.split:
            return [x.strip() for x in output_text[0].split(',')]
        return output_text
