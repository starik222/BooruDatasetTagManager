# -*- mode: python ; coding: utf-8 -*-
from PyInstaller.utils.hooks import copy_metadata
from PyInstaller.utils.hooks import collect_data_files

a = Analysis(
    ['main.py'],
    pathex=[],
    binaries=[],
    datas=[
        *copy_metadata('tqdm'),
        *copy_metadata('tensorflow'),
        *copy_metadata('torch'),
        *copy_metadata('regex'),
        *copy_metadata('requests'),
        *copy_metadata('packaging'),
        *copy_metadata('pillow'),
        *copy_metadata('filelock'),
        *copy_metadata('onnxruntime_gpu'),
        *copy_metadata('numpy'),
        *copy_metadata('tokenizers'),
        *copy_metadata('importlib_metadata'),

        *collect_data_files('transformers', include_py_files=True, includes=['**/*.py']),
    ],
    hiddenimports=[
        'torch',
        'tensorflow',
        'tqdm',
        'onnxruntime',
        'onnxruntime_gpu',
        'PIL',
    ],
    hookspath=[],
    hooksconfig={},
    runtime_hooks=[],
    excludes=[
        'matplotlib',
        'h5py',
    ],
    noarchive=False,
)
pyz = PYZ(a.pure)

exe = EXE(
    pyz,
    a.scripts,
    [],
    exclude_binaries=True,
    name='main',
    debug=False,
    bootloader_ignore_signals=False,
    strip=False,
    upx=True,
    console=True,
    disable_windowed_traceback=False,
    argv_emulation=False,
    target_arch=None,
    codesign_identity=None,
    entitlements_file=None,
)
coll = COLLECT(
    exe,
    a.binaries,
    a.datas,
    strip=False,
    upx=True,
    upx_exclude=[],
    name='interrogator_rpc',
)
