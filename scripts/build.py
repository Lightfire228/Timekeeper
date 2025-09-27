from subprocess import CompletedProcess, run as s_run, PIPE
from pathlib import Path
import shutil

from . import CONFIG

ADB  = shutil.which('adb')
MOST = shutil.which('most')

def main():
    
    done = False

    while not done:

        try:
            done = menu()

        except (KeyboardInterrupt):
            raise
        
        except:
            ...


def menu() -> bool:

    opts = {
        '1': ('build android (default)', build_android),
        '2': ('cat log',                 cat_log),
        '3': ('new migration',           new_migration),
        '4': ('remove migration',        remove_migration),
        '5': ('clean build',             clean_build),
        '6': ('build android release',   build_android_release),
        'q': ('quit',                    lambda: ...),
    }

    print('Menu:')
    for key in opts:
        display = opts[key][0]
        print(f'  {key} - {display}')

    usr_in = input('> ').strip()
    usr_in = usr_in or '1'

    if usr_in == 'q':
        return True

    opts.get(usr_in, lambda: ...)[1]()

    print()
    return False

def build_android_release():
    build_android(release=True)

def build_android(release=False):

    args = []

    if release:
        if not confirm():
            return
        
        args = [
            *args,
            '--configuration', 'Release',
        ]

    run([
         CONFIG.dotnet, 'build', 'Tk.App', '-t:Run',

         '-f:net9.0-android',
        f'-p:DeviceName={CONFIG.device_id_develop}',
        f'-p:AndroidSdkDirectory={CONFIG.android_sdk}',
        f'-p:JavaSdkDirectory={CONFIG.jdk}',
         '-t:InstallAndroidDependencies',
         '-p:AcceptAndroidSDKLicenses=True',
        *args
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
        'su -c "cat /data/data/com.companyname.Tk.App.Develop/files/log.log"'
    ], capture_out=True)

    run([MOST, '+100000'], input=p.stdout)


def clean_build():

    globs = [
        *Path('./').glob('**/obj/'),
        *Path('./').glob('**/bin/'),
    ]

    for f in globs:
        shutil.rmtree(f)


def confirm() -> bool:
    x = input('>> Are you sure? (y/N)\n> ')

    return x.lower().startswith('y')

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