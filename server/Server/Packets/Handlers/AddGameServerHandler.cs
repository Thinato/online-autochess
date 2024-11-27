using Networking;
using Networking.Packets;
using Server.Queue;
using Server.Packets.ClientPackets;
using Server.Logic.Game;
using System.Linq;
using System.Net;

namespace Server.Packets.Handlers;

public class AddGameServerHandler : IPacketHandler<AddGameServerPacket> {
    public byte ID {
        get {
            return (byte)PacketID.ADD_GAME_SERVER;
        }
    }

    public GameManager GameManager { get; set; }

    public AddGameServerHandler(GameManager gameManager) {
        GameManager = gameManager;
    }

    public Client Client { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public void Handle(Client client, IPacket packet) {
        HandlePacket(client, (AddGameServerPacket)packet);
    }

    public void HandlePacket(Client client, AddGameServerPacket packet) {
        if (packet.Password == 123) {
            Console.WriteLine("Game server added");
            GameManager.AddGame(
                new GameEntity(
                    "New Server",
                    new IPAddress(packet.IP)
                )
            );
        }
        else {
            Console.WriteLine("Invalid password");
        }
    }
}