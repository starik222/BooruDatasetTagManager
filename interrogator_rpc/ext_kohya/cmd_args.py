import argparse

def get_args():
    parser = argparse.ArgumentParser()

    parser.add_argument(
        "--device-id", type=int, help="CUDA Device ID to use interrogators", default=None
    )
    parser.add_argument(
        "--force-install-torch",
        choices=['cu117', 'cu118', 'cu120', 'cpu'],
        help="Force install the latest PyTorch with specified compute platform (if not installed in this computer)",
        default=None,
    )

    opts = parser.parse_args()

    return opts



