namespace BitLegend.Entities.Enemies;

public class Fireball : BaseEnemy
{
    public override EnemyType Type => EnemyType.Fireball;

    public override Vector2 Size { get; } = new(2, 1);

    private readonly string[] _spriteSheet =
[
        "FFF",
        "FFF",
    ];

    public override void Draw() => DrawToScreen(_spriteSheet, Position);

    public override void Move()
    {
        var newPosition = Direction switch
        {
            DirectionType.Up => Position.Offset(-3, -2),
            DirectionType.Left => Position.Offset(x: -3),
            DirectionType.Down => Position.Offset(-3, + 2),
            DirectionType.Right => Position.Offset(x: 3),
            _ => throw new NotSupportedException()
        };

        TryMove(newPosition, Direction, -1);
    }
    public override void TakeDamage()
    {
        //nothing
    }
}