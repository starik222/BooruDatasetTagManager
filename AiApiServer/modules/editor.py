from .editors import RMBG2Editor


class editor:
    def __enter__(self):
        self.start()
        return self

    def __exit__(self, exception_type, exception_value, traceback):
        self.stop()
        pass

    def start(self, net_params: dict, skip_online: bool = False):
        pass

    def stop(self):
        pass

    def predict(self, image):
        raise NotImplementedError()

    def name(self):
        raise NotImplementedError()

    def mode_type(self):
        raise NotImplementedError()


class RMBG2(editor):
    def __init__(self, repo_name, resulution, intType):
        self.editor = RMBG2Editor(repo_name)
        self.repo_name = repo_name
        self.resolution = resulution
        self.type = intType

    def start(self, net_params: dict, skip_online: bool = False):
        self.editor.load(self.resolution, skip_online=skip_online)

    def stop(self):
        self.editor.unload()

    def predict(self, image):
        res = self.editor.apply(image)
        # tags = res[0].split(",")
        return res  # [t for t in tags if t]

    def name(self):
        return self.repo_name

    def mode_type(self):
        return self.type
