
namespace Server.Logic.Game;

public class GameEntity {
    public string Name { get; private set; }
    public string IP { get; private set; }
    public GameEntity(string name, string ip) {
        Name = name;
        IP = ip;
    }
}
