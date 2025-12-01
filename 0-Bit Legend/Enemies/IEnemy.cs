using _0_Bit_Legend.Model;

namespace _0_Bit_Legend.Enemies;

public interface IEnemy : IEntity
{
    public EnemyType Type { get; }
}
