
using System.Net.Sockets;
using Networking;

namespace Networking.Packets;

public class Mediator {
    private readonly Dictionary<byte, IPacketHandlerBase> _handlers = new Dictionary<byte, IPacketHandlerBase>();
    private readonly Dictionary<byte, Func<IPacket>> _packetFactory = new Dictionary<byte, Func<IPacket>>();

    public void RegisterPacket(byte id, Func<IPacket> packetFactory) {
        if (_packetFactory.ContainsKey(id)) {
            throw new InvalidOperationException($"Command with id {id} already registered");
        }

        _packetFactory[id] = () => packetFactory();
    }

    public IPacket GetIPacket(byte id) {
        if (!_packetFactory.ContainsKey(id)) {
            throw new InvalidOperationException($"Command with id {id} not registered");
        }

        return _packetFactory[id]();
    }

    public void RegisterHandler<TPacket>(IPacketHandler<TPacket> handler) where TPacket : IPacket {
        if (_handlers.ContainsKey(handler.ID)) {
            throw new InvalidOperationException($"Handler for {typeof(IPacket)} already registered");
        }

        _handlers[handler.ID] = handler;
    }

    public void HandlePacket(Client client, byte packetId, byte[] buffer) {
        var packet = GetIPacket(packetId);

        if (!_handlers.TryGetValue(packetId, out var handlerObj)) {
            throw new InvalidOperationException($"No handler registered for {packet}");
        }

        if (!(handlerObj is IPacketHandlerBase handler)) {
            throw new InvalidOperationException($"Handler for {packet} is not of the correct type");
        }

        handler.Handle(client, packet);
    }

}