import pygame as pg
from websocket import create_connection
from engine.base_sceen import BaseScene
from engine.scene_message import SceneMessage
from ui.button import Button
from collections import defaultdict


class Battleground(BaseScene):
    def __init__(self, *args, **kwargs):
        super().__init__(*args, **kwargs)
        self.name = "battleground"
        self.elements = []
        self.players = [
            pg.Rect(100, 100, 50, 50),
        ]
        self.player_size = (50, 50)
        self.player_pos = pg.Vector2(0, 0)
        self.player_dir = pg.Vector2(0, 0)

    def update(self, dt):
        mpos = pg.mouse.get_pos()
        # message = self.conn.receive()

        # if message:
        #     print("msg:", message.decode())

        if self.player_dir.magnitude() > 0:
            self.player_pos.xy += self.player_dir.normalize() * dt * 500

        int_x = int(self.player_pos.x)
        int_y = int(self.player_pos.y)
        self.conn.send_data(f'{int_x}:{int_y}')

        for element in self.elements:
            element.check_hover(mpos)
            element.update(dt)

    def draw(self):
        self.screen.fill((0, 0, 0))

        for player in self.players:
            pg.draw.rect(self.screen, (255, 0, 0), player)

        pg.draw.rect(self.screen, (0, 255, 0), (self.player_pos, self.player_size))

        for element in self.elements:
            element.draw()

    def handle_event(self, event):
        if event.type == pg.KEYDOWN:
            match event.key:
                case pg.K_ESCAPE:
                    return SceneMessage.MAIN_MENU
                case pg.K_w:
                    self.player_dir.y = -1
                case pg.K_a:
                    self.player_dir.x -= 1
                case pg.K_s:
                    self.player_dir.y += 1
                case pg.K_d:
                    self.player_dir.x += 1
        elif event.type == pg.KEYUP:
            match event.key:
                case pg.K_w:
                    self.player_dir.y = 0
                case pg.K_a:
                    self.player_dir.x = 0
                case pg.K_s:
                    self.player_dir.y = 0
                case pg.K_d:
                    self.player_dir.x = 0
