using System.Net.Sockets;

namespace Networking;
public static class MessageHandler
{
    public static void HandleMessage(TcpClient client, string message)
    {
        // This method would contain logic to process messages from clients
        Console.WriteLine($"Handling message from client: {message}");
    }
}