using _0_Bit_Legend.Enemies;
using _0_Bit_Legend.Model;
using static _0_Bit_Legend.MainProgram;

namespace _0_Bit_Legend;

public class EnemyManager
{
    // Enemy
    private readonly List<int> _posX = [];
    private readonly List<int> _posY = [];

    private readonly List<char[]> _map_storage = [];

    private readonly List<Direction> _prev1 = [];
    private readonly List<Direction> _prev2 = [];

    private readonly List<EnemyType> _type = [];
    private readonly List<int> _hp = [];

    private readonly List<int> _motion = [];

    // Rupee
    private readonly List<int> _rPosX = [];
    private readonly List<int> _rPosY = [];
    private readonly List<char[]> _rupee_storage = [];

    private int _sRPosX;
    private int _sRPosY;
    private EnemyType _sRType = EnemyType.None;

    //    Methods    //
    public int GetPosX(int index) => _posX[index];

    public int GetPosY(int index) => _posY[index];

    public EnemyType GetEnemyType(int index) => _type[index];

    public int GetTotal() => _posX.ToArray().Length;

    public Direction GetPrev1(int index) => _prev1[index];

    public Direction GetPrev2(int index) => _prev2[index];

    public int GetMotion(int index) => _motion[index];

    public void SetMotion(int index, int value) => _motion[index] = value;


    public bool TakeDamage(int posX, int posY, Direction prev, int _)
    {
        var index = GetIndex(posX, posY);
        MainProgram.PlayerController.StoreSword(prev);

        if (index == -1 || _type[index] == EnemyType.Fireball)
        {
            return false;
        }

        if (_type[index] == EnemyType.Dragon)
        {
            waitDragon++;

            var value = 0;
            const string dragon = "*****        ******      **  ***        ***        *********   ********     ***  ** ";
            for (var i = 0; i < 7; i++)
            {
                for (var j = 0; j < 12; j++)
                {
                    Map[GetPosX(index) + j, GetPosY(index) + i] = dragon[value];
                    value++;
                }
            }
        }

        _hp[index]--;
        if (_hp[index] <= 0)
        {
            _sRPosX = _posX[index] + 2;
            _sRPosY = _posY[index] + 1;
            _sRType = _type[index];

            Remove(index, _type[index]);

            MainProgram.PlayerController.SetSpawnRupee(true);

            if (_sRType == EnemyType.Dragon)
            {
                SetFlag(Model.GameFlag.Dragon, true);
                LoadMap(12, MainProgram.PlayerController.PosX, MainProgram.PlayerController.PosY, MainProgram.PlayerController.GetPrev());
            }
        }
        return true;
    }

    public bool Move(int index, EnemyType type, int posX, int posY, Direction direction, int motion, bool spawn)
    {
        if (index == -1)
        {
            index = GetTotal();
        }

        IEnemy? enemy = type switch
        {
            EnemyType.Octorok => new Octorok(),
            EnemyType.Spider => new Spider(),
            EnemyType.Bat => new Bat(),
            EnemyType.Dragon => new Dragon(),
            EnemyType.Fireball => new Fireball(),
            _ => null
        };
        if(enemy is null)
            throw new ArgumentOutOfRangeException(nameof(type), "Invalid enemy type");

        if (spawn)
        {
            _posX.Add(posX);
            _posY.Add(posY);

            _prev1.Add(direction);
            _prev2.Add(direction);

            _type.Add(type);
            _hp.Add(1);

            _motion.Add(motion);

            char[] storage_copy;
            if (type == EnemyType.Octorok)
            {
                storage_copy = new char[12];
            }
            else if (type == EnemyType.Spider)
            {
                storage_copy = new char[15];
            }
            else if (type == EnemyType.Bat)
            {
                storage_copy = new char[10];
            }
            else if (type == EnemyType.Dragon)
            {
                storage_copy = new char[84];
                _hp[GetTotal() - 1] = 3;
            }
            else
            {
                storage_copy = type == EnemyType.Fireball ? (new char[6]) : (new char[12]);
            }

            for (var i = 0; i < storage_copy.Length; i++)
            {
                storage_copy[i] = ' ';
            }

            _map_storage.Add(storage_copy);
        }

        if (InBounds(type, posX, posY))
        {
            var blocking = new char[] { '=', 'X', 't', 'n', 'B', '{', '}', '|', '/', '\\', '_', '~' };
            var blocking2 = new char[] { '|', '_', '\\' };
            Clear(index, type);
            if (type == EnemyType.Dragon || type == EnemyType.Spider || type == EnemyType.Bat || (!enemy.IsTouching(posX, posY, blocking)))
            {
                _prev1[index] = direction;

                if (type == EnemyType.Octorok)
                {
                    if (direction is Direction.Left or Direction.Right)
                    {
                        _prev2[index] = direction;
                    }
                }
                else if (type == EnemyType.Spider)
                {
                    if (direction is Direction.Up or Direction.Down)
                    {
                        _prev2[index] = Direction.Left;
                    }
                    else if (direction is Direction.Left or Direction.Right)
                    {
                        _prev2[index] = Direction.Right;
                    }
                }
                else if (type == EnemyType.Bat)
                {
                    if (_prev2[index] == Direction.Right)
                    {
                        _prev2[index] = Direction.Left;
                    }
                    else if (_prev2[index] == Direction.Left)
                    {
                        _prev2[index] = Direction.Right;
                    }
                }

                Store(index, type, posX, posY);
                Build(index, enemy, posX, posY);

                UpdateRow(posY);
                UpdateRow(posY + 1);
                UpdateRow(posY + 2);

                if (type == EnemyType.Dragon)
                {
                    UpdateRow(posY + 3);
                    UpdateRow(posY + 4);
                    UpdateRow(posY + 5);
                    UpdateRow(posY + 6);
                }

                _posX[index] = posX;
                _posY[index] = posY;

                return true;
            }
            else if (enemy.IsTouching(posX, posY, blocking2))
            {
                MainProgram.PlayerController.Hit();
                if (type == EnemyType.Fireball)
                {
                    Remove(GetIndex(_posX[index], _posY[index]), type);
                }
                else
                {
                    Build(index, enemy, _posX[index], _posY[index]);
                }
            }
            else
            {
                if (type == EnemyType.Fireball)
                {
                    Remove(GetIndex(_posX[index], _posY[index]), type);
                }
                else
                {
                    Build(index, enemy, _posX[index], _posY[index]);
                }
            }
        }
        return false;
    }

    public void Build(int index, IEnemy enemy, int posX, int posY)
    {
        switch (enemy.Type)
        {
            case EnemyType.Octorok:
            case EnemyType.Spider:
            case EnemyType.Bat:
                enemy.Build(posX, posY, _prev2[index]);
                break;
            case EnemyType.Dragon:
                enemy.Build(posX, posY, _prev1[index]);
                break;
            case EnemyType.Fireball:
                enemy.Build(posX, posY, Direction.Up);
                break;
        }
    }

    public void Store(int index, EnemyType type, int posX, int posY)
    {
        Clear(index, type);

        if (type == EnemyType.Octorok)
        {
            var value = 0;
            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 4; j++)
                {
                    _map_storage[index][value] = Map[posX + j, posY + i];
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
                    _map_storage[index][value] = Map[posX + j, posY + i];
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
                    _map_storage[index][value] = Map[posX + j, posY + i];
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
                    _map_storage[index][value] = Map[posX + j, posY + i];
                    value++;
                }
            }
        }

        for (var i = 0; i < _map_storage[index].Length; i++)
        {
            if (_map_storage[index][i] is '*' or 'F' or 's' or '-' or '/' or '\\' or '|' or '^' or '#' or 'r' or 'R' or 'V')
            {
                _map_storage[index][i] = ' ';
            }
        }

        UpdateRow(_posY[index]);
        UpdateRow(_posY[index] + 1);
        UpdateRow(_posY[index] + 2);

        if (type == EnemyType.Dragon)
        {
            UpdateRow(_posY[index] + 3);
            UpdateRow(_posY[index] + 4);
            UpdateRow(_posY[index] + 5);
            UpdateRow(_posY[index] + 6);
        }
    }

    public void Clear(int index, EnemyType type)
    {
        if (type == EnemyType.Octorok)
        {
            Map[_posX[index] + 0, _posY[index]] = _map_storage[index][0];
            Map[_posX[index] + 1, _posY[index]] = _map_storage[index][1];
            Map[_posX[index] + 2, _posY[index]] = _map_storage[index][2];
            Map[_posX[index] + 3, _posY[index]] = _map_storage[index][3];

            Map[_posX[index] + 0, _posY[index] + 1] = _map_storage[index][4];
            Map[_posX[index] + 1, _posY[index] + 1] = _map_storage[index][5];
            Map[_posX[index] + 2, _posY[index] + 1] = _map_storage[index][6];
            Map[_posX[index] + 3, _posY[index] + 1] = _map_storage[index][7];

            Map[_posX[index] + 0, _posY[index] + 2] = _map_storage[index][8];
            Map[_posX[index] + 1, _posY[index] + 2] = _map_storage[index][9];
            Map[_posX[index] + 2, _posY[index] + 2] = _map_storage[index][10];
            Map[_posX[index] + 3, _posY[index] + 2] = _map_storage[index][11];
        }
        else if (type == EnemyType.Spider)
        {
            Map[_posX[index] + 0, _posY[index]] = _map_storage[index][0];
            Map[_posX[index] + 1, _posY[index]] = _map_storage[index][1];
            Map[_posX[index] + 2, _posY[index]] = _map_storage[index][2];
            Map[_posX[index] + 3, _posY[index]] = _map_storage[index][3];
            Map[_posX[index] + 4, _posY[index]] = _map_storage[index][4];

            Map[_posX[index] + 0, _posY[index] + 1] = _map_storage[index][5];
            Map[_posX[index] + 1, _posY[index] + 1] = _map_storage[index][6];
            Map[_posX[index] + 2, _posY[index] + 1] = _map_storage[index][7];
            Map[_posX[index] + 3, _posY[index] + 1] = _map_storage[index][8];
            Map[_posX[index] + 4, _posY[index] + 1] = _map_storage[index][9];

            Map[_posX[index] + 0, _posY[index] + 2] = _map_storage[index][10];
            Map[_posX[index] + 1, _posY[index] + 2] = _map_storage[index][11];
            Map[_posX[index] + 2, _posY[index] + 2] = _map_storage[index][12];
            Map[_posX[index] + 3, _posY[index] + 2] = _map_storage[index][13];
            Map[_posX[index] + 4, _posY[index] + 2] = _map_storage[index][14];
        }
        else if (type == EnemyType.Bat)
        {
            Map[_posX[index] + 0, _posY[index]] = _map_storage[index][0];
            Map[_posX[index] + 1, _posY[index]] = _map_storage[index][1];
            Map[_posX[index] + 2, _posY[index]] = _map_storage[index][2];
            Map[_posX[index] + 3, _posY[index]] = _map_storage[index][3];
            Map[_posX[index] + 4, _posY[index]] = _map_storage[index][4];

            Map[_posX[index] + 0, _posY[index] + 1] = _map_storage[index][5];
            Map[_posX[index] + 1, _posY[index] + 1] = _map_storage[index][6];
            Map[_posX[index] + 2, _posY[index] + 1] = _map_storage[index][7];
            Map[_posX[index] + 3, _posY[index] + 1] = _map_storage[index][8];
            Map[_posX[index] + 4, _posY[index] + 1] = _map_storage[index][9];
        }
        else if (type == EnemyType.Dragon)
        {
            var value = 0;
            for (var i = 0; i < 7; i++)
            {
                for (var j = 0; j < 12; j++)
                {
                    Map[_posX[index] + j, _posY[index] + i] = ' ';
                    value++;
                }
            }
        }
        else if (type == EnemyType.Fireball)
        {
            Map[_posX[index] + 0, _posY[index]] = _map_storage[index][0];
            Map[_posX[index] + 1, _posY[index]] = _map_storage[index][1];
            Map[_posX[index] + 2, _posY[index]] = _map_storage[index][2];

            Map[_posX[index] + 0, _posY[index] + 1] = _map_storage[index][3];
            Map[_posX[index] + 1, _posY[index] + 1] = _map_storage[index][4];
            Map[_posX[index] + 2, _posY[index] + 1] = _map_storage[index][5];
        }
    }

    public void Remove(int index, EnemyType type)
    {
        Clear(index, type);

        UpdateRow(_posY[index]);
        UpdateRow(_posY[index] + 1);
        UpdateRow(_posY[index] + 2);

        if (type == EnemyType.Dragon)
        {
            UpdateRow(_posY[index] + 3);
            UpdateRow(_posY[index] + 4);
            UpdateRow(_posY[index] + 5);
            UpdateRow(_posY[index] + 6);
        }

        _posX.RemoveAt(index);
        _posY.RemoveAt(index);
        _type.RemoveAt(index);
        _prev1.RemoveAt(index);
        _prev2.RemoveAt(index);
        _hp.RemoveAt(index);
        _motion.RemoveAt(index);
        _map_storage.RemoveAt(index);

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

    public void SpawnRupee()
    {
        if (_sRType != EnemyType.Dragon && _sRType != EnemyType.Bat && Random.Shared.Next(2) == 1)
        {
            var rupee_storage_copy = new char[9];

            var value = 0;
            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    rupee_storage_copy[value] = Map[_sRPosX - 1 + j, _sRPosY - 1 + i] is not '-' and not 'S'
                        ? Map[_sRPosX - 1 + j, _sRPosY - 1 + i]
                        : ' ';
                    value++;
                }
            }

            _rPosX.Add(_sRPosX);
            _rPosY.Add(_sRPosY);
            _rupee_storage.Add(rupee_storage_copy);

            Map[_sRPosX, _sRPosY]
                = Random.Shared.Next(5) == 4 || (_sRType == EnemyType.Spider && Random.Shared.Next(10) == 9) ? 'V' : 'R';

            Map[_sRPosX - 1, _sRPosY] = 'R';
            Map[_sRPosX + 1, _sRPosY] = 'R';
            Map[_sRPosX, _sRPosY - 1] = 'r';
            Map[_sRPosX, _sRPosY + 1] = 'r';

            UpdateRow(_sRPosY - 1);
            UpdateRow(_sRPosY);
            UpdateRow(_sRPosY + 1);
        }
    }

    public void RemoveRupee(int posX, int posY)
    {
        for (var i = 0; i < _rPosX.ToArray().Length; i++)
        {
            if (posX >= _rPosX[i] - 1 && posX <= _rPosX[i] + 1 && posY >= _rPosY[i] - 1 && posY <= _rPosY[i] + 1)
            {
                if (Map[_rPosX[i], _rPosY[i]] == 'V')
                {
                    Rupees += 5;
                }
                else
                {
                    Rupees++;
                }

                Map[_rPosX[i] - 1, _rPosY[i] - 1] = _rupee_storage[i][0];
                Map[_rPosX[i] + 0, _rPosY[i] - 1] = _rupee_storage[i][1];
                Map[_rPosX[i] + 1, _rPosY[i] - 1] = _rupee_storage[i][2];

                Map[_rPosX[i] - 1, _rPosY[i]] = _rupee_storage[i][3];
                Map[_rPosX[i] + 0, _rPosY[i]] = _rupee_storage[i][4];
                Map[_rPosX[i] + 1, _rPosY[i]] = _rupee_storage[i][5];

                Map[_rPosX[i] - 1, _rPosY[i] + 1] = _rupee_storage[i][6];
                Map[_rPosX[i] + 0, _rPosY[i] + 1] = _rupee_storage[i][7];
                Map[_rPosX[i] + 1, _rPosY[i] + 1] = _rupee_storage[i][8];

                UpdateRow(_rPosY[i] - 1);
                UpdateRow(_rPosY[i]);
                UpdateRow(_rPosY[i] + 1);

                _rPosX.RemoveAt(i);
                _rPosY.RemoveAt(i);
                _rupee_storage.RemoveAt(i);
            }
        }
    }

    public bool InBounds(EnemyType type, int posX, int posY)
    {
        var inPosX = 0;
        var inPosY = 0;
        if (type == EnemyType.Octorok)
        {
            inPosX = posX + 3;
            inPosY = posY + 2;
        }
        else if (type == EnemyType.Spider)
        {
            inPosX = posX + 4;
            inPosY = posY + 2;
        }
        else if (type == EnemyType.Bat)
        {
            inPosX = posX + 4;
            inPosY = posY + 1;
        }
        else if (type == EnemyType.Fireball)
        {
            inPosX = posX + 3;
            inPosY = posY + 1;
        }

        return posX > 0 && inPosX < 102 && posY > 0 && inPosY < 33;
    }

    public int GetIndex(int posX, int posY)
    {
        for (var i = 0; i < GetTotal(); i++)
        {
            var inPosX = 0;
            var inPosY = 0;
            if (_type[i] == EnemyType.Octorok)
            {
                inPosX = 4;
                inPosY = 3;
            }
            else if (_type[i] == EnemyType.Spider)
            {
                inPosX = 5;
                inPosY = 3;
            }
            else if (_type[i] == EnemyType.Bat)
            {
                inPosX = 5;
                inPosY = 2;
            }
            else if (_type[i] == EnemyType.Dragon)
            {
                inPosX = 12;
                inPosY = 7;
            }
            else if (_type[i] == EnemyType.Fireball)
            {
                inPosX = 3;
                inPosY = 2;
            }

            if (posX >= _posX[i] && posX <= _posX[i] + inPosX && posY >= _posY[i] && posY <= _posY[i] + inPosY)
            {
                return i;
            }
        }
        return -1;
    }
}