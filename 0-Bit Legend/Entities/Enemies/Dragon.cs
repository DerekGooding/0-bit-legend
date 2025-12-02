namespace _0_Bit_Legend.Entities.Enemies;

public class Dragon : IEnemy
{
    public EnemyType Type => EnemyType.Dragon;
    public Vector2 Position { get; set; } = Vector2.Zero;
    public DirectionType Direction { get; set; }
    public void Draw(int posX, int posY, DirectionType previousIndex)
    {
        var dragon = "<***>        S^SSS>      *S  SS>        =S>        =*SSSS**>   =*SSSSS*     ===  == ";
        if (previousIndex == DirectionType.Down) dragon = "<***>        F^FFF>      *F  FS>        FF>        FF*SSS**>   F**SSSS*     ===  == ";

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

    public bool InBounds(int posX, int posY) => posX > 0 && posY > 0;

    public bool IsTouching(int posX, int posY, char symbol) => false;

    public bool IsTouching(int posX, int posY, char[] symbols) => false;
}
