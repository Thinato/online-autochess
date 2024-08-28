#!./venv/bin/python3

from websocket import create_connection
import pygame as pg
from engine.game import Game

if __name__ == "__main__":
    pg.init()
    Game().start()

# ws = create_connection("ws://127.0.0.1:6969")
# while 1:
#     msg = input("> ")
#     ws.send(msg)
#     result = ws.recv()
#     print("repsonse:", result)
# ws.close()
