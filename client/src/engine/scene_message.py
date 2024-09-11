from enum import Enum


class SceneMessage(Enum):
    BATTLEGROUND = 1
    FAILED_TO_START_GAME = 2
    OPTIONS = 3
    EXIT = 4
    MAIN_MENU = 5
