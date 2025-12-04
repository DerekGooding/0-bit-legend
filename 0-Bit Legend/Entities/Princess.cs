namespace _0_Bit_Legend.Entities;

public class Princess : IEntity, IBoundingBox
{
    public int Hp { get; set; } = 3;
    public Vector2 Position { get; set; } = Vector2.Zero;
    public DirectionType Direction { get; set; } = DirectionType.Up;

    public (Vector2 TopLeft, Vector2 BottomRight) BoundingBox { get; } = (new(-2, -1), new(3, 2));

    private readonly string[] _spriteSheet =
    [
        " =<>= ",
        " s^^s ",
        "ss~~ss",
        "~~~~~~",
    ];

    public void Draw()
    {
        for (var x = BoundingBox.TopLeft.X; x <= BoundingBox.BottomRight.X; x++)
        {
            for (var y = BoundingBox.TopLeft.Y; y <= BoundingBox.BottomRight.Y; y++)
            {
                Map[Position.X + x, Position.Y + y] = _spriteSheet[y + 1][x + 2];
            }
        }
    }

    public bool InsideBoundingBox(char symbol) => false;
    public bool InsideBoundingBox(char[] symbols) => false;
    public bool IsTouching(char symbol) => false;
    public bool IsTouching(char[] symbols) => false;
}
