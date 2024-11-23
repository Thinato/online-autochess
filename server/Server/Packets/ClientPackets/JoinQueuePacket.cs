using Networking;
using Networking.Packets;
using server;
using Server.Packets;

namespace Server.Packets.ClientPackets;

public class JoinQueuePacket : IPacket {
    public static byte ID { get { return (byte)PacketID.JOIN_QUEUE; } }

    public int QueueID { get; set; }

    public static IPacket Create() {
        return new JoinQueuePacket();
    }

    public void Read(Client client, BReader reader) {
    }

    public void Write(Client client, BWriter writer) {
    }
}