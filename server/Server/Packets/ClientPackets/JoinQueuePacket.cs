using common;
using Networking;
using Networking.Packets;
using Server.Packets;

namespace Server.Packets.ClientPackets;

public class JoinQueuePacket : IPacket {
    public static new byte ID { get { return (byte)PacketID.JOIN_QUEUE; } }

    public static IPacket Create() {
        return new JoinQueuePacket();
    }

    public void Read(Client client, BReader reader) {
        throw new NotImplementedException();
    }

    public int Write(Client client, BWriter writer) {
        throw new NotImplementedException();
    }
}