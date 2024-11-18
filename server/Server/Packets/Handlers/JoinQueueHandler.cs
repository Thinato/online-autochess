using Networking;
using Networking.Packets;
using Server.Packets.ClientPackets;

namespace Server.Packets.Handlers;

public class JoinQueueHandler : IPacketHandler<JoinQueuePacket> {
    public byte ID {
        get {
            return (byte)PacketID.JOIN_QUEUE;
        }
    }

    public Client Client { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public void Handle(Client client, IPacket packet) {
        HandlePacket(client, (JoinQueuePacket)packet);
    }

    public void HandlePacket(Client client, JoinQueuePacket packet) {
        Console.WriteLine("JoinQueuePacket handled");
        Console.WriteLine("should not close the connection now:");
    }
}