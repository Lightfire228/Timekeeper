def bail(msg: str):
    import sys

    print(msg, file=sys.stderr)
    sys.exit(1)


try:
    from . import _usr_config
    del _usr_config
except ImportError:
    bail("'_usr_config.py' not found")

try:
    from ._usr_config import CONFIG
except ImportError:
    bail("CONFIG var not set in '_usr_config'")


from ._usr_config import CONFIG

