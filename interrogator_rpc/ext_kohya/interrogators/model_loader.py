from pathlib import Path

from torch.hub import download_url_to_file

def load(model_path:Path, model_url:str, progress:bool=True, force_download:bool=False):
    model_path = Path(model_path)
    if model_path.exists():
        return model_path

    if model_url is not None and (force_download or not model_path.is_file()):
        if not model_path.parent.is_dir():
            model_path.parent.mkdir(parents=True)
        download_url_to_file(model_url, model_path, progress=progress)
        return model_path

    return model_path
