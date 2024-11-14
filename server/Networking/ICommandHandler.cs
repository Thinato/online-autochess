using System.Net.Sockets;

namespace Networking;

public interface Mediator<TCommand> where TCommand : ICommand {
    void Handle(TcpClient client, TCommand command);
}
