using System.Net;
using common;

namespace Networking.Packets;

public interface IPacket {

    public static byte ID { get; }

    public void Read(Client client, byte[] body, int offset, int len) {
        // decrypt data
        // decrypt(body, offset, len);
        Read(client, new BReader(new MemoryStream(body)));
    }

    public int Write(Client client, byte[] buff, int offset) {
        MemoryStream s = new MemoryStream(buff, offset + 5, buff.Length - offset - 5);
        Write(client, new BWriter(s));

        int len = (int)s.Position;
        // Encrypt data
        // Encrypt(client, buff, offset + 5, len);

        // we probably dont need this:
        // Buffer.BlockCopy(BitConverter.GetBytes(IPAddress.HostToNetworkOrder(len + 5)), 0, buff, offset, 4);
        buff[offset + 4] = (byte)ID;
        return len + 5;
    }

    public abstract void Read(Client client, BReader reader);

    public abstract int Write(Client client, BWriter writer);

}