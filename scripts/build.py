from subprocess import CompletedProcess, run as s_run
from pathlib import Path

from . import CONFIG

def main():
    
    build_android()
    # build_linux()
    # new_migration('Init')
    # remove_migration()
    # add_dependency('Microsoft.Extensions.Logging.Console')

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

def build_linux():

    run([
         CONFIG.dotnet, 'run', '--project', 'Tk.App.Linux'
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

def add_dependency(dep: str):
    run([
         CONFIG.dotnet, 'add', 'package', dep,
         '--project', 'Tk.App',
    ])


def run(cmds: list[str | Path], allow_error = False, cwd: str = None) -> CompletedProcess[bytes]:
    cmds = [str(x) for x in cmds]

    p = s_run(cmds, cwd=cwd)

    if not allow_error:
        p.check_returncode()

    return p


if __name__ == '__main__':
    main()