namespace _0_Bit_Legend;

public class EnemyMovement : MainProgram
{
    // Enemy
    private readonly List<int> _posX = [];
    private readonly List<int> _posY = [];

    private readonly List<string[]> _map_storage = [];

    private readonly List<string> _prev1 = [];
    private readonly List<string> _prev2 = [];

    private readonly List<string> _type = [];
    private readonly List<int> _hp = [];

    private readonly List<int> _motion = [];

    // Rupee
    private readonly List<int> _rPosX = [];
    private readonly List<int> _rPosY = [];
    private readonly List<string[]> _rupee_storage = [];

    private int _sRPosX;
    private int _sRPosY;
    private string _sRType = "";

    //    Methods    //
    public int GetPosX(int index) => _posX[index];

    public int GetPosY(int index) => _posY[index];

    public string GetEnemyType(int index) => _type[index];

    public int GetTotal() => _posX.ToArray().Length;

    public string GetPrev1(int index) => _prev1[index];

    public string GetPrev2(int index) => _prev2[index];

    public int GetMotion(int index) => _motion[index];

    public void SetMotion(int index, int value) => _motion[index] = value;


    public bool TakeDamage(int posX, int posY, string prev, int damage)
    {
        int index = GetIndex(posX, posY);
        linkMovement.StoreSword(prev);

        if (index == -1 || _type[index] == "fireball")
        {
            return false;
        }

        if (_type[index] == "dragon")
        {
            waitDragon++;

            int value = 0;
            string dragon = "*****        ******      **  ***        ***        *********   ********     ***  ** ";
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    map[GetPosX(index) + j, GetPosY(index) + i] = dragon[value].ToString();
                    value++;
                }
            }
        }

        _hp[index] -= 1;
        if (_hp[index] <= 0)
        {
            _sRPosX = _posX[index] + 2;
            _sRPosY = _posY[index] + 1;
            _sRType = _type[index];

            Remove(index, _type[index]);

            linkMovement.SetSpawnRupee(true);

            if (_sRType == "dragon")
            {
                cDragon = true;
                LoadMap(12, linkMovement.GetPosX(), linkMovement.GetPosY(), linkMovement.GetPrev());
            }
        }
        return true;
    }

    public bool Move(int index, string type, int posX, int posY, string direction, int motion, bool spawn)
    {
        if (index == -1)
        {
            index = GetTotal();
        }

        if (spawn)
        {
            _posX.Add(posX);
            _posY.Add(posY);

            _prev1.Add(direction);
            _prev2.Add(direction);

            _type.Add(type);
            _hp.Add(1);

            _motion.Add(motion);

            string[] storage_copy;
            if (type == "octorok")
            {
                storage_copy = new string[12];
            }
            else if (type == "spider")
            {
                storage_copy = new string[15];
            }
            else if (type == "bat")
            {
                storage_copy = new string[10];
            }
            else if (type == "dragon")
            {
                storage_copy = new string[84];
                _hp[GetTotal() - 1] = 3;
            }
            else if (type == "fireball")
            {
                storage_copy = new string[6];
            }
            else
            {
                storage_copy = new string[12];
            }

            for (int i = 0; i < storage_copy.Length; i++)
            {
                storage_copy[i] = " ";
            }

            _map_storage.Add(storage_copy);
        }

        if (InBounds(type, posX, posY))
        {
            Clear(index, type);
            if (type == "dragon" || (type == "spider" || type == "bat" || (!IsTouching(type, posX, posY, "=") && !IsTouching(type, posX, posY, "X"))) && !IsTouching(type, posX, posY, "t") && !IsTouching(type, posX, posY, "n") && !IsTouching(type, posX, posY, "B") && !IsTouching(type, posX, posY, "{") && !IsTouching(type, posX, posY, "}") && !IsTouching(type, posX, posY, "|") && !IsTouching(type, posX, posY, "/") && !IsTouching(type, posX, posY, "\\") && !IsTouching(type, posX, posY, "_") && !IsTouching(type, posX, posY, "~"))
            {
                _prev1[index] = direction;

                if (type == "octorok")
                {
                    if (direction is "a" or "d")
                    {
                        _prev2[index] = direction;
                    }
                }
                else if (type == "spider")
                {
                    if (direction is "w" or "s")
                    {
                        _prev2[index] = "a";
                    }
                    else if (direction is "a" or "d")
                    {
                        _prev2[index] = "d";
                    }
                }
                else if (type == "bat")
                {
                    if (_prev2[index] == "d")
                    {
                        _prev2[index] = "a";
                    }
                    else if (_prev2[index] == "a")
                    {
                        _prev2[index] = "d";
                    }
                }

                Store(index, type, posX, posY);
                Build(index, type, posX, posY);

                UpdateRow(posY);
                UpdateRow(posY + 1);
                UpdateRow(posY + 2);

                if (type == "dragon")
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
            else if (IsTouching(type, posX, posY, "|") || IsTouching(type, posX, posY, "_") || IsTouching(type, posX, posY, "\\"))
            {
                linkMovement.Hit();
                if (type == "fireball")
                {
                    Remove(GetIndex(_posX[index], _posY[index]), type);
                }
                else
                {
                    Build(index, type, _posX[index], _posY[index]);
                }
            }
            else
            {
                if (type == "fireball")
                {
                    Remove(GetIndex(_posX[index], _posY[index]), type);
                }
                else
                {
                    Build(index, type, _posX[index], _posY[index]);
                }
            }
        }
        return false;
    }

    public void Build(int index, string type, int posX, int posY)
    {
        if (_prev2[index] == "a")
        {
            if (type == "octorok")
            {
                map[posX + 0, posY] = " ";
                map[posX + 1, posY] = "t";
                map[posX + 2, posY] = "t";
                map[posX + 3, posY] = "t";

                map[posX + 0, posY + 1] = "t";
                map[posX + 1, posY + 1] = "^";
                map[posX + 2, posY + 1] = "t";
                map[posX + 3, posY + 1] = "t";

                map[posX + 0, posY + 2] = "t";
                map[posX + 1, posY + 2] = "t";
                map[posX + 2, posY + 2] = "t";
                map[posX + 3, posY + 2] = "t";
            }
            else if (type == "spider")
            {
                map[posX + 0, posY] = " ";
                map[posX + 1, posY] = "t";
                map[posX + 2, posY] = "t";
                map[posX + 3, posY] = "t";
                map[posX + 4, posY] = " ";

                map[posX + 0, posY + 1] = "n";
                map[posX + 1, posY + 1] = "0";
                map[posX + 2, posY + 1] = "0";
                map[posX + 3, posY + 1] = "t";
                map[posX + 4, posY + 1] = "t";

                map[posX + 0, posY + 2] = " ";
                map[posX + 1, posY + 2] = "n";
                map[posX + 2, posY + 2] = "t";
                map[posX + 4, posY + 2] = " ";
                map[posX + 3, posY + 2] = "n";
            }
            else if (type == "bat")
            {
                map[posX + 0, posY] = "{";
                map[posX + 1, posY] = "t";
                map[posX + 2, posY] = " ";
                map[posX + 3, posY] = "t";
                map[posX + 4, posY] = "}";

                map[posX + 0, posY + 1] = " ";
                map[posX + 1, posY + 1] = " ";
                map[posX + 2, posY + 1] = "B";
                map[posX + 3, posY + 1] = " ";
                map[posX + 4, posY + 1] = " ";
            }
        }
        else
        {
            if (type == "octorok")
            {
                map[posX + 0, posY] = "t";
                map[posX + 1, posY] = "t";
                map[posX + 2, posY] = "t";
                map[posX + 3, posY] = " ";

                map[posX + 0, posY + 1] = "t";
                map[posX + 1, posY + 1] = "t";
                map[posX + 2, posY + 1] = "^";
                map[posX + 3, posY + 1] = "t";

                map[posX + 0, posY + 2] = "t";
                map[posX + 1, posY + 2] = "t";
                map[posX + 2, posY + 2] = "t";
                map[posX + 3, posY + 2] = "t";
            }
            else if (type == "spider")
            {
                map[posX + 0, posY] = " ";
                map[posX + 1, posY] = "t";
                map[posX + 2, posY] = "t";
                map[posX + 3, posY] = "t";
                map[posX + 4, posY] = " ";

                map[posX + 0, posY + 1] = "t";
                map[posX + 1, posY + 1] = "t";
                map[posX + 2, posY + 1] = "0";
                map[posX + 3, posY + 1] = "0";
                map[posX + 4, posY + 1] = "n";

                map[posX + 0, posY + 2] = " ";
                map[posX + 1, posY + 2] = "n";
                map[posX + 2, posY + 2] = "t";
                map[posX + 3, posY + 2] = "n";
                map[posX + 4, posY + 2] = " ";
            }
            else if (type == "bat")
            {
                map[posX + 0, posY] = " ";
                map[posX + 1, posY] = " ";
                map[posX + 2, posY] = "B";
                map[posX + 3, posY] = " ";
                map[posX + 4, posY] = " ";

                map[posX + 0, posY + 1] = "{";
                map[posX + 1, posY + 1] = "t";
                map[posX + 2, posY + 1] = " ";
                map[posX + 3, posY + 1] = "t";
                map[posX + 4, posY + 1] = "}";
            }
        }
        if (type == "dragon")
        {
            string dragon = "<***>        S^SSS>      *S  SS>        =S>        =*SSSS**>   =*SSSSS*     ===  == ";
            if (_prev1[index] == "d") dragon = "<***>        F^FFF>      *F  FS>        FF>        FF*SSS**>   F**SSSS*     ===  == ";

            bool debounce = false;
            int value = 0;
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    if (map[posX + j, posY + i] == "/" || map[posX + j, posY + i] == "\\" || map[posX + j, posY + i] == "|" || map[posX + j, posY + i] == "_" && !debounce)
                    {
                        debounce = true;
                        linkMovement.Hit();
                    }
                    map[posX + j, posY + i] = dragon[value].ToString();
                    value++;
                }
            }
        }
        else if (type == "fireball")
        {
            map[posX + 0, posY] = "F";
            map[posX + 1, posY] = "F";
            map[posX + 2, posY] = "F";

            map[posX + 0, posY + 1] = "F";
            map[posX + 1, posY + 1] = "F";
            map[posX + 2, posY + 1] = "F";
        }
    }

    public void Store(int index, string type, int posX, int posY)
    {
        Clear(index, type);

        if (type == "octorok")
        {
            int value = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    _map_storage[index][value] = map[posX + j, posY + i];
                    value++;
                }
            }
        }
        else if (type == "spider")
        {
            int value = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    _map_storage[index][value] = map[posX + j, posY + i];
                    value++;
                }
            }
        }
        else if (type == "bat")
        {
            int value = 0;
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    _map_storage[index][value] = map[posX + j, posY + i];
                    value++;
                }
            }
        }
        else if (type == "fireball")
        {
            int value = 0;
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    _map_storage[index][value] = map[posX + j, posY + i];
                    value++;
                }
            }
        }

        for (int i = 0; i < _map_storage[index].Length; i++)
        {
            if (_map_storage[index][i] is "*" or "F" or "S" or "-" or "/" or "\\" or "|" or "^" or "#" or "r" or "R" or "V")
            {
                _map_storage[index][i] = " ";
            }
        }

        UpdateRow(_posY[index]);
        UpdateRow(_posY[index] + 1);
        UpdateRow(_posY[index] + 2);

        if (type == "dragon")
        {
            UpdateRow(_posY[index] + 3);
            UpdateRow(_posY[index] + 4);
            UpdateRow(_posY[index] + 5);
            UpdateRow(_posY[index] + 6);
        }
    }

    public void Clear(int index, string type)
    {
        if (type == "octorok")
        {
            map[_posX[index] + 0, _posY[index]] = _map_storage[index][0];
            map[_posX[index] + 1, _posY[index]] = _map_storage[index][1];
            map[_posX[index] + 2, _posY[index]] = _map_storage[index][2];
            map[_posX[index] + 3, _posY[index]] = _map_storage[index][3];

            map[_posX[index] + 0, _posY[index] + 1] = _map_storage[index][4];
            map[_posX[index] + 1, _posY[index] + 1] = _map_storage[index][5];
            map[_posX[index] + 2, _posY[index] + 1] = _map_storage[index][6];
            map[_posX[index] + 3, _posY[index] + 1] = _map_storage[index][7];

            map[_posX[index] + 0, _posY[index] + 2] = _map_storage[index][8];
            map[_posX[index] + 1, _posY[index] + 2] = _map_storage[index][9];
            map[_posX[index] + 2, _posY[index] + 2] = _map_storage[index][10];
            map[_posX[index] + 3, _posY[index] + 2] = _map_storage[index][11];
        }
        else if (type == "spider")
        {
            map[_posX[index] + 0, _posY[index]] = _map_storage[index][0];
            map[_posX[index] + 1, _posY[index]] = _map_storage[index][1];
            map[_posX[index] + 2, _posY[index]] = _map_storage[index][2];
            map[_posX[index] + 3, _posY[index]] = _map_storage[index][3];
            map[_posX[index] + 4, _posY[index]] = _map_storage[index][4];

            map[_posX[index] + 0, _posY[index] + 1] = _map_storage[index][5];
            map[_posX[index] + 1, _posY[index] + 1] = _map_storage[index][6];
            map[_posX[index] + 2, _posY[index] + 1] = _map_storage[index][7];
            map[_posX[index] + 3, _posY[index] + 1] = _map_storage[index][8];
            map[_posX[index] + 4, _posY[index] + 1] = _map_storage[index][9];

            map[_posX[index] + 0, _posY[index] + 2] = _map_storage[index][10];
            map[_posX[index] + 1, _posY[index] + 2] = _map_storage[index][11];
            map[_posX[index] + 2, _posY[index] + 2] = _map_storage[index][12];
            map[_posX[index] + 3, _posY[index] + 2] = _map_storage[index][13];
            map[_posX[index] + 4, _posY[index] + 2] = _map_storage[index][14];
        }
        else if (type == "bat")
        {
            map[_posX[index] + 0, _posY[index]] = _map_storage[index][0];
            map[_posX[index] + 1, _posY[index]] = _map_storage[index][1];
            map[_posX[index] + 2, _posY[index]] = _map_storage[index][2];
            map[_posX[index] + 3, _posY[index]] = _map_storage[index][3];
            map[_posX[index] + 4, _posY[index]] = _map_storage[index][4];

            map[_posX[index] + 0, _posY[index] + 1] = _map_storage[index][5];
            map[_posX[index] + 1, _posY[index] + 1] = _map_storage[index][6];
            map[_posX[index] + 2, _posY[index] + 1] = _map_storage[index][7];
            map[_posX[index] + 3, _posY[index] + 1] = _map_storage[index][8];
            map[_posX[index] + 4, _posY[index] + 1] = _map_storage[index][9];
        }
        else if (type == "dragon")
        {
            int value = 0;
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    map[_posX[index] + j, _posY[index] + i] = " ";
                    value++;
                }
            }
        }
        else if (type == "fireball")
        {
            map[_posX[index] + 0, _posY[index]] = _map_storage[index][0];
            map[_posX[index] + 1, _posY[index]] = _map_storage[index][1];
            map[_posX[index] + 2, _posY[index]] = _map_storage[index][2];

            map[_posX[index] + 0, _posY[index] + 1] = _map_storage[index][3];
            map[_posX[index] + 1, _posY[index] + 1] = _map_storage[index][4];
            map[_posX[index] + 2, _posY[index] + 1] = _map_storage[index][5];
        }
    }

    public void Remove(int index, string type)
    {
        Clear(index, type);

        UpdateRow(_posY[index]);
        UpdateRow(_posY[index] + 1);
        UpdateRow(_posY[index] + 2);

        if (type == "dragon")
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

        if (type == "bat")
        {
            if (currentMap == 10)
            {
                cEnemies1--;
            }
            else if (currentMap == 11)
            {
                cEnemies2--;
            }
        }
    }

    public void SpawnRupee()
    {
        if (_sRType != "dragon" && _sRType != "bat" && new Random().Next(2) == 1)
        {
            string[] rupee_storage_copy = new string[9];

            int value = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (map[_sRPosX - 1 + j, _sRPosY - 1 + i] is not "-" and not "S")
                    {
                        rupee_storage_copy[value] = map[_sRPosX - 1 + j, _sRPosY - 1 + i];
                    }
                    else
                    {
                        rupee_storage_copy[value] = " ";
                    }
                    value++;
                }
            }

            _rPosX.Add(_sRPosX);
            _rPosY.Add(_sRPosY);
            _rupee_storage.Add(rupee_storage_copy);

            if (new Random().Next(5) == 4 || (_sRType == "spider" && new Random().Next(10) == 9))
            {
                map[_sRPosX, _sRPosY] = "V";
            }
            else
            {
                map[_sRPosX, _sRPosY] = "R";
            }

            map[_sRPosX - 1, _sRPosY] = "R";
            map[_sRPosX + 1, _sRPosY] = "R";
            map[_sRPosX, _sRPosY - 1] = "r";
            map[_sRPosX, _sRPosY + 1] = "r";

            UpdateRow(_sRPosY - 1);
            UpdateRow(_sRPosY);
            UpdateRow(_sRPosY + 1);
        }
    }

    public void RemoveRupee(int posX, int posY)
    {
        for (int i = 0; i < _rPosX.ToArray().Length; i++)
        {
            if (posX >= _rPosX[i] - 1 && posX <= _rPosX[i] + 1 && posY >= _rPosY[i] - 1 && posY <= _rPosY[i] + 1)
            {
                if (map[_rPosX[i], _rPosY[i]] == "V")
                {
                    rupees += 5;
                }
                else
                {
                    rupees++;
                }

                map[_rPosX[i] - 1, _rPosY[i] - 1] = _rupee_storage[i][0];
                map[_rPosX[i] + 0, _rPosY[i] - 1] = _rupee_storage[i][1];
                map[_rPosX[i] + 1, _rPosY[i] - 1] = _rupee_storage[i][2];

                map[_rPosX[i] - 1, _rPosY[i]] = _rupee_storage[i][3];
                map[_rPosX[i] + 0, _rPosY[i]] = _rupee_storage[i][4];
                map[_rPosX[i] + 1, _rPosY[i]] = _rupee_storage[i][5];

                map[_rPosX[i] - 1, _rPosY[i] + 1] = _rupee_storage[i][6];
                map[_rPosX[i] + 0, _rPosY[i] + 1] = _rupee_storage[i][7];
                map[_rPosX[i] + 1, _rPosY[i] + 1] = _rupee_storage[i][8];

                UpdateRow(_rPosY[i] - 1);
                UpdateRow(_rPosY[i]);
                UpdateRow(_rPosY[i] + 1);

                _rPosX.RemoveAt(i);
                _rPosY.RemoveAt(i);
                _rupee_storage.RemoveAt(i);
            }
        }
    }

    public bool InBounds(string type, int posX, int posY)
    {
        int inPosX = 0;
        int inPosY = 0;
        if (type == "octorok")
        {
            inPosX = posX + 3;
            inPosY = posY + 2;
        }
        else if (type == "spider")
        {
            inPosX = posX + 4;
            inPosY = posY + 2;
        }
        else if (type == "bat")
        {
            inPosX = posX + 4;
            inPosY = posY + 1;
        }
        else if (type == "fireball")
        {
            inPosX = posX + 3;
            inPosY = posY + 1;
        }

        if (posX <= 0 || inPosX >= 102 || posY <= 0 || inPosY >= 33)
        {
            return false;
        }
        return true;
    }

    public bool IsTouching(string type, int posX, int posY, string symbol)
    {
        if (type == "octorok" && (map[posX, posY] == symbol || map[posX + 1, posY] == symbol || map[posX + 2, posY] == symbol || map[posX + 3, posY] == symbol || map[posX, posY + 1] == symbol || map[posX + 1, posY + 1] == symbol || map[posX + 2, posY + 1] == symbol || map[posX + 3, posY + 1] == symbol || map[posX, posY + 2] == symbol || map[posX + 1, posY + 2] == symbol || map[posX + 2, posY + 2] == symbol || map[posX + 3, posY + 2] == symbol))
        {
            return true;
        }
        if (type == "spider" && (map[posX, posY] == symbol || map[posX + 1, posY] == symbol || map[posX + 2, posY] == symbol || map[posX + 3, posY] == symbol || map[posX + 4, posY] == symbol || map[posX, posY + 1] == symbol || map[posX + 1, posY + 1] == symbol || map[posX + 2, posY + 1] == symbol || map[posX + 3, posY + 1] == symbol || map[posX + 4, posY + 1] == symbol || map[posX, posY + 2] == symbol || map[posX + 1, posY + 2] == symbol || map[posX + 2, posY + 2] == symbol || map[posX + 3, posY + 2] == symbol || map[posX + 4, posY + 2] == symbol))
        {
            return true;
        }
        if (type == "bat" && (map[posX, posY] == symbol || map[posX + 1, posY] == symbol || map[posX + 2, posY] == symbol || map[posX + 3, posY] == symbol || map[posX + 4, posY] == symbol || map[posX, posY + 1] == symbol || map[posX + 1, posY + 1] == symbol || map[posX + 2, posY + 1] == symbol || map[posX + 3, posY + 1] == symbol || map[posX + 4, posY + 1] == symbol))
        {
            return true;
        }
        if (type == "fireball" && (map[posX, posY] == symbol || map[posX + 1, posY] == symbol || map[posX + 2, posY] == symbol || map[posX, posY + 1] == symbol || map[posX + 1, posY + 1] == symbol || map[posX + 2, posY + 1] == symbol))
        {
            return true;
        }
        return false;
    }

    public int GetIndex(int posX, int posY)
    {
        for (int i = 0; i < GetTotal(); i++)
        {
            int inPosX = 0;
            int inPosY = 0;
            if (_type[i] == "octorok")
            {
                inPosX = 4;
                inPosY = 3;
            }
            else if (_type[i] == "spider")
            {
                inPosX = 5;
                inPosY = 3;
            }
            else if (_type[i] == "bat")
            {
                inPosX = 5;
                inPosY = 2;
            }
            else if (_type[i] == "dragon")
            {
                inPosX = 12;
                inPosY = 7;
            }
            else if (_type[i] == "fireball")
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