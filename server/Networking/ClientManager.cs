
using System;
using System.Collections.Concurrent;
using System.Dynamic;
using System.Net.Sockets;
using System.Text;

namespace Networking;

public abstract class ClientManager<TClient> where TClient : Client {
    private readonly ConcurrentDictionary<string, TClient> _clients;

    public ClientManager() {
        _clients = new ConcurrentDictionary<string, TClient>();
    }

    public List<TClient> GetPlayers() {
        return _clients.Values.ToList();
    }


    // Removes a player by their ID
    public bool RemovePlayer(string clientId) {
        if (_clients.TryRemove(clientId, out var removedPlayer)) {
            removedPlayer.Disconnect(); // Ensure the player's connection is closed
            Console.WriteLine($"Player {clientId} removed.");
            return true;
        }

        Console.WriteLine($"Player {clientId} could not be found.");
        return false;
    }

    // Retrieves a player by their ID
    public TClient? GetPlayer(string playerId) {
        if (_clients.TryGetValue(playerId, out var player)) {
            return player;
        }

        Console.WriteLine($"Player {playerId} not found.");
        return null;
    }

    // Sends a message to a specific player
    public void SendMessageToPlayer(string playerId, string message) {
        var client = GetPlayer(playerId);
        if (client != null && client.Connected) {
            client.Write(Encoding.ASCII.GetBytes(message));
        }
        else {
            Console.WriteLine($"Failed to send message to player {playerId}: Player not connected.");
        }
    }

    // Broadcasts a message to all connected players
    public void BroadcastMessage(string message) {
        foreach (var client in _clients.Values) {
            if (client.Connected) {
                client.Write(Encoding.ASCII.GetBytes(message));
            }
        }
    }
}
