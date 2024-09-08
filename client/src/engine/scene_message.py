from enum import Enum


class SceneMessage(Enum):
    START_GAME = 1
    FAILED_TO_START_GAME = 2
    OPTIONS = 3
    EXIT = 4
