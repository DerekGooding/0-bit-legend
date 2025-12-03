namespace _0_Bit_Legend.Entities.Enemies;

public class Octorok : BaseEnemy
{
    public override EnemyType Type { get; } = EnemyType.Octorok;
    public override char[] MapStorage { get; } = new string(' ', 12).ToCharArray();

    public override void SpawnRupee(Vector2 position)
    {
        var newRupee = new Rupee();
        var sRPosX = position.X;
        var sRPosY = position.Y;

        var value = 0;
        for (var i = 0; i < 3; i++)
        {
            for (var j = 0; j < 3; j++)
            {
                newRupee.MapStorage[value] = Map[sRPosX - 1 + j, sRPosY - 1 + i] is not '-' and not 'S'
                    ? Map[sRPosX - 1 + j, sRPosY - 1 + i]
                    : ' ';
                value++;
            }
        }
        newRupee.Position = new(sRPosX, sRPosY);
        EnemyManager.AddRupee(newRupee);

        Map[sRPosX - 1, sRPosY] = 'R';
        Map[sRPosX + 1, sRPosY] = 'R';
        Map[sRPosX, sRPosY - 1] = 'r';
        Map[sRPosX, sRPosY + 1] = 'r';

        UpdateRow(sRPosY - 1);
        UpdateRow(sRPosY);
        UpdateRow(sRPosY + 1);
    }

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

    public override void Move()
    {
        var rnd1 = Random.Shared.Next(10);
        var passed = rnd1 <= 2;
        var newPosition = DirectionToOffset(Prev1);

        if (!passed)
            passed = !TryMove(newPosition, Prev1, -1);

        if (!passed)
            return;

        var randomDirection = Random.Shared.RandomEnum<DirectionType>();
        newPosition = DirectionToOffset(randomDirection);

        TryMove(newPosition, randomDirection, -1);
    }

    private Vector2 DirectionToOffset(DirectionType type) => type switch
    {
        DirectionType.Up => Position.Offset(y: -1),
        DirectionType.Left => Position.Offset(x: -2),
        DirectionType.Down => Position.Offset(y: 1),
        DirectionType.Right => Position.Offset(x: 2),
        _ => throw new NotSupportedException()
    };
}

