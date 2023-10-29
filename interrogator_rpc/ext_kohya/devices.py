import sys
from . import cmd_args
import torch


# ================================================================
# borrowed from AUTOMATIC1111/stable-diffusion-webui

# has_mps is only available in nightly pytorch (for now) and macOS 12.3+.
# check `getattr` and try it for compatibility
def check_for_mps() -> bool:

    try:
        return torch.backends.mps.is_built()
    except Exception:
        return False


_has_mps = check_for_mps()

# ================================================================


def has_mps():
    if sys.platform != "darwin":
        return False
    else:
        return _has_mps


def get_cuda():
    opts = cmd_args.get_args()
    if opts.device_id is not None:
        return torch.cuda.device(f"cuda:{opts.device_id}")
    else:
        return torch.cuda.device("cuda")


def get_cuda_device():
    opts = cmd_args.get_args()
    if opts.device_id is not None:
        return torch.device(f"cuda:{opts.device_id}")
    else:
        return torch.device("cuda")


def get_optimal_device():
    if torch.cuda.is_available():
        return get_cuda_device()

    if has_mps():
        torch.device("mps")

    try:
        import torch_directml
        return torch_directml.device()
    except:
        pass

    return torch.device("cpu")


# from AUTOMATIC1111's SDwebUI
def torch_gc():
    if torch.cuda.is_available():
        with get_cuda():
            torch.cuda.empty_cache()
            torch.cuda.ipc_collect()


device = None
cpu = None


def init_interrogator():
    global device
    global cpu

    device = get_optimal_device()
    cpu = torch.device("cpu")
