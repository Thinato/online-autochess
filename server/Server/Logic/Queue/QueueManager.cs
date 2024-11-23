
namespace Server.Queue;

public class QueueManager {
    // Thread-safe collection to store the queue entities
    private readonly List<QueueEntity> _queue;

    public int Count => _queue.Count;

    private int _maxPlayers = 2;

    public QueueManager() {
        _queue = new List<QueueEntity>();
    }

    // Adds a new player to the queue
    public void EnqueuePlayer(Guid playerId, int priority = 0) {
        var queueEntity = new QueueEntity(playerId, priority);
        _queue.Add(queueEntity);
        Console.WriteLine($"Player {playerId} added to the queue with priority {priority}.");
    }

    // Removes and returns the next player in the queue
    public QueueEntity? DequeuePlayer(Guid playerId) {
        var queueEntity = _queue.FirstOrDefault(x => x.PlayerId == playerId);

        if (queueEntity != null) {
            _queue.Remove(queueEntity);
            Console.WriteLine($"Player {playerId} removed from the queue.");
            return queueEntity;
        }

        return null;
    }

    // Check if there are enough players for a game
    private bool CanStartGame() {
        return _queue.Count >= _maxPlayers;
    }

    // Returns the next players in the queue
    public IEnumerable<QueueEntity>? GetNextPlayers() {
        if (!CanStartGame()) {
            return null;
        }
        return _queue.Take(_maxPlayers);
    }
}
