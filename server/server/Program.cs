using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using common;
using log4net;
using server;
using server.ping;

namespace server;
// The HTTP server class
public class SimpleHttpServer {
    private readonly TcpListener _listener;
    private readonly int _port;

    public SimpleHttpServer(int port) {
        _port = port;
        _listener = new TcpListener(IPAddress.Any, port);
    }

    // Start listening for incoming connections
    public async Task StartAsync() {
        _listener.Start();
        Console.WriteLine($"Server started on port {_port}");

        while (true) {
            var client = await _listener.AcceptTcpClientAsync();
            _ = ProcessClientAsync(client); // Handle each client connection asynchronously
        }
    }

    // Handle incoming client connection
    private async Task ProcessClientAsync(TcpClient client) {
        using var networkStream = client.GetStream();
        using var reader = new StreamReader(networkStream, Encoding.UTF8);
        using var writer = new StreamWriter(networkStream, Encoding.UTF8) { AutoFlush = true };



        // Parse the request line and headers
        var requestContext = await RequestContext.ParseAsync(reader);

        Console.WriteLine($"Received request: {requestContext.Method} {requestContext.Path} {requestContext.Protocol}");

        // Handle "Ping" request if matched
        if (requestContext.Path == "/ping") {
            Console.WriteLine("Received ping request");
            var pingRequest = new PingRequest();
            var response = pingRequest.Handle(requestContext);
            await writer.WriteAsync(response);
        }
        else {
            await writer.WriteAsync("HTTP/1.1 404 Not Found\r\n\r\n");
        }

        client.Close();
    }
}

class Program {
    internal static readonly ILog Log = LogManager.GetLogger("Server");
    internal static readonly ManualResetEvent Shutdown = new ManualResetEvent(false);
    internal static ServerConfig? Config;

    static async Task Main(string[] args) {

        Config = args.Length > 0 ? ServerConfig.ReadFile(args[0]) : ServerConfig.ReadFile("config.json");

        var server = new SimpleHttpServer(8080);
        await server.StartAsync();
    }
}

