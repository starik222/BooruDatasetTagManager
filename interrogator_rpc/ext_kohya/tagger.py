# pylint: disable=bad-indentation

from PIL import Image
from typing import Optional

from . import devices
from .interrogator import Interrogator
from .interrogators import WaifuDiffusionTagger, DepDanbooruTagger
from . import settings


class Tagger(Interrogator):
    def start(self, skip_online: bool=False):
        pass

    def stop(self):
        pass

    def predict(self, image: Image.Image, threshold: Optional[float]):
        raise NotImplementedError()

    def name(self):
        raise NotImplementedError()

def get_replaced_tag(tag: str):
    use_spaces = settings.current.tagger_use_spaces
    if use_spaces:
        tag = tag.replace("_", " ")
    return tag

def get_arranged_tags(probs: dict[str, float]):
    return [tag for tag, _ in sorted(probs.items(), key=lambda x: -x[1])]


class DeepDanbooru(Tagger):
    def __init__(self):
        self.tagger_inst = DepDanbooruTagger()

    def start(self, skip_online: bool=False):
        self.tagger_inst.load(skip_online=skip_online)
        return self

    def stop(self):
        self.tagger_inst.unload()

    def predict(self, image: Image.Image, threshold: Optional[float] = None):
        labels = self.tagger_inst.apply(image)

        if threshold is not None:
            probability_dict = dict(
                [(get_replaced_tag(x[0]), x[1]) for x in labels[4:] if x[1] > threshold]
            )
        else:
            probability_dict = dict(
                [(get_replaced_tag(x[0]), x[1]) for x in labels[4:]]
            )

        return probability_dict

    def name(self):
        return "DeepDanbooru"


class WaifuDiffusion(Tagger):
    def __init__(self, repo_name, threshold):
        self.repo_name = repo_name
        self.tagger_inst = WaifuDiffusionTagger("SmilingWolf/" + repo_name)
        self.threshold = threshold

    def start(self, skip_online: bool=False):
        self.tagger_inst.load(skip_online=skip_online)
        return self

    def stop(self):
        self.tagger_inst.unload()

    # brought from https://huggingface.co/spaces/SmilingWolf/wd-v1-4-tags/blob/main/app.py and modified
    # set threshold<0 to use default value for now...
    def predict(self, image: Image.Image, threshold: Optional[float] = None):
        # may not use ratings
        # rating = dict(labels[:4])

        labels = self.tagger_inst.apply(image)

        if threshold is not None:
            if threshold < 0:
                threshold = self.threshold
            probability_dict = dict(
                [(get_replaced_tag(x[0]), x[1]) for x in labels[4:] if x[1] > threshold]
            )
        else:
            probability_dict = dict(
                [(get_replaced_tag(x[0]), x[1]) for x in labels[4:]]
            )

        return probability_dict

    def name(self):
        return self.repo_name
