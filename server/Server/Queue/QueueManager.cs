using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace server.Queue;

public class QueueManager {
    // Thread-safe collection to store the queue entities
    private readonly ConcurrentQueue<QueueEntity> _queue;

    public QueueManager() {
        _queue = new ConcurrentQueue<QueueEntity>();
    }

    // Adds a new player to the queue
    public void EnqueuePlayer(string playerId, int priority = 0) {
        var queueEntity = new QueueEntity(playerId, priority);
        _queue.Enqueue(queueEntity);
        Console.WriteLine($"Player {playerId} added to the queue with priority {priority}.");
    }

    // Removes and returns the next player in the queue
    public QueueEntity? DequeuePlayer() {
        if (_queue.TryDequeue(out var queueEntity)) {
            Console.WriteLine($"Player {queueEntity.PlayerId} dequeued.");
            return queueEntity;
        }

        Console.WriteLine("Queue is empty.");
        return null;
    }

    // Peek at the next player without removing
    public QueueEntity? PeekNextPlayer() {
        if (_queue.TryPeek(out var queueEntity)) {
            return queueEntity;
        }

        return null;
    }

    // Retrieves the count of players in the queue
    public int GetQueueCount() {
        return _queue.Count;
    }

    // Future extension: Efficient retrieval and sorting of prioritized items
    // Placeholder method for potential custom priority logic
    public IEnumerable<QueueEntity> GetAllQueuedPlayers() {
        // For potential sorting or filtering logic
        return _queue.ToList();
    }
}
