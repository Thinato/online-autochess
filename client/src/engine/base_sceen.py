import pygame as pg
from websocket import WebSocket

from engine.camera import Camera
from engine.config import Config
from engine.connection import Connection


class BaseScene:
    def __init__(self, screen, conn: Connection, config: Config):
        self.screen = screen
        self.screen.fill((255, 255, 255))
        self.running = True
        self.elements = []
        self.camera = Camera(size=(800, 600), speed=5)
        self.conn = conn
        self.config = config

    def update(self, dt):
        pass

    def draw(self):
        pass

    def handle_event(self, event):
        pass
