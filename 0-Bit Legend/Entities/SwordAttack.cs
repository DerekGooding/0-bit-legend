namespace _0_Bit_Legend.Entities;

public class SwordAttack : IEntity, ICollider
{
    public bool IsActive { get; set; } = true;
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
        throw new NotImplementedException();
    }

    public void HandleCollision()
    {
        throw new NotImplementedException();
    }
}
