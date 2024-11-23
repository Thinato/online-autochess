using System.Net.Sockets;
using System.Text;
using Networking;

namespace Server.Player;

public class PlayerEntity : Client {
    public PlayerEntity() : base(new TcpClient()) {
        // Optionally perform any initialization here.
    }

    public PlayerEntity(TcpClient tcpClient) : base(tcpClient) {
    }

    public string PlayerId { get; private set; }

    public string Nickname { get; set; }
}
