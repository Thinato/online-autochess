from websocket import WebSocket, create_connection
from data.server_message import ServerMessage
import socket

class Connection:
    host: str
    port: int
    sock: socket.SocketType

    def connect(self, host: str, port: int) -> bool:
        self.host = host
        self.port = port
        self.sock = None
        try:
            self.sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
            self.sock.connect((self.host, self.port))
            print(f"Connected to {self.host}:{self.port}")
            return True
        except socket.error as e:
            print(f"Connection failed: {e}")
            return False

    def send_data(self, data: str):
        if self.sock:
            try:
                self.sock.sendall(data.encode())
                print(f"Sent: {data}")
            except socket.error as e:
                print(f"Sending failed: {e}")
        else:
            print("Connection not established. Call connect() first.")

    # def send_player_pos(self, x: int, y: int) -> None:
    #     self.sock.sendall(f"{x}{y}".encode())

    def receive_data(self, buffer_size: int = 1024) -> bytes:
        if self.sock:
            try:
                received_data = self.sock.recv(buffer_size)
                return received_data
            except socket.error as e:
                print(f"Receiving failed: {e}")
                return 
        else:
            print("Connection not established. Call connect() first.")
            return 

    def close(self):
        """Close the TCP connection."""
        if self.sock:
            try:
                self.sock.close()
                print("Connection closed")
            except socket.error as e:
                print(f"Error closing connection: {e}")
        else:
            print("Connection not established.")

# Example usage:
# conn = TCPConnection('127.0.0.1', 8080)
# conn.connect()
# conn.send_data("Hello, Server!")
# response = conn.receive_data()
# print(f"Received: {response}")
# conn.close()


# Websocket version
# class Connection:
#     host: str
#     port: int
#     ws: WebSocket = None

#     def connect(self, host: str, port: int) -> bool:
#         self.host = host
#         self.port = port
#         try:
#             self.ws = create_connection(f"ws://{host}:{port}")
#             # player_id = self.ws.recv()
#             # print('player_id:', player_id.decode())
#             return True # player_id.decode() 
#         except Exception as e:
#             print(e)
#             return False

#     def close(self) -> None:
#         if self.ws:
#             self.ws.close()
    
#     def send_player_pos(self, x: int, y: int) -> None:
#         self.ws.send_binary(
#             bytes(ServerMessage.SEND_POSITION + [x // 256, x % 256, y // 256, y % 256])
#         )

#     def send(self, data: bytes) -> None:
#         self.ws.send_binary(data)

#     def receive(self) -> str | bytes:
#         return self.ws.recv()

#     def is_connected(self) -> bool:
#         if self.ws:
#             return self.ws.connected
#         else:
#             return False
