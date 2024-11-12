using log4net;
using gameServer.networking.packets;
using gameServer.networking.packets.incoming;

namespace gameServer.networking;

interface IPacketHandler
{
    PacketId ID { get; }
    void Handle(Client client, IncomingMessage packet);
}

abstract class PacketHandlerBase<T> : IPacketHandler where T : IncomingMessage
{
    protected static readonly ILog Log = LogManager.GetLogger(typeof(PacketHandlers));

    protected abstract void HandlePacket(Client client, T packet);

    public abstract PacketId ID { get; }

    public void Handle(Client client, IncomingMessage packet)
    {
        HandlePacket(client, (T)packet);
    }

    // protected bool IsTest(Client cli)
    // {
    //     return cli?.Player?.Owner is Test;
    // }
}

class PacketHandlers
{
    public static Dictionary<PacketId, IPacketHandler> Handlers = new Dictionary<PacketId, IPacketHandler>();
    static PacketHandlers()
    {
        foreach (var i in typeof(Packet).Assembly.GetTypes())
            if (typeof(IPacketHandler).IsAssignableFrom(i) &&
                !i.IsAbstract && !i.IsInterface)
            {
                IPacketHandler pkt = (IPacketHandler)Activator.CreateInstance(i);
                Handlers.Add(pkt.ID, pkt);
            }
    }
}