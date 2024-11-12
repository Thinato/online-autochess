using System.Net.Sockets;
using System.Text;

namespace Server.Player;

public class PlayerEntity {
    // Unique identifier for the player (could be assigned at connection or based on authentication)
    public string PlayerId { get; private set; }

    // Network stream associated with the player's connection
    public TcpClient TcpClient { get; private set; }

    // Connection state of the player
    public bool IsConnected => TcpClient?.Connected ?? false;

    // Additional metadata for the player (e.g., nickname, game state, etc.)
    public string Nickname { get; set; }

    // Constructor
    public PlayerEntity(string playerId, TcpClient client, string nickname = "Guest") {
        PlayerId = playerId;
        TcpClient = client;
        Nickname = nickname;
    }

    // Method to send a message to the player (basic example for sending data)
    public async Task SendMessageAsync(string message) {
        if (IsConnected && TcpClient?.GetStream() != null) {
            byte[] messageBytes = Encoding.ASCII.GetBytes(message);
            await TcpClient.GetStream().WriteAsync(messageBytes, 0, messageBytes.Length);
        }
    }

    // Method to disconnect the player
    public void Disconnect() {
        if (TcpClient != null) {
            TcpClient.Close();
        }
    }
}
