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
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"{client.ID} has joined queue");
        Console.ForegroundColor = ConsoleColor.White;
        QueueManager.EnqueuePlayer(client.ID);

        var nextPlayers = QueueManager.GetNextPlayers();

        if (nextPlayers == null) {
            Console.WriteLine($"No match for {client.ID} yet");
            return;
        }

        var nextPlayersList = nextPlayers.ToList();

        foreach (var player in nextPlayersList) {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Match found for {player.PlayerId}");
            Console.ForegroundColor = ConsoleColor.White;
            QueueManager.DequeuePlayer(player.PlayerId);
        }
    }
}