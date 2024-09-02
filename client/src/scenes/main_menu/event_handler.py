from engine.event_handler import EventHandler
import pygame as pg


class MainMenuEventHandler(EventHandler):
    def on_key_press(self, event):
        if event.key == pg.K_RETURN:
            self.director.next_scene()

    def on_click(self, event):
        pass

    def on_left_click(self, event):
        pass
