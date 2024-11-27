using System.Net;

namespace Server.Logic.Game;

public class GameManager {
    private readonly Dictionary<IPAddress, GameEntity> games = new Dictionary<IPAddress, GameEntity>();

    public void AddGame(GameEntity game) {
        games.Add(game.IP, game);
    }
}
