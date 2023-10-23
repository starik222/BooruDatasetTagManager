class Interrogator:
    def __enter__(self):
        self.start()
        return self

    def __exit__(self, exception_type, exception_value, traceback):
        self.stop()
        pass

    def start(self):
        pass

    def stop(self):
        pass

    def predict(self, image, **kwargs):
        raise NotImplementedError()

    def name(self):
        raise NotImplementedError()
