import pygame as pg
from enum import Enum

class Element:
    def __init__(
        self,
        idx: int,
        screen: pg.Surface,
        pos: tuple[int, int],
        size: tuple[int, int],
        text: str,
        font: pg.font.Font = None,
    ):
        self.id = idx
        self.x = pos[0]
        self.y = pos[1]
        self.width = size[0]
        self.height = size[1]
        self.text = text
        self.screen = screen
        self.hover = False
        self.font = font or pg.font.Font(None, 32)

    def update(self, dt):
        pass

    def draw(self):
        pass

    def check_hover(self, mouse_pos):
        pass
