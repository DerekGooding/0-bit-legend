namespace BitLegend.Entities.Enemies;

public abstract class BaseEnemy : IEnemy
{
    public abstract EnemyType Type { get; }

    public abstract  Vector2 Size { get; }

    public int Hp { get; set; } = 1;
    public int Motion { get; set; }
    public Vector2 Position { get; set; } = Vector2.Zero;
    public DirectionType Direction { get; set; }

    public abstract void Draw();
    public abstract void Move();
    public virtual void TakeDamage()
    {
        Hp--;
        if (Hp <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        EntityManager.Remove(this);
        SpawnRupee(Position);
    }

    public virtual void SpawnRupee(Vector2 position) { }

    public bool TryMove(Vector2 position, DirectionType direction, int motion)
    {
        Motion = motion;
        var magnitude = 1;

        var points = GetMovePoints(direction);
        if (!CanMove(points))
        {
            return false;
        }
        Direction = direction;
        Position = direction switch
        {
            DirectionType.Up => Position.Offset(0, -magnitude),
            DirectionType.Left => Position.Offset(-magnitude * 2, 0),
            DirectionType.Down => Position.Offset(0, magnitude),
            DirectionType.Right => Position.Offset(magnitude * 2, 0),
            _ => throw new Exception(),
        };

        return true;
    }

    public void HandleCollision() => SetGameState(GameState.Hit);

    private List<Vector2> GetMovePoints(DirectionType direction)
    {
        var bottom = Position.Y + Size.Y;
        var right = Position.X + Size.X;
        return direction switch
        {
            DirectionType.Up =>
            [.. Enumerable.Range(Position.X, Size.X + 1).Select(p => new Vector2(p, Position.Y - 1))],
            DirectionType.Down =>
            [.. Enumerable.Range(Position.X, Size.X + 1).Select(p => new Vector2(p, bottom + 1))],
            DirectionType.Left =>
            [.. Enumerable.Range(Position.Y, Size.Y + 1).Select(p => new Vector2(Position.X - 2, p))],
            DirectionType.Right =>
            [.. Enumerable.Range(Position.Y, Size.Y + 1).Select(p => new Vector2(right + 2, p))],
            _ => throw new Exception()
        };
    }

    private bool CanMove(List<Vector2> points) => !points.Any(OutsideGameSpace) && !points.Any(IsBlocking);

    private bool IsBlocking(Vector2 point) => WallMap[point.X, point.Y];

    private bool OutsideGameSpace(Vector2 point)
    => point.X < 0
    || point.X >= GlobalSize.X
    || point.Y < 0
    || point.Y >= GlobalSize.Y;
}
