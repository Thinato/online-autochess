#!./venv/bin/python3

import json
import pygame as pg
import os

from engine.config import Config
from engine.connection import Connection
from engine.game import Game
from scenes.battleground import Battleground
from scenes.main_menu import MainMenu


if __name__ == "__main__":
    pg.init()
    pg.font.init()

    config = Config()

    if os.path.isfile("config.json"):
        with open("config.json", "r") as f:
            config.parse(json.load(f))
        f.close()
    else:
        config.default()
        config.save()

    connection = Connection()

    game = Game(config=config, conn=connection)

    game.add_scene(MainMenu)
    game.add_scene(Battleground)
    
    game.start()

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
