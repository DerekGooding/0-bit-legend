namespace BitLegend.Entities.Enemies;

public interface IEnemy : IEntity, ICollider
{
    public EnemyType Type { get; }
    public int Hp { get; set; }
    public int Motion { get; set; }

    public void TakeDamage();
    public void Move();
    public void Die();

    public void SpawnRupee(Vector2 position);

    public bool TryMove(Vector2 position, DirectionType direction, int motion);
}
