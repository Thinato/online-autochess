import pygame as pg

class Game:
    playing = False
    window = None
    clock = None
    def __init__(self) -> None:
        pass

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

            self.update()
            self.draw()

    def draw(self):
        self.window.fill((0, 0, 0))

    def update(self):
        pg.display.flip()

    def handle_event(self, event):
        if event.type == pg.QUIT:
            self.playing = False
            pg.quit()
            exit()