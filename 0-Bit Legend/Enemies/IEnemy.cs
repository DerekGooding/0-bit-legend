using _0_Bit_Legend.Model.Enums;

namespace _0_Bit_Legend.Enemies;

public interface IEnemy : IEntity
{
    public EnemyType Type { get; }
}
