
import torch
from PIL import Image

device = torch.device("cuda") if torch.cuda.is_available() else "cpu"

print("Have CUDA: ", torch.cuda.is_available())
print("CUDA Count: ", torch.cuda.device_count())

print("Using interrogator device:", device)


from ext_kohya import (
    tagger,
    captioning,
    paths,
    devices,
)

paths.initialize()

BLIP2_CAPTIONING_NAMES = [
    "blip2-opt-2.7b",
    "blip2-opt-2.7b-coco",
    "blip2-opt-6.7b",
    "blip2-opt-6.7b-coco",
    "blip2-flan-t5-xl",
    "blip2-flan-t5-xl-coco",
    "blip2-flan-t5-xxl",
]

WD_TAGGER_NAMES = [
    "wd-v1-4-convnext-tagger",
    "wd-v1-4-convnext-tagger-v2",
    "wd-v1-4-convnextv2-tagger-v2",
    "wd-v1-4-swinv2-tagger-v2",
    "wd-v1-4-vit-tagger",
    "wd-v1-4-vit-tagger-v2",
    "wd-v1-4-moat-tagger-v2",
]

WD_TAGGER_THRESHOLDS = [
    0.35,
    0.35,
    0.35,
    0.35,
    0.35,
    0.35,
    0.35,
]  # v1: idk if it's okay  v2: P=R thresholds on each repo https://huggingface.co/SmilingWolf




INTERROGATORS = (
    [captioning.BLIP()]
    + [captioning.BLIP2(name) for name in BLIP2_CAPTIONING_NAMES]
    + [captioning.GITLarge()]
    + [tagger.DeepDanbooru()]
    + [
        tagger.WaifuDiffusion(name, WD_TAGGER_THRESHOLDS[i])
        for i, name in enumerate(WD_TAGGER_NAMES)
    ]
)

INTERROGATOR_NAMES = [it.name() for it in INTERROGATORS]

INTERROGATOR_MAP = dict(zip(INTERROGATOR_NAMES, INTERROGATORS))


def init():
    devices.init_interrogator()




