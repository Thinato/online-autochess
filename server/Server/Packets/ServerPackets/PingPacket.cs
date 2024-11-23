using common;
using Networking;
using Networking.Packets;
using server;

namespace Server.Packets.ServerPackets;

public class PingPacket : IPacket {
    public static byte ID { get { return (byte)PacketID.PING; } }

    public static IPacket Create() {
        return new PingPacket();
    }

    public void Read(Client client, BReader reader) {
        throw new NotImplementedException();
    }

    void IPacket.Write(Client client, BWriter writer) {
        throw new NotImplementedException();
    }
}