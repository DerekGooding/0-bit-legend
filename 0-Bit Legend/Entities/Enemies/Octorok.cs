using _0_Bit_Legend.Entities.Pickups;

namespace _0_Bit_Legend.Entities.Enemies;

public class Octorok : BaseEnemy
{
    public override EnemyType Type { get; } = EnemyType.Octorok;
    public override (Vector2 TopLeft, Vector2 BottomRight) BoundingBox { get; } = (new(0, 0), new(3, 2));

    private readonly Dictionary<DirectionType, string[]> _spriteSheet = new()
    {
        { DirectionType.Left,
        [
            " ttt",
            "t^tt",
            "tttt",
        ]},
        { DirectionType.Right,
        [
            "ttt ",
            "tt^t",
            "tttt",
        ]},
    };

    public override void SpawnRupee(Vector2 position)
    {
        var newRupee = new Rupee();
        var sRPosX = position.X;
        var sRPosY = position.Y;

        var value = 0;
        for (var i = 0; i < 3; i++)
        {
            for (var j = 0; j < 3; j++)
            {
                newRupee.MapStorage[value] = Map[sRPosX - 1 + j, sRPosY - 1 + i] is not '-' and not 'S'
                    ? Map[sRPosX - 1 + j, sRPosY - 1 + i]
                    : ' ';
                value++;
            }
        }
        newRupee.Position = new(sRPosX, sRPosY);
        EnemyManager.AddRupee(newRupee);

        Map[sRPosX - 1, sRPosY] = 'R';
        Map[sRPosX + 1, sRPosY] = 'R';
        Map[sRPosX, sRPosY - 1] = 'r';
        Map[sRPosX, sRPosY + 1] = 'r';
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
        var inPosX = position.X + 3;
        var inPosY = position.Y + 2;

        return position.X > 0 && inPosX < 102 && position.Y > 0 && inPosY < 33;
    }

    public override void Move()
    {
        var rnd1 = Random.Shared.Next(10);
        var passed = rnd1 <= 2;
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
        DirectionType.Up => Position.Offset(y: -1),
        DirectionType.Left => Position.Offset(x: -2),
        DirectionType.Down => Position.Offset(y: 1),
        DirectionType.Right => Position.Offset(x: 2),
        _ => throw new NotSupportedException()
    };
}

