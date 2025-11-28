#!/bin/env python

import os
from subprocess import run
from pathlib import Path

from shutil import which

def main():
    android_home = Path.home()  / 'Android/Sdk'
    ndk_home     = android_home / 'ndk'
    os.environ['JAVA_HOME']        = '/opt/android-studio/jbr'
    os.environ['ANDROID_HOME']     = str(android_home)
    os.environ['ANDROID_SDK_ROOT'] = ''
    os.environ['NDK_HOME']         = str(ndk_home)

    cargo = which('cargo')

    run([cargo, 'tauri', 'android', 'dev'])



if __name__ == '__main__':
    main()