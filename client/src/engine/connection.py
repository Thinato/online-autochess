from websocket import WebSocket, create_connection


class Connection:
    host: str
    port: int
    ws: WebSocket = None

    def connect(self, host: str, port: int) -> bool:
        self.host = host
        self.port = port
        try:
            self.ws = create_connection(f"ws://{host}:{port}")
            return True
        except Exception as e:
            print(e)
            return False

    def close(self) -> None:
        if self.ws:
            self.ws.close()

    def send(self, data: bytes) -> None:
        self.ws.send_binary(data)

    def receive(self) -> str | bytes:
        return self.ws.recv()

    def is_connected(self) -> bool:
        if self.ws:
            return self.ws.connected
        else:
            return False
