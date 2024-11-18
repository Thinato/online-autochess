
using System.Net.Sockets;
using Networking;

namespace Networking.Packets;

struct PacketEntry {
    internal Func<IPacket> packetFactory;
    internal IPacketHandlerBase handler;
}

public class Mediator {
    private readonly Dictionary<byte, PacketEntry> _handlers = new Dictionary<byte, PacketEntry>();

    public void RegisterHandler(byte id, Func<IPacket> packetFactory, IPacketHandlerBase handler) {
        if (_handlers.ContainsKey(id)) {
            throw new InvalidOperationException($"Handler for {typeof(IPacket)} already registered");
        }

        _handlers[handler.ID] = new PacketEntry {
            packetFactory = packetFactory,
            handler = handler
        };
    }

    internal void HandlePacket(Client client, byte packetId, byte[] buffer) {
        if (!_handlers.TryGetValue(packetId, out var handlerObj)) {
            throw new InvalidOperationException($"No packet/handler registered for {packetId}");
        }

        var packet = handlerObj.packetFactory();

        if (!(handlerObj.handler is IPacketHandlerBase handler)) {
            throw new InvalidOperationException($"Handler for {packetId} is not of the correct type");
        }

        // start from the second byte, because the first byte is the packet ID
        packet.Read(client, buffer);

        handler.Handle(client, packet);
    }

}