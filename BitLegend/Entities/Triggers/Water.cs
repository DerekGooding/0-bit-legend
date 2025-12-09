namespace BitLegend.Entities.Triggers;

public class Water : IEntity, ICollider
{
    public Action OnContact { get; } = () =>
    {

    };

    public Vector2 Position { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public DirectionType Direction { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public Vector2 Size => new(13,0);

    private string[] _spriteSheet = ["~~~~~~~~~~~~~~"];

    public void Draw() => throw new NotImplementedException();
    public void HandleCollision() => OnContact.Invoke();
}
