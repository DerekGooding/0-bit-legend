namespace _0_Bit_Legend;

public class LinkMovement : MainProgram
{
    private int _posX;
    private int _posY;

    private int _preHitPosX;
    private int _preHitPosY;

    private readonly string[] _storage_map = [" ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " "];
    private static readonly string[] _storage_sword = new string[6];
    private static readonly string[] _storage_detect_enemy = new string[6];

    private string _prev = "w";
    private string _prev2 = "a";

    private bool _debounce;
    private bool _spawnRupee;

    public int GetPosX() => _posX;
    public int GetPosY() => _posY;
    public string GetPrev() => _prev;
    public string GetPrev2() => _prev2;
    public void SetPosX(int posX) => _posX = posX;
    public void SetPosY(int posY) => _posY = posY;
    public void SetPreHitPosX(int posX) => _preHitPosX = posX;
    public void SetPreHitPosY(int posY) => _preHitPosY = posY;
    public void SetPrev(string prev) => _prev = prev;
    public void SetSpawnRupee(bool spawnRupee) => _spawnRupee = spawnRupee;

    public void Attack(string prev, bool attacking)
    {
        if (prev == "w" && _posY > 3)
        {
            if (!attacking)
            {
                _storage_sword[0] = map[_posX - 1, _posY - 2];
                _storage_sword[1] = map[_posX, _posY - 2];
                _storage_sword[2] = map[_posX + 1, _posY - 2];
                _storage_sword[3] = map[_posX, _posY - 3];
                _storage_sword[4] = map[_posX, _posY - 4];

                map[_posX - 1, _posY - 2] = "-";
                map[_posX, _posY - 2] = "-";
                map[_posX + 1, _posY - 2] = "-";
                map[_posX, _posY - 3] = "S";
                map[_posX, _posY - 4] = "S";

                _preHitPosX = _posX;
                _preHitPosY = _posY;
            }
            else
            {
                _storage_detect_enemy[0] = map[_preHitPosX - 1, _preHitPosY - 2];
                _storage_detect_enemy[1] = map[_preHitPosX, _preHitPosY - 2];
                _storage_detect_enemy[2] = map[_preHitPosX + 1, _preHitPosY - 2];
                _storage_detect_enemy[3] = map[_preHitPosX, _preHitPosY - 3];
                _storage_detect_enemy[4] = map[_preHitPosX, _preHitPosY - 4];

                int[,] swordArr = new int[2, 5];
                swordArr[0, 0] = _preHitPosX - 1;
                swordArr[1, 0] = _preHitPosY - 2;
                swordArr[0, 1] = _preHitPosX;
                swordArr[1, 1] = _preHitPosY - 2;
                swordArr[0, 2] = _preHitPosX + 1;
                swordArr[1, 2] = _preHitPosY - 2;
                swordArr[0, 3] = _preHitPosX;
                swordArr[1, 3] = _preHitPosY - 3;
                swordArr[0, 4] = _preHitPosX;
                swordArr[1, 4] = _preHitPosY - 4;

                Stab(swordArr, prev, 5, 1);
            }

            UpdateRow(_preHitPosY - 2);
            UpdateRow(_preHitPosY - 3);
            UpdateRow(_preHitPosY - 4);
        }
        else if (prev == "a" && _posX > 4)
        {
            if (!attacking)
            {
                _storage_sword[0] = map[_posX - 3, _posY];
                _storage_sword[1] = map[_posX - 3, _posY + 1];
                _storage_sword[2] = map[_posX - 3, _posY + 2];
                _storage_sword[3] = map[_posX - 4, _posY + 1];
                _storage_sword[4] = map[_posX - 5, _posY + 1];
                _storage_sword[5] = map[_posX - 6, _posY + 1];

                map[_posX - 3, _posY] = "-";
                map[_posX - 3, _posY + 1] = "-";
                map[_posX - 3, _posY + 2] = "-";
                map[_posX - 4, _posY + 1] = "S";
                map[_posX - 5, _posY + 1] = "S";
                map[_posX - 6, _posY + 1] = "S";

                _preHitPosX = _posX;
                _preHitPosY = _posY;
            }
            else
            {
                _storage_detect_enemy[0] = map[_preHitPosX - 3, _preHitPosY];
                _storage_detect_enemy[1] = map[_preHitPosX - 3, _preHitPosY + 1];
                _storage_detect_enemy[2] = map[_preHitPosX - 3, _preHitPosY + 2];
                _storage_detect_enemy[3] = map[_preHitPosX - 4, _preHitPosY + 1];
                _storage_detect_enemy[4] = map[_preHitPosX - 5, _preHitPosY + 1];
                _storage_detect_enemy[5] = map[_preHitPosX - 6, _preHitPosY + 1];

                int[,] swordArr = new int[2, 6];
                swordArr[0, 0] = _preHitPosX - 3;
                swordArr[1, 0] = _preHitPosY;
                swordArr[0, 1] = _preHitPosX - 3;
                swordArr[1, 1] = _preHitPosY + 1;
                swordArr[0, 2] = _preHitPosX - 3;
                swordArr[1, 2] = _preHitPosY + 2;
                swordArr[0, 3] = _preHitPosX - 4;
                swordArr[1, 3] = _preHitPosY + 1;
                swordArr[0, 4] = _preHitPosX - 5;
                swordArr[1, 4] = _preHitPosY + 1;
                swordArr[0, 5] = _preHitPosX - 6;
                swordArr[1, 5] = _preHitPosY + 1;

                Stab(swordArr, prev, 6, 1);
            }

            UpdateRow(_preHitPosY);
            UpdateRow(_preHitPosY + 1);
            UpdateRow(_preHitPosY + 2);
        }
        else if (prev == "s" && _posY + 4 < 33)
        {
            if (!attacking)
            {
                _storage_sword[0] = map[_posX - 1, _posY + 3];
                _storage_sword[1] = map[_posX, _posY + 3];
                _storage_sword[2] = map[_posX + 1, _posY + 3];
                _storage_sword[3] = map[_posX, _posY + 4];
                _storage_sword[4] = map[_posX, _posY + 5];

                map[_posX - 1, _posY + 3] = "-";
                map[_posX, _posY + 3] = "-";
                map[_posX + 1, _posY + 3] = "-";
                map[_posX, _posY + 4] = "S";
                map[_posX, _posY + 5] = "S";

                _preHitPosX = _posX;
                _preHitPosY = _posY;
            }
            else
            {
                _storage_detect_enemy[0] = map[_preHitPosX - 1, _preHitPosY + 3];
                _storage_detect_enemy[1] = map[_preHitPosX, _preHitPosY + 3];
                _storage_detect_enemy[2] = map[_preHitPosX + 1, _preHitPosY + 3];
                _storage_detect_enemy[3] = map[_preHitPosX, _preHitPosY + 4];
                _storage_detect_enemy[4] = map[_preHitPosX, _preHitPosY + 5];

                int[,] swordArr = new int[2, 5];
                swordArr[0, 0] = _preHitPosX - 1;
                swordArr[1, 0] = _preHitPosY + 3;
                swordArr[0, 1] = _preHitPosX;
                swordArr[1, 1] = _preHitPosY + 3;
                swordArr[0, 2] = _preHitPosX + 1;
                swordArr[1, 2] = _preHitPosY + 3;
                swordArr[0, 3] = _preHitPosX;
                swordArr[1, 3] = _preHitPosY + 4;
                swordArr[0, 4] = _preHitPosX;
                swordArr[1, 4] = _preHitPosY + 5;

                Stab(swordArr, prev, 5, 1);
            }

            UpdateRow(_preHitPosY + 3);
            UpdateRow(_preHitPosY + 4);
            UpdateRow(_preHitPosY + 5);
        }
        else if (prev == "d" && _posX + 6 < 102)
        {
            if (!attacking)
            {
                _storage_sword[0] = map[_posX + 3, _posY];
                _storage_sword[1] = map[_posX + 3, _posY + 1];
                _storage_sword[2] = map[_posX + 3, _posY + 2];
                _storage_sword[3] = map[_posX + 4, _posY + 1];
                _storage_sword[4] = map[_posX + 5, _posY + 1];
                _storage_sword[5] = map[_posX + 6, _posY + 1];

                map[_posX + 3, _posY] = "-";
                map[_posX + 3, _posY + 1] = "-";
                map[_posX + 3, _posY + 2] = "-";
                map[_posX + 4, _posY + 1] = "S";
                map[_posX + 5, _posY + 1] = "S";
                map[_posX + 6, _posY + 1] = "S";

                _preHitPosX = _posX;
                _preHitPosY = _posY;
            }
            else
            {
                _storage_detect_enemy[0] = map[_preHitPosX + 3, _preHitPosY];
                _storage_detect_enemy[1] = map[_preHitPosX + 3, _preHitPosY + 1];
                _storage_detect_enemy[2] = map[_preHitPosX + 3, _preHitPosY + 2];
                _storage_detect_enemy[3] = map[_preHitPosX + 4, _preHitPosY + 1];
                _storage_detect_enemy[4] = map[_preHitPosX + 5, _preHitPosY + 1];
                _storage_detect_enemy[5] = map[_preHitPosX + 6, _preHitPosY + 1];

                int[,] swordArr = new int[2, 6];
                swordArr[0, 0] = _preHitPosX + 3;
                swordArr[1, 0] = _preHitPosY;
                swordArr[0, 1] = _preHitPosX + 3;
                swordArr[1, 1] = _preHitPosY + 1;
                swordArr[0, 2] = _preHitPosX + 3;
                swordArr[1, 2] = _preHitPosY + 2;
                swordArr[0, 3] = _preHitPosX + 4;
                swordArr[1, 3] = _preHitPosY + 1;
                swordArr[0, 4] = _preHitPosX + 5;
                swordArr[1, 4] = _preHitPosY + 1;
                swordArr[0, 5] = _preHitPosX + 6;
                swordArr[1, 5] = _preHitPosY + 1;

                Stab(swordArr, prev, 6, 1);
            }

            UpdateRow(_preHitPosY);
            UpdateRow(_preHitPosY + 1);
            UpdateRow(_preHitPosY + 2);
        }
    }

    public void Stab(int[,] swordArr, string prev, int amt, int dmg)
    {
        bool hit = false;
        for (int i = 0; i < amt; i++)
        {
            if (_storage_sword[i] == "t" || _storage_sword[i] == "n" || _storage_sword[i] == "B" || _storage_sword[i] == "{" || _storage_sword[i] == "}" || _storage_sword[i] == "F" || _storage_detect_enemy[i] == "t" || _storage_detect_enemy[i] == "n" || _storage_detect_enemy[i] == "B" || _storage_detect_enemy[i] == "{" || _storage_detect_enemy[i] == "}" || _storage_detect_enemy[i] == "F")
            {
                hit = true;
                if (enemyMovement.TakeDamage(swordArr[0, i], swordArr[1, i], prev, dmg) && _spawnRupee)
                {
                    _spawnRupee = false;
                    enemyMovement.SpawnRupee();
                }
                break;
            }
        }
        if (!hit)
        {
            StoreSword(prev);
        }
    }

    public void StoreSword(string prev)
    {
        for (int i = 0; i < 6; i++)
        {
            if (_storage_sword[i] is "t" or "^" or "n" or "0" or "B" or "{" or "}" or "F" or "S" or ">" or "*")
            {
                _storage_sword[i] = " ";
            }
        }

        if (prev == "w")
        {
            map[_preHitPosX - 1, _preHitPosY - 2] = _storage_sword[0];
            map[_preHitPosX, _preHitPosY - 2] = _storage_sword[1];
            map[_preHitPosX + 1, _preHitPosY - 2] = _storage_sword[2];
            map[_preHitPosX, _preHitPosY - 3] = _storage_sword[3];
            map[_preHitPosX, _preHitPosY - 4] = _storage_sword[4];
        }
        else if (prev == "a")
        {
            map[_preHitPosX - 3, _preHitPosY] = _storage_sword[0];
            map[_preHitPosX - 3, _preHitPosY + 1] = _storage_sword[1];
            map[_preHitPosX - 3, _preHitPosY + 2] = _storage_sword[2];
            map[_preHitPosX - 4, _preHitPosY + 1] = _storage_sword[3];
            map[_preHitPosX - 5, _preHitPosY + 1] = _storage_sword[4];
            map[_preHitPosX - 6, _preHitPosY + 1] = _storage_sword[5];
        }
        else if (prev == "s")
        {
            map[_preHitPosX - 1, _preHitPosY + 3] = _storage_sword[0];
            map[_preHitPosX, _preHitPosY + 3] = _storage_sword[1];
            map[_preHitPosX + 1, _preHitPosY + 3] = _storage_sword[2];
            map[_preHitPosX, _preHitPosY + 4] = _storage_sword[3];
            map[_preHitPosX, _preHitPosY + 5] = _storage_sword[4];
        }
        else if (prev == "d")
        {
            map[_preHitPosX + 3, _preHitPosY] = _storage_sword[0];
            map[_preHitPosX + 3, _preHitPosY + 1] = _storage_sword[1];
            map[_preHitPosX + 3, _preHitPosY + 2] = _storage_sword[2];
            map[_preHitPosX + 4, _preHitPosY + 1] = _storage_sword[3];
            map[_preHitPosX + 5, _preHitPosY + 1] = _storage_sword[4];
            map[_preHitPosX + 6, _preHitPosY + 1] = _storage_sword[5];
        }
    }

    public void MoveLink(int posX, int posY, string direction, bool spawn)
    {
        if (spawn)
        {
            _posX = posX;
            _posY = posY;

            _storage_map[0] = map[posX - 2, posY - 1];
            _storage_map[1] = map[posX - 1, posY - 1];
            _storage_map[2] = map[posX, posY - 1];
            _storage_map[3] = map[posX + 1, posY - 1];
            _storage_map[4] = map[posX + 2, posY - 1];

            _storage_map[5] = map[posX - 2, posY];
            _storage_map[6] = map[posX - 1, posY];
            _storage_map[7] = map[posX, posY];
            _storage_map[8] = map[posX + 1, posY];
            _storage_map[9] = map[posX + 2, posY];

            _storage_map[10] = map[posX - 2, posY + 1];
            _storage_map[11] = map[posX - 1, posY + 1];
            _storage_map[12] = map[posX, posY + 1];
            _storage_map[13] = map[posX + 1, posY + 1];
            _storage_map[14] = map[posX + 2, posY + 1];

            _storage_map[15] = map[posX - 2, posY + 2];
            _storage_map[16] = map[posX - 1, posY + 2];
            _storage_map[17] = map[posX, posY + 2];
            _storage_map[18] = map[posX + 1, posY + 2];
            _storage_map[19] = map[posX + 2, posY + 2];
        }

        _prev = direction;

        if (direction == "w")
        {
            if (_posX == 21 && ((currentMap == 4 && posY > 9) || currentMap == 2))
            {
                if (posY > 1)
                {
                    for (int y = _posY - 2; y <= _posY + 3; y++)
                    {
                        for (int x = _posX - 3; x <= _posX + 3; x++)
                        {
                            map[x, y] = "~";
                        }
                    }

                    _posY--;
                    DeployRaft(_prev2);

                    UpdateRow(_posY + 4);
                }
                else
                {
                    LoadMap(4, 21, 29, "w");
                }
            }
            else if (posY >= 1 && !(_posX == 21 && (currentMap == 4 || currentMap == 2)))
            {
                IsTouching(posX, posY, "r");
                StoreChar(_posX, _posY);
                bool inCave = false;

                if (currentMap == 6 && (IsTouching(posX, posY, "-") || IsTouching(posX, posY, "S")))
                {
                    hasSword = true;
                    LoadMap(6, posX, posY, direction);
                }
                else if (currentMap == 7 && IsTouching(posX, posY, "*") && rupees >= 35)
                {
                    rupees -= 35;

                    hasRaft = true;
                    LoadMap(7, posX, posY, direction);
                }
                else if (currentMap == 7 && IsTouching(posX, posY, "Y") && rupees >= 5)
                {
                    rupees -= 10;
                    keys++;
                }
                else if (currentMap == 7 && IsTouching(posX, posY, "#") && rupees >= 15)
                {
                    rupees -= 25;

                    hasArmor = true;
                    LoadMap(7, posX, posY, direction);
                }
                else if (currentMap == 9 && IsTouching(posX, posY, "X") && !IsTouching(posX, posY, "=") && cDoor1 && cDoor2 && cDoor3 && cEnemies1 <= 0 && cEnemies2 <= 0 && !_debounce)
                {
                    LoadMap(12, 50, 24, direction);
                }
                else if (currentMap == 9 && _posX >= 48 && _posX <= 52 && _posY == 7 && !cDoor3 && keys > 0)
                {
                    _debounce = true;
                    keys--;

                    cDoor3 = true;
                    LoadMap(9, _posX, _posY, direction);
                }

                if (!IsTouching(posX, posY, "=") && !IsTouching(posX, posY, "X") && !IsTouching(posX, posY, "t") && !IsTouching(posX, posY, "n") && !IsTouching(posX, posY, "B") && !IsTouching(posX, posY, "{") && !IsTouching(posX, posY, "}") && !IsTouching(posX, posY, "S") && !IsTouching(posX, posY, "<") && !IsTouching(posX, posY, ">") && !IsTouching(posX, posY, "*") && !(IsTouching(posX, posY, "F") && currentMap != 7) && !IsTouching(posX, posY, "~") && !((currentMap == 6 || currentMap == 7) && posY < 17) && ((currentMap >= 9 && !IsTouching(posX, posY, "/")) || currentMap < 9))
                {
                    if (IsTouching(posX, posY, "/"))
                    {
                        inCave = true;
                    }

                    StoreChar(posX, posY);
                    BuildChar(posX, posY, direction);

                    UpdateRow(posY - 1);
                    UpdateRow(posY);
                    UpdateRow(posY + 1);
                    UpdateRow(posY + 2);

                    _posX = posX;
                    _posY = posY;
                }
                else if (IsTouching(posX, posY, "t") || IsTouching(posX, posY, "n") || IsTouching(posX, posY, "B") || IsTouching(posX, posY, "{") || IsTouching(posX, posY, "}") || IsTouching(posX, posY, "S") || IsTouching(posX, posY, "<") || (IsTouching(posX, posY, "F") && currentMap != 7))
                {
                    BuildChar(_posX, _posY, direction);

                    UpdateRow(_posY - 1);
                    UpdateRow(_posY);
                    UpdateRow(_posY + 1);
                    UpdateRow(_posY + 2);

                    Hit();
                }
                else if (currentMap == 13 && IsTouching(posX, posY, "~"))
                {
                    BuildChar(_posX, _posY, direction);

                    UpdateRow(_posY - 1);
                    UpdateRow(_posY);
                    UpdateRow(_posY + 1);
                    UpdateRow(_posY + 2);

                    LoadMap(13, 58, 15, "a");
                    gameOver = true;
                }
                else
                {
                    BuildChar(_posX, _posY, direction);

                    UpdateRow(_posY - 1);
                    UpdateRow(_posY);
                    UpdateRow(_posY + 1);
                    UpdateRow(_posY + 2);
                }

                if (inCave)
                {
                    Wait(2);
                }
            }
            else
            {
                if (currentMap == 0)
                {
                    LoadMap(1, 63, 29, "w");
                }
                else if (currentMap == 2)
                {
                    if (posX > 29)
                    {
                        LoadMap(4, 55, 30, "w");
                    }
                    else if (posX == 21)
                    {
                        LoadMap(4, 21, 29, "w");
                    }
                    else
                    {
                        LoadMap(4, 10, 29, "w");
                    }
                }
                else if (currentMap == 3)
                {
                    LoadMap(5, 49, 30, "w");
                }
            }
            if (cText)
            {
                cText = false;
                LoadMap(9, posX, posY, "w");
            }
        }
        else if (direction == "a")
        {
            _prev2 = "a";
            if (posX >= 2)
            {
                IsTouching(posX, posY, "r");
                StoreChar(_posX, _posY);

                if (currentMap == 6 && (IsTouching(posX, posY, "-") || IsTouching(posX, posY, "S")))
                {
                    hasSword = true;
                    LoadMap(6, posX, posY, direction);
                }
                else if (currentMap == 7 && IsTouching(posX, posY, "*") && rupees >= 35)
                {
                    rupees -= 35;

                    hasRaft = true;
                    LoadMap(7, posX, posY, direction);
                }
                else if (currentMap == 7 && IsTouching(posX, posY, "Y") && rupees >= 10)
                {
                    rupees -= 10;
                    keys++;
                }
                else if (currentMap == 7 && IsTouching(posX, posY, "#") && rupees >= 25)
                {
                    rupees -= 25;

                    hasArmor = true;
                    LoadMap(7, posX, posY, direction);
                }
                else if (currentMap == 9 && IsTouching(posX, posY, "X") && !IsTouching(posX, posY, "=") && cDoor1 && !_debounce)
                {
                    LoadMap(10, 87, 15, direction);
                }
                else if (currentMap == 9 && _posX == 14 && _posY >= 14 && _posY <= 16 && !cDoor1 && keys > 0)
                {
                    _debounce = true;
                    keys--;

                    cDoor1 = true;
                    LoadMap(9, _posX, _posY, direction);
                }

                if (!IsTouching(posX, posY, "=") && !IsTouching(posX, posY, "X") && !IsTouching(posX, posY, "t") && !IsTouching(posX, posY, "n") && !IsTouching(posX, posY, "B") && !IsTouching(posX, posY, "{") && !IsTouching(posX, posY, "}") && !IsTouching(posX, posY, "S") && !IsTouching(posX, posY, "<") && !(IsTouching(posX, posY, "F") && currentMap != 7) && !IsTouching(posX, posY, "~") && ((currentMap >= 9 && !IsTouching(posX, posY, "/")) || currentMap < 9))
                {
                    StoreChar(posX, posY);
                    BuildChar(posX, posY, direction);

                    UpdateRow(posY - 1);
                    UpdateRow(posY);
                    UpdateRow(posY + 1);
                    UpdateRow(posY + 2);

                    _posX = posX;
                    _posY = posY;
                }
                else if (IsTouching(posX, posY, "t") || IsTouching(posX, posY, "n") || IsTouching(posX, posY, "B") || IsTouching(posX, posY, "{") || IsTouching(posX, posY, "}") || IsTouching(posX, posY, "S") || IsTouching(posX, posY, "<") || (IsTouching(posX, posY, "F") && currentMap != 7))
                {
                    BuildChar(_posX, _posY, direction);

                    UpdateRow(_posY - 1);
                    UpdateRow(_posY);
                    UpdateRow(_posY + 1);
                    UpdateRow(_posY + 2);

                    Hit();
                }
                else if (IsTouching(posX, posY, "~") && _posX != 21 && hasRaft && !IsTouching(posX, posY, "=") && !IsTouching(posX, posY, "X") && !IsTouching(posX, posY, "t") && !IsTouching(posX, posY, "n"))
                {
                    StoreChar(_posX, _posY);

                    UpdateRow(_posY - 1);
                    UpdateRow(_posY);
                    UpdateRow(_posY + 1);
                    UpdateRow(_posY + 2);

                    _posX = 21;
                    DeployRaft("a");
                    wait = 150;
                }
                else if (_posX == 21 && ((posY > 11 && currentMap == 4) || currentMap == 2) && ((currentMap == 2 && posY < 25) || currentMap == 4))
                {
                    BuildChar(_posX, _posY, direction);

                    UpdateRow(_posY - 1);
                    UpdateRow(_posY);
                    UpdateRow(_posY + 1);
                    UpdateRow(_posY + 2);

                    for (int y = _posY - 2; y <= _posY + 3; y++)
                    {
                        for (int x = _posX - 3; x <= _posX + 3; x++)
                        {
                            map[x, y] = "~";
                        }
                    }

                    _posX = 11;
                    posX = 11;

                    BuildChar(posX, posY, direction);

                    UpdateRow(posY - 2);
                    UpdateRow(posY - 1);
                    UpdateRow(posY);
                    UpdateRow(posY + 1);
                    UpdateRow(posY + 2);
                    UpdateRow(posY + 3);
                }
                else if (currentMap == 11 && IsTouching(posX, posY, "X") && !IsTouching(posX, posY, "="))
                {
                    LoadMap(9, 86, 15, "a");
                }
                else if (currentMap == 13 && IsTouching(posX, posY, "X") && !IsTouching(posX, posY, "="))
                {
                    LoadMap(12, 86, 15, "a");
                }
                else
                {
                    BuildChar(_posX, _posY, direction);

                    UpdateRow(_posY - 1);
                    UpdateRow(_posY);
                    UpdateRow(_posY + 1);
                    UpdateRow(_posY + 2);
                }

            }
            else
            {
                if (currentMap == 0)
                {
                    LoadMap(2, 99, 12, "a");
                }
                else if (currentMap == 3)
                {
                    LoadMap(0, 98, 17, "a");
                }
                else if (currentMap == 1)
                {
                    LoadMap(4, 98, 13, "a");
                }
                else if (currentMap == 5)
                {
                    LoadMap(1, 98, 16, "a");
                }
                else if (currentMap == 4)
                {
                    LoadMap(8, 99, 16, "a");
                }
            }
            if (cText)
            {
                cText = false;
                LoadMap(9, posX, posY, "w");
            }
        }
        else if (direction == "s")
        {
            if ((currentMap == 2 || currentMap == 4) && posX == 21)
            {
                if ((posY < 30) && ((currentMap == 2 && posY < 27) || currentMap == 4))
                {
                    for (int y = _posY - 2; y <= _posY + 3; y++)
                    {
                        for (int x = _posX - 3; x <= _posX + 3; x++)
                        {
                            map[x, y] = "~";
                        }
                    }

                    _posY++;
                    DeployRaft(_prev2);

                    UpdateRow(_posY - 3);
                }
                else if (currentMap == 4)
                {
                    LoadMap(2, 21, 2, "s");
                }
            }
            else if (posY <= 29)
            {
                IsTouching(posX, posY, "r");

                StoreChar(_posX, _posY);
                if (!IsTouching(posX, posY, "=") && !IsTouching(posX, posY, "X") && !IsTouching(posX, posY, "t") && !IsTouching(posX, posY, "n") && !IsTouching(posX, posY, "B") && !IsTouching(posX, posY, "{") && !IsTouching(posX, posY, "}") && !IsTouching(posX, posY, "S") && !IsTouching(posX, posY, "<") && !(IsTouching(posX, posY, "F") && currentMap != 7) && !IsTouching(posX, posY, "~") && ((currentMap >= 9 && !IsTouching(posX, posY, "/")) || currentMap < 9))
                {
                    StoreChar(posX, posY);
                    BuildChar(posX, posY, direction);

                    UpdateRow(posY - 1);
                    UpdateRow(posY);
                    UpdateRow(posY + 1);
                    UpdateRow(posY + 2);

                    _posX = posX;
                    _posY = posY;
                }
                else if (IsTouching(posX, posY, "t") || IsTouching(posX, posY, "n") || IsTouching(posX, posY, "B") || IsTouching(posX, posY, "{") || IsTouching(posX, posY, "}") || IsTouching(posX, posY, "S") || IsTouching(posX, posY, "<") || (IsTouching(posX, posY, "F") && currentMap != 7))
                {
                    BuildChar(_posX, _posY, direction);

                    UpdateRow(_posY - 1);
                    UpdateRow(_posY);
                    UpdateRow(_posY + 1);
                    UpdateRow(_posY + 2);

                    Hit();
                }
                else if (currentMap == 12 && IsTouching(posX, posY, "X") && !IsTouching(posX, posY, "="))
                {
                    BuildChar(_posX, _posY, direction);

                    UpdateRow(_posY - 1);
                    UpdateRow(_posY);
                    UpdateRow(_posY + 1);
                    UpdateRow(_posY + 2);

                    LoadMap(9, 50, 9, "s");
                }
                else
                {
                    BuildChar(_posX, _posY, direction);

                    UpdateRow(_posY - 1);
                    UpdateRow(_posY);
                    UpdateRow(_posY + 1);
                    UpdateRow(_posY + 2);
                }
            }
            else
            {
                if (currentMap == 1)
                {
                    LoadMap(0, 63, 1, "s");
                }
                else if (currentMap == 4)
                {
                    if (posX > 29)
                    {
                        LoadMap(2, 55, 1, "s");
                    }
                    else if (posX == 21)
                    {
                        LoadMap(2, 21, 2, "s");
                    }
                    else
                    {
                        LoadMap(2, 10, 2, "s");
                    }
                }
                else if (currentMap == 5)
                {
                    LoadMap(3, 49, 2, "s");
                }
                else if (currentMap == 6)
                {
                    LoadMap(0, 16, 6, "s");
                    Wait(2);
                }
                else if (currentMap == 7)
                {
                    LoadMap(4, 86, 7, "s");
                    Wait(2);
                }
                else if (currentMap == 9)
                {
                    LoadMap(8, 51, 17, "s");
                    Wait(2);
                }
            }
            cText = false;
        }
        else if (direction == "d")
        {
            _prev2 = "d";
            if (posX <= 99)
            {
                IsTouching(posX, posY, "r");
                StoreChar(_posX, _posY);

                bool persist = true;
                if (currentMap == 6 && (IsTouching(posX, posY, "-") || IsTouching(posX, posY, "S")))
                {
                    hasSword = true;
                    LoadMap(6, posX, posY, direction);
                }
                else if (currentMap == 7 && IsTouching(posX, posY, "*") && rupees >= 35)
                {
                    rupees -= 35;

                    hasRaft = true;
                    LoadMap(7, posX, posY, direction);
                }
                else if (currentMap == 7 && IsTouching(posX, posY, "Y") && rupees >= 10)
                {
                    rupees -= 10;
                    keys++;
                }
                else if (currentMap == 7 && IsTouching(posX, posY, "#") && rupees >= 25)
                {
                    rupees -= 25;

                    hasArmor = true;
                    LoadMap(7, posX, posY, direction);
                }
                else if (currentMap == 9 && IsTouching(posX, posY, "X") && !IsTouching(posX, posY, "=") && cDoor2 && !_debounce)
                {
                    persist = false;
                    LoadMap(11, 14, 15, direction);
                }
                else if (currentMap == 9 && _posX == 86 && _posY >= 14 && _posY <= 16 && !cDoor2 && keys > 0)
                {
                    _debounce = true;
                    keys--;

                    cDoor2 = true;
                    LoadMap(9, _posX, _posY, direction);
                }

                if (!IsTouching(posX, posY, "=") && !IsTouching(posX, posY, "X") && !IsTouching(posX, posY, "t") && !IsTouching(posX, posY, "n") && !IsTouching(posX, posY, "B") && !IsTouching(posX, posY, "{") && !IsTouching(posX, posY, "}") && !IsTouching(posX, posY, "S") && !IsTouching(posX, posY, "<") && !(IsTouching(posX, posY, "F") && currentMap != 7) && !IsTouching(posX, posY, "~") && ((currentMap >= 9 && !IsTouching(posX, posY, "/")) || currentMap < 9) && persist)
                {
                    StoreChar(posX, posY);
                    BuildChar(posX, posY, direction);

                    UpdateRow(posY - 1);
                    UpdateRow(posY);
                    UpdateRow(posY + 1);
                    UpdateRow(posY + 2);

                    _posX = posX;
                    _posY = posY;
                }
                else if (IsTouching(posX, posY, "t") || IsTouching(posX, posY, "n") || IsTouching(posX, posY, "B") || IsTouching(posX, posY, "{") || IsTouching(posX, posY, "}") || IsTouching(posX, posY, "S") || IsTouching(posX, posY, "<") || (IsTouching(posX, posY, "F") && currentMap != 7))
                {
                    BuildChar(_posX, _posY, direction);

                    UpdateRow(_posY - 1);
                    UpdateRow(_posY);
                    UpdateRow(_posY + 1);
                    UpdateRow(_posY + 2);

                    Hit();
                }
                else if (IsTouching(posX, posY, "~") && _posX != 21 && hasRaft && !IsTouching(posX, posY, "=") && !IsTouching(posX, posY, "X") && !IsTouching(posX, posY, "t") && !IsTouching(posX, posY, "n"))
                {
                    StoreChar(_posX, _posY);

                    UpdateRow(_posY - 1);
                    UpdateRow(_posY);
                    UpdateRow(_posY + 1);
                    UpdateRow(_posY + 2);

                    _posX = 21;
                    DeployRaft("d");
                    wait = 150;
                }
                else if (_posX == 21 && posY < 25 && ((posY > 3 && currentMap == 2) || (posY < 25 && currentMap == 4)))
                {
                    BuildChar(_posX, _posY, direction);

                    UpdateRow(_posY - 1);
                    UpdateRow(_posY);
                    UpdateRow(_posY + 1);
                    UpdateRow(_posY + 2);

                    for (int y = _posY - 2; y <= _posY + 3; y++)
                    {
                        for (int x = _posX - 3; x <= _posX + 3; x++)
                        {
                            map[x, y] = "~";
                        }
                    }

                    _posX = 30;
                    posX = 30;

                    BuildChar(posX, posY, direction);

                    UpdateRow(posY - 2);
                    UpdateRow(posY - 1);
                    UpdateRow(posY);
                    UpdateRow(posY + 1);
                    UpdateRow(posY + 2);
                    UpdateRow(posY + 3);
                }
                else if (currentMap == 10 && IsTouching(posX, posY, "X") && !IsTouching(posX, posY, "="))
                {
                    LoadMap(9, 14, 15, "d");
                }
                else if (currentMap == 12 && IsTouching(posX, posY, "X") && !IsTouching(posX, posY, "="))
                {
                    LoadMap(13, 15, 15, "d");
                }
                else
                {
                    BuildChar(_posX, _posY, direction);

                    UpdateRow(_posY - 1);
                    UpdateRow(_posY);
                    UpdateRow(_posY + 1);
                    UpdateRow(_posY + 2);
                }
            }
            else
            {
                if (currentMap == 2)
                {
                    LoadMap(0, 4, 12, "d");
                }
                else if (currentMap == 0)
                {
                    LoadMap(3, 2, 18, "d");
                }
                else if (currentMap == 4)
                {
                    LoadMap(1, 2, 13, "d");
                }
                else if (currentMap == 1)
                {
                    LoadMap(5, 2, 15, "d");
                }
                else if (currentMap == 8)
                {
                    LoadMap(4, 2, 16, "d");
                }
            }
            if (cText)
            {
                cText = false;
                LoadMap(9, posX, posY, "w");
            }
        }
        _debounce = false;
    }

    public void BuildChar(int posX, int posY, string direction)
    {
        string spaceslot = " ";
        string underslot = "_";
        if (hasArmor)
        {
            spaceslot = "#";
            underslot = "#";
        }

        if (direction == "w")
        {
            map[posX - 2, posY - 1] = " ";
            map[posX - 1, posY - 1] = "_";
            map[posX, posY - 1] = "_";
            map[posX + 1, posY - 1] = "_";
            map[posX + 2, posY - 1] = " ";

            map[posX - 2, posY] = "|";
            map[posX - 1, posY] = spaceslot;
            map[posX, posY] = "=";
            map[posX + 1, posY] = spaceslot;
            map[posX + 2, posY] = "|";

            map[posX - 2, posY + 1] = "|";
            map[posX - 1, posY + 1] = "^";
            map[posX, posY + 1] = spaceslot;
            map[posX + 1, posY + 1] = "^";
            map[posX + 2, posY + 1] = "|";

            map[posX - 2, posY + 2] = " ";
            map[posX - 1, posY + 2] = "\\";
            map[posX, posY + 2] = "_";
            map[posX + 1, posY + 2] = "/";
            map[posX + 2, posY + 2] = " ";
        }
        else if (direction == "a")
        {
            map[posX - 2, posY - 1] = " ";
            map[posX - 1, posY - 1] = " ";
            map[posX, posY - 1] = "/";
            map[posX + 1, posY - 1] = "\\";
            map[posX + 2, posY - 1] = " ";

            map[posX - 2, posY] = " ";
            map[posX - 1, posY] = "/";
            map[posX, posY] = " ";
            map[posX + 1, posY] = " ";
            map[posX + 2, posY] = "|";

            map[posX - 2, posY + 1] = "|";
            map[posX - 1, posY + 1] = "^";
            map[posX, posY + 1] = spaceslot;
            map[posX + 1, posY + 1] = spaceslot;
            map[posX + 2, posY + 1] = "|";

            map[posX - 2, posY + 2] = "|";
            map[posX - 1, posY + 2] = underslot;
            map[posX, posY + 2] = "=";
            map[posX + 1, posY + 2] = underslot;
            map[posX + 2, posY + 2] = "|";
        }
        else if (direction == "s")
        {
            map[posX - 2, posY - 1] = " ";
            map[posX - 1, posY - 1] = " ";
            map[posX, posY - 1] = "_";
            map[posX + 1, posY - 1] = " ";
            map[posX + 2, posY - 1] = " ";

            map[posX - 2, posY] = " ";
            map[posX - 1, posY] = "/";
            map[posX, posY] = " ";
            map[posX + 1, posY] = "\\";
            map[posX + 2, posY] = " ";

            map[posX - 2, posY + 1] = "|";
            map[posX - 1, posY + 1] = "^";
            map[posX, posY + 1] = spaceslot;
            map[posX + 1, posY + 1] = "^";
            map[posX + 2, posY + 1] = "|";

            map[posX - 2, posY + 2] = "|";
            map[posX - 1, posY + 2] = underslot;
            map[posX, posY + 2] = "=";
            map[posX + 1, posY + 2] = underslot;
            map[posX + 2, posY + 2] = "|";
        }
        else if (direction == "d")
        {
            map[posX - 2, posY - 1] = " ";
            map[posX - 1, posY - 1] = "/";
            map[posX, posY - 1] = "\\";
            map[posX + 1, posY - 1] = " ";
            map[posX + 2, posY - 1] = " ";

            map[posX - 2, posY] = "|";
            map[posX - 1, posY] = " ";
            map[posX, posY] = " ";
            map[posX + 1, posY] = "\\";
            map[posX + 2, posY] = " ";

            map[posX - 2, posY + 1] = "|";
            map[posX - 1, posY + 1] = spaceslot;
            map[posX, posY + 1] = spaceslot;
            map[posX + 1, posY + 1] = "^";
            map[posX + 2, posY + 1] = "|";

            map[posX - 2, posY + 2] = "|";
            map[posX - 1, posY + 2] = underslot;
            map[posX, posY + 2] = "=";
            map[posX + 1, posY + 2] = underslot;
            map[posX + 2, posY + 2] = "|";
        }
    }

    public void StoreChar(int posX, int posY)
    {
        map[_posX - 2, _posY - 1] = _storage_map[0];
        map[_posX - 1, _posY - 1] = _storage_map[1];
        map[_posX, _posY - 1] = _storage_map[2];
        map[_posX + 1, _posY - 1] = _storage_map[3];
        map[_posX + 2, _posY - 1] = _storage_map[4];

        map[_posX - 2, _posY] = _storage_map[5];
        map[_posX - 1, _posY] = _storage_map[6];
        map[_posX, _posY] = _storage_map[7];
        map[_posX + 1, _posY] = _storage_map[8];
        map[_posX + 2, _posY] = _storage_map[9];

        map[_posX - 2, _posY + 1] = _storage_map[10];
        map[_posX - 1, _posY + 1] = _storage_map[11];
        map[_posX, _posY + 1] = _storage_map[12];
        map[_posX + 1, _posY + 1] = _storage_map[13];
        map[_posX + 2, _posY + 1] = _storage_map[14];

        map[_posX - 2, _posY + 2] = _storage_map[15];
        map[_posX - 1, _posY + 2] = _storage_map[16];
        map[_posX, _posY + 2] = _storage_map[17];
        map[_posX + 1, _posY + 2] = _storage_map[18];
        map[_posX + 2, _posY + 2] = _storage_map[19];

        UpdateRow(_posY - 1);
        UpdateRow(_posY);
        UpdateRow(_posY + 1);
        UpdateRow(_posY + 2);

        _storage_map[0] = map[posX - 2, posY - 1];
        _storage_map[1] = map[posX - 1, posY - 1];
        _storage_map[2] = map[posX, posY - 1];
        _storage_map[3] = map[posX + 1, posY - 1];
        _storage_map[4] = map[posX + 2, posY - 1];

        _storage_map[5] = map[posX - 2, posY];
        _storage_map[6] = map[posX - 1, posY];
        _storage_map[7] = map[posX, posY];
        _storage_map[8] = map[posX + 1, posY];
        _storage_map[9] = map[posX + 2, posY];

        _storage_map[10] = map[posX - 2, posY + 1];
        _storage_map[11] = map[posX - 1, posY + 1];
        _storage_map[12] = map[posX, posY + 1];
        _storage_map[13] = map[posX + 1, posY + 1];
        _storage_map[14] = map[posX + 2, posY + 1];

        _storage_map[15] = map[posX - 2, posY + 2];
        _storage_map[16] = map[posX - 1, posY + 2];
        _storage_map[17] = map[posX, posY + 2];
        _storage_map[18] = map[posX + 1, posY + 2];
        _storage_map[19] = map[posX + 2, posY + 2];
    }

    public void DeployRaft(string direction)
    {
        string spaceslot = " ";
        string underslot = "_";
        if (hasArmor)
        {
            spaceslot = "#";
            underslot = "#";
        }

        map[_posX - 3, _posY - 2] = "*";
        map[_posX - 2, _posY - 2] = "*";
        map[_posX - 1, _posY - 2] = "*";
        map[_posX, _posY - 2] = "*";
        map[_posX + 1, _posY - 2] = "*";
        map[_posX + 2, _posY - 2] = "*";
        map[_posX + 3, _posY - 2] = "*";

        map[_posX - 3, _posY - 1] = "=";
        map[_posX, _posY - 1] = " ";
        map[_posX + 3, _posY - 1] = "=";

        map[_posX - 3, _posY] = "*";
        map[_posX - 2, _posY] = "|";
        map[_posX + 2, _posY] = "|";
        map[_posX + 3, _posY] = "*";

        map[_posX - 3, _posY + 1] = "*";
        map[_posX - 2, _posY + 1] = "|";
        map[_posX - 1, _posY + 1] = underslot;
        map[_posX, _posY + 1] = "=";
        map[_posX + 1, _posY + 1] = underslot;
        map[_posX + 2, _posY + 1] = "|";
        map[_posX + 3, _posY + 1] = "*";

        map[_posX - 3, _posY + 2] = "=";
        map[_posX - 2, _posY + 2] = "=";
        map[_posX - 1, _posY + 2] = "=";
        map[_posX, _posY + 2] = "=";
        map[_posX + 1, _posY + 2] = "=";
        map[_posX + 2, _posY + 2] = "=";
        map[_posX + 3, _posY + 2] = "=";

        map[_posX - 3, _posY + 3] = "*";
        map[_posX - 2, _posY + 3] = "*";
        map[_posX - 1, _posY + 3] = "*";
        map[_posX, _posY + 3] = "*";
        map[_posX + 1, _posY + 3] = "*";
        map[_posX + 2, _posY + 3] = "*";
        map[_posX + 3, _posY + 3] = "*";

        if (direction == "a")
        {
            map[_posX - 2, _posY - 1] = "=";
            map[_posX - 1, _posY - 1] = "/";
            map[_posX + 1, _posY - 1] = " ";
            map[_posX + 2, _posY - 1] = "|";

            map[_posX - 1, _posY] = "^";
            map[_posX, _posY] = spaceslot;
            map[_posX + 1, _posY] = spaceslot;
        }
        else if (direction == "d")
        {
            map[_posX - 2, _posY - 1] = "|";
            map[_posX - 1, _posY - 1] = " ";
            map[_posX + 1, _posY - 1] = "\\";
            map[_posX + 2, _posY - 1] = "=";

            map[_posX - 1, _posY] = spaceslot;
            map[_posX, _posY] = spaceslot;
            map[_posX + 1, _posY] = "^";
        }

        UpdateRow(_posY - 2);
        UpdateRow(_posY - 1);
        UpdateRow(_posY);
        UpdateRow(_posY + 1);
        UpdateRow(_posY + 2);
        UpdateRow(_posY + 3);
    }

    public void PlayEffect(string symbol)
    {
        map[_posX - 2, _posY - 1] = symbol;
        map[_posX - 1, _posY - 1] = symbol;
        map[_posX, _posY - 1] = symbol;
        map[_posX + 1, _posY - 1] = symbol;
        map[_posX + 2, _posY - 1] = symbol;

        map[_posX - 2, _posY] = symbol;
        map[_posX - 1, _posY] = symbol;
        map[_posX, _posY] = symbol;
        map[_posX + 1, _posY] = symbol;
        map[_posX + 2, _posY] = symbol;

        map[_posX - 2, _posY + 1] = symbol;
        map[_posX - 1, _posY + 1] = symbol;
        map[_posX, _posY + 1] = symbol;
        map[_posX + 1, _posY + 1] = symbol;
        map[_posX + 2, _posY + 1] = symbol;

        map[_posX - 2, _posY + 2] = symbol;
        map[_posX - 1, _posY + 2] = symbol;
        map[_posX, _posY + 2] = symbol;
        map[_posX + 1, _posY + 2] = symbol;
        map[_posX + 2, _posY + 2] = symbol;
    }

    public void PlaceZelda()
    {
        map[50, 14] = "=";
        map[51, 14] = "<";
        map[52, 14] = ">";
        map[53, 14] = "=";

        map[50, 15] = "s";
        map[51, 15] = "^";
        map[52, 15] = "^";
        map[53, 15] = "s";

        map[49, 16] = "s";
        map[50, 16] = "s";
        map[51, 16] = "~";
        map[52, 16] = "~";
        map[53, 16] = "s";
        map[54, 16] = "s";

        map[49, 16] = "~";
        map[50, 16] = "~";
        map[51, 16] = "~";
        map[52, 16] = "~";
        map[53, 16] = "~";
        map[54, 16] = "~";
    }

    public void Hit()
    {
        if (iFrames <= 0)
        {
            iFrames = 6;

            if (hasArmor)
            {
                health -= 0.5;
            }
            else
            {
                health--;
            }
            hit = true;

            StoreChar(_posX, _posY);

            PlayEffect("*");

            UpdateRow(_posY - 1);
            UpdateRow(_posY);
            UpdateRow(_posY + 1);
            UpdateRow(_posY + 2);
        }
    }

    public bool IsTouching(int posX, int posY, string symbol)
    {
        if (symbol == "/")
        {
            return map[posX, posY - 1] == "/";
        }

        if (map[posX - 2, posY - 1] == symbol || map[posX - 1, posY - 1] == symbol || map[posX, posY - 1] == symbol || map[posX + 1, posY - 1] == symbol || map[posX + 2, posY - 1] == symbol || map[posX - 2, posY] == symbol || map[posX - 1, posY] == symbol || map[posX, posY] == symbol || map[posX + 1, posY] == symbol || map[posX + 2, posY] == symbol || map[posX - 2, posY + 1] == symbol || map[posX - 1, posY + 1] == symbol || map[posX, posY + 1] == symbol || map[posX + 1, posY + 1] == symbol || map[posX + 2, posY + 1] == symbol || map[posX - 2, posY + 2] == symbol || map[posX - 1, posY + 2] == symbol || map[posX, posY + 2] == symbol || map[posX + 1, posY + 2] == symbol || map[posX + 2, posY + 2] == symbol)
        {
            if (symbol is "R" or "r")
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (map[posX - 2 + j, posY - 1 + i] is "R" or "r")
                        {
                            enemyMovement.RemoveRupee(posX - 2 + j, posY - 1 + i);
                        }
                    }
                }
            }
            return true;
        }
        return false;
    }
}
