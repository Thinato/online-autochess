import pygame as pg

class Camera:
    def __init__(self, size: tuple[int, int], speed: int):
        self.speed = speed 
        self.size = pg.math.Vector2(size)