from pathlib import Path

class Config():

    def __init__(self,
        jdk:               str | Path,
        android_sdk:       str | Path,
        dotnet:            str | Path,
        device_id:         str,
    ):
        self.jdk         = Path(jdk)        .resolve()
        self.android_sdk = Path(android_sdk).resolve()
        self.dotnet      = Path(dotnet)     .resolve()
        
        self.device_id = device_id

    