import pygame as pg
from ui.element import Element


class Button(Element):
    def __init__(self, *args, **kwargs):
        super().__init__(*args, **kwargs)
        self.hover = False
        self.focus = False
        self.text = self.font.render(self.text, True, (255, 255, 255))
        self.text_pos = (
            self.x + (self.width - self.text.get_width()) // 2,
            self.y + (self.height - self.text.get_height()) // 2,
        )

    def update(self, dt):
        pass

    def draw(self):
        if self.hover:
            pg.draw.rect(
                self.screen, (0, 255, 0), (self.x, self.y, self.width, self.height)
            )
        else:
            pg.draw.rect(
                self.screen, (255, 0, 0), (self.x, self.y, self.width, self.height)
            )
        self.screen.blit(self.text, self.text_pos)

    def check_hover(self, mouse_pos):
        x, y = mouse_pos
        if self.x <= x <= self.x + self.width and self.y <= y <= self.y + self.height:
            self.hover = True
        else:
            self.hover = False

    def remove_focus(self):
        self.focus = False
