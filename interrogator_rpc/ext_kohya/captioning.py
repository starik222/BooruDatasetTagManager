from .interrogator import Interrogator
from .interrogators import BLIPLargeCaptioning, BLIP2Captioning, GITLargeCaptioning


class Captioning(Interrogator):
    def start(self):
        pass

    def stop(self):
        pass

    def predict(self, image):
        raise NotImplementedError()
    
    def predict_multi(self, image):
        raise NotImplementedError()

    def name(self):
        raise NotImplementedError()


class BLIP(Captioning):
    def __init__(self):
        self.interrogator = BLIPLargeCaptioning()
    
    def start(self):
        self.interrogator.load()

    def stop(self):
        self.interrogator.unload()

    def predict(self, image):
        tags = self.interrogator.apply(image)[0].split(",")
        return [t for t in tags if t]
    
    def predict_multi(self, images:list):
        captions = self.interrogator.apply(images)
        return [[t for t in caption.split(',') if t] for caption in captions]

    def name(self):
        return "BLIP"


class BLIP2(Captioning):
    def __init__(self, repo_name):
        self.interrogator = BLIP2Captioning("Salesforce/" + repo_name)
        self.repo_name = repo_name
    
    def start(self):
        self.interrogator.load()

    def stop(self):
        self.interrogator.unload()

    def predict(self, image):
        tags = self.interrogator.apply(image)[0].split(",")
        return [t for t in tags if t]
    
    def predict_multi(self, images:list):
        captions = self.interrogator.apply(images)
        return [[t for t in caption.split(',') if t] for caption in captions]

    def name(self):
        return self.repo_name


class GITLarge(Captioning):
    def __init__(self):
        self.interrogator = GITLargeCaptioning()

    def start(self):
        self.interrogator.load()

    def stop(self):
        self.interrogator.unload()

    def predict(self, image):
        tags = self.interrogator.apply(image)[0].split(",")
        return [t for t in tags if t]
    
    def predict_multi(self, images:list):
        captions = self.interrogator.apply(images)
        return [[t for t in caption.split(',') if t] for caption in captions]

    def name(self):
        return "GIT-large-COCO"
