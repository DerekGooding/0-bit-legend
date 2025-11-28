using _0_Bit_Legend.Model;
using static _0_Bit_Legend.MainProgram;

namespace _0_Bit_Legend.Enemies;

public class Fireball : IEnemy
{
    public EnemyType Type => EnemyType.Fireball;
    public void Build(int posX, int posY, char previousIndex)
    {
        Map[posX + 0, posY] = 'F';
        Map[posX + 1, posY] = 'F';
        Map[posX + 2, posY] = 'F';

        Map[posX + 0, posY + 1] = 'F';
        Map[posX + 1, posY + 1] = 'F';
        Map[posX + 2, posY + 1] = 'F';
    }
}