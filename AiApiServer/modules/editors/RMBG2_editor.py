# pylint: disable=bad-indentation
import os

from PIL import Image
from transformers import AutoModelForImageSegmentation
from torchvision import transforms
import torch
#import matplotlib.pyplot as plt

from .. import settings
from .. import devices as devices
from .. import utilities, paths



class RMBG2Editor:

    def __init__(self, model_name):
        self.MODEL_REPO = model_name
        self.model = None
        self.transform_image = None
        self.resolution = (1024, 1024)

    def load(self, resolution, skip_online: bool = False):
        self.resolution = resolution
        if self.model is None:
            torch.set_float32_matmul_precision(["high", "highest"][0])
            self.model = AutoModelForImageSegmentation.from_pretrained(self.MODEL_REPO,
                                                              trust_remote_code=True,
                                                              cache_dir=paths.setting_model_path,
                                                              local_files_only=skip_online
                                                              ).to(devices.device)
            self.model.eval()
            self.transform_image = transforms.Compose([
                transforms.Resize(self.resolution),
                transforms.ToTensor(),
                transforms.Normalize([0.485, 0.456, 0.406], [0.229, 0.224, 0.225])
            ])


    def unload(self):
        if not settings.current.interrogator_keep_in_memory:
            self.model = None
            devices.torch_gc()

    def apply(self, image: Image.Image):
        if self.model is None:
            return ""
        if image.mode != "RGB":
            image = image.convert("RGB")
        input_images = self.transform_image(image).unsqueeze(0).to(devices.device)
        with torch.no_grad():
            preds = self.model(input_images)[-1].sigmoid().cpu()
        pred = preds[0].squeeze()
        pred_pil = transforms.ToPILImage()(pred)
        mask = pred_pil.resize(image.size)
        image.putalpha(mask)
        return image
