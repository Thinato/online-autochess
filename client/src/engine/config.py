import json
import os


class Config:
    SERVER_URL: str
    SERVER_PORT: int
    RESOLUTION: tuple[int, int]

    def parse(self, data):
        self.SERVER_URL = data["server_url"]
        self.SERVER_PORT = data["server_port"]
        self.RESOLUTION = data["resolution"]
        print('parsed')

    def default(self):
        self.SERVER_URL = "localhost"
        self.SERVER_PORT = 6969
        self.RESOLUTION = (800, 600)

    def save(self):
        with open("config.json", "w") as f:
            json.dump(
                {
                    "server_url": self.SERVER_URL,
                    "server_port": self.SERVER_PORT,
                    "resolution": self.RESOLUTION,
                },
                f,
            )
        f.close()
        print('file saved')
