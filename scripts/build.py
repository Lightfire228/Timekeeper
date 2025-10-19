from subprocess import CompletedProcess, run as s_run, PIPE
from pathlib import Path
import shutil
import traceback
import re

from . import CONFIG

ADB  = shutil.which('adb')
MOST = shutil.which('most')

def main():
    
    while not menu(): ...


def menu() -> bool:

    EXIT = True

    def no_op():
        ...

    class Op():
        def __init__(self, name: str, func: callable):
            self.name = name
            self.func = func

    opts = {
        '1': Op('build android (default)', build_android),
        '2': Op('cat log',                 cat_log),
        '3': Op('build kotlin',            build_kotlin),
        '4': Op('',                        no_op),
        '5': Op('build problems',          build_problems),
        '6': Op('build android release',   build_android_release),
        'q': Op('quit',                    no_op),
    }

    print('Menu:')
    for key in opts:
        display = opts[key].name
        print(f'  {key} - {display}')

    usr_in = input('> ').strip()
    usr_in = usr_in or '1'

    if usr_in == 'q':
        return EXIT
    
    op = opts.get(usr_in, Op('', no_op))
    op.func()

    print()
    return not EXIT

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

    device_id = _get_device_id()

    if device_id == None:
        print('\a!! No device id found')
        return

    run([
         CONFIG.dotnet, 'build', 'Tk.App', '-t:Run',

         '-f:net9.0-android',
        f'-p:DeviceName={device_id}',
        f'-p:AndroidSdkDirectory={CONFIG.android_sdk}',
        f'-p:JavaSdkDirectory={CONFIG.jdk}',
         '-t:InstallAndroidDependencies',
         '-p:AcceptAndroidSDKLicenses=True',
        *args
    ])

def build_kotlin():

    dir = Path('./kotlin/')

    run(['./gradlew', 'build'], cwd = dir)


def cat_log():

    print('>> 1 -            log.log (default)')
    print('>> 2 -        startup.log')
    print('>> 3 -         notifs.log')
    print('>> 4 - notif_receiver.log')

    usr_in = input('>>> ')

    usr_in = usr_in.strip() or '1'

    match usr_in:
        case '1': file =            'log.log'
        case '2': file =        'startup.log'
        case '3': file =         'notifs.log'
        case '4': file = 'notif_receiver.log'

    p = run([
        ADB, 'shell',
        f'su -c "cat /data/data/Tk.App.Develop/files/{file}"'
    ], capture_out=True)

    run([MOST, '+100000'], input=p.stdout)

def build_problems():

    globs = [
        *Path('./').glob('**/obj/'),
        *Path('./').glob('**/bin/'),
    ]

    print('>> Removing build files')
    for f in globs:
        shutil.rmtree(f)

    print('>> dotnet workload repair')
    run([CONFIG.dotnet, 'workload', 'repair'])

    print('>> dotnet clean')
    run([CONFIG.dotnet, 'clean'])


def confirm() -> bool:
    x = input('>> Are you sure? (y/N)\n> ')

    return x.lower().startswith('y')

def run(
        cmds: list[str | Path],

        allow_error             = False,
        capture_out             = False,
        cwd:        str | Path  = None,
        input:      str         = None,
) -> CompletedProcess[bytes]:

    cmds   = [str(x) for x in cmds]
    cwd    = cwd and str(cwd)

    stdout = PIPE if capture_out else None

    p = s_run(cmds, cwd=cwd, stdout=stdout, input=input)

    if not allow_error:
        p.check_returncode()

    return p

def _get_device_id() -> str | None:

    p = run([ADB, 'devices'], capture_out=True)

    lines = p.stdout.decode('utf-8').splitlines()
    lines = lines[1:]
    lines = [l.strip() for l in lines]
    lines = [l         for l in lines if l != '']

    for line in lines:
        if m := re.match(r'\w+', line):
            return m.group(0)
        
        

    return None


if __name__ == '__main__':
    main()