using System;

namespace _0_Bit_Legend.Entities.Enemies;

public class Spider : BaseEnemy
{
    public override EnemyType Type => EnemyType.Spider;
    public override char[] MapStorage { get; } = new string(' ', 15).ToCharArray();

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

        Map[Position.X + 0, Position.Y + 2] = MapStorage[10];
        Map[Position.X + 1, Position.Y + 2] = MapStorage[11];
        Map[Position.X + 2, Position.Y + 2] = MapStorage[12];
        Map[Position.X + 3, Position.Y + 2] = MapStorage[13];
        Map[Position.X + 4, Position.Y + 2] = MapStorage[14];
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

    public override bool InBounds(Vector2 position)
    {
        var inPosX = position.X + 4;
        var inPosY = position.Y + 2;

        return position.X > 0 && inPosX < 102 && position.Y > 0 && inPosY < 33;
    }

    public override bool IsTouching(char symbol)
    {
        var posX = Position.X;
        var posY = Position.Y;

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

    public override bool IsTouching(char[] symbols)
    {
        var posX = Position.X;
        var posY = Position.Y;

        for (var i = 0; i < 5; i++)
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
        var passed = false;
        var rnd1 = Random.Shared.Next(10);
        EnemyManager.SetMotion(i, EnemyManager.GetMotion(i) - 1);
        if (EnemyManager.GetMotion(i) > 0)
        {
            if (rnd1 > 2)
            {
                if (EnemyManager.GetPrev1(i) == DirectionType.Up)
                {
                    var newPosition = enemy.Position.Offset(-2, -1);
                    passed = !EnemyManager.Move(enemy, newPosition, DirectionType.Up, -1);
                }
                else if (EnemyManager.GetPrev1(i) == DirectionType.Left)
                {
                    var newPosition = enemy.Position.Offset(2, -1);
                    passed = !EnemyManager.Move(enemy, newPosition, DirectionType.Left, -1);
                }
                else if (EnemyManager.GetPrev1(i) == DirectionType.Down)
                {
                    var newPosition = enemy.Position.Offset(-2 + 1);
                    passed = !EnemyManager.Move(enemy, newPosition, DirectionType.Down, -1);
                }
                else if (EnemyManager.GetPrev1(i) == DirectionType.Right)
                {
                    var newPosition = enemy.Position.Offset(2, 1);
                    passed = !EnemyManager.Move(enemy, newPosition, DirectionType.Right, -1);
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
        else if (enemy.Motion <= -5)
        {
            enemy.Motion = 10;
        }
    }
}
