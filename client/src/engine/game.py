import pygame as pg
from engine.base_sceen import BaseScene
from websocket import create_connection
from engine.config import Config


class Game:
    playing = False
    window = None
    clock = None
    ws = None 
    current_scene = None

    def __init__(self, config: Config):
        self.window = pg.display.set_mode((800, 600))
        self.screen = pg.Surface((800, 600))
        self.config = config
        self.scenes = {}

    def add_scene(self, scene, current: bool = False):
        new_scene = scene(self.screen, self)
        if self.current_scene is None or current:
            self.current_scene = new_scene.name 
        self.scenes[new_scene.name] = new_scene

    def connect(self):
        self.ws = create_connection(
            "ws://{}:{}".format(self.config.SERVER_URL, self.config.SERVER_PORT)
        )

    def start(self):
        self.playing = True
        self.window = pg.display.set_mode((800, 600))
        self.clock = pg.time.Clock()
        self.main_loop()

    def main_loop(self):
        while self.playing:
            dt = self.clock.tick() / 1000
            for event in pg.event.get():
                self.handle_event(event)
            self.update(dt)

    def update(self, dt):
        self.scenes[self.current_scene].update(dt)
        self.scenes[self.current_scene].draw()
        self.window.blit(self.screen, (0, 0))

        pg.display.flip()

    def handle_event(self, event):
        if event.type == pg.QUIT:
            self.playing = False
            pg.quit()
            exit()
        self.scenes[ self.current_scene ].handle_event(event)
