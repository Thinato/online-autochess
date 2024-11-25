using Networking;
using Networking.Packets;
using server;

namespace Server.Packets.ServerPackets;

public class GameAddressPacket : IPacket {
    public static byte ID { get { return (byte)PacketID.GAME_ADDRESS; } }
    public int IP { get; set; }
    public int Port { get; set; }

    public static IPacket Create() {
        return new GameAddressPacket();
    }

    public void Read(Client client, BReader reader) {
        IP = reader.ReadInt32();
        Port = reader.ReadInt32();
    }

    void IPacket.Write(Client client, BWriter writer) {
        writer.Write(IP);
        writer.Write(Port);
    }
}