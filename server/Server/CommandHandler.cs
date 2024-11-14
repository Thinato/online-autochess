using System.Net.Sockets;
using Networking;

public class CommandHandler : Mediator {
    public void Handle(TcpClient client, string command) {
        Console.WriteLine($"Received command HAHAHA: {command}");
    }
}