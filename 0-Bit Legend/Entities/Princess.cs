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
        var xOffset = 0 - BoundingBox.TopLeft.X;
        var yOffset = 0 - BoundingBox.TopLeft.Y;

        for (var x = BoundingBox.TopLeft.X; x <= BoundingBox.BottomRight.X; x++)
        {
            for (var y = BoundingBox.TopLeft.Y; y <= BoundingBox.BottomRight.Y; y++)
            {
                Map[Position.X + x, Position.Y + y] = _spriteSheet[y + yOffset][x + xOffset];
            }
        }
    }

    public bool InsideBoundingBox(char symbol) => false;
    public bool InsideBoundingBox(char[] symbols) => false;
    public bool IsTouching(char symbol) => false;
    public bool IsTouching(char[] symbols) => false;
}
