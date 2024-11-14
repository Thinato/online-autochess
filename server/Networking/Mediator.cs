
using System.Net.Sockets;
using Networking;

public class Mediator {
    private readonly Dictionary<Type, object> _handlers = new Dictionary<Type, object>();
    private readonly Dictionary<byte, ICommand> _commands = new Dictionary<byte, ICommand>();

    public void RegisterCommand(byte id, ICommand command) {
        // if it already contains the key, throw an exception
        if (_commands.ContainsKey(id)) {
            throw new InvalidOperationException($"Command with id {id} already registered");
        }

        _commands[id] = command;
    }

    public ICommand GetCommand(byte id) {
        return _commands[id];
    }

    public void RegisterHandler<TCommand>(Mediator<TCommand> handler) where TCommand : ICommand {
        // if it already contains the key, throw an exception
        if (_handlers.ContainsKey(typeof(TCommand))) {
            throw new InvalidOperationException($"Handler for {typeof(TCommand)} already registered");
        }

        _handlers[typeof(TCommand)] = handler;
    }

    public void HandleCommand<TCommand>(TcpClient client, TCommand command) where TCommand : ICommand {
        var handler = (Mediator<TCommand>)_handlers[typeof(TCommand)];

        // check if handler was registered 
        if (handler == null) {
            throw new InvalidOperationException($"No handler registered for {typeof(TCommand)}");
        }

        handler.Handle(client, command);
    }
}