using Networking;
using Networking.Packets;
using Server.Queue;
using Server.Packets.ClientPackets;

namespace Server.Packets.Handlers;

public class JoinQueueHandler : IPacketHandler<JoinQueuePacket> {
    public byte ID {
        get {
            return (byte)PacketID.JOIN_QUEUE;
        }
    }

    public QueueManager QueueManager { get; set; }

    public JoinQueueHandler(QueueManager queueManager) {
        QueueManager = queueManager;
    }

    public Client Client { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public void Handle(Client client, IPacket packet) {
        HandlePacket(client, (JoinQueuePacket)packet);
    }

    public void HandlePacket(Client client, JoinQueuePacket packet) {
        Console.WriteLine($"{client.ID} has joined queue");
        QueueManager.EnqueuePlayer(client.ID);

        var nextPlayer = QueueManager.GetNextPlayers();

        if (nextPlayer != null) {
            Console.WriteLine($"Match found for {client.ID} yet");
            return;
        }

        Console.WriteLine($"Match found for {client.ID}");


    }
}