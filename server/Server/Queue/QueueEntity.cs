namespace server.Queue;

public class QueueEntity
{
    // Identifier of the player (or group)
    public string PlayerId { get; private set; }

    // Priority of the player in the queue (optional for future use)
    public int Priority { get; set; }

    // Timestamp when the player entered the queue
    public DateTime EnqueueTime { get; private set; }

    // Constructor
    public QueueEntity(string playerId, int priority = 0)
    {
        PlayerId = playerId;
        Priority = priority;
        EnqueueTime = DateTime.UtcNow;
    }
}
