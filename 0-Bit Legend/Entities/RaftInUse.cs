namespace _0_Bit_Legend.Entities;

public class RaftInUse : IEntity, IUsable
{
    public bool IsActive { get; set; }
    public DirectionType Direction { get; set; } = DirectionType.Up;
    public Vector2 Position { get; set; } = Vector2.Zero;

    public readonly string[] _spriteSheet =
[
        "*****",
        "*****",
        "*****",
        "*****",
        "*****",
        "*****",
        "*****",
    ];

    public void Draw() => throw new NotImplementedException();
}