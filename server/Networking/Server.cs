using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Networking.Packets;

namespace Networking;

public class TcpServer<Client> where Client : Networking.Client, new() {
    private TcpListener? _listener;
    private readonly int _port;
    private bool _isRunning;
    private List<Client> _clients = new List<Client>();
    private Func<TcpClient, Client> _clientFactory;
    private readonly ClientManager<Client> _clientManager;
    private readonly Mediator _mediator;

    public TcpServer(Mediator mediator, int port, Func<TcpClient, Client> clientFactory) {
        _port = port;
        _mediator = mediator;
        _clientFactory = clientFactory;
    }

    public void Start() {
        _listener = new TcpListener(IPAddress.Any, _port);
        _listener.Start();
        _isRunning = true;

        Console.WriteLine($"Server started on port {_port}");

        // Accept clients asynchronously
        Task.Run(() => AcceptClientsAsync());
    }

    public void Stop() {
        if (_listener == null) return;
        _isRunning = false;
        foreach (var client in _clients) {
            // save their data
            // client.Save();
            client.Disconnect();
        }

        _listener.Stop();
        Console.WriteLine("Server stopped.");
    }

    private async Task AcceptClientsAsync() {
        if (_listener == null) return;
        while (_isRunning) {
            Console.WriteLine("accepting connections");
            try {
                // Accept a new client
                var tcpClient = await _listener.AcceptTcpClientAsync();
                var client = _clientFactory(tcpClient);
                Console.WriteLine("Client connected.");

                // Handle client in a separate task
                _ = Task.Run(() => HandleClientAsync(client));
            }
            catch (Exception ex) when (!_isRunning) {
                // Listener has been stopped, safe to exit
                Console.WriteLine($"Server stopped listening: {ex.Message}");
            }
        }
    }

    private async Task HandleClientAsync(Client client) {
        using (client) {
            try {
                var stream = client.GetStream;
                var buffer = new byte[1024];
                while (_isRunning && client.Connected && stream != null) {
                    int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                    if (bytesRead == 0) {
                        Console.WriteLine("Client disconnected.");
                        break;
                    }

                    Console.WriteLine($"Received {bytesRead} bytes: {BitConverter.ToString(buffer, 0, bytesRead)}");

                    // Assuming packet handling logic
                    byte packetId = buffer[0];

                    _mediator.HandlePacket(client, packetId, buffer);
                }
            }
            catch (Exception ex) {
                Console.WriteLine($"Error handling client: {ex.Message}");
            }
            finally {
                Console.WriteLine("Closing client connection.");
            }
        }
    }

}

