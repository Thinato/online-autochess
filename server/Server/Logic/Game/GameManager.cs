namespace Server.Logic.Game;

public class GameManager {
    private readonly Dictionary<string, GameEntity> games = new Dictionary<string, GameEntity>();

    public void AddGame(GameEntity game) {
        games.Add(game.IP, game);
    }
}
