using Networking;
using Server.Player;

int port = 6969;
Mediator mediator = new Mediator();

var server = new TcpServer<PlayerEntity>(mediator, port);

server.Start();

// criar uma UI com ncurses
Console.WriteLine("Press ENTER to end");
Console.ReadLine();

server.Stop();