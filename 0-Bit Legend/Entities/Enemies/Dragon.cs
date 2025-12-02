using _0_Bit_Legend.Model.Enums;
using static _0_Bit_Legend.MainProgram;

namespace _0_Bit_Legend.Entities.Enemies;

public class Dragon : IEnemy
{
    public EnemyType Type => EnemyType.Dragon;
    public void Draw(int posX, int posY, Direction previousIndex)
    {
        var dragon = "<***>        S^SSS>      *S  SS>        =S>        =*SSSS**>   =*SSSSS*     ===  == ";
        if (previousIndex == Direction.Down) dragon = "<***>        F^FFF>      *F  FS>        FF>        FF*SSS**>   F**SSSS*     ===  == ";

        var debounce = false;
        var value = 0;
        for (var i = 0; i < 7; i++)
        {
            for (var j = 0; j < 12; j++)
            {
                if (Map[posX + j, posY + i] == '/'
                    || Map[posX + j, posY + i] == '\\'
                    || Map[posX + j, posY + i] == '|'
                    || (Map[posX + j, posY + i] == '_' && !debounce))
                {
                    debounce = true;
                    MainProgram.PlayerController.Hit();
                }
                Map[posX + j, posY + i] = dragon[value];
                value++;
            }
        }
    }

    public bool IsTouching(int posX, int posY, char symbol) => false;

    public bool IsTouching(int posX, int posY, char[] symbols) => false;
}
