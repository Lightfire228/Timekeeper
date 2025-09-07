from subprocess import CompletedProcess, run as s_run
from pathlib import Path

from . import CONFIG

def main():
    
    # build_android()
    # new_migration('test')
    remove_migration()

def build_android():

    run([
         CONFIG.dotnet, 'build', 'Tk.App', '-t:Run',

        f'-p:DeviceName={CONFIG.device_id}',
        f'-p:AndroidSdkDirectory={CONFIG.android_sdk}',
        f'-p:JavaSdkDirectory={CONFIG.jdk}',
         '-f:net9.0-android',
         '-t:InstallAndroidDependencies',
         '-p:AcceptAndroidSDKLicenses=True',
    ])

def new_migration(name: str):

    run([
         CONFIG.dotnet, 'ef', 'migrations', 'add', name, 
         
         '--startup-project', 'Tk.App.Linux',
         '--project',         'Tk.Database',
    ])

def remove_migration():

    run([
         CONFIG.dotnet, 'ef', 'migrations', 'remove',
         
         '--startup-project', 'Tk.App.Linux',
         '--project',         'Tk.Database',
    ])

def run(cmds: list[str | Path], allow_error = False) -> CompletedProcess[bytes]:
    cmds = [str(x) for x in cmds]

    p = s_run(cmds)

    if not allow_error:
        p.check_returncode()

    return p


if __name__ == '__main__':
    main()