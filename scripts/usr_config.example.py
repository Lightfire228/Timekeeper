# copy this into _usr_config.py

from .config import Config
from pathlib import Path

CONFIG = Config(
    jdk         = Path(''),
    android_sdk = Path.home() / 'Android/sdk',
    device_id   = '',

    # https://github.com/dotnet/source-build/issues/3242
    dotnet      = Path.home() / '.dotnet/dotnet',
)


