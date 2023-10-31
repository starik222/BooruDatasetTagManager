# pylint: disable=bad-indentation

from PIL import Image
import numpy as np
from typing import Tuple

from .. import settings
from .. import devices as devices
from .. import launch, utilities, paths


class WaifuDiffusionTagger:
    # brought from https://huggingface.co/spaces/SmilingWolf/wd-v1-4-tags/blob/main/app.py and modified
    MODEL_FILENAME = "model.onnx"
    LABEL_FILENAME = "selected_tags.csv"

    def __init__(self, model_name):
        self.MODEL_REPO = model_name
        self.model = None
        self.labels = []

    def load(self, skip_online: bool=False):
        from huggingface_hub import hf_hub_download, try_to_load_from_cache, _CACHED_NO_EXIST

        if not self.model:
            path_model = try_to_load_from_cache(
                self.MODEL_REPO, self.MODEL_FILENAME, cache_dir=paths.setting_model_path)
            if path_model is _CACHED_NO_EXIST or not isinstance(path_model, str):
                if skip_online:
                    raise RuntimeError("Model %s is not available locally, and downloading is disabled." % (self.MODEL_FILENAME, ))

                path_model = hf_hub_download(
                    self.MODEL_REPO, self.MODEL_FILENAME, cache_dir=paths.setting_model_path
                )
            if settings.current.interrogator_use_cpu:
                providers = ["CPUExecutionProvider"]
            else:
                providers = [
                    "CUDAExecutionProvider",
                    "DmlExecutionProvider",
                    "CPUExecutionProvider",
                ]

            def check_available_device():
                import torch

                if torch.cuda.is_available():
                    return "cuda"
                elif launch.is_installed("torch-directml"):
                    # This code cannot detect DirectML available device without pytorch-directml
                    try:
                        import torch_directml
                        torch_directml.device()
                    except:
                        pass
                    else:
                        return "directml"
                return "cpu"

            if not launch.is_installed("onnxruntime"):
                dev = check_available_device()
                if dev == "cuda":
                    launch.run_pip(
                        "install -U onnxruntime-gpu",
                        "onnxruntime-gpu",
                    )
                elif dev == "directml":
                    launch.run_pip(
                        "install -U onnxruntime-directml",
                        "onnxruntime-directml",
                    )
                else:
                    print(
                        "Your device is not compatible with onnx hardware acceleration. CPU only version will be installed and it may be very slow."
                    )
                    launch.run_pip(
                        "install -U onnxruntime",
                        "onnxruntime for CPU",
                    )
            import onnxruntime as ort

            self.model = ort.InferenceSession(path_model, providers=providers)
            path_label = try_to_load_from_cache(
                self.MODEL_REPO, self.LABEL_FILENAME, cache_dir=paths.setting_model_path
            )
            if path_label is _CACHED_NO_EXIST or not isinstance(path_label, str):
                if skip_online:
                    raise RuntimeError("Label file %s is not available locally, and downloading is disabled." % (self.LABEL_FILENAME, ))

                path_label = hf_hub_download(
                    self.MODEL_REPO, self.LABEL_FILENAME, cache_dir=paths.setting_model_path
                )
            #import pandas as pd
            from pandas import read_csv

            self.labels = read_csv(path_label)["name"].tolist()

    def unload(self):
        if not settings.current.interrogator_keep_in_memory:
            self.model = None
            devices.torch_gc()

    # brought from https://huggingface.co/spaces/SmilingWolf/wd-v1-4-tags/blob/main/app.py and modified
    def apply(self, image: Image.Image):
        if not self.model:
            return []

        _, height, width, _ = self.model.get_inputs()[0].shape

        # the way to fill empty pixels is quite different from original one;
        # original: fill by white pixels
        # this: repeat the pixels on the edge
        image = utilities.resize_and_fill(image.convert("RGB"), (width, height))
        image_np = np.array(image, dtype=np.float32)
        # PIL RGB to OpenCV BGR
        image_np = image_np[:, :, ::-1]
        image_np = np.expand_dims(image_np, 0)

        input_name = self.model.get_inputs()[0].name
        label_name = self.model.get_outputs()[0].name
        probs = self.model.run([label_name], {input_name: image_np})[0]
        labels: list[Tuple[str, float]] = list(zip(self.labels, probs[0].astype(float)))

        return labels
