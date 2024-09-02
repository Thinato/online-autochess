import pygame as pg
from ui.element import Element


class Button(Element):
    def __init__(self, *args, **kwargs):
        super().__init__(*args, **kwargs)
        self.hover = False

    def update(self, dt):
        pass

    def draw(self):
        if self.hover:
            pg.draw.rect(
                self.screen, (0, 255, 0), (self.x, self.y, self.width, self.height)
            )
        else:
            pg.draw.rect(
                self.screen, (255, 0, 0), (self.x, self.y, self.width, self.height)
            )

    def check_hover(self, mouse_pos):
        x, y = mouse_pos
        if self.x <= x <= self.x + self.width and self.y <= y <= self.y + self.height:
            self.hover = True
        else:
            self.hover = False
