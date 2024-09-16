using Fleck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public class Program {
    // Dictionary to store the latest messages (could be positions or game data) from each player
    static Dictionary<Guid, byte[]> latestBinaryMessages = new Dictionary<Guid, byte[]>();
    static Dictionary<Guid, string> latestTextMessages = new Dictionary<Guid, string>();

    public static void Main(string[] args) {

        var allSockets = new List<IWebSocketConnection>();
        var server = new WebSocketServer("ws://127.0.0.1:6969");

        const int ticksPerSecond = 64;
        const int tickInterval = 1000 / ticksPerSecond; // in milliseconds (15.625 ms)
        var cts = new CancellationTokenSource();

        // Start server
        server.Start(socket => {
            socket.OnOpen = () => {
                Console.WriteLine("new connection " + socket.ConnectionInfo.Id);
                allSockets.Add(socket);
                // socket.Send(BitConverter.GetBytes(allSockets.Count));
            };

            socket.OnClose = () => {
                Console.WriteLine("Close!");
                allSockets.Remove(socket);
                // Remove player's data on disconnect
                latestBinaryMessages.Remove(socket.ConnectionInfo.Id);
                latestTextMessages.Remove(socket.ConnectionInfo.Id);
            };

            socket.OnMessage = message => {
                // Store the latest message in the dictionary, overwriting the previous one
                latestTextMessages[socket.ConnectionInfo.Id] = message;
            };

            socket.OnBinary = binary => {
                // Store the latest binary message (could be player positions, etc.)
                latestBinaryMessages[socket.ConnectionInfo.Id] = binary;
            };
        });

        // Task to handle sending messages at X ticks per second
        Task.Run(async () => {
            while (!cts.Token.IsCancellationRequested) {
                // Broadcast the latest binary and text messages to all players
                foreach (var socket in allSockets.ToList()) {
                    if (latestBinaryMessages.TryGetValue(socket.ConnectionInfo.Id, out var binaryData)) {
                        allSockets.ToList().ForEach(s => s.Send(binaryData));
                    }

                    if (latestTextMessages.TryGetValue(socket.ConnectionInfo.Id, out var textData)) {
                        allSockets.ToList().ForEach(s => s.Send("Echo: " + textData));
                    }
                }

                await Task.Delay(tickInterval);
            }
        });

        // Allow user to send manual messages from the console
        var input = Console.ReadLine();
        while (input != "exit") {
            foreach (var socket in allSockets.ToList()) {
                socket.Send(input);
            }
            input = Console.ReadLine();
        }

        // Stop the server on exit
        cts.Cancel();
    }
}
