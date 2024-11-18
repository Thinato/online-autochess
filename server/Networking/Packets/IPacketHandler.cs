
namespace Networking.Packets;

public interface IPacketHandler<TPacket> : IPacketHandlerBase where TPacket : IPacket {
    public Client Client { get; set; }
    public abstract void HandlePacket(Client client, TPacket packet);
}