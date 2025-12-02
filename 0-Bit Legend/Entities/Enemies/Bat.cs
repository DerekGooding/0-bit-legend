using _0_Bit_Legend.Model.Enums;
using static _0_Bit_Legend.MainProgram;

namespace _0_Bit_Legend.Entities.Enemies;

public class Bat : IEnemy
{
    public EnemyType Type => EnemyType.Bat;
    public void Draw(int posX, int posY, Direction previousIndex)
    {
        if (previousIndex == Direction.Left)
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

    public bool IsTouching(int posX, int posY, char symbol)
    {
        //(Map[posX, posY] == symbol || Map[posX + 1, posY] == symbol || Map[posX + 2, posY] == symbol || Map[posX + 3, posY] == symbol || Map[posX + 4, posY] == symbol || Map[posX, posY + 1] == symbol || Map[posX + 1, posY + 1] == symbol || Map[posX + 2, posY + 1] == symbol || Map[posX + 3, posY + 1] == symbol || Map[posX + 4, posY + 1] == symbol))
        for (var i = 0; i < 5; i++)
        {
            for (var j = 0; j < 2; j++)
            {
                if (Map[posX + i, posY + j] == symbol)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public bool IsTouching(int posX, int posY, char[] symbols)
    {
        for (var i = 0; i < 5; i++)
        {
            for (var j = 0; j < 2; j++)
            {
                if (symbols.Any(x => x == Map[posX + i, posY + j]))
                {
                    return true;
                }
            }
        }
        return false;
    }
}
