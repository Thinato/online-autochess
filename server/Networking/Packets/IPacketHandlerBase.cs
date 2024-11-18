namespace Networking.Packets;

public interface IPacketHandlerBase {
    public byte ID { get; }
    public void Handle(Client client, IPacket packet);
}
