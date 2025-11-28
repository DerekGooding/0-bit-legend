using _0_Bit_Legend.Model;
using static _0_Bit_Legend.MainProgram;

namespace _0_Bit_Legend.Enemies;

public class Bat : IEnemy
{
    public EnemyType Type => EnemyType.Bat;
    public void Build(int posX, int posY, char previousIndex)
    {
        // Implementation for building a Bat enemy
    }
}
