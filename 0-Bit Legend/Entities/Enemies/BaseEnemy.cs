namespace _0_Bit_Legend.Entities.Enemies;

public abstract class BaseEnemy : IEnemy
{
    public abstract EnemyType Type { get; }
    public abstract char[] MapStorage { get; }

    public int Hp { get; set; } = 1;
    public int Motion { get; set; }
    public DirectionType Prev1 { get; set; }
    public DirectionType Prev2 { get; set; }
    public Vector2 Position { get; set; } = Vector2.Zero;
    public DirectionType Direction { get; set; }

    public abstract void Clear();
    public abstract void Draw();
    public abstract bool InBounds(Vector2 position);
    public abstract bool IsTouching(char symbol);
    public abstract bool IsTouching(char[] symbols);
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
        EnemyManager.SetStoredRupeePosition(new(Position.X + 2, Position.Y + 1));

        EnemyManager.Remove(this);

        PlayerController.SetSpawnRupee(true);
    }
}
