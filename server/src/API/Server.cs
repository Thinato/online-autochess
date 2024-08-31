using System.Net;
using System.Net.WebSockets;

namespace API;

public class Server {
    private readonly TokenService tokenService;
    private readonly List<WebSocket> webSockets = new List<WebSocket>();

    public Server(string secretKey) {
        tokenService = new TokenService(secretKey);
    }

    public async Task StartAsync() {
        HttpListener listener = new HttpListener();
        listener.Prefixes.Add("http://localhost:6969/");
        listener.Start();
        Console.WriteLine("WebSocket server started at ws://localhost:6969/");

        while (true) {
            var context = await listener.GetContextAsync();

            if (context.Request.IsWebSocketRequest) {
                var wsContext = await context.AcceptWebSocketAsync(null);
                _ = HandleWebSocketConnection(wsContext.WebSocket);
            }
            else {
                context.Response.StatusCode = 400;
                context.Response.Close();
            }
        }
    }

    private async Task HandleWebSocketConnection(WebSocket webSocket) {
        var buffer = new byte[1024 * 4];
        WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

        while (result.MessageType != WebSocketMessageType.Close) {
            this.HandleEvent(buffer);
            // var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
            // var responseMessage = ProcessMovementMessage(message);

            // if (!string.IsNullOrEmpty(responseMessage)) {
            //     var encodedMessage = Encoding.UTF8.GetBytes(responseMessage);
            //     await webSocket.SendAsync(new ArraySegment<byte>(encodedMessage, 0, encodedMessage.Length), WebSocketMessageType.Text, true, CancellationToken.None);
            // }

            // result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
        }

        await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
    }

    private string HandleEvent(byte[] buffer) {


        return null;
    }
}

public class EventResponse<T> {
    public bool StreamAll { get; set; }
    public required T Data { get; set; }
}

enum Query {
    test = 0,
}