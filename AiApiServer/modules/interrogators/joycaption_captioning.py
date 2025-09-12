# pylint: disable=bad-indentation
import os

import torch
from PIL import Image
from transformers import AutoProcessor, LlavaForConditionalGeneration

from .. import settings
from .. import devices as devices
from .. import utilities, paths


class JoyCaptionCaptioning:
    def __init__(self, model_name):
        self.MODEL_REPO = model_name
        self.model = None
        self.processor = None
        # self.cmd = None
        self.prompt = None
        self.split = False

    def load(self, prompt, split, skip_online: bool = False):
        # self.cmd = cmd
        self.prompt = prompt
        self.split = split
        if self.model is None or self.processor is None:
            self.processor = AutoProcessor.from_pretrained(self.MODEL_REPO,
                                                           trust_remote_code=True,
                                                           cache_dir=paths.setting_model_path,
                                                           local_files_only=skip_online)
            self.model = LlavaForConditionalGeneration.from_pretrained(self.MODEL_REPO,
                                                                       torch_dtype=devices.get_torch_dtype(),
                                                                       cache_dir=paths.setting_model_path,
                                                                       local_files_only=skip_online,
                                                                       device_map=0)  # .to(devices.device)
            self.model.eval()

    def unload(self):
        if not settings.current.interrogator_keep_in_memory:
            self.model = None
            self.processor = None
            devices.torch_gc()

    def apply(self, image: Image.Image):
        if self.model is None or self.processor is None:
            return ""

        with torch.no_grad():
            # Build the conversation
            convo = [
                {
                    "role": "system",
                    "content": "You are a helpful image captioner.",
                },
                {
                    "role": "user",
                    "content": self.prompt,
                },
            ]

            # Format the conversation
            # WARNING: HF's handling of chat's on Llava models is very fragile.  This specific combination of processor.apply_chat_template(), and processor() works
            # but if using other combinations always inspect the final input_ids to ensure they are correct.  Often times you will end up with multiple <bos> tokens
            # if not careful, which can make the model perform poorly.
            convo_string = self.processor.apply_chat_template(convo, tokenize=False, add_generation_prompt=True)
            assert isinstance(convo_string, str)

            # Process the inputs
            inputs = self.processor(text=[convo_string], images=[image], return_tensors="pt").to(devices.device)
            inputs['pixel_values'] = inputs['pixel_values'].to(devices.get_torch_dtype())

            # Generate the captions
            generate_ids = self.model.generate(
                **inputs,
                max_new_tokens=512,
                do_sample=True,
                suppress_tokens=None,
                use_cache=True,
                temperature=0.6,
                top_k=None,
                top_p=0.9,
            )[0]

            # Trim off the prompt
            generate_ids = generate_ids[inputs['input_ids'].shape[1]:]

            # Decode the caption
            caption = self.processor.tokenizer.decode(generate_ids, skip_special_tokens=True,
                                                      clean_up_tokenization_spaces=False)
            caption = caption.strip()
            if self.split:
                return [x.strip() for x in caption.split(',')]
            return [caption]
