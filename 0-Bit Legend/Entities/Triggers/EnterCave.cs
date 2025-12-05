using _0_Bit_Legend.Animations;

namespace _0_Bit_Legend.Entities.Triggers;

public class EnterCave(int mapId, Vector2 startPosition, DirectionType startDirection) : IEntity, ICollider
{
    public Action OnContact { get; } = () =>
    {
        new CaveTransition().Call();
        LoadMap(mapId, startPosition, startDirection);
    };

    public Vector2 Position { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public DirectionType Direction { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public Vector2 Size => throw new NotImplementedException();

    public void Draw() => throw new NotImplementedException();
}
