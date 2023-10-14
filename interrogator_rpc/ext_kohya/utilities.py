from typing import Tuple
from pathlib import Path
import math
import threading

from PIL import Image

if not hasattr(Image, 'Resampling'):  # Pillow<9.0
    Image.Resampling = Image

def base_dir_path():
    return Path(__file__).parents[1].absolute()


def base_dir():
    return str(base_dir_path())


def resize(image: Image.Image, size: Tuple[int, int]):
    return image.resize(size, resample=Image.Resampling.LANCZOS)


def resize_and_fill(image: Image.Image, size: Tuple[int, int]):
    width, height = size
    scale_w, scale_h = width / image.width, height / image.height
    resized_w, resized_h = width, height
    if scale_w < scale_h:
        resized_h = image.height * resized_w // image.width
    elif scale_h < scale_w:
        resized_w = image.width * resized_h // image.height

    resized = resize(image, (resized_w, resized_h))
    if resized_w == width and resized_h == height:
        return resized

    fill_l = math.floor((width - resized_w) / 2)
    fill_r = width - resized_w - fill_l
    fill_t = math.floor((height - resized_h) / 2)
    fill_b = height - resized_h - fill_t
    result = Image.new("RGB", (width, height))
    result.paste(resized, (fill_l, fill_t))
    if fill_t > 0:
        result.paste(resized.resize((width, fill_t), box=(0, 0, width, 0)), (0, 0))
    if fill_b > 0:
        result.paste(
            resized.resize(
                (width, fill_b), box=(0, resized.height, width, resized.height)
            ),
            (0, resized.height + fill_t),
        )
    if fill_l > 0:
        result.paste(resized.resize((fill_l, height), box=(0, 0, 0, height)), (0, 0))
    if fill_r > 0:
        result.paste(
            resized.resize(
                (fill_r, height), box=(resized.width, 0, resized.width, height)
            ),
            (resized.width + fill_l, 0),
        )
    return result
