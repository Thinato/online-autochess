using System.Net.Sockets;

namespace Networking;

public abstract class Client : IDisposable {
    protected TcpClient? Connection;

    public Guid ID { get; } = Guid.NewGuid();

    public Client(TcpClient tcpClient) {
        Connection = tcpClient;
    }

    public bool Connected { get { return Connection != null ? Connection.Connected : false; } }

    public NetworkStream? GetStream { get { return Connection?.GetStream(); } }

    public void Disconnect() {
        if (Connection != null) Connection.Close();
    }

    public void Dispose() {
        if (Connection != null) {
            Connection.Close();
            Connection = null;
        }
    }

    public void Write(byte[] data) {
        if (Connection != null && Connection.Connected) {
            Connection.GetStream().Write(data, 0, data.Length);
        }
    }
}
