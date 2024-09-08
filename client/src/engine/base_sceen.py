import pygame as pg
from websocket import WebSocket

from engine.camera import Camera
from engine.config import Config


class BaseScene:
    def __init__(self, screen, ws: WebSocket, config: Config):
        self.screen = screen
        self.screen.fill((255, 255, 255))
        self.running = True
        self.elements = []
        self.camera = Camera(size=(800, 600), speed=5)
        self.ws = ws
        self.config = config

    def update(self, dt):
        pass

    def draw(self):
        pass

    def handle_event(self, event):
        pass
