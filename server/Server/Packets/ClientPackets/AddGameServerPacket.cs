
using Networking;
using Networking.Packets;
using server;

namespace Server.Packets.ClientPackets;

public class AddGameServerPacket : IPacket {
    public static byte ID { get { return (byte)PacketID.ADD_GAME_SERVER; } }
    public int Password { get; set; }
    public byte[] _ip = new byte[4];
    public byte[] IP {
        get => _ip;
        set {
            if (value.Length == 4) {
                _ip = value;
            }
            else {
                throw new ArgumentException("IP must be exactly 4 bytes.");
            }
        }
    }

    public static IPacket Create() {
        return new AddGameServerPacket();
    }

    public void Read(Client client, BReader reader) {
        Password = reader.ReadInt32();
        IP = reader.ReadBytes(4);
    }

    public void Write(Client client, BWriter writer) {
        writer.Write(Password);
        writer.Write(IP);
    }
}