import pygame as pg

from engine.base_sceen import BaseScene
from engine.config import Config
from engine.connection import Connection
from engine.scene_message import SceneMessage


class Game:
    playing: bool = False
    window: pg.Surface
    clock: pg.Clock
    current_scene: str = None
    conn: Connection

    def __init__(self, config: Config, conn: Connection):
        self.window = pg.display.set_mode((config.RESOLUTION))
        self.screen = pg.Surface(config.RESOLUTION)
        self.config = config
        self.scenes = {}
        self.conn = conn

    def add_scene(self, scene: BaseScene, current: bool = False):
        new_scene = scene(self.screen, self.conn, self.config)
        if self.current_scene is None or current:
            self.current_scene = new_scene.name
        self.scenes[new_scene.name] = new_scene

    def start(self):
        self.playing = True
        self.window = pg.display.set_mode(self.config.RESOLUTION)
        self.clock = pg.time.Clock()
        self.main_loop()

    def main_loop(self):
        while self.playing:
            dt = self.clock.tick() / 1000
            for event in pg.event.get():
                self.handle_event(event)
            self.update(dt)

    def update(self, dt: float):
        self.scenes[self.current_scene].update(dt)
        self.scenes[self.current_scene].draw()
        self.window.blit(self.screen, (0, 0))

        pg.display.flip()

    def handle_event(self, event: pg.Event):
        # this should be a separated class, but ¯\_(ツ)_/¯
        message = self.scenes[self.current_scene].handle_event(event)

        match message:
            case SceneMessage.BATTLEGROUND:
                self.current_scene = "battleground"
            case SceneMessage.FAILED_TO_START_GAME:
                print("Failed to start game")
            case SceneMessage.OPTIONS:
                print("Options")
            case SceneMessage.EXIT:
                self.stop()

        if event.type == pg.QUIT:
            self.stop()

    def stop(self):
        self.playing = False
        if self.conn.is_connected:
            self.conn.close()
        pg.quit()
        exit()
