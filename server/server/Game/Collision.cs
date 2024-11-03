namespace server.game;

public interface ICollidable<T> where T : ICollidable<T> {
    // CollisionMap<T> Parent { get; set; }
    // CollisionNode<T> CollisionNode { get; set; }
    float X { get; }
    float Y { get; }
}
