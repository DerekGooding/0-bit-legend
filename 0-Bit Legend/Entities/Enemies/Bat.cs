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

        ]},
        { DirectionType.Right,
        [

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
        var posX = Position.X;
        var posY = Position.Y;

        if (Prev2 == DirectionType.Left)
        {
            Map[posX + 0, posY] = '{';
            Map[posX + 1, posY] = 't';
            Map[posX + 2, posY] = ' ';
            Map[posX + 3, posY] = 't';
            Map[posX + 4, posY] = '}';

            Map[posX + 0, posY + 1] = ' ';
            Map[posX + 1, posY + 1] = ' ';
            Map[posX + 2, posY + 1] = 'B';
            Map[posX + 3, posY + 1] = ' ';
            Map[posX + 4, posY + 1] = ' ';
        }
        else
        {
            Map[posX + 0, posY] = ' ';
            Map[posX + 1, posY] = ' ';
            Map[posX + 2, posY] = 'B';
            Map[posX + 3, posY] = ' ';
            Map[posX + 4, posY] = ' ';

            Map[posX + 0, posY + 1] = '{';
            Map[posX + 1, posY + 1] = 't';
            Map[posX + 2, posY + 1] = ' ';
            Map[posX + 3, posY + 1] = 't';
            Map[posX + 4, posY + 1] = '}';
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
