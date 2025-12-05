using _0_Bit_Legend.Entities.Pickups;

namespace _0_Bit_Legend.Entities.Enemies;

public class Octorok : BaseEnemy
{
    public override EnemyType Type { get; } = EnemyType.Octorok;
    public override Vector2 Size { get; } = new(3, 2);

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
        var newRupee = new Rupee
        {
            Position = new(position.X, position.Y)
        };
        EnemyManager.AddRupee(newRupee);
    }

    public override void Draw()
    {
        var image = _spriteSheet[Prev2];

        DrawToScreen(image, Position);
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

