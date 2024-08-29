using System;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace API;

public class Server
{
    private readonly TokenService _tokenService;

    public Server(string secretKey)
    {
        _tokenService = new TokenService(secretKey);
    }

    public async Task StartAsync()
    {
        HttpListener listener = new HttpListener();
        listener.Prefixes.Add("http://localhost:6969/");
        listener.Start();
        Console.WriteLine("WebSocket server started at ws://localhost:6969/");

        while (true)
        {
            var context = await listener.GetContextAsync();
            if (context.Request.IsWebSocketRequest)
            {
                var wsContext = await context.AcceptWebSocketAsync(null);
                _ = HandleWebSocketConnection(wsContext.WebSocket);
            }
            else
            {
                context.Response.StatusCode = 400;
                context.Response.Close();
            }
        }
    }

    private async Task HandleWebSocketConnection(WebSocket webSocket)
    {
        var buffer = new byte[1024 * 4];
        WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

        while (result.MessageType != WebSocketMessageType.Close)
        {
            var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
            var responseMessage = ProcessMovementMessage(message);

            if (!string.IsNullOrEmpty(responseMessage))
            {
                var encodedMessage = Encoding.UTF8.GetBytes(responseMessage);
                await webSocket.SendAsync(new ArraySegment<byte>(encodedMessage, 0, encodedMessage.Length), WebSocketMessageType.Text, true, CancellationToken.None);
            }

            result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
        }

        await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
    }

    private string ProcessMovementMessage(string message)
    {
        var parts = message.Split('|');
        if (parts.Length != 3)
        {
            return "Invalid message format";
        }

        string token = parts[0];
        // review this later, maybe validate token all the time is not necessary
        // since the connection with the given ip is already authenticated
        var playerId = _tokenService.ValidateToken(token);

        if (playerId == null)
        {
            return "Invalid token";
        }

        var movementData = parts[1];
        var movementParts = movementData.Split(',');

        if (movementParts.Length != 4)
        {
            return "Invalid movement data format";
        }

        var playerMovement = new PlayerMovement
        {
            PlayerId = int.Parse(movementParts[0]),
            PositionX = int.Parse(movementParts[1]),
            PositionY = int.Parse(movementParts[2]),
            Direction = byte.Parse(movementParts[3])
        };

        Console.WriteLine($"Player {playerMovement.PlayerId} moved to ({playerMovement.PositionX}, {playerMovement.PositionY}) with direction {playerMovement.Direction}");

        return $"Player {playerMovement.PlayerId} move processed successfully";
    }
}

public class PlayerMovement
{
    public int PlayerId { get; set; }
    public int PositionX { get; set; }
    public int PositionY { get; set; }
    public byte Direction { get; set; }
}
