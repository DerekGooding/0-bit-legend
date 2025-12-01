using _0_Bit_Legend.Model;
using static _0_Bit_Legend.MainProgram;

namespace _0_Bit_Legend.Enemies;

public class Fireball : IEnemy
{
    public EnemyType Type => EnemyType.Fireball;
    public void Build(int posX, int posY, Direction _)
    {
        Map[posX + 0, posY] = 'F';
        Map[posX + 1, posY] = 'F';
        Map[posX + 2, posY] = 'F';

        Map[posX + 0, posY + 1] = 'F';
        Map[posX + 1, posY + 1] = 'F';
        Map[posX + 2, posY + 1] = 'F';
    }

    public bool IsTouching(int posX, int posY, char symbol)
    {
        //(Map[posX, posY] == symbol || Map[posX + 1, posY] == symbol || Map[posX + 2, posY] == symbol || Map[posX, posY + 1] == symbol || Map[posX + 1, posY + 1] == symbol || Map[posX + 2, posY + 1] == symbol))))
        for (var i = 0; i < 3; i++)
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
        for (var i = 0; i < 3; i++)
        {
            for (var j = 0; j < 2; j++)
            {
                if (symbols.Any(x => x ==Map[posX + i, posY + j]))
                {
                    return true;
                }
            }
        }
        return false;
    }
}