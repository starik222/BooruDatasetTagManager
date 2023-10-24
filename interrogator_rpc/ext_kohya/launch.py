import importlib.util
import os
import subprocess
import sys

from . import cmd_args, logger


# ================================================================
# brought from AUTOMATIC1111/stable-diffusion-webui and modified

python = sys.executable


def check_python_version():
    major = sys.version_info.major
    minor = sys.version_info.minor
    micro = sys.version_info.micro

    if not (major == 3 and minor >= 9):
        logger.error(
            f"""
INCOMPATIBLE PYTHON VERSION

This program is aimed to work on Python >=3.9 (developed with 3.10.11), but you have {major}.{minor}.{micro}.
"""
        )


def is_installed(package):
    try:
        spec = importlib.util.find_spec(package)
    except ModuleNotFoundError:
        return False

    return spec is not None


def run(command):
    return subprocess.run(
        command,
        stdout=subprocess.PIPE,
        stderr=subprocess.PIPE,
        shell=True,
        env=os.environ,
    )


def run_pip(args, desc=None):
    if desc is not None:
        print(f"Installing {desc}")

    command = f'"{sys.executable}" -m pip {args}'
    result = run(command)
    if result.returncode != 0:
        message = f"""
Couldn't Install {desc}.
Command: {command}
Error code: {result.returncode}
stdout: {result.stdout.decode(encoding="utf8", errors="ignore") if len(result.stdout)>0 else '<empty>'}
stderr: {result.stderr.decode(encoding="utf8", errors="ignore") if len(result.stderr)>0 else '<empty>'}
"""
        raise RuntimeError(message)


# ================================================================


def prepare_environment():
    opts = cmd_args.get_args()

    if opts.force_install_torch is None:
        pass
    elif opts.force_install_torch == "cpu":
        torch_command = "pip install -U torch torchvision"
    else:
        torch_command = f"pip install -U torch torchvision --index-url https://download.pytorch.org/whl/{opts.force_install_torch}"
    if (
        not is_installed("torch")
        or not is_installed("torchvision")
        or opts.force_install_torch is not None
    ):
        run(f'"{python}" -m {torch_command}')
    check_python_version()

    import devices

    logger.write(f"PyTorch device: {devices.device}")


if __name__ == "__main__":
    prepare_environment()

    import interface

    interface.main()
