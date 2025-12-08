using _0_Bit_Legend.Entities.Pickups;

namespace _0_Bit_Legend.Entities.Enemies;

public class Spider : BaseEnemy
{
    public override EnemyType Type => EnemyType.Spider;

    public override Vector2 Size { get; } = new(4, 2);

    public new DirectionType Direction { get; set; } = DirectionType.Left;

    private readonly Dictionary<DirectionType, string[]> _spriteSheet = new()
    {
        { DirectionType.Left,
        [
            " ttt ",
            "n00tt",
            " nt n",
        ]},
        { DirectionType.Right,
        [
            " ttt ",
            "tt00n",
            " ntn ",
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
        if (SkipMoveCheck())
            return;

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
        DirectionType.Up => Position.Offset(-3, -2),
        DirectionType.Left => Position.Offset(x: -3),
        DirectionType.Down => Position.Offset(-3, +2),
        DirectionType.Right => Position.Offset(x: 3),
        _ => throw new NotSupportedException()
    };

    private bool SkipMoveCheck()
    {
        Motion--;
        if (Motion <= -5)
        {
            Motion = 10;
        }
        return Motion > 0;
    }
}
