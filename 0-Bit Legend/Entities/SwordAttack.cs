namespace _0_Bit_Legend.Entities;

public class SwordAttack : IEntity, ICollider
{
    public Vector2 Position { get; set;  } = Vector2.Zero;
    public DirectionType Direction { get; set; } = DirectionType.Up;

    public Vector2 Size { get; } = new(0,0);

    private readonly Dictionary<DirectionType, string[]> _spriteSheet = new()
    {
        { DirectionType.Up,
        [
            " ___ ",
            "| = |",
            "|^ ^|",
           @" \_/ ",
        ]},
        { DirectionType.Left,
        [
           @"  /\ ",
            " /  |",
            "|^  |",
            "|_=_|",
        ]},
        { DirectionType.Down,
        [
            "  _  ",
           @" / \ ",
            "|^ ^|",
            "| = |",
        ]},
        { DirectionType.Right,
        [
           @" /\  ",
           @"|  \ ",
            "|  ^|",
            "|_=_|",
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
