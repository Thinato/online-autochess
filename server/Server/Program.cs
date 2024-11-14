using Networking;
using Server.Player;

int port = 6969;

var server = new TcpServer<PlayerEntity>(port);

server.Start();


Console.WriteLine("Press ENTER to end");
Console.ReadLine();

server.Stop();