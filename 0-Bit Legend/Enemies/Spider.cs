using _0_Bit_Legend.Model;
using static _0_Bit_Legend.MainProgram;

namespace _0_Bit_Legend.Enemies;

public class Spider : IEnemy
{
    public EnemyType Type => EnemyType.Spider;
    public void Build(int posX, int posY, Direction previousIndex)
    {
        if (previousIndex == Direction.Left)
        {
            Map[posX + 0, posY] = ' ';
            Map[posX + 1, posY] = 't';
            Map[posX + 2, posY] = 't';
            Map[posX + 3, posY] = 't';
            Map[posX + 4, posY] = ' ';

            Map[posX + 0, posY + 1] = 'n';
            Map[posX + 1, posY + 1] = '0';
            Map[posX + 2, posY + 1] = '0';
            Map[posX + 3, posY + 1] = 't';
            Map[posX + 4, posY + 1] = 't';

            Map[posX + 0, posY + 2] = ' ';
            Map[posX + 1, posY + 2] = 'n';
            Map[posX + 2, posY + 2] = 't';
            Map[posX + 4, posY + 2] = ' ';
            Map[posX + 3, posY + 2] = 'n';
        }
        else
        {
            Map[posX + 0, posY] = ' ';
            Map[posX + 1, posY] = 't';
            Map[posX + 2, posY] = 't';
            Map[posX + 3, posY] = 't';
            Map[posX + 4, posY] = ' ';

            Map[posX + 0, posY + 1] = 't';
            Map[posX + 1, posY + 1] = 't';
            Map[posX + 2, posY + 1] = '0';
            Map[posX + 3, posY + 1] = '0';
            Map[posX + 4, posY + 1] = 'n';

            Map[posX + 0, posY + 2] = ' ';
            Map[posX + 1, posY + 2] = 'n';
            Map[posX + 2, posY + 2] = 't';
            Map[posX + 3, posY + 2] = 'n';
            Map[posX + 4, posY + 2] = ' ';
        }
    }

    public bool IsTouching(int posX, int posY, char symbol)
    {
        //(Map[posX, posY] == symbol || Map[posX + 1, posY] == symbol || Map[posX + 2, posY] == symbol || Map[posX + 3, posY] == symbol || Map[posX + 4, posY] == symbol || Map[posX, posY + 1] == symbol || Map[posX + 1, posY + 1] == symbol || Map[posX + 2, posY + 1] == symbol || Map[posX + 3, posY + 1] == symbol || Map[posX + 4, posY + 1] == symbol || Map[posX, posY + 2] == symbol || Map[posX + 1, posY + 2] == symbol || Map[posX + 2, posY + 2] == symbol || Map[posX + 3, posY + 2] == symbol || Map[posX + 4, posY + 2] == symbol))
        for (var i = 0; i < 5; i++)
        {
            for (var j = 0; j < 3; j++)
            {
                if (Map[posX + i, posY + j] == symbol)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
