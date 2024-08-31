import pygame as pg
from engine.base_sceen import BaseScreen


class MainMenu(BaseScreen):
    def __init__(self):
        super().__init__(pg.display.set_mode((800, 600)))

    def update(self):
        for element in self.elements:
            element.update()

    def draw(self, dt):
        for element in self.elements:
            element.draw()
