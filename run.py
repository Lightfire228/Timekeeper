#!/bin/env python

from subprocess import run, PIPE, CompletedProcess
import shutil
from pathlib import Path
from typing import Generator
import json

ADB   = shutil.which('adb')
CARGO = shutil.which('cargo')

android_dir  = Path('./build/android')
backend      = Path('./tk_backend')

android_gen  = backend / 'gen/android/'
apk          = android_gen / 'app/build/outputs/apk/universal/release/app-universal-release.apk'

dev_config   = backend / 'tauri.conf.dev.json'
tauri_config = backend / 'tauri.conf.json'



def main():
    # android_prod()
    android_dev()

def android_dev():

    flag_file = android_gen / '.dev_build'

    # cache build
    if not flag_file.exists():
        print('building android dev')

        if android_gen.exists():
            shutil.rmtree(android_gen)

        run([CARGO, 'tauri', 'android', 'init', '--config', dev_config])

        copy_android_files(get_app_id())

    flag_file.touch()

    print('Run android dev')
    run([CARGO, 'tauri', 'android', 'dev',  '--config', dev_config])



def android_prod():
    print('building android prod')

    if android_gen.exists():
        shutil.rmtree(android_gen)

    run([CARGO, 'tauri', 'android', 'init'])

    copy_android_files(get_app_id(is_prod=True))

    run([CARGO, 'tauri', 'android', 'build'])

    run([ADB, 'install', apk])


def iter_android_dir():
    for f in android_dir.glob('**/*'):
        if f.is_file() and '.gradle/' not in str(f):
            yield f

def relative_to(path: Path, other: Path):
    result = []

    for (part, other_part) in zip_(path.parts, other.parts):
        
        if len(result) > 0 or part != other_part:
            result.append(other_part)
    
    return Path('/'.join(result))



def zip_(a, b) -> Generator[str, str]:
    a = list(a)
    b = list(b)

    count = max(len(a), len(b))

    for i in range(count):
        x1 = None
        x2 = None
    
        try:
            x1 = a[i]
        except:
            ...

        try:
            x2 = b[i]
        except:
            ...

        yield (x1, x2)

def copy_android_files(app_id: str):

    for file in iter_android_dir():
        text = file.read_text()
        text = text.replace('#app_id#', app_id)

        dest = android_gen / relative_to(android_dir, file)
        dest.write_text(text)

def get_app_id(is_prod=False) -> str:

    text = tauri_config.read_text() if is_prod else dev_config.read_text()

    data = json.loads(text)
    return data['identifier']
    

if __name__ == '__main__':
    main()