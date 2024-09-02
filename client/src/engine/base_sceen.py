import pygame as pg
from engine.camera import Camera


class BaseScene:
    def __init__(self, screen):
        self.screen = screen
        self.screen.fill((255, 255, 255))
        self.running = True
        self.elements = []
        self.camera = Camera(size=(800, 600), speed=5)

    def update(self, dt):
        pass

    def draw(self):
        pass

    def handle_event(self, event):
        pass
