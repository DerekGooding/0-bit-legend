using _0_Bit_Legend.Model;
using static _0_Bit_Legend.MainProgram;

namespace _0_Bit_Legend.Enemies;

public class Octorok : IEnemy
{
    public EnemyType Type => EnemyType.Octorok;
    public void Build(int posX, int posY, char previousIndex)
    {
        if (previousIndex == 'a')
        {
            Map[posX + 0, posY] = ' ';
            Map[posX + 1, posY] = 't';
            Map[posX + 2, posY] = 't';
            Map[posX + 3, posY] = 't';

            Map[posX + 0, posY + 1] = 't';
            Map[posX + 1, posY + 1] = '^';
            Map[posX + 2, posY + 1] = 't';
            Map[posX + 3, posY + 1] = 't';

            Map[posX + 0, posY + 2] = 't';
            Map[posX + 1, posY + 2] = 't';
            Map[posX + 2, posY + 2] = 't';
            Map[posX + 3, posY + 2] = 't';
        }
        else
        {
            Map[posX + 0, posY] = 't';
            Map[posX + 1, posY] = 't';
            Map[posX + 2, posY] = 't';
            Map[posX + 3, posY] = ' ';

            Map[posX + 0, posY + 1] = 't';
            Map[posX + 1, posY + 1] = 't';
            Map[posX + 2, posY + 1] = '^';
            Map[posX + 3, posY + 1] = 't';

            Map[posX + 0, posY + 2] = 't';
            Map[posX + 1, posY + 2] = 't';
            Map[posX + 2, posY + 2] = 't';
            Map[posX + 3, posY + 2] = 't';
        }
    }
}
