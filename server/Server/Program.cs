﻿using Networking;
using Networking.Packets;
using Server.Queue;
using Server.Packets.ClientPackets;
using Server.Packets.Handlers;
using Server.Player;

PlayerManager? playerManager = null;

try {
    int port = 6969;
    Mediator mediator = new Mediator();
    playerManager = new PlayerManager();
    QueueManager queueManager = new QueueManager();

    var joinQueueHandler = new JoinQueueHandler(queueManager);
    mediator.RegisterHandler(JoinQueuePacket.ID, JoinQueuePacket.Create, joinQueueHandler);


    var server = new TcpServer<PlayerEntity>(mediator, port, (tcpClient) => new PlayerEntity(tcpClient));

    server.Start();

    // criar uma UI com ncurses
    Console.WriteLine("Press ENTER to end");
    Console.ReadLine();

    uint key;
    while ((key = (uint)Console.ReadKey(true).Key) != (uint)ConsoleKey.Escape) {
        if (key == (2 | 80))
            break;
        // Settings.Reload();
    }

    server.Stop();
}
catch (Exception ex) {
    Console.WriteLine($"An error occurred: {ex.Message}");
    if (playerManager == null) return;
    foreach (var p in playerManager.GetPlayers()) {
        p.Disconnect();
    }
}