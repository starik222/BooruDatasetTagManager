# pylint: disable=bad-indentation

from PIL import Image
import numpy as np
import torch

from .. import settings, devices, utilities, paths
from . import deepbooru_model, model_loader


class DepDanbooruTagger:
    def __init__(self):
        self.model = None
        self.labels = []

    def load(self, skip_online: bool=False):
        if self.model is not None:
            return

        model_file = model_loader.load(
            model_path  = paths.models_path / "model-resnet_custom_v3.pt",
            model_url   = 'https://github.com/AUTOMATIC1111/TorchDeepDanbooru/releases/download/v1/model-resnet_custom_v3.pt',
            skip_online = skip_online,
        )
        self.model = deepbooru_model.DeepDanbooruModel()
        self.model.load_state_dict(torch.load(model_file, map_location="cpu"))
        self.model.eval()
        self.model.to(devices.device, torch.float16)

    def unload(self):
        if not settings.current.interrogator_keep_in_memory:
            self.model = None
            devices.torch_gc()

    def apply(self, image: Image.Image):
        if not self.model:
            return []
        image = utilities.resize_and_fill(image.convert("RGB"), (512, 512))
        image_np = np.expand_dims(np.array(image, dtype=np.float32), 0) / 255
        with torch.no_grad(), torch.autocast('cuda'):
            x = torch.from_numpy(image_np).half().to(devices.device)
            y = self.model(x)[0].detach().cpu().numpy()
        
        return list(zip(self.model.tags, y))
