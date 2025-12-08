using _0_Bit_Legend.Entities.Pickups;

namespace _0_Bit_Legend.Entities.Enemies;

public class Octorok : BaseEnemy
{
    public override EnemyType Type { get; } = EnemyType.Octorok;
    public override Vector2 Size { get; } = new(3, 2);

    public new DirectionType Direction { get; set; } = DirectionType.Left;

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
        EntityManager.Add(newRupee);
    }

    public override void Draw()
    {
        var image = _spriteSheet[Direction];

        DrawToScreen(image, Position);
    }

    public override void Move()
    {
        var rnd1 = Random.Shared.Next(10);
        var passed = rnd1 <= 2;
        var newPosition = DirectionToOffset(Direction);

        if (!passed)
            passed = !TryMove(newPosition, Direction, -1);

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

