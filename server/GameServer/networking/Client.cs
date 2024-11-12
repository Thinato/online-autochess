using gameServer.networking.server;

namespace gameServer.networking;

public enum ProtocolState {
    Disconnected,
    Connected,
    Handshaked,
    Queued,
    Ready
}

public partial class Client {
    private readonly Server _server;
    private volatile ProtocolState _state;
    public ProtocolState State {
        get { return _state; }
        internal set { _state = value; }
    }
    private readonly CommandHandler _handler;

    private int Id { get; internal set; }

    


}