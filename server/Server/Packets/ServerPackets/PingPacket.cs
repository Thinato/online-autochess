using common;
using Networking;
using Networking.Packets;

namespace Server.Packets.ServerPackets;

public class PingPacket : Packet {
    public static new byte ID { get { return (byte)PacketID.PING; } }

    public static Packet Create() {
        return new PingPacket();
    }

    public override void Read(Client client, BReader reader) {
        throw new NotImplementedException();
    }

    public override int Write(Client client, BWriter writer) {
        throw new NotImplementedException();
    }
}