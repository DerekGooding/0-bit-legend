namespace _0_bit_Legend.Entities;

public class SwordInUse : IEntity, ICollider, IUsable
{
    public bool IsActive { get; set; }
    public Vector2 Position { get; set;  } = Vector2.Zero;
    public DirectionType Direction { get; set; } = DirectionType.Up;

    public Vector2 Size { get; } = new(2,2);

    private readonly Dictionary<DirectionType, string[]> _spriteSheet = new()
    {
        { DirectionType.Up,
        [
            " S ",
            " S ",
            "---",
        ]},
        { DirectionType.Left,
        [
            "  |",
            "SS|",
            "  |",
        ]},
        { DirectionType.Down,
        [
            "---",
            " S ",
            " S ",
        ]},
        { DirectionType.Right,
        [
            "|  ",
            "|S ",
            "|  ",
        ] },
    };

    public void Draw()
    {
        var image = _spriteSheet[Direction];
        DrawToScreen(image, Position);
    }

    public void HandleCollision()
    {
        throw new NotImplementedException();
    }
}
