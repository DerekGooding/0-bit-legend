namespace _0_Bit_Legend.Entities.Triggers;

public class Water : IEntity, ICollider
{
    public Action OnContact { get; } = () =>
    {

    };

    public Vector2 Position { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public DirectionType Direction { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public Vector2 Size => throw new NotImplementedException();

    public void Draw() => throw new NotImplementedException();
}
