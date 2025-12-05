namespace _0_Bit_Legend.Entities.Enemies;

public class Fireball : BaseEnemy
{
    public override EnemyType Type => EnemyType.Fireball;
    public override char[] MapStorage { get; } = new string(' ', 6).ToCharArray();

    public override (Vector2 TopLeft, Vector2 BottomRight) BoundingBox { get; } = (new(0, 0), new(2, 1));

    private readonly string[] _spriteSheet =
[
        "FFF",
        "FFF",
    ];


    public override void Clear()
    {
        Map[Position.X + 0, Position.Y] = MapStorage[0];
        Map[Position.X + 1, Position.Y] = MapStorage[1];
        Map[Position.X + 2, Position.Y] = MapStorage[2];

        Map[Position.X + 0, Position.Y + 1] = MapStorage[3];
        Map[Position.X + 1, Position.Y + 1] = MapStorage[4];
        Map[Position.X + 2, Position.Y + 1] = MapStorage[5];
    }

    public override void Draw()
    {
        for (var x = BoundingBox.TopLeft.X; x <= BoundingBox.BottomRight.X; x++)
        {
            for (var y = BoundingBox.TopLeft.Y; y <= BoundingBox.BottomRight.Y; y++)
            {
                Map[Position.X + x, Position.Y + y] = _spriteSheet[y][x];
            }
        }
    }

    public override bool InBounds(Vector2 position)
    {
        var inPosX = position.X + 3;
        var inPosY = position.Y + 1;

        return position.X > 0 && inPosX < 102 && position.Y > 0 && inPosY < 33;
    }

    public override void Move()
    {
        var newPosition = Prev1 switch
        {
            DirectionType.Up => Position.Offset(-3, -2),
            DirectionType.Left => Position.Offset(x: -3),
            DirectionType.Down => Position.Offset(-3, + 2),
            DirectionType.Right => Position.Offset(x: 3),
            _ => throw new NotSupportedException()
        };

        TryMove(newPosition, Prev1, -1);
    }
    public override void TakeDamage()
    {
        //nothing
    }
}