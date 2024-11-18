
using System.Net.Sockets;
using Networking;

namespace Networking.Packets;

public class Mediator {
    private readonly Dictionary<Type, object> _handlers = new Dictionary<Type, object>();
    private readonly Dictionary<byte, Func<Packet>> _packetFactory = new Dictionary<byte, Func<Packet>>();

    public void RegisterPacket(byte id, Func<Packet> packetFactory) {
        // if it already contains the key, throw an exception
        if (_packetFactory.ContainsKey(id)) {
            throw new InvalidOperationException($"Command with id {id} already registered");
        }

        System.Console.WriteLine($"Registering packet with id {id}");

        _packetFactory[id] = () => packetFactory();
    }

    public Packet GetPacket(byte id) {
        // if it doesn't contain the key, throw an exception
        System.Console.WriteLine($"Getting packet with id {id}");

        if (!_packetFactory.ContainsKey(id)) {
            throw new InvalidOperationException($"Command with id {id} not registered");
        }

        return _packetFactory[id]();
    }

    public void RegisterHandler<TPacket>(PacketHandler<TPacket> handler) where TPacket : Packet {
        // if it already contains the key, throw an exception
        Console.WriteLine($"Registering handler for {typeof(TPacket)}");

        if (_handlers.ContainsKey(typeof(TPacket))) {
            throw new InvalidOperationException($"Handler for {typeof(TPacket)} already registered");
        }

        _handlers[typeof(TPacket)] = handler;
    }

    public void HandlePacket<TPacket>(Client client, TPacket packet, byte[] buffer) where TPacket : Packet {
        Console.WriteLine($"Handling packet of type: {typeof(TPacket).Name}");


        // log all packets inside _handlers
        Console.WriteLine($"Handler count: {_handlers.Count}");
        foreach (var t in _handlers) {
            Console.WriteLine($"Handler: {t.Key}");
        }

        if (!_handlers.TryGetValue(typeof(TPacket), out var handlerObj)) {
            throw new InvalidOperationException($"No handler registered for {typeof(TPacket)}");
        }

        if (handlerObj is not PacketHandler<TPacket> handler) {
            throw new InvalidOperationException($"Handler for {typeof(TPacket)} is not of the correct type");
        }

        handler.Handle(client, packet);
    }

}