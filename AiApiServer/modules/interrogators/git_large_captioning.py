# pylint: disable=bad-indentation

from transformers import AutoProcessor, AutoModelForCausalLM
from .. import devices, settings, paths

# brought from https://huggingface.co/docs/transformers/main/en/model_doc/git and modified
class GITLargeCaptioning:
    MODEL_REPO = "microsoft/git-large-coco"

    def __init__(self):
        self.processor: AutoProcessor = None
        self.model: AutoModelForCausalLM = None

    def load(self, skip_online: bool=False):
        if self.model is None or self.processor is None:
            self.processor = AutoProcessor.from_pretrained(
                    self.MODEL_REPO,
                    cache_dir        = paths.setting_model_path,
                    local_files_only = skip_online,
                )
            self.model = AutoModelForCausalLM.from_pretrained(
                    self.MODEL_REPO,
                    cache_dir        = paths.setting_model_path,
                    local_files_only = skip_online,
                ).to(
                devices.device
            )

    def unload(self):
        if not settings.current.interrogator_keep_in_memory:
            self.model = None
            self.processor = None
            devices.torch_gc()

    def apply(self, image):
        if self.model is None or self.processor is None:
            return ""
        inputs = self.processor(images=image, return_tensors="pt").to(devices.device)
        ids = self.model.generate(
            pixel_values=inputs.pixel_values,
            max_length=settings.current.interrogator_max_length,
        )
        return self.processor.batch_decode(ids, skip_special_tokens=True)
