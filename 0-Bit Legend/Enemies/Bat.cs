using _0_Bit_Legend.Model;
using static _0_Bit_Legend.MainProgram;

namespace _0_Bit_Legend.Enemies;

public class Bat : IEnemy
{
    public EnemyType Type => EnemyType.Bat;
    public void Build(int posX, int posY, char previousIndex)
    {
        if (previousIndex == 'a')
        {
            Map[posX + 0, posY] = '{';
            Map[posX + 1, posY] = 't';
            Map[posX + 2, posY] = ' ';
            Map[posX + 3, posY] = 't';
            Map[posX + 4, posY] = '}';

            Map[posX + 0, posY + 1] = ' ';
            Map[posX + 1, posY + 1] = ' ';
            Map[posX + 2, posY + 1] = 'B';
            Map[posX + 3, posY + 1] = ' ';
            Map[posX + 4, posY + 1] = ' ';
        }
        else
        {
            Map[posX + 0, posY] = ' ';
            Map[posX + 1, posY] = ' ';
            Map[posX + 2, posY] = 'B';
            Map[posX + 3, posY] = ' ';
            Map[posX + 4, posY] = ' ';

            Map[posX + 0, posY + 1] = '{';
            Map[posX + 1, posY + 1] = 't';
            Map[posX + 2, posY + 1] = ' ';
            Map[posX + 3, posY + 1] = 't';
            Map[posX + 4, posY + 1] = '}';
        }
    }
}
