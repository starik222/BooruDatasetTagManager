from pathlib import Path

from . import utilities
from . import settings

setting_model_path: Path = None
models_path: Path = None

def initialize():
    global setting_model_path, models_path

    setting_model_path = (
        Path(settings.current.interrogator_model_dir)
        if settings.current.interrogator_model_dir
        else None
    )

    if setting_model_path is not None and not setting_model_path.is_dir():
        setting_model_path.mkdir(parents=True)

    models_path = (
        setting_model_path
        if setting_model_path is not None
        else utilities.base_dir_path() / "models"
    )
