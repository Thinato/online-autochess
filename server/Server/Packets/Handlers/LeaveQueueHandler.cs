
using Networking;
using Networking.Packets;
using Server.Queue;
using Server.Packets.ClientPackets;

namespace Server.Packets.Handlers;

public class LeaveQueueHandler : IPacketHandler<LeaveQueuePacket> {
    public byte ID {
        get {
            return (byte)PacketID.LEAVE_QUEUE;
        }
    }

    public QueueManager QueueManager { get; set; }

    public LeaveQueueHandler(QueueManager queueManager) {
        QueueManager = queueManager;
    }

    public Client Client { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public void Handle(Client client, IPacket packet) {
        HandlePacket(client, (LeaveQueuePacket)packet);
    }

    public void HandlePacket(Client client, LeaveQueuePacket packet) {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"{client.ID} has left queue");
        Console.ForegroundColor = ConsoleColor.White;
        QueueManager.DequeuePlayer(client.ID);
    }
}