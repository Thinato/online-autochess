using API;
using Fleck;

public class Program {
    static void Main(string[] args) {

        FleckLog.Level = LogLevel.Debug;
        var allSockets = new List<IWebSocketConnection>();
        var server = new WebSocketServer("ws://127.0.0.1:6969");
        server.Start(socket => {
            socket.OnOpen = () => {
                Console.WriteLine("Open!");
                allSockets.Add(socket);
            };
            socket.OnClose = () => {
                Console.WriteLine("Close!");
                allSockets.Remove(socket);
            };
            socket.OnMessage = message => {
                Console.WriteLine(message);
                allSockets.ToList().ForEach(s => s.Send("Echo: " + message));
            };
            socket.OnBinary = binary => {
                Console.WriteLine("Binary: " + binary.Length + " bytes");
                Console.WriteLine(BitConverter.ToString(binary) + "...");
            };
        });


        var input = Console.ReadLine();
        while (input != "exit") {
            foreach (var socket in allSockets.ToList()) {
                socket.Send(input);
            }
            input = Console.ReadLine();
        }

    }
}
