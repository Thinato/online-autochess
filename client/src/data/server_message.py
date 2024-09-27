from enum import Enum

class ServerMessage(Enum):
    GET_MY_ID = [1]
    SEND_POSITION = [2]

    def __add__(self, other):
        return self.value + other
    