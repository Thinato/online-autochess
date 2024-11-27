
using System.Net;

namespace Server.Logic.Game;

public class GameEntity {
    public string Name { get; private set; }
    public IPAddress IP { get; private set; }
    public GameEntity(string name, IPAddress ip) {
        Name = name;
        IP = ip;
    }
}
