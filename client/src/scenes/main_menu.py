import pygame as pg
from engine.base_sceen import BaseScene
from ui.button import Button
from collections import defaultdict


class MainMenu(BaseScene):
    def __init__(self, *args, **kwargs):
        super().__init__(*args, **kwargs)
        self.name = 'main_menu'
        self.elements = [
            Button(1, self.screen, (100, 100), (100, 50), "Connect"),
            Button(2, self.screen, (100, 200), (100, 50), "Options"),
            Button(3, self.screen, (100, 300), (100, 50), "Exit"),
        ]

    def update(self, dt):
        mpos = pg.mouse.get_pos()

        for element in self.elements:
            element.check_hover(mpos)
            element.update(dt)

    def draw(self):
        for element in self.elements:
            element.draw()

    def handle_event(self, event):
        if event.type == pg.MOUSEBUTTONDOWN:
            for element in self.elements:
                if element.hover:
                    match element.id:
                        case 1:
                            self.ws.send("connect")
                        case 2:
                            print("Options")
                        case 3:
                            print("Exit")
        # return super().handle_event(event)
