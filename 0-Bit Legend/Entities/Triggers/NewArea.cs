namespace _0_Bit_Legend.Entities.Triggers;

public class NewArea(int mapId, Vector2 startPosition, DirectionType startDirection) : IEntity, ICollider
{
    public Action OnContact { get; } = () => LoadMap(mapId, startPosition, startDirection);

    public Vector2 Position { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public DirectionType Direction { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public Vector2 Size => throw new NotImplementedException();

    public void Draw()
    {
        //Nothing
    }
}
