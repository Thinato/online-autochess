using common;
using Networking;
using Networking.Packets;
using Server.Packets;

namespace Server.Packets.ClientPackets;

public class JoinQueuePacket : Packet {
    public static new byte ID { get { return (byte)PacketID.JOIN_QUEUE; } }

    public static Packet Create() {
        System.Console.WriteLine("JoinQueuePacket created");
        return new JoinQueuePacket();
    }

    public override void Read(Client client, BReader reader) {
        throw new NotImplementedException();
    }

    public override int Write(Client client, BWriter writer) {
        throw new NotImplementedException();
    }
}