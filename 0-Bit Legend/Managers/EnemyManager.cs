using _0_Bit_Legend.Entities.Enemies;

namespace _0_Bit_Legend.Managers;

public class EnemyManager
{
    private readonly List<IEnemy> _enemies = [];

    // Rupee
    private readonly List<Vector2> _rupeePositions = [];
    private readonly List<char[]> _rupee_storage = [];

    private Vector2 _storedRupeePosition = Vector2.Zero;
    public void SetStoredRupeePosition(Vector2 storedRupeePosition) => _storedRupeePosition = storedRupeePosition;

    public int GetPosX(int index) => _enemies[index].Position.X;
    public int GetPosY(int index) => _enemies[index].Position.Y;

    public IEnemy GetEnemy(int index) => _enemies[index];
    public EnemyType GetEnemyType(int index) => _enemies[index].Type;

    public DirectionType GetPrev1(int index) => _enemies[index].Prev1;

    public DirectionType GetPrev2(int index) => _enemies[index].Prev2;

    public int GetMotion(int index) => _enemies[index].Motion;

    public void SetMotion(int index, int value) => _enemies[index].Motion = value;

    public bool TakeDamage(Vector2 target, DirectionType prev)
    {
        var enemy = GetEnemyAt(target);
        MainProgram.PlayerController.StoreSword(prev);

        enemy.TakeDamage();

        return true;
    }

    public void SpawnEnemy(EnemyType type, Vector2 position, DirectionType direction, int motion)
    {
        IEnemy enemy = type switch
        {
            EnemyType.Octorok => new Octorok(),
            EnemyType.Spider => new Spider(),
            EnemyType.Bat => new Bat(),
            EnemyType.Dragon => new Dragon(),
            EnemyType.Fireball => new Fireball(),
            _ => throw new ArgumentOutOfRangeException(nameof(type), "Invalid enemy type")
        };

        enemy.Position = position;
        enemy.Direction = direction;
        enemy.Motion = motion;
        enemy.Prev1 = direction;
        enemy.Prev2 = direction;

        _enemies.Add(enemy);
    }

    public void RemoveAll() => _enemies.Clear();
    public void MoveAll()
    {
        foreach (var enemy in _enemies)
            enemy.Move();
    }

    public void Store(IEnemy enemy)
    {
        enemy.Clear();
        var type = enemy.Type;
        var posX = enemy.Position.X;
        var posY = enemy.Position.Y;

        if (type == EnemyType.Octorok)
        {
            var value = 0;
            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 4; j++)
                {
                    enemy.MapStorage[value] = Map[posX + j, posY + i];
                    value++;
                }
            }
        }
        else if (type == EnemyType.Spider)
        {
            var value = 0;
            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    enemy.MapStorage[value] = Map[posX + j, posY + i];
                    value++;
                }
            }
        }
        else if (type == EnemyType.Bat)
        {
            var value = 0;
            for (var i = 0; i < 2; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    enemy.MapStorage[value] = Map[posX + j, posY + i];
                    value++;
                }
            }
        }
        else if (type == EnemyType.Fireball)
        {
            var value = 0;
            for (var i = 0; i < 2; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    enemy.MapStorage[value] = Map[posX + j, posY + i];
                    value++;
                }
            }
        }

        for (var i = 0; i < enemy.MapStorage.Length; i++)
        {
            if (enemy.MapStorage[i] is '*' or 'F' or 's' or '-' or '/' or '\\' or '|' or '^' or '#' or 'r' or 'R' or 'V')
            {
                enemy.MapStorage[i] = ' ';
            }
        }

        UpdateRow(posY);
        UpdateRow(posY + 1);
        UpdateRow(posY + 2);

        if (type == EnemyType.Dragon)
        {
            UpdateRow(posY + 3);
            UpdateRow(posY + 4);
            UpdateRow(posY + 5);
            UpdateRow(posY  + 6);
        }
    }

    public void Remove(IEnemy enemy)
    {
        enemy.Clear();
        var type = enemy.Type;

        UpdateRow(enemy.Position.Y);
        UpdateRow(enemy.Position.Y + 1);
        UpdateRow(enemy.Position.Y + 2);

        if (type == EnemyType.Dragon)
        {
            UpdateRow(enemy.Position.Y + 3);
            UpdateRow(enemy.Position.Y + 4);
            UpdateRow(enemy.Position.Y + 5);
            UpdateRow(enemy.Position.Y + 6);
        }


        if (type == EnemyType.Bat)
        {
            if (CurrentMap == 10)
            {
                cEnemies1--;
            }
            else if (CurrentMap == 11)
            {
                cEnemies2--;
            }
        }
    }

    //TODO => Move to Die in enemy and handle random chance there

    //public void SpawnRupee()
    //{
    //    if (_sRType != EnemyType.Dragon && _sRType != EnemyType.Bat && Random.Shared.Next(2) == 1)
    //    {
    //        var rupee_storage_copy = new char[9];
    //        var sRPosX = _storedRupeePosition.X;
    //        var sRPosY = _storedRupeePosition.Y;

    //        var value = 0;
    //        for (var i = 0; i < 3; i++)
    //        {
    //            for (var j = 0; j < 3; j++)
    //            {
    //                rupee_storage_copy[value] = Map[sRPosX - 1 + j, sRPosY - 1 + i] is not '-' and not 'S'
    //                    ? Map[sRPosX - 1 + j, sRPosY - 1 + i]
    //                    : ' ';
    //                value++;
    //            }
    //        }

    //        _rupeePositions.Add(new(sRPosX, sRPosY));
    //        _rupee_storage.Add(rupee_storage_copy);

    //        Map[sRPosX, sRPosY]
    //            = Random.Shared.Next(5) == 4 || (_sRType == EnemyType.Spider && Random.Shared.Next(10) == 9) ? 'V' : 'R';

    //        Map[sRPosX - 1, sRPosY] = 'R';
    //        Map[sRPosX + 1, sRPosY] = 'R';
    //        Map[sRPosX, sRPosY - 1] = 'r';
    //        Map[sRPosX, sRPosY + 1] = 'r';

    //        UpdateRow(sRPosY - 1);
    //        UpdateRow(sRPosY);
    //        UpdateRow(sRPosY + 1);
    //    }
    //}

    public void RemoveRupee(int posX, int posY)
    {
        //Always reverse order for removing from lists.
        //Bottom up so when the reorganize, you don't break things in the middle of a loop.
        for (var i = _rupeePositions.Count - 1; i >= 0; i--) 
        {
            var position = _rupeePositions[i];
            var X = position.X;
            var Y = position.Y;
            if (posX >= X - 1 && posX <= X + 1 && posY >= Y - 1 && posY <= Y + 1)
            {
                if (Map[X, Y] == 'V')
                {
                    Rupees += 5;
                }
                else
                {
                    Rupees++;
                }

                Map[X - 1, Y - 1] = _rupee_storage[i][0];
                Map[X + 0, Y - 1] = _rupee_storage[i][1];
                Map[X + 1, Y - 1] = _rupee_storage[i][2];

                Map[X - 1, Y ] = _rupee_storage[i][3];
                Map[X + 0, Y ] = _rupee_storage[i][4];
                Map[X + 1, Y ] = _rupee_storage[i][5];

                Map[X - 1, Y + 1] = _rupee_storage[i][6];
                Map[X + 0, Y + 1] = _rupee_storage[i][7];
                Map[X + 1, Y + 1] = _rupee_storage[i][8];

                UpdateRow(Y - 1);
                UpdateRow(Y);
                UpdateRow(Y + 1);

                _rupeePositions.RemoveAt(i);
                _rupee_storage.RemoveAt(i);
            }
        }
    }

    public IEnemy GetEnemyAt(Vector2 target)
    {
        var posX = target.X;
        var posY = target.Y;

        for (var i = 0; i < _enemies.Count; i++)
        {
            var inPosX = 0;
            var inPosY = 0;
            if (_enemies[i].Type == EnemyType.Octorok)
            {
                inPosX = 4;
                inPosY = 3;
            }
            else if (_enemies[i].Type == EnemyType.Spider)
            {
                inPosX = 5;
                inPosY = 3;
            }
            else if (_enemies[i].Type == EnemyType.Bat)
            {
                inPosX = 5;
                inPosY = 2;
            }
            else if (_enemies[i].Type == EnemyType.Dragon)
            {
                inPosX = 12;
                inPosY = 7;
            }
            else if (_enemies[i].Type == EnemyType.Fireball)
            {
                inPosX = 3;
                inPosY = 2;
            }

            var position = _enemies[i].Position;

            if (posX >= position.X && posX <= position.X + inPosX && posY >= position.Y && posY <= position.Y + inPosY)
            {
                return _enemies[i];
            }
        }
        return _enemies[0];
    }
}