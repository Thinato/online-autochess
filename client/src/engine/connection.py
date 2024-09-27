from websocket import WebSocket, create_connection
from data.server_message import ServerMessage


class Connection:
    host: str
    port: int
    ws: WebSocket = None

    def connect(self, host: str, port: int) -> bool:
        self.host = host
        self.port = port
        try:
            self.ws = create_connection(f"ws://{host}:{port}")
            # player_id = self.ws.recv()
            # print('player_id:', player_id.decode())
            return True # player_id.decode() 
        except Exception as e:
            print(e)
            return False

    def close(self) -> None:
        if self.ws:
            self.ws.close()
    
    def send_player_pos(self, x: int, y: int) -> None:
        self.ws.send_binary(
            bytes(ServerMessage.SEND_POSITION + [x // 256, x % 256, y // 256, y % 256])
        )

    def send(self, data: bytes) -> None:
        self.ws.send_binary(data)

    def receive(self) -> str | bytes:
        return self.ws.recv()

    def is_connected(self) -> bool:
        if self.ws:
            return self.ws.connected
        else:
            return False
