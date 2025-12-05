namespace _0_Bit_Legend.Entities.Enemies;

public class Bat : BaseEnemy
{
    public override EnemyType Type { get; } = EnemyType.Bat;
    public override char[] MapStorage { get; } = new string(' ', 10).ToCharArray();

    public override (Vector2 TopLeft, Vector2 BottomRight) BoundingBox { get; } = (new(0, 0), new(4, 1));

    private readonly Dictionary<DirectionType, string[]> _spriteSheet = new()
    {
        { DirectionType.Left,
        [
            "{t t}",
            "  B  ",
        ]},
        { DirectionType.Right,
        [
            "  B  ",
            "{t t}",
        ]},
    };

    public override void Clear()
    {
        Map[Position.X + 0, Position.Y] = MapStorage[0];
        Map[Position.X + 1, Position.Y] = MapStorage[1];
        Map[Position.X + 2, Position.Y] = MapStorage[2];
        Map[Position.X + 3, Position.Y] = MapStorage[3];
        Map[Position.X + 4, Position.Y] = MapStorage[4];

        Map[Position.X + 0, Position.Y + 1] = MapStorage[5];
        Map[Position.X + 1, Position.Y + 1] = MapStorage[6];
        Map[Position.X + 2, Position.Y + 1] = MapStorage[7];
        Map[Position.X + 3, Position.Y + 1] = MapStorage[8];
        Map[Position.X + 4, Position.Y + 1] = MapStorage[9];
    }

    public override void Draw()
    {
        var image = _spriteSheet[Prev2];

        for (var x = BoundingBox.TopLeft.X; x <= BoundingBox.BottomRight.X; x++)
        {
            for (var y = BoundingBox.TopLeft.Y; y <= BoundingBox.BottomRight.Y; y++)
            {
                Map[Position.X + x, Position.Y + y] = image[y][x];
            }
        }
    }

    public override bool InBounds(Vector2 position)
    {
        var inPosX = position.X + 4;
        var inPosY = position.Y + 1;

        return position.X > 0 && inPosX < 102 && position.Y > 0 && inPosY < 33;
    }

    public override void Move()
    {
        var rnd1 = Random.Shared.Next(10);
        var passed = rnd1 <= 4;
        var newPosition = DirectionToOffset(Prev1);

        if (!passed)
            passed = !TryMove(newPosition, Prev1, -1);

        if (!passed)
            return;


        var randomDirection = Random.Shared.RandomEnum<DirectionType>();
        newPosition = DirectionToOffset(randomDirection);

        TryMove(newPosition, randomDirection, -1);
    }

    private Vector2 DirectionToOffset(DirectionType type) => type switch
    {
        DirectionType.Up => Position.Offset(-2, -1),
        DirectionType.Left => Position.Offset(x: 2, -1),
        DirectionType.Down => Position.Offset(-2, 1),
        DirectionType.Right => Position.Offset(2, 1),
        _ => throw new NotSupportedException()
    };
}
