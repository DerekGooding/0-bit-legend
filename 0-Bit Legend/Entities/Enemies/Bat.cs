namespace _0_Bit_Legend.Entities.Enemies;

public class Bat : BaseEnemy
{
    public override EnemyType Type { get; } = EnemyType.Bat;

    public override Vector2 Size { get; } = new(4, 1);

    public new DirectionType Direction { get; set; } = DirectionType.Left;

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

    public override void Draw()
    {
        var image = _spriteSheet[Direction];
        DrawToScreen(image, Position);
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
        DirectionType.Up => Position.Offset(-2, -1),
        DirectionType.Left => Position.Offset(x: 2, -1),
        DirectionType.Down => Position.Offset(-2, 1),
        DirectionType.Right => Position.Offset(2, 1),
        _ => throw new NotSupportedException()
    };
}
