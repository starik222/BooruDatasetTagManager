
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

FLORENCE2_CAPTIONING_NAMES = [
    "Florence-2-base-ft",
    "Florence-2-base",
    "Florence-2-large-ft",
    "Florence-2-large",
]

WD_TAGGER_NAMES = [
    "wd-v1-4-convnext-tagger",
    "wd-v1-4-convnext-tagger-v2",
    "wd-v1-4-convnextv2-tagger-v2",
    "wd-v1-4-swinv2-tagger-v2",
    "wd-v1-4-vit-tagger",
    "wd-v1-4-vit-tagger-v2",
    "wd-v1-4-moat-tagger-v2",
    "wd-vit-tagger-v3",
    "wd-swinv2-tagger-v3",
    "wd-convnext-tagger-v3",
    "wd-vit-large-tagger-v3",
    "wd-eva02-large-tagger-v3",
]

FLORENCE2_COMMANDS = [
    "<CAPTION>",
    "<DETAILED_CAPTION>",
    "<MORE_DETAILED_CAPTION>",
    "<CAPTION_TO_PHRASE_GROUNDING>",
    "<OD>",
]

WD_TAGGER_THRESHOLDS = [
    0.35,
    0.35,
    0.35,
    0.35,
    0.35,
    0.35,
    0.35,
    0.25,
    0.25,
    0.25,
    0.26,
    0.52,
]  # v1: idk if it's okay  v2: P=R thresholds on each repo https://huggingface.co/SmilingWolf




INTERROGATORS = (
    [captioning.BLIP("blip")]
    + [captioning.BLIP2(name, "blip2") for name in BLIP2_CAPTIONING_NAMES]
    + [captioning.GITLarge("gitlarge")]
    + [captioning.Florence2(name, FLORENCE2_COMMANDS, "", "florence2") for name in FLORENCE2_CAPTIONING_NAMES]
    + [tagger.DeepDanbooru("dd")]
    + [
        tagger.WaifuDiffusion(name, WD_TAGGER_THRESHOLDS[i], "wd")
        for i, name in enumerate(WD_TAGGER_NAMES)
    ]
)

INTERROGATOR_NAMES = [it.name() for it in INTERROGATORS]

INTERROGATOR_MAP = dict(zip(INTERROGATOR_NAMES, INTERROGATORS))


def init():
    devices.init_interrogator()




