#!./venv/bin/python3

from websocket import create_connection
import pygame as pg
from engine.game import Game
import configparser as cp
from screens.main_menu import MainMenu


if __name__ == "__main__":
    pg.init()
    Game(screens={"main_menu": MainMenu()}).start()

# config = cp.ConfigParser()

# print(config.sections())

# config.read("config.ini")

# # print(config.sections())

# # print(config['connection']['host'])

# server_cfg = config["connection"]

# ws = create_connection("ws://{}:{}".format(server_cfg["host"], server_cfg["port"]))
# while 1:
#     # numbers greater than 255 should throw an error in client
#     ws.send_binary([129, 255, 43, 300])
#     msg = input("> ")
#     ws.send_binary(msg.encode())
#     ws.send(msg)
#     result = ws.recv()
#     print("repsonse:", result)
# ws.close()
