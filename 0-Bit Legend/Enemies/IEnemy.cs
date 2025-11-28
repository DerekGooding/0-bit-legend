using _0_Bit_Legend.Model;

namespace _0_Bit_Legend.Enemies;

public interface IEnemy
{
    public EnemyType Type { get; }
    public void Build(int posX, int posY, char previousIndex);
}
