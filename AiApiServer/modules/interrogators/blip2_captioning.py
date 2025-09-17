# pylint: disable=bad-indentation

from transformers import Blip2Processor, Blip2ForConditionalGeneration
from .. import devices, settings, paths, utilities
from ..server_dataclasses import ObjectDataType


class BLIP2Captioning:
    def __init__(self, model_repo: str):
        self.MODEL_REPO = model_repo
        self.processor: Blip2Processor = None
        self.model: Blip2ForConditionalGeneration = None

    def load(self, skip_online: bool = False):
        if self.model is None or self.processor is None:
            self.processor = Blip2Processor.from_pretrained(
                self.MODEL_REPO,
                cache_dir=paths.setting_model_path,
                local_files_only=skip_online,
            )
            self.model = Blip2ForConditionalGeneration.from_pretrained(
                self.MODEL_REPO,
                cache_dir=paths.setting_model_path,
                local_files_only=skip_online,
            ).to(devices.device)

    def unload(self):
        if not settings.current.interrogator_keep_in_memory:
            self.model = None
            self.processor = None
            devices.torch_gc()

    def apply(self, data_obj, data_type):
        if self.model is None or self.processor is None:
            return ""
        if data_type != ObjectDataType.IMAGE_BYTE_ARRAY:
            raise Exception('Model supported only image format.')
        image = utilities.byte_array_to_image(data_obj)
        inputs = self.processor(images=image, return_tensors="pt").to(devices.device)
        ids = self.model.generate(**inputs)
        return self.processor.batch_decode(ids, skip_special_tokens=True)
