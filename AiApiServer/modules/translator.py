# pylint: disable=bad-indentation
from abc import ABC

from .translators import seed_x_translator


class translator:
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

    def translate(self, original_text: str, from_lang: str, to_lang: str):
        raise NotImplementedError()

    def name(self):
        raise NotImplementedError()

    def mode_type(self):
        raise NotImplementedError()


class seed_x(translator):
    def __init__(self, repo_name, intType):
        self.translator = seed_x_translator(repo_name)
        self.repo_name = repo_name
        self.type = intType

    def start(self, net_params: dict, skip_online: bool = False):
        self.translator.load(skip_online=skip_online)

    def stop(self):
        self.translator.unload()

    def translate(self, original_text: str, from_lang: str, to_lang: str):
        return self.translator.translate(original_text, from_lang, to_lang)

    def name(self):
        return self.repo_name

    def mode_type(self):
        return self.type
