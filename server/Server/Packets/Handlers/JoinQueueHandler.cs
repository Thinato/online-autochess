using Networking;
using Networking.Packets;
using Server.Packets.ClientPackets;

namespace Server.Packets.Handlers;

public class JoinQueueHandler : PacketHandler<JoinQueuePacket> {
    public override byte ID {
        get {
            return (byte)PacketID.JOIN_QUEUE;
        }
    }

    public override void HandlePacket(Client client, JoinQueuePacket packet) {
        Console.WriteLine("JoinQueuePacket handled");
        throw new NotImplementedException();
    }
}