class Interrogator:
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

    def predict(self, data_obj, data_type, **kwargs):
        raise NotImplementedError()

    def name(self):
        raise NotImplementedError()

    def mode_type(self):
        raise NotImplementedError()
