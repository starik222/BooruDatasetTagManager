# pylint: disable=bad-indentation

from .interrogator import Interrogator
from .interrogators import BLIPLargeCaptioning, BLIP2Captioning, GITLargeCaptioning, Florence2Captioning, \
    Moondream2Captioning, JoyCaptionCaptioning, Qwen25CaptionCaptioning, KeyeCaptionCaptioning


class Captioning(Interrogator):
    def start(self, net_params: dict, skip_online: bool = False):
        pass

    def stop(self):
        pass

    def predict(self, data_obj, data_type):
        raise NotImplementedError()

    def name(self):
        raise NotImplementedError()

    def mode_type(self):
        raise NotImplementedError()


class BLIP(Captioning):
    def __init__(self, intType):
        self.interrogator = BLIPLargeCaptioning()
        self.type = intType
        self.video_supported = False
        self.repo_name = ""

    def start(self, net_params: dict, skip_online: bool = False):
        self.interrogator.load(skip_online=skip_online)

    def stop(self):
        self.interrogator.unload()

    def predict(self, data_obj, data_type):
        tags = self.interrogator.apply(data_obj, data_type)[0].split(",")
        return [t for t in tags if t]

    def name(self):
        return "BLIP"

    def mode_type(self):
        return self.type


class BLIP2(Captioning):
    def __init__(self, repo_name, intType):
        self.interrogator = BLIP2Captioning(repo_name)
        self.repo_name = repo_name
        self.type = intType
        self.video_supported = False

    def start(self, net_params: dict, skip_online: bool = False):
        self.interrogator.load(skip_online=skip_online)

    def stop(self):
        self.interrogator.unload()

    def predict(self, data_obj, data_type):
        tags = self.interrogator.apply(data_obj, data_type)[0].split(",")
        return [t for t in tags if t]

    def name(self):
        return self.repo_name

    def mode_type(self):
        return self.type


class GITLarge(Captioning):
    def __init__(self, intType):
        self.interrogator = GITLargeCaptioning()
        self.type = intType
        self.video_supported = False
        self.repo_name = ""

    def start(self, net_params: dict, skip_online: bool = False):
        self.interrogator.load(skip_online=skip_online)

    def stop(self):
        self.interrogator.unload()

    def predict(self, data_obj, data_type):
        tags = self.interrogator.apply(data_obj, data_type)[0].split(",")
        return [t for t in tags if t]

    def name(self):
        return "GIT-large-COCO"

    def mode_type(self):
        return self.type


class Florence2(Captioning):
    def __init__(self, repo_name, commandsList, defPrompt, needSplit, intType):
        self.interrogator = Florence2Captioning(repo_name)
        self.repo_name = repo_name
        self.commands = commandsList
        self.defaultCommand = commandsList[0]
        self.defaultPrompt = defPrompt
        self.split = needSplit
        self.type = intType
        self.video_supported = False

    def start(self, net_params: dict, skip_online: bool = False):
        if 'cmd' in net_params:
            self.defaultCommand = net_params['cmd']
        if 'prompt' in net_params:
            self.defaultPrompt = net_params['prompt']
        if 'split' in net_params:
            self.split = net_params['split'] == "true"
        self.interrogator.load(self.defaultCommand, self.defaultPrompt, self.split, skip_online=skip_online)

    def stop(self):
        self.interrogator.unload()

    def predict(self, data_obj, data_type):
        res = self.interrogator.apply(data_obj, data_type)
        # tags = res[0].split(",")
        return res  # [t for t in tags if t]

    def name(self):
        return self.repo_name

    def mode_type(self):
        return self.type


class Moondream2(Captioning):
    def __init__(self, repo_name, commandsList, defPrompt, needSplit, intType):
        self.interrogator = Moondream2Captioning(repo_name)
        self.repo_name = repo_name
        self.commands = commandsList
        self.defaultCommand = commandsList[0]
        self.defaultPrompt = defPrompt
        self.split = needSplit
        self.type = intType
        self.video_supported = False

    def start(self, net_params: dict, skip_online: bool = False):
        if 'cmd' in net_params:
            self.defaultCommand = net_params['cmd']
        if 'query' in net_params:
            self.defaultPrompt = net_params['query']
        if 'split' in net_params:
            self.split = net_params['split'] == "true"
        self.interrogator.load(self.defaultCommand, self.defaultPrompt, self.split, skip_online=skip_online)

    def stop(self):
        self.interrogator.unload()

    def predict(self, data_obj, data_type):
        res = self.interrogator.apply(data_obj, data_type)
        # tags = res[0].split(",")
        return res  # [t for t in tags if t]

    def name(self):
        return self.repo_name

    def mode_type(self):
        return self.type


class JoyCaption(Captioning):
    def __init__(self, repo_name, defPrompt, needSplit, intType):
        self.interrogator = JoyCaptionCaptioning(repo_name)
        self.repo_name = repo_name
        # self.commands = commandsList
        # self.defaultCommand = commandsList[0]
        self.defaultPrompt = defPrompt
        self.split = needSplit
        self.type = intType
        self.video_supported = False

    def start(self, net_params: dict, skip_online: bool = False):
        # if 'cmd' in net_params:
        #     self.defaultCommand = net_params['cmd']
        if 'query' in net_params:
            self.defaultPrompt = net_params['query']
        if 'split' in net_params:
            self.split = net_params['split'] == "true"
        self.interrogator.load(self.defaultPrompt, self.split, skip_online=skip_online)

    def stop(self):
        self.interrogator.unload()

    def predict(self, data_obj, data_type):
        res = self.interrogator.apply(data_obj, data_type)
        # tags = res[0].split(",")
        return res  # [t for t in tags if t]

    def name(self):
        return self.repo_name

    def mode_type(self):
        return self.type


class Qwen25Caption(Captioning):
    def __init__(self, repo_name, defPrompt, needSplit, video_supported, intType):
        self.interrogator = Qwen25CaptionCaptioning(repo_name)
        self.repo_name = repo_name
        # self.commands = commandsList
        # self.defaultCommand = commandsList[0]
        self.defaultPrompt = defPrompt
        self.split = needSplit
        self.type = intType
        self.video_supported = video_supported
        self.fps = 1
        self.max_frames = -1
        self.min_pixels = -1
        self.max_pixels = -1

    def start(self, net_params: dict, skip_online: bool = False):
        # if 'cmd' in net_params:
        #     self.defaultCommand = net_params['cmd']
        if 'query' in net_params:
            self.defaultPrompt = net_params['query']
        if 'split' in net_params:
            self.split = net_params['split'] == "true"
        if 'fps' in net_params:
            self.fps = net_params['fps']
        if 'max_frames' in net_params:
            self.max_frames = net_params['max_frames']
        if 'min_pixels' in net_params:
            self.min_pixels = net_params['min_pixels']
        if 'max_pixels' in net_params:
            self.max_pixels = net_params['max_pixels']
        self.interrogator.load(self.defaultPrompt, self.split, self.fps, self.max_frames, self.min_pixels, self.max_pixels, skip_online=skip_online)

    def stop(self):
        self.interrogator.unload()

    def predict(self, data_obj, data_type):
        res = self.interrogator.apply(data_obj, data_type)
        # tags = res[0].split(",")
        return res  # [t for t in tags if t]

    def name(self):
        return self.repo_name

    def mode_type(self):
        return self.type

class KeyeCaption(Captioning):
    def __init__(self, repo_name, defPrompt, needSplit, video_supported, intType):
        self.interrogator = KeyeCaptionCaptioning(repo_name)
        self.repo_name = repo_name
        # self.commands = commandsList
        # self.defaultCommand = commandsList[0]
        self.defaultPrompt = defPrompt
        self.split = needSplit
        self.type = intType
        self.video_supported = video_supported
        self.fps = 1
        self.max_frames = -1
        self.min_pixels = -1
        self.max_pixels = -1

    def start(self, net_params: dict, skip_online: bool = False):
        # if 'cmd' in net_params:
        #     self.defaultCommand = net_params['cmd']
        if 'query' in net_params:
            self.defaultPrompt = net_params['query']
        if 'split' in net_params:
            self.split = net_params['split'] == "true"
        if 'fps' in net_params:
            self.fps = net_params['fps']
        if 'max_frames' in net_params:
            self.max_frames = net_params['max_frames']
        if 'min_pixels' in net_params:
            self.min_pixels = net_params['min_pixels']
        if 'max_pixels' in net_params:
            self.max_pixels = net_params['max_pixels']
        self.interrogator.load(self.defaultPrompt, self.split, self.fps, self.max_frames, self.min_pixels, self.max_pixels, skip_online=skip_online)

    def stop(self):
        self.interrogator.unload()

    def predict(self, data_obj, data_type):
        res = self.interrogator.apply(data_obj, data_type)
        # tags = res[0].split(",")
        return res  # [t for t in tags if t]

    def name(self):
        return self.repo_name

    def mode_type(self):
        return self.type
