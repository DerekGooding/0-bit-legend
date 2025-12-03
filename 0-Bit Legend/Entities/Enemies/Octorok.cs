namespace _0_Bit_Legend.Entities.Enemies;

public class Octorok : BaseEnemy
{
    public override EnemyType Type { get; } = EnemyType.Octorok;
    public override char[] MapStorage { get; } = new string(' ', 12).ToCharArray();

    public override void Clear()
    {
        Map[Position.X + 0, Position.Y] = MapStorage[0];
        Map[Position.X + 1, Position.Y] = MapStorage[1];
        Map[Position.X + 2, Position.Y] = MapStorage[2];
        Map[Position.X + 3, Position.Y] = MapStorage[3];

        Map[Position.X + 0, Position.Y + 1] = MapStorage[4];
        Map[Position.X + 1, Position.Y + 1] = MapStorage[5];
        Map[Position.X + 2, Position.Y + 1] = MapStorage[6];
        Map[Position.X + 3, Position.Y + 1] = MapStorage[7];

        Map[Position.X + 0, Position.Y + 2] = MapStorage[8];
        Map[Position.X + 1, Position.Y + 2] = MapStorage[9];
        Map[Position.X + 2, Position.Y + 2] = MapStorage[10];
        Map[Position.X + 3, Position.Y + 2] = MapStorage[11];
    }

    public override void Draw()
    {
        var posX = Position.X;
        var posY = Position.Y;

        if (Prev2 == DirectionType.Left)
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

    public override bool InBounds(Vector2 position)
    {
        var inPosX = position.X + 3;
        var inPosY = position.Y + 2;

        return position.X > 0 && inPosX < 102 && position.Y > 0 && inPosY < 33;
    }

    public override bool IsTouching(char symbol)
    {
        var posX = Position.X;
        var posY = Position.Y;

        //(Map[posX, posY] == symbol || Map[posX + 1, posY] == symbol || Map[posX + 2, posY] == symbol || Map[posX + 3, posY] == symbol || Map[posX, posY + 1] == symbol || Map[posX + 1, posY + 1] == symbol || Map[posX + 2, posY + 1] == symbol || Map[posX + 3, posY + 1] == symbol || Map[posX, posY + 2] == symbol || Map[posX + 1, posY + 2] == symbol || Map[posX + 2, posY + 2] == symbol || Map[posX + 3, posY + 2] == symbol))
        for (var i = 0; i < 4; i++)
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

    public override bool IsTouching(char[] symbols)
    {
        var posX = Position.X;
        var posY = Position.Y;

        for (var i = 0; i < 4; i++)
        {
            for (var j = 0; j < 3; j++)
            {
                if (symbols.Any(x => x == Map[posX + i, posY + j]))
                {
                    return true;
                }
            }
        }
        return false;
    }

    public override void Move() => throw new NotImplementedException();
}
