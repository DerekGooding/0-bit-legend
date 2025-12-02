namespace _0_Bit_Legend.Entities.Enemies;

public class Fireball : IEnemy
{
    public EnemyType Type => EnemyType.Fireball;
    public Vector2 Position { get; set; } = Vector2.Zero;
    public DirectionType Direction { get; set; }
    public void Draw(int posX, int posY, DirectionType _)
    {
        Map[posX + 0, posY] = 'F';
        Map[posX + 1, posY] = 'F';
        Map[posX + 2, posY] = 'F';

        Map[posX + 0, posY + 1] = 'F';
        Map[posX + 1, posY + 1] = 'F';
        Map[posX + 2, posY + 1] = 'F';
    }

    public bool InBounds(int posX, int posY)
    {
        var inPosX = posX + 3;
        var inPosY = posY + 1;

        return posX > 0 && inPosX < 102 && posY > 0 && inPosY < 33;
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