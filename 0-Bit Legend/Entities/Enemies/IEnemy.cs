namespace _0_Bit_Legend.Entities.Enemies;

public interface IEnemy : IEntity
{
    public EnemyType Type { get; }

    public bool InBounds(int posX, int posY);
}
