import pygame as pg
from engine.base_sceen import BaseScene
from websocket import create_connection
from engine.config import Config


class Game:
    playing = False
    window = None
    clock = None

    def __init__(
        self, screens: dict[str, BaseScene], current_screen: str, config: Config
    ):
        self.scenes = screens
        self.current_scene = self.scenes[current_screen]
        self.screen = pg.Surface((800, 600))
        self.ws = create_connection(
            "ws://{}:{}".format(config.SERVER_URL, config.SERVER_PORT)
        )

    def start(self):
        self.playing = True
        self.window = pg.display.set_mode((800, 600))
        self.clock = pg.time.Clock()
        self.scenes = {}
        self.main_loop()

    def main_loop(self):
        while self.playing:
            dt = self.clock.tick() / 1000
            for event in pg.event.get():
                self.handle_event(event)

            self.update(dt)

    def update(self, dt):
        self.current_scene.update(dt)
        self.current_scene.draw()

        pg.display.flip()

    def handle_event(self, event):
        if event.type == pg.QUIT:
            self.playing = False
            pg.quit()
            exit()
        self.current_scene.handle_event(event)
