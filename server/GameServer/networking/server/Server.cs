using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using log4net;

namespace gameServer.networking.server;

public class Server
{

    private Socket _listenSocket;
    readonly BufferManager _buffManager;
    private readonly SocketAsyncEventArgsPool _eventArgsPoolAccept;
    private readonly ClientPool _clientPool;

    internal void Start()
    {
        _listenSocket.Bind(new IPEndPoint(IPAddress.Any, 7777));
        _listenSocket.Listen(10);

        this.StartAccept();

        _listenSocket.BeginAccept(AcceptCallback, null);
    }

    private void StartAccept()
    {


    }
}