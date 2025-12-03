namespace _0_Bit_Legend.Entities.Enemies;

public class Dragon : BaseEnemy
{
    public Dragon() => Hp = 3;

    public override EnemyType Type => EnemyType.Dragon;
    public override char[] MapStorage { get; } = new string(' ', 84).ToCharArray();

    public override void Clear()
    {
        var value = 0;
        for (var i = 0; i < 7; i++)
        {
            for (var j = 0; j < 12; j++)
            {
                Map[Position.X + j, Position.Y + i] = ' ';
                value++;
            }
        }
    }

    public override void Draw()
    {
        var posX = Position.X;
        var posY = Position.Y;

        var dragon = "<***>        S^SSS>      *S  SS>        =S>        =*SSSS**>   =*SSSSS*     ===  == ";
        if (Prev1 == DirectionType.Down) dragon = "<***>        F^FFF>      *F  FS>        FF>        FF*SSS**>   F**SSSS*     ===  == ";

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
                    PlayerController.Hit();
                }
                Map[posX + j, posY + i] = dragon[value];
                value++;
            }
        }
    }

    public override bool InBounds(Vector2 position) => position.X > 0 && position.Y > 0;

    public override bool IsTouching(char symbol) => false;

    public override bool IsTouching(char[] symbols) => false;
    public override void Move()
    {
        var rnd1 = Random.Shared.Next(10);
        waitDragon = 4;
        EnemyManager.SetMotion(i, EnemyManager.GetMotion(i) - 1);

        var phase = DirectionType.Left;
        var speed = 1;
        if (EnemyManager.GetMotion(i) <= 1)
        {
            phase = DirectionType.Right;
            speed = 0;
            if (EnemyManager.GetMotion(i) <= 0)
            {
                EnemyManager.Move(-1,
                                    EnemyType.Fireball,
                                    EnemyManager.GetPosX(i) - 3,
                                    EnemyManager.GetPosY(i) + 3,
                                    DirectionType.Up,
                                    -1,
                                    true);
                EnemyManager.Move(-1,
                                    EnemyType.Fireball,
                                    EnemyManager.GetPosX(i) - 3,
                                    EnemyManager.GetPosY(i) + 1,
                                    DirectionType.Left,
                                    -1,
                                    true);
                EnemyManager.Move(-1,
                                    EnemyType.Fireball,
                                    EnemyManager.GetPosX(i) - 3,
                                    EnemyManager.GetPosY(i) - 1,
                                    DirectionType.Down,
                                    -1,
                                    true);
                EnemyManager.SetMotion(i, 12);
            }
        }

        if (EnemyManager.GetPosY(i) <= 7)
        {
            EnemyManager.Move(i,
                                EnemyManager.GetEnemyType(i),
                                EnemyManager.GetPosX(i),
                                EnemyManager.GetPosY(i) + speed,
                                phase,
                                -1,
                                false);
        }
        else if (EnemyManager.GetPosY(i) >= 19)
        {
            EnemyManager.Move(
                i,
                EnemyManager.GetEnemyType(i),
                EnemyManager.GetPosX(i),
                EnemyManager.GetPosY(i) - speed,
                phase,
                -1,
                false);
        }
        else
        {
            if (rnd1 <= 4)
            {
                EnemyManager.Move(i,
                                    EnemyManager.GetEnemyType(i),
                                    EnemyManager.GetPosX(i),
                                    EnemyManager.GetPosY(i) + speed,
                                    phase,
                                    -1,
                                    false);
            }
            else
            {
                EnemyManager.Move(i,
                                    EnemyManager.GetEnemyType(i),
                                    EnemyManager.GetPosX(i),
                                    EnemyManager.GetPosY(i) - speed,
                                    phase,
                                    -1,
                                    false);
            }
        }
    }
    public override void TakeDamage()
    {
        waitDragon++;

        var value = 0;
        const string dragon = "*****        ******      **  ***        ***        *********   ********     ***  ** ";
        for (var i = 0; i < 7; i++)
        {
            for (var j = 0; j < 12; j++)
            {
                Map[Position.X + j, Position.Y + i] = dragon[value];
                value++;
            }
        }

        base.TakeDamage();
    }

    public override void Die()
    {
        base.Die();

        SetFlag(GameFlag.Dragon, true);
        LoadMap(12, PlayerController.Position.X, PlayerController.Position.Y, PlayerController.GetPrev());
    }
}
