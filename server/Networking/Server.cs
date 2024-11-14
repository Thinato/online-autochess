using System;
using System.ComponentModel.Design;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Networking;

public class TcpServer<Client> {
    private TcpListener? _listener;
    private readonly int _port;
    private bool _isRunning;
    private List<Client> _clients = new List<Client>();
    private Mediator _mediator;

    public TcpServer(Mediator mediator, int port) {
        _port = port;
        _mediator = mediator;
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
        _listener.Stop();
        Console.WriteLine("Server stopped.");
    }

    private async Task AcceptClientsAsync() {
        if (_listener == null) return;
        while (_isRunning) {
            Console.WriteLine("accepting connections");
            try {
                // Accept a new client
                var client = await _listener.AcceptTcpClientAsync();
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

    private async Task HandleClientAsync(TcpClient client) {
        using (client) {
            var stream = client.GetStream();
            var buffer = new byte[1024]; // Buffer size can be adjusted as needed

            try {
                while (_isRunning && client.Connected) {
                    // Read data from the client
                    int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                    if (bytesRead == 0) {
                        // TODO: remove client from client pool once we have it lol
                        Console.WriteLine("Client disconnected.");
                        break;
                    }

                    // get first byte
                    byte commandId = buffer[0];

                    // Convert binary data to ASCII
                    // string command = Encoding.ASCII.GetString(buffer, 0, bytesRead);

                    // Handle command
                    var cmd = _mediator.GetCommand(commandId);
                    _mediator.HandleCommand<ICommand>(client, cmd);

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

