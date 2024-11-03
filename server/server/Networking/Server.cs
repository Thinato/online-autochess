
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace server.networking;

class SimpleTcpServer {
    private TcpListener _server;
    private bool _isRunning;

    public SimpleTcpServer(string ipAddress, int port) {
        _server = new TcpListener(IPAddress.Parse(ipAddress), port);
        _server.Start();
        _isRunning = true;

        Console.WriteLine($"Server started on {ipAddress}:{port}");

        StartListening();
    }

    private void StartListening() {
        while (_isRunning) {
            // Accept incoming client connection
            TcpClient client = _server.AcceptTcpClient();
            Console.WriteLine("Client connected.");

            // Handle client in a separate thread
            _ = Task.Run(() => HandleClient(client));
        }
    }

    private void HandleClient(TcpClient client) {
        using (NetworkStream stream = client.GetStream()) {
            byte[] buffer = new byte[1024];
            int bytesRead;

            while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0) {
                // Convert received bytes to string for display
                string receivedMessage = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                Console.WriteLine($"Received: {receivedMessage}");

                // Respond to client
                byte[] responseBytes = Encoding.ASCII.GetBytes("Message received!");
                stream.Write(responseBytes, 0, responseBytes.Length);
                Console.WriteLine("Response sent.");

                // Optional: Break on "end" message from the client
                if (receivedMessage.Trim().ToLower() == "end") {
                    Console.WriteLine("End message received. Closing connection.");
                    break;
                }
            }
        }

        client.Close();
        Console.WriteLine("Client disconnected.");
    }
}