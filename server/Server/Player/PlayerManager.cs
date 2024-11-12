using System;
using System.Collections.Concurrent;
using System.Net.Sockets;

namespace Server.Player;

public class PlayerManager {
    // Thread-safe dictionary to store connected players
    private readonly ConcurrentDictionary<string, PlayerEntity> _players;

    public PlayerManager() {
        _players = new ConcurrentDictionary<string, PlayerEntity>();
    }

    // Adds a new player to the manager
    public bool AddPlayer(string playerId, TcpClient tcpClient, string nickname = "Guest") {
        if (_players.ContainsKey(playerId)) {
            Console.WriteLine($"Player with ID {playerId} already exists.");
            return false;
        }

        var player = new PlayerEntity(playerId, tcpClient, nickname);
        bool added = _players.TryAdd(playerId, player);

        if (added) {
            Console.WriteLine($"Player {playerId} added successfully.");
        }
        return added;
    }

    // Removes a player by their ID
    public bool RemovePlayer(string playerId) {
        if (_players.TryRemove(playerId, out var removedPlayer)) {
            removedPlayer.Disconnect(); // Ensure the player's connection is closed
            Console.WriteLine($"Player {playerId} removed.");
            return true;
        }

        Console.WriteLine($"Player {playerId} could not be found.");
        return false;
    }

    // Retrieves a player by their ID
    public PlayerEntity? GetPlayer(string playerId) {
        if (_players.TryGetValue(playerId, out var player)) {
            return player;
        }

        Console.WriteLine($"Player {playerId} not found.");
        return null;
    }

    // Sends a message to a specific player
    public async void SendMessageToPlayer(string playerId, string message) {
        var player = GetPlayer(playerId);
        if (player != null && player.IsConnected) {
            await player.SendMessageAsync(message);
        }
        else {
            Console.WriteLine($"Failed to send message to player {playerId}: Player not connected.");
        }
    }

    // Broadcasts a message to all connected players
    public async void BroadcastMessage(string message) {
        foreach (var player in _players.Values) {
            if (player.IsConnected) {
                await player.SendMessageAsync(message);
            }
        }
    }
}
