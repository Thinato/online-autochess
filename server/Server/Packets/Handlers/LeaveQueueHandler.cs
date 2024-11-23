
using Networking;
using Networking.Packets;
using Server.Queue;
using Server.Packets.ClientPackets;

namespace Server.Packets.Handlers;

public class LeaveQueueHandler : IPacketHandler<JoinQueuePacket> {
    public byte ID {
        get {
            return (byte)PacketID.JOIN_QUEUE;
        }
    }

    public QueueManager QueueManager { get; set; }

    public LeaveQueueHandler(QueueManager queueManager) {
        QueueManager = queueManager;
    }

    public Client Client { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public void Handle(Client client, IPacket packet) {
        HandlePacket(client, (JoinQueuePacket)packet);
    }

    public void HandlePacket(Client client, JoinQueuePacket packet) {
        Console.WriteLine($"{client.ID} has left queue");
        QueueManager.DequeuePlayer(client.ID);
    }
}