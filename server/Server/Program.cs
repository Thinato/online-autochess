using Networking;

int port = 6969;

var server = new TcpServer(port);

server.Start();