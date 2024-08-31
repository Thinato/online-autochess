import pygame as pg


class BaseScreen:
    def __init__(self, screen):
        self.screen = screen
        self.screen.fill((255, 255, 255))
        self.running = True
        self.components = []

    def update(self):
        pass

    def draw(self):
        pass
