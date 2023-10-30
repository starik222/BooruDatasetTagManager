# pylint: disable=bad-indentation

from transformers import BlipProcessor, BlipForConditionalGeneration
from .. import devices, settings, paths


class BLIPLargeCaptioning:
    MODEL_REPO = "Salesforce/blip-image-captioning-large"

    def __init__(self):
        self.processor: BlipProcessor = None
        self.model: BlipForConditionalGeneration = None

    def load(self, skip_online: bool=False):
        if self.model is None or self.processor is None:
            self.processor = BlipProcessor.from_pretrained(
                self.MODEL_REPO,
                cache_dir        = paths.setting_model_path,
                local_files_only = skip_online,
            )
            self.model = BlipForConditionalGeneration.from_pretrained(
                self.MODEL_REPO,
                cache_dir        = paths.setting_model_path,
                local_files_only = skip_online,
            ).to(devices.device)

    def unload(self):
        if not settings.current.interrogator_keep_in_memory:
            self.model = None
            self.processor = None
            devices.torch_gc()

    def apply(self, image):
        if self.model is None or self.processor is None:
            return ""
        inputs = self.processor(images=image, return_tensors="pt").to(devices.device)
        ids = self.model.generate(**inputs)
        return self.processor.batch_decode(ids, skip_special_tokens=True)
