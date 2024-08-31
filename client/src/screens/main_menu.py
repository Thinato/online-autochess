import pygame as pg
from src.screens.base_sceen import BaseScreen

class MainMenu(BaseScreen):
    def __init__(self):
        super().__init__(pg.display.set_mode((800, 600)))

    def update(self):
        for compoenet in self.components:
            compoenet.update()
        
    