from subprocess import CompletedProcess, run as s_run, PIPE
from pathlib import Path
import shutil

from . import CONFIG

ADB  = shutil.which('adb')
MOST = shutil.which('most')

def main():
    
    while True:
        print('Menu:')
        print('  1 - build android (default)')
        print('  2 - cat log')
        print('  3 - new migration')
        print('  4 - remove migration')

        usr_in = input('> ').strip()
        usr_in = usr_in or '1'

        match usr_in:
            case '1': build_android()
            case '2': cat_log()
            case '3': new_migration('')
            case '4': remove_migration()

        print()

def build_android():

    run([
         CONFIG.dotnet, 'build', 'Tk.App', '-t:Run',

         '-f:net9.0-android',
        #  '--configuration', 'Release',
        f'-p:DeviceName={CONFIG.device_id_develop}',
        f'-p:AndroidSdkDirectory={CONFIG.android_sdk}',
        f'-p:JavaSdkDirectory={CONFIG.jdk}',
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

def cat_log():

    p = run([
        ADB, 'shell',
        'su -c "cat /data/data/com.companyname.Tk.App/files/log.log"'
    ], capture_out=True)

    run([MOST, '+100000'], input=p.stdout)


def run(
        cmds: list[str | Path],

        allow_error      = False,
        capture_out      = False,
        cwd:        str  = None,
        input:      str  = None,
) -> CompletedProcess[bytes]:

    cmds   = [str(x) for x in cmds]

    stdout = PIPE if capture_out else None

    p = s_run(cmds, cwd=cwd, stdout=stdout, input=input)

    if not allow_error:
        p.check_returncode()

    return p


if __name__ == '__main__':
    main()