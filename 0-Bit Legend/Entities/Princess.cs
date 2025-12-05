namespace _0_Bit_Legend.Entities;

public class Princess : IEntity, ICollider
{
    public int Hp { get; set; } = 3;
    public Vector2 Position { get; set; } = Vector2.Zero;
    public DirectionType Direction { get; set; } = DirectionType.Up;

    public Vector2 Size { get; } = new(5, 3);

    private readonly string[] _spriteSheet =
    [
        " =<>= ",
        " s^^s ",
        "ss~~ss",
        "~~~~~~",
    ];

    public void Draw() => DrawToScreen(_spriteSheet, Position);

    public bool InsideBoundingBox(char symbol) => false;
    public bool InsideBoundingBox(char[] symbols) => false;
    public bool IsTouching(char symbol) => false;
    public bool IsTouching(char[] symbols) => false;
}
