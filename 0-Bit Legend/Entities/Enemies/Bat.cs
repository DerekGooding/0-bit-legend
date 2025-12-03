namespace _0_Bit_Legend.Entities.Enemies;

public class Bat : BaseEnemy
{
    public override EnemyType Type { get; } = EnemyType.Bat;
    public override char[] MapStorage { get; } = new string(' ', 10).ToCharArray();

    public override void Clear()
    {
        Map[Position.X + 0, Position.Y] = MapStorage[0];
        Map[Position.X + 1, Position.Y] = MapStorage[1];
        Map[Position.X + 2, Position.Y] = MapStorage[2];
        Map[Position.X + 3, Position.Y] = MapStorage[3];
        Map[Position.X + 4, Position.Y] = MapStorage[4];

        Map[Position.X + 0, Position.Y + 1] = MapStorage[5];
        Map[Position.X + 1, Position.Y + 1] = MapStorage[6];
        Map[Position.X + 2, Position.Y + 1] = MapStorage[7];
        Map[Position.X + 3, Position.Y + 1] = MapStorage[8];
        Map[Position.X + 4, Position.Y + 1] = MapStorage[9];
    }

    public override void Draw()
    {
        var posX = Position.X;
        var posY = Position.Y;

        if (Prev2 == DirectionType.Left)
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

    public override bool InBounds(Vector2 position)
    {
        var inPosX = position.X + 4;
        var inPosY = position.Y + 1;

        return position.X > 0 && inPosX < 102 && position.Y > 0 && inPosY < 33;
    }

    public override bool IsTouching(char symbol)
    {
        var posX = Position.X;
        var posY = Position.Y;

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

    public override bool IsTouching(char[] symbols)
    {
        var posX = Position.X;
        var posY = Position.Y;

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

    public override void Move()
    {
        var passed = false;
        var rnd1 = Random.Shared.Next(10);
        if (rnd1 > 4)
        {
            if (EnemyManager.GetPrev1(i) == DirectionType.Up)
            {
                passed = !EnemyManager.Move(i,
                                                EnemyManager.GetEnemyType(i),
                                                EnemyManager.GetPosX(i) - 2,
                                                EnemyManager.GetPosY(i) - 1,
                                                DirectionType.Up,
                                                -1,
                                                false);
            }
            else if (EnemyManager.GetPrev1(i) == DirectionType.Left)
            {
                passed = !EnemyManager.Move(i,
                                                EnemyManager.GetEnemyType(i),
                                                EnemyManager.GetPosX(i) + 2,
                                                EnemyManager.GetPosY(i) - 1,
                                                DirectionType.Left,
                                                -1,
                                                false);
            }
            else if (EnemyManager.GetPrev1(i) == DirectionType.Down)
            {
                passed = !EnemyManager.Move(i,
                                                EnemyManager.GetEnemyType(i),
                                                EnemyManager.GetPosX(i) - 2,
                                                EnemyManager.GetPosY(i) + 1,
                                                DirectionType.Down,
                                                -1,
                                                false);
            }
            else if (EnemyManager.GetPrev1(i) == DirectionType.Right)
            {
                passed = !EnemyManager.Move(i,
                                                EnemyManager.GetEnemyType(i),
                                                EnemyManager.GetPosX(i) + 2,
                                                EnemyManager.GetPosY(i) + 1,
                                                DirectionType.Right,
                                                -1,
                                                false);
            }
        }
        else
        {
            passed = true;
        }

        if (passed)
        {
            var rnd2 = Random.Shared.Next(4) + 1;
            if (rnd2 == 1)
            {
                EnemyManager.Move(i,
                                    EnemyManager.GetEnemyType(i),
                                    EnemyManager.GetPosX(i) - 2,
                                    EnemyManager.GetPosY(i) - 1,
                                    DirectionType.Up,
                                    -1,
                                    false);
            }
            else if (rnd2 == 2)
            {
                EnemyManager.Move(i,
                                    EnemyManager.GetEnemyType(i),
                                    EnemyManager.GetPosX(i) + 2,
                                    EnemyManager.GetPosY(i) - 1,
                                    DirectionType.Left,
                                    -1,
                                    false);
            }
            else if (rnd2 == 3)
            {
                EnemyManager.Move(i,
                                    EnemyManager.GetEnemyType(i),
                                    EnemyManager.GetPosX(i) - 2,
                                    EnemyManager.GetPosY(i) + 1,
                                    DirectionType.Down,
                                    -1,
                                    false);
            }
            else if (rnd2 == 4)
            {
                EnemyManager.Move(i,
                                    EnemyManager.GetEnemyType(i),
                                    EnemyManager.GetPosX(i) + 2,
                                    EnemyManager.GetPosY(i) + 1,
                                    DirectionType.Right,
                                    -1,
                                    false);
            }
        }
    }
}
