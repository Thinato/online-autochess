import pygame as pg
from websocket import create_connection
from engine.base_sceen import BaseScene
from engine.scene_message import SceneMessage
from ui.button import Button
from collections import defaultdict


class MainMenu(BaseScene):
    def __init__(self, *args, **kwargs):
        super().__init__(*args, **kwargs)
        self.name = "main_menu"
        self.elements = [
            Button(1, self.screen, (100, 100), (100, 50), "Search Game"),
            Button(2, self.screen, (100, 200), (100, 50), "Options"),
            Button(3, self.screen, (100, 300), (100, 50), "Logout"),
        ]

    def update(self, dt):
        mpos = pg.mouse.get_pos()

        for element in self.elements:
            element.check_hover(mpos)
            element.update(dt)

    def draw(self):
        self.screen.fill((0, 0, 0))
        for element in self.elements:
            element.draw()

    def handle_event(self, event):
        if event.type == pg.MOUSEBUTTONDOWN:
            for element in self.elements:
                if element.hover:
                    match element.id:
                        case 1:
                            success = self.conn.connect(
                                self.config.SERVER_URL, self.config.SERVER_PORT
                            )

                            if success:
                                return SceneMessage.BATTLEGROUND
                            return SceneMessage.FAILED_TO_START_GAME
                        case 2:
                            return SceneMessage.OPTIONS
                        case 3:
                            return SceneMessage.EXIT
