
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
    editors
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
    "microsoft/Florence-2-base-ft",
    "microsoft/Florence-2-base",
    "microsoft/Florence-2-large-ft",
    "microsoft/Florence-2-large",
    "thwri/CogFlorence-2.2-Large",
]

FLORENCE2PG_CAPTIONING_NAMES = [
    "MiaoshouAI/Florence-2-large-PromptGen-v2.0",
    "MiaoshouAI/Florence-2-base-PromptGen-v2.0",
]

MOONDREAM2_CAPTIONING_NAMES = [
    "moondream2",
]

JOYCAPTION_CAPTIONING_NAMES = [
    "fancyfeast/llama-joycaption-alpha-two-hf-llava",
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

FLORENCE2PG_COMMANDS = [
    "<GENERATE_TAGS>",
    "<CAPTION>",
    "<DETAILED_CAPTION>",
    "<MORE_DETAILED_CAPTION>",
    "<ANALYZE>",
    "<MIXED_CAPTION>",
    "<MIXED_CAPTION_PLUS>",
]

FLORENCE2_COMMANDS = [
    "<CAPTION>",
    "<DETAILED_CAPTION>",
    "<MORE_DETAILED_CAPTION>",
    "<CAPTION_TO_PHRASE_GROUNDING>",
    "<OD>",
]

MOONDREAM2_COMMANDS = [
    "Short_caption",
    "Normal_caption",
    "Visual_query",
    "Object_detection",
    "Pointing",
]

BG_REMOVAL = [
    "briaai/RMBG-2.0",
    "ZhengPeng7/BiRefNet",
    "ZhengPeng7/BiRefNet_HR",
    "zhengpeng7/BiRefNet_lite",
    "ZhengPeng7/BiRefNet_lite-2K",
    "ZhengPeng7/BiRefNet-matting",
    "ZhengPeng7/BiRefNet_512x512",
]

BG_REMOVAL_RESOLUTION = [
    (1024, 1024),
    (1024, 1024),
    (2048, 2048),
    (1024, 1024),
    (2560, 1440),
    (1024, 1024),
    (512, 512),
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
    + [captioning.Florence2(name, FLORENCE2_COMMANDS, "", False, "florence2") for name in FLORENCE2_CAPTIONING_NAMES]
    + [captioning.Florence2(name, FLORENCE2PG_COMMANDS, "", False, "florence2") for name in FLORENCE2PG_CAPTIONING_NAMES]
    + [captioning.Moondream2(name, MOONDREAM2_COMMANDS, "", False, "moondream2") for name in MOONDREAM2_CAPTIONING_NAMES]
    + [captioning.JoyCaption(name, "", False, "joycaption") for name in JOYCAPTION_CAPTIONING_NAMES]
    + [tagger.DeepDanbooru("dd")]
    + [
        tagger.WaifuDiffusion(name, WD_TAGGER_THRESHOLDS[i], "wd")
        for i, name in enumerate(WD_TAGGER_NAMES)
    ]
)

INTERROGATOR_NAMES = [it.name() for it in INTERROGATORS]

INTERROGATOR_MAP = dict(zip(INTERROGATOR_NAMES, INTERROGATORS))

EDITORS = (
    [
        editors.RMBG2(name, BG_REMOVAL_RESOLUTION[i], "rmbg2")
        for i, name in enumerate(BG_REMOVAL)
    ]
)

EDITOR_NAMES = [it.name() for it in EDITORS]

EDITOR_MAP = dict(zip(EDITOR_NAMES, EDITORS))

def init():
    devices.init_interrogator()




