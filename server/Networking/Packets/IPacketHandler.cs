namespace Networking.Packets;

public abstract class PacketHandler<TPacket> where TPacket : Packet {
    public abstract byte ID { get; }
    public Client Client { get; set; }
    public void Handle(Client client, TPacket packet) {
        Client = client;
        HandlePacket(client, packet);
    }

    public abstract void HandlePacket(Client client, TPacket packet);


}