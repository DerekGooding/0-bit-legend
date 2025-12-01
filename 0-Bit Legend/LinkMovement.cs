using _0_Bit_Legend.Model;
using static _0_Bit_Legend.MainProgram;

namespace _0_Bit_Legend;

public class LinkMovement
{
    private int _posX;
    private int _posY;

    private int _preHitPosX;
    private int _preHitPosY;

    private readonly char[] _storage_map = new string(' ', 20).ToArray();
    private static readonly char[] _storage_sword = new char[6];
    private static readonly char[] _storage_detect_enemy = new char[6];

    private Direction _prev = Direction.Up;
    private Direction _prev2 = Direction.Left;

    private bool _debounce;
    private bool _spawnRupee;

    public int PosX => _posX;
    public int PosY => _posY;
    public Direction GetPrev() => _prev;
    public Direction GetPrev2() => _prev2;
    public void SetPosX(int posX) => _posX = posX;
    public void SetPosY(int posY) => _posY = posY;
    public void SetPreHitPosX(int posX) => _preHitPosX = posX;
    public void SetPreHitPosY(int posY) => _preHitPosY = posY;
    public void SetPrev(Direction prev) => _prev = prev;
    public void SetSpawnRupee(bool spawnRupee) => _spawnRupee = spawnRupee;

    public int MovementWait;

    public void Attack(Direction direction, bool attacking)
    {
        switch (direction)
        {
            case Direction.Up:
                HandleAttackUp(attacking);
                break;
            case Direction.Left:
                HandleAttackLeft(attacking);
                break;
            case Direction.Down:
                HandleAttackDown(attacking);
                break;
            case Direction.Right:
                HandleAttackRight(attacking);
                break;
        }
    }

    private void HandleAttackUp(bool attacking)
    {
        //if (_posY > 3)
        //    return;
        if (!attacking)
        {
            _storage_sword[0] = Map[_posX - 1, _posY - 2];
            _storage_sword[1] = Map[_posX, _posY - 2];
            _storage_sword[2] = Map[_posX + 1, _posY - 2];
            _storage_sword[3] = Map[_posX, _posY - 3];
            _storage_sword[4] = Map[_posX, _posY - 4];

            Map[_posX - 1, _posY - 2] = '-';
            Map[_posX, _posY - 2] = '-';
            Map[_posX + 1, _posY - 2] = '-';
            Map[_posX, _posY - 3] = 'S';
            Map[_posX, _posY - 4] = 'S';

            _preHitPosX = _posX;
            _preHitPosY = _posY;
        }
        else
        {
            _storage_detect_enemy[0] = Map[_preHitPosX - 1, _preHitPosY - 2];
            _storage_detect_enemy[1] = Map[_preHitPosX, _preHitPosY - 2];
            _storage_detect_enemy[2] = Map[_preHitPosX + 1, _preHitPosY - 2];
            _storage_detect_enemy[3] = Map[_preHitPosX, _preHitPosY - 3];
            _storage_detect_enemy[4] = Map[_preHitPosX, _preHitPosY - 4];

            var swordArr = new int[2, 5];
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

            Stab(swordArr, Direction.Up, 5, 1);
        }

        UpdateRow(_preHitPosY - 2);
        UpdateRow(_preHitPosY - 3);
        UpdateRow(_preHitPosY - 4);
    }
    private void HandleAttackLeft(bool attacking)
    {
        //if(_posX > 4)
        //    return;
        if (!attacking)
        {
            _storage_sword[0] = Map[_posX - 3, _posY];
            _storage_sword[1] = Map[_posX - 3, _posY + 1];
            _storage_sword[2] = Map[_posX - 3, _posY + 2];
            _storage_sword[3] = Map[_posX - 4, _posY + 1];
            _storage_sword[4] = Map[_posX - 5, _posY + 1];
            _storage_sword[5] = Map[_posX - 6, _posY + 1];

            Map[_posX - 3, _posY] = '-';
            Map[_posX - 3, _posY + 1] = '-';
            Map[_posX - 3, _posY + 2] = '-';
            Map[_posX - 4, _posY + 1] = 'S';
            Map[_posX - 5, _posY + 1] = 'S';
            Map[_posX - 6, _posY + 1] = 'S';

            _preHitPosX = _posX;
            _preHitPosY = _posY;
        }
        else
        {
            _storage_detect_enemy[0] = Map[_preHitPosX - 3, _preHitPosY];
            _storage_detect_enemy[1] = Map[_preHitPosX - 3, _preHitPosY + 1];
            _storage_detect_enemy[2] = Map[_preHitPosX - 3, _preHitPosY + 2];
            _storage_detect_enemy[3] = Map[_preHitPosX - 4, _preHitPosY + 1];
            _storage_detect_enemy[4] = Map[_preHitPosX - 5, _preHitPosY + 1];
            _storage_detect_enemy[5] = Map[_preHitPosX - 6, _preHitPosY + 1];

            var swordArr = new int[2, 6];
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

            Stab(swordArr, Direction.Left, 6, 1);
        }

        UpdateRow(_preHitPosY);
        UpdateRow(_preHitPosY + 1);
        UpdateRow(_preHitPosY + 2);
    }
    private void HandleAttackDown(bool attacking)
    {
        //if (_posY + 4 < 33)
        //    return;
        if (!attacking)
        {
            _storage_sword[0] = Map[_posX - 1, _posY + 3];
            _storage_sword[1] = Map[_posX, _posY + 3];
            _storage_sword[2] = Map[_posX + 1, _posY + 3];
            _storage_sword[3] = Map[_posX, _posY + 4];
            _storage_sword[4] = Map[_posX, _posY + 5];

            Map[_posX - 1, _posY + 3] = '-';
            Map[_posX, _posY + 3] = '-';
            Map[_posX + 1, _posY + 3] = '-';
            Map[_posX, _posY + 4] = 'S';
            Map[_posX, _posY + 5] = 'S';

            _preHitPosX = _posX;
            _preHitPosY = _posY;
        }
        else
        {
            _storage_detect_enemy[0] = Map[_preHitPosX - 1, _preHitPosY + 3];
            _storage_detect_enemy[1] = Map[_preHitPosX, _preHitPosY + 3];
            _storage_detect_enemy[2] = Map[_preHitPosX + 1, _preHitPosY + 3];
            _storage_detect_enemy[3] = Map[_preHitPosX, _preHitPosY + 4];
            _storage_detect_enemy[4] = Map[_preHitPosX, _preHitPosY + 5];

            var swordArr = new int[2, 5];
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

            Stab(swordArr, Direction.Down, 5, 1);
        }

        UpdateRow(_preHitPosY + 3);
        UpdateRow(_preHitPosY + 4);
        UpdateRow(_preHitPosY + 5);
    }
    private void HandleAttackRight(bool attacking)
    {
        //if (_posX + 6 < 102)
        //    return;
        if (!attacking)
        {
            _storage_sword[0] = Map[_posX + 3, _posY];
            _storage_sword[1] = Map[_posX + 3, _posY + 1];
            _storage_sword[2] = Map[_posX + 3, _posY + 2];
            _storage_sword[3] = Map[_posX + 4, _posY + 1];
            _storage_sword[4] = Map[_posX + 5, _posY + 1];
            _storage_sword[5] = Map[_posX + 6, _posY + 1];

            Map[_posX + 3, _posY] = '-';
            Map[_posX + 3, _posY + 1] = '-';
            Map[_posX + 3, _posY + 2] = '-';
            Map[_posX + 4, _posY + 1] = 'S';
            Map[_posX + 5, _posY + 1] = 'S';
            Map[_posX + 6, _posY + 1] = 'S';

            _preHitPosX = _posX;
            _preHitPosY = _posY;
        }
        else
        {
            _storage_detect_enemy[0] = Map[_preHitPosX + 3, _preHitPosY];
            _storage_detect_enemy[1] = Map[_preHitPosX + 3, _preHitPosY + 1];
            _storage_detect_enemy[2] = Map[_preHitPosX + 3, _preHitPosY + 2];
            _storage_detect_enemy[3] = Map[_preHitPosX + 4, _preHitPosY + 1];
            _storage_detect_enemy[4] = Map[_preHitPosX + 5, _preHitPosY + 1];
            _storage_detect_enemy[5] = Map[_preHitPosX + 6, _preHitPosY + 1];

            var swordArr = new int[2, 6];
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

            Stab(swordArr, Direction.Right, 6, 1);
        }

        UpdateRow(_preHitPosY);
        UpdateRow(_preHitPosY + 1);
        UpdateRow(_preHitPosY + 2);
    }

    public void Stab(int[,] swordArr, Direction prev, int amt, int dmg)
    {
        var hit = false;
        for (var i = 0; i < amt; i++)
        {
            char[] detect = ['t', 'n', 'B', '{', '}', 'F'];
            if (detect.Any(x => x == _storage_sword[i]) || detect.Any(x => x == _storage_detect_enemy[i]))
            {
                hit = true;
                if (MainProgram.EnemyMovement.TakeDamage(swordArr[0, i], swordArr[1, i], prev, dmg) && _spawnRupee)
                {
                    _spawnRupee = false;
                    MainProgram.EnemyMovement.SpawnRupee();
                }
                break;
            }
        }
        if (!hit)
        {
            StoreSword(prev);
        }
    }

    public void StoreSword(Direction prev)
    {
        char[] convert = ['t', '^', 'n', '0', 'B', '{', '}', 'F', 'S', '>', '*'];
        for (var i = 0; i < 6; i++)
        {
            if (convert.Any(x => x == _storage_sword[i]))
            {
                _storage_sword[i] = ' ';
            }
        }

        if (prev == Direction.Up)
        {
            Map[_preHitPosX - 1, _preHitPosY - 2] = _storage_sword[0];
            Map[_preHitPosX, _preHitPosY - 2] = _storage_sword[1];
            Map[_preHitPosX + 1, _preHitPosY - 2] = _storage_sword[2];
            Map[_preHitPosX, _preHitPosY - 3] = _storage_sword[3];
            Map[_preHitPosX, _preHitPosY - 4] = _storage_sword[4];
        }
        else if (prev == Direction.Left)
        {
            Map[_preHitPosX - 3, _preHitPosY] = _storage_sword[0];
            Map[_preHitPosX - 3, _preHitPosY + 1] = _storage_sword[1];
            Map[_preHitPosX - 3, _preHitPosY + 2] = _storage_sword[2];
            Map[_preHitPosX - 4, _preHitPosY + 1] = _storage_sword[3];
            Map[_preHitPosX - 5, _preHitPosY + 1] = _storage_sword[4];
            Map[_preHitPosX - 6, _preHitPosY + 1] = _storage_sword[5];
        }
        else if (prev == Direction.Down)
        {
            Map[_preHitPosX - 1, _preHitPosY + 3] = _storage_sword[0];
            Map[_preHitPosX, _preHitPosY + 3] = _storage_sword[1];
            Map[_preHitPosX + 1, _preHitPosY + 3] = _storage_sword[2];
            Map[_preHitPosX, _preHitPosY + 4] = _storage_sword[3];
            Map[_preHitPosX, _preHitPosY + 5] = _storage_sword[4];
        }
        else if (prev == Direction.Right)
        {
            Map[_preHitPosX + 3, _preHitPosY] = _storage_sword[0];
            Map[_preHitPosX + 3, _preHitPosY + 1] = _storage_sword[1];
            Map[_preHitPosX + 3, _preHitPosY + 2] = _storage_sword[2];
            Map[_preHitPosX + 4, _preHitPosY + 1] = _storage_sword[3];
            Map[_preHitPosX + 5, _preHitPosY + 1] = _storage_sword[4];
            Map[_preHitPosX + 6, _preHitPosY + 1] = _storage_sword[5];
        }
    }

    public void MoveUp(int posX, int posY)
    {
        _prev = Direction.Up;
        if (_posX == 21 && ((CurrentMap == 4 && posY > 9) || CurrentMap == 2))
        {
            if (posY > 1)
            {
                for (var y = _posY - 2; y <= _posY + 3; y++)
                {
                    for (var x = _posX - 3; x <= _posX + 3; x++)
                    {
                        Map[x, y] = '~';
                    }
                }

                _posY--;
                DeployRaft(_prev2);

                UpdateRow(_posY + 4);
            }
            else
            {
                LoadMap(4, 21, 29, Direction.Up);
            }
        }
        else if (posY >= 1 && !(_posX == 21 && (CurrentMap == 4 || CurrentMap == 2)))
        {
            IsTouching(posX, posY, 'r');
            StoreChar(_posX, _posY);
            var inCave = false;

            if (CurrentMap == 6 && (IsTouching(posX, posY, '-') || IsTouching(posX, posY, 'S')))
            {
                SetFlag(GameFlag.HasSword, true);
                LoadMap(6, posX, posY, Direction.Up);
            }
            else if (CurrentMap == 7 && IsTouching(posX, posY, '*') && Rupees >= 35)
            {
                Rupees -= 35;

                SetFlag(GameFlag.HasRaft, true);
                LoadMap(7, posX, posY, Direction.Up);
            }
            else if (CurrentMap == 7 && IsTouching(posX, posY, 'Y') && Rupees >= 5)
            {
                Rupees -= 10;
                Keys++;
            }
            else if (CurrentMap == 7 && IsTouching(posX, posY, '#') && Rupees >= 15)
            {
                Rupees -= 25;

                SetFlag(GameFlag.HasArmor, true);
                LoadMap(7, posX, posY, Direction.Up);
            }
            else if (CurrentMap == 9
                && IsTouching(posX, posY, 'X')
                && !IsTouching(posX, posY, '=')
                && HasFlags([GameFlag.Door1, GameFlag.Door2, GameFlag.Door3])
                && cEnemies1 <= 0
                && cEnemies2 <= 0
                && !_debounce)
            {
                LoadMap(12, 50, 24, Direction.Up);
            }
            else if (CurrentMap == 9 && _posX >= 48 && _posX <= 52 && _posY == 7 && !HasFlag(GameFlag.Door3) && Keys > 0)
            {
                _debounce = true;
                Keys--;

                SetFlag(GameFlag.Door3, true);
                LoadMap(9, _posX, _posY, Direction.Up);
            }

            if (!IsTouching(posX, posY, '=')
                && !IsTouching(posX, posY, 'X')
                && !IsTouching(posX, posY, 't')
                && !IsTouching(posX, posY, 'n')
                && !IsTouching(posX, posY, 'B')
                && !IsTouching(posX, posY, '{')
                && !IsTouching(posX, posY, '}')
                && !IsTouching(posX, posY, 'S')
                && !IsTouching(posX, posY, '<')
                && !IsTouching(posX, posY, '>')
                && !IsTouching(posX, posY, '*')
                && !(IsTouching(posX, posY, 'F') && CurrentMap != 7)
                && !IsTouching(posX, posY, '~')
                && !((CurrentMap == 6 || CurrentMap == 7) && posY < 17)
                && ((CurrentMap >= 9 && !IsTouching(posX, posY, '/')) || CurrentMap < 9))
            {
                if (IsTouching(posX, posY, '/'))
                {
                    inCave = true;
                }

                StoreChar(posX, posY);
                BuildChar(posX, posY, Direction.Up);

                UpdateRow(posY - 1);
                UpdateRow(posY);
                UpdateRow(posY + 1);
                UpdateRow(posY + 2);

                _posX = posX;
                _posY = posY;
            }
            else if (IsTouching(posX, posY, 't')
                || IsTouching(posX, posY, 'n')
                || IsTouching(posX, posY, 'B')
                || IsTouching(posX, posY, '{')
                || IsTouching(posX, posY, '}')
                || IsTouching(posX, posY, 'S')
                || IsTouching(posX, posY, '<')
                || (IsTouching(posX, posY, 'F') && CurrentMap != 7))
            {
                BuildChar(_posX, _posY, Direction.Up);

                UpdateRow(_posY - 1);
                UpdateRow(_posY);
                UpdateRow(_posY + 1);
                UpdateRow(_posY + 2);

                Hit();
            }
            else if (CurrentMap == 13 && IsTouching(posX, posY, '~'))
            {
                BuildChar(_posX, _posY, Direction.Up);

                UpdateRow(_posY - 1);
                UpdateRow(_posY);
                UpdateRow(_posY + 1);
                UpdateRow(_posY + 2);

                LoadMap(13, 58, 15, Direction.Left);
                SetFlag(GameFlag.GameOver, true);
            }
            else
            {
                BuildChar(_posX, _posY, Direction.Up);

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
            if (CurrentMap == 0)
            {
                LoadMap(1, 63, 29, Direction.Up);
            }
            else if (CurrentMap == 2)
            {
                if (posX > 29)
                {
                    LoadMap(4, 55, 30, Direction.Up);
                }
                else if (posX == 21)
                {
                    LoadMap(4, 21, 29, Direction.Up);
                }
                else
                {
                    LoadMap(4, 10, 29, Direction.Up);
                }
            }
            else if (CurrentMap == 3)
            {
                LoadMap(5, 49, 30, Direction.Up);
            }
        }
        if (HasFlag(GameFlag.Text))
        {
            SetFlag(GameFlag.Text, false);
            LoadMap(9, posX, posY, Direction.Up);
        }

        _debounce = false;
    }
    public void MoveLeft(int posX, int posY)
    {
        _prev = Direction.Left;
        _prev2 = Direction.Left;
        if (posX >= 2)
        {
            IsTouching(posX, posY, 'r');
            StoreChar(_posX, _posY);

            if (CurrentMap == 6 && (IsTouching(posX, posY, '-') || IsTouching(posX, posY, 'S')))
            {
                SetFlag(GameFlag.HasSword, true);
                LoadMap(6, posX, posY, Direction.Left);
            }
            else if (CurrentMap == 7 && IsTouching(posX, posY, '*') && Rupees >= 35)
            {
                Rupees -= 35;

                SetFlag(GameFlag.HasRaft, true);
                LoadMap(7, posX, posY, Direction.Left);
            }
            else if (CurrentMap == 7 && IsTouching(posX, posY, 'Y') && Rupees >= 10)
            {
                Rupees -= 10;
                Keys++;
            }
            else if (CurrentMap == 7 && IsTouching(posX, posY, '#') && Rupees >= 25)
            {
                Rupees -= 25;

                SetFlag(GameFlag.HasArmor, true);
                LoadMap(7, posX, posY, Direction.Left);
            }
            else if (CurrentMap == 9
                && IsTouching(posX, posY, 'X')
                && !IsTouching(posX, posY, '=')
                && HasFlag(GameFlag.Door1)
                && !_debounce)
            {
                LoadMap(10, 87, 15, Direction.Left);
            }
            else if (CurrentMap == 9 && _posX == 14 && _posY >= 14 && _posY <= 16 && !HasFlag(GameFlag.Door1) && Keys > 0)
            {
                _debounce = true;
                Keys--;

                SetFlag(GameFlag.Door1, true);
                LoadMap(9, _posX, _posY, Direction.Left);
            }

            if (!IsTouching(posX, posY, '=')
                && !IsTouching(posX, posY, 'X')
                && !IsTouching(posX, posY, 't')
                && !IsTouching(posX, posY, 'n')
                && !IsTouching(posX, posY, 'B')
                && !IsTouching(posX, posY, '{')
                && !IsTouching(posX, posY, '}')
                && !IsTouching(posX, posY, 'S')
                && !IsTouching(posX, posY, '<')
                && !(IsTouching(posX, posY, 'F') && CurrentMap != 7)
                && !IsTouching(posX, posY, '~')
                && ((CurrentMap >= 9 && !IsTouching(posX, posY, '/')) || CurrentMap < 9))
            {
                StoreChar(posX, posY);
                BuildChar(posX, posY, Direction.Left);

                UpdateRow(posY - 1);
                UpdateRow(posY);
                UpdateRow(posY + 1);
                UpdateRow(posY + 2);

                _posX = posX;
                _posY = posY;
            }
            else if (IsTouching(posX, posY, 't')
                || IsTouching(posX, posY, 'n')
                || IsTouching(posX, posY, 'B')
                || IsTouching(posX, posY, '{')
                || IsTouching(posX, posY, '}')
                || IsTouching(posX, posY, 'S')
                || IsTouching(posX, posY, '<')
                || (IsTouching(posX, posY, 'F') && CurrentMap != 7))
            {
                BuildChar(_posX, _posY, Direction.Left);

                UpdateRow(_posY - 1);
                UpdateRow(_posY);
                UpdateRow(_posY + 1);
                UpdateRow(_posY + 2);

                Hit();
            }
            else if (IsTouching(posX, posY, '~')
                && _posX != 21
                && HasFlag(GameFlag.HasRaft)
                && !IsTouching(posX, posY, '=')
                && !IsTouching(posX, posY, 'X')
                && !IsTouching(posX, posY, 't')
                && !IsTouching(posX, posY, 'n'))
            {
                StoreChar(_posX, _posY);

                UpdateRow(_posY - 1);
                UpdateRow(_posY);
                UpdateRow(_posY + 1);
                UpdateRow(_posY + 2);

                _posX = 21;
                DeployRaft(Direction.Left);
                wait = 150;
            }
            else if (_posX == 21 && ((posY > 11 && CurrentMap == 4)
                || CurrentMap == 2) && ((CurrentMap == 2 && posY < 25) || CurrentMap == 4))
            {
                BuildChar(_posX, _posY, Direction.Left);

                UpdateRow(_posY - 1);
                UpdateRow(_posY);
                UpdateRow(_posY + 1);
                UpdateRow(_posY + 2);

                for (var y = _posY - 2; y <= _posY + 3; y++)
                {
                    for (var x = _posX - 3; x <= _posX + 3; x++)
                    {
                        Map[x, y] = '~';
                    }
                }

                _posX = 11;
                posX = 11;

                BuildChar(posX, posY, Direction.Left);

                UpdateRow(posY - 2);
                UpdateRow(posY - 1);
                UpdateRow(posY);
                UpdateRow(posY + 1);
                UpdateRow(posY + 2);
                UpdateRow(posY + 3);
            }
            else if (CurrentMap == 11 && IsTouching(posX, posY, 'X') && !IsTouching(posX, posY, '='))
            {
                LoadMap(9, 86, 15, Direction.Left);
            }
            else if (CurrentMap == 13 && IsTouching(posX, posY, 'X') && !IsTouching(posX, posY, '='))
            {
                LoadMap(12, 86, 15, Direction.Left);
            }
            else
            {
                BuildChar(_posX, _posY, Direction.Left);

                UpdateRow(_posY - 1);
                UpdateRow(_posY);
                UpdateRow(_posY + 1);
                UpdateRow(_posY + 2);
            }

        }
        else
        {
            if (CurrentMap == 0)
            {
                LoadMap(2, 99, 12, Direction.Left);
            }
            else if (CurrentMap == 3)
            {
                LoadMap(0, 98, 17, Direction.Left);
            }
            else if (CurrentMap == 1)
            {
                LoadMap(4, 98, 13, Direction.Left);
            }
            else if (CurrentMap == 5)
            {
                LoadMap(1, 98, 16, Direction.Left);
            }
            else if (CurrentMap == 4)
            {
                LoadMap(8, 99, 16, Direction.Left);
            }
        }
        if (HasFlag(GameFlag.Text))
        {
            SetFlag(GameFlag.Text, false);
            LoadMap(9, posX, posY, Direction.Up);
        }

        _debounce = false;
    }
    public void MoveDown(int posX, int posY)
    {
        _prev = Direction.Down;
        if ((CurrentMap == 2 || CurrentMap == 4) && posX == 21)
        {
            if ((posY < 30) && ((CurrentMap == 2 && posY < 27) || CurrentMap == 4))
            {
                for (var y = _posY - 2; y <= _posY + 3; y++)
                {
                    for (var x = _posX - 3; x <= _posX + 3; x++)
                    {
                        Map[x, y] = '~';
                    }
                }

                _posY++;
                DeployRaft(_prev2);

                UpdateRow(_posY - 3);
            }
            else if (CurrentMap == 4)
            {
                LoadMap(2, 21, 2, Direction.Down);
            }
        }
        else if (posY <= 29)
        {
            if (//!IsTouching(posX, posY, '=')
                !IsTouching(posX, posY, 'X')
                && !IsTouching(posX, posY, 't')
                && !IsTouching(posX, posY, 'n')
                && !IsTouching(posX, posY, 'B')
                && !IsTouching(posX, posY, '{')
                && !IsTouching(posX, posY, '}')
                && !IsTouching(posX, posY, 'S')
                && !IsTouching(posX, posY, '<')
                && !(IsTouching(posX, posY, 'F') && CurrentMap != 7)
                && !IsTouching(posX, posY, '~')
                && ((CurrentMap >= 9 && !IsTouching(posX, posY, '/')) || CurrentMap < 9))
            {
                StoreChar(posX, posY);
                BuildChar(posX, posY, Direction.Down);

                UpdateRow(posY - 1);
                UpdateRow(posY);
                UpdateRow(posY + 1);
                UpdateRow(posY + 2);

                _posX = posX;
                _posY = posY;
            }
            else if (IsTouching(posX, posY, 't')
                || IsTouching(posX, posY, 'n')
                || IsTouching(posX, posY, 'B')
                || IsTouching(posX, posY, '{')
                || IsTouching(posX, posY, '}')
                || IsTouching(posX, posY, 'S')
                || IsTouching(posX, posY, '<')
                || (IsTouching(posX, posY, 'F') && CurrentMap != 7))
            {
                BuildChar(_posX, _posY, Direction.Down);

                UpdateRow(_posY - 1);
                UpdateRow(_posY);
                UpdateRow(_posY + 1);
                UpdateRow(_posY + 2);

                Hit();
            }
            else if (CurrentMap == 12 && IsTouching(posX, posY, 'X') && !IsTouching(posX, posY, '='))
            {
                BuildChar(_posX, _posY, Direction.Down);

                UpdateRow(_posY - 1);
                UpdateRow(_posY);
                UpdateRow(_posY + 1);
                UpdateRow(_posY + 2);

                LoadMap(9, 50, 9, Direction.Down);
            }
            else
            {
                BuildChar(_posX, _posY, Direction.Down);

                UpdateRow(_posY - 1);
                UpdateRow(_posY);
                UpdateRow(_posY + 1);
                UpdateRow(_posY + 2);
            }
        }
        else
        {
            if (CurrentMap == 1)
            {
                LoadMap(0, 63, 1, Direction.Down);
            }
            else if (CurrentMap == 4)
            {
                if (posX > 29)
                {
                    LoadMap(2, 55, 1, Direction.Down);
                }
                else if (posX == 21)
                {
                    LoadMap(2, 21, 2, Direction.Down);
                }
                else
                {
                    LoadMap(2, 10, 2, Direction.Down);
                }
            }
            else if (CurrentMap == 5)
            {
                LoadMap(3, 49, 2, Direction.Down);
            }
            else if (CurrentMap == 6)
            {
                LoadMap(0, 16, 6, Direction.Down);
                Wait(2);
            }
            else if (CurrentMap == 7)
            {
                LoadMap(4, 86, 7, Direction.Down);
                Wait(2);
            }
            else if (CurrentMap == 9)
            {
                LoadMap(8, 51, 17, Direction.Down);
                Wait(2);
            }
        }
        SetFlag(GameFlag.Text, false);
        _debounce = false;
    }
    public void MoveRight(int posX, int posY)
    {
        _prev = Direction.Right;
        _prev2 = Direction.Right;
        if (posX <= 99)
        {
            IsTouching(posX, posY, 'r');
            StoreChar(_posX, _posY);

            var persist = true;
            if (CurrentMap == 6 && (IsTouching(posX, posY, '-') || IsTouching(posX, posY, 's')))
            {
                SetFlag(GameFlag.HasSword, true);
                LoadMap(6, posX, posY, Direction.Right);
            }
            else if (CurrentMap == 7 && IsTouching(posX, posY, '*') && Rupees >= 35)
            {
                Rupees -= 35;

                SetFlag(GameFlag.HasRaft, true);
                LoadMap(7, posX, posY, Direction.Right);
            }
            else if (CurrentMap == 7 && IsTouching(posX, posY, 'Y') && Rupees >= 10)
            {
                Rupees -= 10;
                Keys++;
            }
            else if (CurrentMap == 7 && IsTouching(posX, posY, '#') && Rupees >= 25)
            {
                Rupees -= 25;

                SetFlag(GameFlag.HasArmor, true);
                LoadMap(7, posX, posY, Direction.Right);
            }
            else if (CurrentMap == 9
                && IsTouching(posX, posY, 'X')
                && !IsTouching(posX, posY, '=')
                && HasFlag(GameFlag.Door2)
                && !_debounce)
            {
                persist = false;
                LoadMap(11, 14, 15, Direction.Right);
            }
            else if (CurrentMap == 9 && _posX == 86 && _posY >= 14 && _posY <= 16 && !HasFlag(GameFlag.Door2) && Keys > 0)
            {
                _debounce = true;
                Keys--;

                SetFlag(GameFlag.Door2, true);
                LoadMap(9, _posX, _posY, Direction.Right);
            }

            if (!IsTouching(posX, posY, '=')
                && !IsTouching(posX, posY, 'X')
                && !IsTouching(posX, posY, 't')
                && !IsTouching(posX, posY, 'n')
                && !IsTouching(posX, posY, 'B')
                && !IsTouching(posX, posY, '{')
                && !IsTouching(posX, posY, '}')
                && !IsTouching(posX, posY, 'S')
                && !IsTouching(posX, posY, '<')
                && !(IsTouching(posX, posY, 'F') && CurrentMap != 7)
                && !IsTouching(posX, posY, '~')
                && ((CurrentMap >= 9 && !IsTouching(posX, posY, '/')) || CurrentMap < 9)
                && persist)
            {
                StoreChar(posX, posY);
                BuildChar(posX, posY, Direction.Right);

                UpdateRow(posY - 1);
                UpdateRow(posY);
                UpdateRow(posY + 1);
                UpdateRow(posY + 2);

                _posX = posX;
                _posY = posY;
            }
            else if (IsTouching(posX, posY, 't')
                || IsTouching(posX, posY, 'n')
                || IsTouching(posX, posY, 'B')
                || IsTouching(posX, posY, '{')
                || IsTouching(posX, posY, '}')
                || IsTouching(posX, posY, 'S')
                || IsTouching(posX, posY, '<')
                || (IsTouching(posX, posY, 'F') && CurrentMap != 7))
            {
                BuildChar(_posX, _posY, Direction.Right);

                UpdateRow(_posY - 1);
                UpdateRow(_posY);
                UpdateRow(_posY + 1);
                UpdateRow(_posY + 2);

                Hit();
            }
            else if (IsTouching(posX, posY, '~')
                && _posX != 21
                && HasFlag(GameFlag.HasRaft)
                && !IsTouching(posX, posY, '=')
                && !IsTouching(posX, posY, 'X')
                && !IsTouching(posX, posY, 't')
                && !IsTouching(posX, posY, 'n'))
            {
                StoreChar(_posX, _posY);

                UpdateRow(_posY - 1);
                UpdateRow(_posY);
                UpdateRow(_posY + 1);
                UpdateRow(_posY + 2);

                _posX = 21;
                DeployRaft(Direction.Right);
                wait = 150;
            }
            else if (_posX == 21 && posY < 25 && ((posY > 3 && CurrentMap == 2) || (posY < 25 && CurrentMap == 4)))
            {
                BuildChar(_posX, _posY, Direction.Right);

                UpdateRow(_posY - 1);
                UpdateRow(_posY);
                UpdateRow(_posY + 1);
                UpdateRow(_posY + 2);

                for (var y = _posY - 2; y <= _posY + 3; y++)
                {
                    for (var x = _posX - 3; x <= _posX + 3; x++)
                    {
                        Map[x, y] = '~';
                    }
                }

                _posX = 30;
                posX = 30;

                BuildChar(posX, posY, Direction.Right);

                UpdateRow(posY - 2);
                UpdateRow(posY - 1);
                UpdateRow(posY);
                UpdateRow(posY + 1);
                UpdateRow(posY + 2);
                UpdateRow(posY + 3);
            }
            else if (CurrentMap == 10 && IsTouching(posX, posY, 'X') && !IsTouching(posX, posY, '='))
            {
                LoadMap(9, 14, 15, Direction.Right);
            }
            else if (CurrentMap == 12 && IsTouching(posX, posY, 'X') && !IsTouching(posX, posY, '='))
            {
                LoadMap(13, 15, 15, Direction.Right);
            }
            else
            {
                BuildChar(_posX, _posY, Direction.Right);

                UpdateRow(_posY - 1);
                UpdateRow(_posY);
                UpdateRow(_posY + 1);
                UpdateRow(_posY + 2);
            }
        }
        else
        {
            if (CurrentMap == 2)
            {
                LoadMap(0, 4, 12, Direction.Right);
            }
            else if (CurrentMap == 0)
            {
                LoadMap(3, 2, 18, Direction.Right);
            }
            else if (CurrentMap == 4)
            {
                LoadMap(1, 2, 13, Direction.Right);
            }
            else if (CurrentMap == 1)
            {
                LoadMap(5, 2, 15, Direction.Right);
            }
            else if (CurrentMap == 8)
            {
                LoadMap(4, 2, 16, Direction.Right);
            }
        }
        if (HasFlag(GameFlag.Text))
        {
            SetFlag(GameFlag.Text, false);
            LoadMap(9, posX, posY, Direction.Up);
        }
        _debounce = false;
    }

    public void SpawnLink(int posX, int posY, Direction direction)
    {
        _posX = posX;
        _posY = posY;

        _storage_map[0] = Map[posX - 2, posY - 1];
        _storage_map[1] = Map[posX - 1, posY - 1];
        _storage_map[2] = Map[posX, posY - 1];
        _storage_map[3] = Map[posX + 1, posY - 1];
        _storage_map[4] = Map[posX + 2, posY - 1];

        _storage_map[5] = Map[posX - 2, posY];
        _storage_map[6] = Map[posX - 1, posY];
        _storage_map[7] = Map[posX, posY];
        _storage_map[8] = Map[posX + 1, posY];
        _storage_map[9] = Map[posX + 2, posY];

        _storage_map[10] = Map[posX - 2, posY + 1];
        _storage_map[11] = Map[posX - 1, posY + 1];
        _storage_map[12] = Map[posX, posY + 1];
        _storage_map[13] = Map[posX + 1, posY + 1];
        _storage_map[14] = Map[posX + 2, posY + 1];

        _storage_map[15] = Map[posX - 2, posY + 2];
        _storage_map[16] = Map[posX - 1, posY + 2];
        _storage_map[17] = Map[posX, posY + 2];
        _storage_map[18] = Map[posX + 1, posY + 2];
        _storage_map[19] = Map[posX + 2, posY + 2];

        switch (direction)
        {
            case Direction.Up:
                MoveUp(posX, posY);
                break;
            case Direction.Down:
                MoveDown(posX, posY);
                break;
            case Direction.Left:
                MoveLeft(posX, posY);
                break;
            case Direction.Right:
                MoveRight(posX, posY);
                break;
        }
    }

    public void BuildChar(int posX, int posY, Direction direction)
    {
        var spaceslot = ' ';
        var underslot = '_';
        if (HasFlag(GameFlag.HasArmor))
        {
            spaceslot = '#';
            underslot = '#';
        }

        if (direction == Direction.Up)
        {
            Map[posX - 2, posY - 1] = ' ';
            Map[posX - 1, posY - 1] = '_';
            Map[posX, posY - 1] = '_';
            Map[posX + 1, posY - 1] = '_';
            Map[posX + 2, posY - 1] = ' ';

            Map[posX - 2, posY] = '|';
            Map[posX - 1, posY] = spaceslot;
            Map[posX, posY] = '=';
            Map[posX + 1, posY] = spaceslot;
            Map[posX + 2, posY] = '|';

            Map[posX - 2, posY + 1] = '|';
            Map[posX - 1, posY + 1] = '^';
            Map[posX, posY + 1] = spaceslot;
            Map[posX + 1, posY + 1] = '^';
            Map[posX + 2, posY + 1] = '|';

            Map[posX - 2, posY + 2] = ' ';
            Map[posX - 1, posY + 2] = '\\';
            Map[posX, posY + 2] = '_';
            Map[posX + 1, posY + 2] = '/';
            Map[posX + 2, posY + 2] = ' ';
        }
        else if (direction == Direction.Left)
        {
            Map[posX - 2, posY - 1] = ' ';
            Map[posX - 1, posY - 1] = ' ';
            Map[posX, posY - 1] = '/';
            Map[posX + 1, posY - 1] = '\\';
            Map[posX + 2, posY - 1] = ' ';

            Map[posX - 2, posY] = ' ';
            Map[posX - 1, posY] = '/';
            Map[posX, posY] = ' ';
            Map[posX + 1, posY] = ' ';
            Map[posX + 2, posY] = '|';

            Map[posX - 2, posY + 1] = '|';
            Map[posX - 1, posY + 1] = '^';
            Map[posX, posY + 1] = spaceslot;
            Map[posX + 1, posY + 1] = spaceslot;
            Map[posX + 2, posY + 1] = '|';

            Map[posX - 2, posY + 2] = '|';
            Map[posX - 1, posY + 2] = underslot;
            Map[posX, posY + 2] = '=';
            Map[posX + 1, posY + 2] = underslot;
            Map[posX + 2, posY + 2] = '|';
        }
        else if (direction == Direction.Down)
        {
            Map[posX - 2, posY - 1] = ' ';
            Map[posX - 1, posY - 1] = ' ';
            Map[posX, posY - 1] = '_';
            Map[posX + 1, posY - 1] = ' ';
            Map[posX + 2, posY - 1] = ' ';

            Map[posX - 2, posY] = ' ';
            Map[posX - 1, posY] = '/';
            Map[posX, posY] = ' ';
            Map[posX + 1, posY] = '\\';
            Map[posX + 2, posY] = ' ';

            Map[posX - 2, posY + 1] = '|';
            Map[posX - 1, posY + 1] = '^';
            Map[posX, posY + 1] = spaceslot;
            Map[posX + 1, posY + 1] = '^';
            Map[posX + 2, posY + 1] = '|';

            Map[posX - 2, posY + 2] = '|';
            Map[posX - 1, posY + 2] = underslot;
            Map[posX, posY + 2] = '=';
            Map[posX + 1, posY + 2] = underslot;
            Map[posX + 2, posY + 2] = '|';
        }
        else if (direction == Direction.Right)
        {
            Map[posX - 2, posY - 1] = ' ';
            Map[posX - 1, posY - 1] = '/';
            Map[posX, posY - 1] = '\\';
            Map[posX + 1, posY - 1] = ' ';
            Map[posX + 2, posY - 1] = ' ';

            Map[posX - 2, posY] = '|';
            Map[posX - 1, posY] = ' ';
            Map[posX, posY] = ' ';
            Map[posX + 1, posY] = '\\';
            Map[posX + 2, posY] = ' ';

            Map[posX - 2, posY + 1] = '|';
            Map[posX - 1, posY + 1] = spaceslot;
            Map[posX, posY + 1] = spaceslot;
            Map[posX + 1, posY + 1] = '^';
            Map[posX + 2, posY + 1] = '|';

            Map[posX - 2, posY + 2] = '|';
            Map[posX - 1, posY + 2] = underslot;
            Map[posX, posY + 2] = '=';
            Map[posX + 1, posY + 2] = underslot;
            Map[posX + 2, posY + 2] = '|';
        }
    }

    public void StoreChar(int posX, int posY)
    {
        Map[_posX - 2, _posY - 1] = _storage_map[0];
        Map[_posX - 1, _posY - 1] = _storage_map[1];
        Map[_posX, _posY - 1] = _storage_map[2];
        Map[_posX + 1, _posY - 1] = _storage_map[3];
        Map[_posX + 2, _posY - 1] = _storage_map[4];

        Map[_posX - 2, _posY] = _storage_map[5];
        Map[_posX - 1, _posY] = _storage_map[6];
        Map[_posX, _posY] = _storage_map[7];
        Map[_posX + 1, _posY] = _storage_map[8];
        Map[_posX + 2, _posY] = _storage_map[9];

        Map[_posX - 2, _posY + 1] = _storage_map[10];
        Map[_posX - 1, _posY + 1] = _storage_map[11];
        Map[_posX, _posY + 1] = _storage_map[12];
        Map[_posX + 1, _posY + 1] = _storage_map[13];
        Map[_posX + 2, _posY + 1] = _storage_map[14];

        Map[_posX - 2, _posY + 2] = _storage_map[15];
        Map[_posX - 1, _posY + 2] = _storage_map[16];
        Map[_posX, _posY + 2] = _storage_map[17];
        Map[_posX + 1, _posY + 2] = _storage_map[18];
        Map[_posX + 2, _posY + 2] = _storage_map[19];

        UpdateRow(_posY - 1);
        UpdateRow(_posY);
        UpdateRow(_posY + 1);
        UpdateRow(_posY + 2);

        _storage_map[0] = Map[posX - 2, posY - 1];
        _storage_map[1] = Map[posX - 1, posY - 1];
        _storage_map[2] = Map[posX, posY - 1];
        _storage_map[3] = Map[posX + 1, posY - 1];
        _storage_map[4] = Map[posX + 2, posY - 1];

        _storage_map[5] = Map[posX - 2, posY];
        _storage_map[6] = Map[posX - 1, posY];
        _storage_map[7] = Map[posX, posY];
        _storage_map[8] = Map[posX + 1, posY];
        _storage_map[9] = Map[posX + 2, posY];

        _storage_map[10] = Map[posX - 2, posY + 1];
        _storage_map[11] = Map[posX - 1, posY + 1];
        _storage_map[12] = Map[posX, posY + 1];
        _storage_map[13] = Map[posX + 1, posY + 1];
        _storage_map[14] = Map[posX + 2, posY + 1];

        _storage_map[15] = Map[posX - 2, posY + 2];
        _storage_map[16] = Map[posX - 1, posY + 2];
        _storage_map[17] = Map[posX, posY + 2];
        _storage_map[18] = Map[posX + 1, posY + 2];
        _storage_map[19] = Map[posX + 2, posY + 2];
    }

    public void DeployRaft(Direction direction)
    {
        var spaceslot = ' ';
        var underslot = '_';
        if (HasFlag(GameFlag.HasArmor))
        {
            spaceslot = '#';
            underslot = '#';
        }

        Map[_posX - 3, _posY - 2] = '*';
        Map[_posX - 2, _posY - 2] = '*';
        Map[_posX - 1, _posY - 2] = '*';
        Map[_posX, _posY - 2] = '*';
        Map[_posX + 1, _posY - 2] = '*';
        Map[_posX + 2, _posY - 2] = '*';
        Map[_posX + 3, _posY - 2] = '*';

        Map[_posX - 3, _posY - 1] = '=';
        Map[_posX, _posY - 1] = ' ';
        Map[_posX + 3, _posY - 1] = '=';

        Map[_posX - 3, _posY] = '*';
        Map[_posX - 2, _posY] = '|';
        Map[_posX + 2, _posY] = '|';
        Map[_posX + 3, _posY] = '*';

        Map[_posX - 3, _posY + 1] = '*';
        Map[_posX - 2, _posY + 1] = '|';
        Map[_posX - 1, _posY + 1] = underslot;
        Map[_posX, _posY + 1] = '=';
        Map[_posX + 1, _posY + 1] = underslot;
        Map[_posX + 2, _posY + 1] = '|';
        Map[_posX + 3, _posY + 1] = '*';

        Map[_posX - 3, _posY + 2] = '=';
        Map[_posX - 2, _posY + 2] = '=';
        Map[_posX - 1, _posY + 2] = '=';
        Map[_posX, _posY + 2] = '=';
        Map[_posX + 1, _posY + 2] = '=';
        Map[_posX + 2, _posY + 2] = '=';
        Map[_posX + 3, _posY + 2] = '=';

        Map[_posX - 3, _posY + 3] = '*';
        Map[_posX - 2, _posY + 3] = '*';
        Map[_posX - 1, _posY + 3] = '*';
        Map[_posX, _posY + 3] = '*';
        Map[_posX + 1, _posY + 3] = '*';
        Map[_posX + 2, _posY + 3] = '*';
        Map[_posX + 3, _posY + 3] = '*';

        if (direction == Direction.Left)
        {
            Map[_posX - 2, _posY - 1] = '=';
            Map[_posX - 1, _posY - 1] = '/';
            Map[_posX + 1, _posY - 1] = ' ';
            Map[_posX + 2, _posY - 1] = '|';

            Map[_posX - 1, _posY] = '^';
            Map[_posX, _posY] = spaceslot;
            Map[_posX + 1, _posY] = spaceslot;
        }
        else if (direction == Direction.Right)
        {
            Map[_posX - 2, _posY - 1] = '|';
            Map[_posX - 1, _posY - 1] = ' ';
            Map[_posX + 1, _posY - 1] = '\\';
            Map[_posX + 2, _posY - 1] = '=';

            Map[_posX - 1, _posY] = spaceslot;
            Map[_posX, _posY] = spaceslot;
            Map[_posX + 1, _posY] = '^';
        }

        UpdateRow(_posY - 2);
        UpdateRow(_posY - 1);
        UpdateRow(_posY);
        UpdateRow(_posY + 1);
        UpdateRow(_posY + 2);
        UpdateRow(_posY + 3);
    }

    public void PlayEffect(char symbol)
    {
        Map[_posX - 2, _posY - 1] = symbol;
        Map[_posX - 1, _posY - 1] = symbol;
        Map[_posX, _posY - 1] = symbol;
        Map[_posX + 1, _posY - 1] = symbol;
        Map[_posX + 2, _posY - 1] = symbol;

        Map[_posX - 2, _posY] = symbol;
        Map[_posX - 1, _posY] = symbol;
        Map[_posX, _posY] = symbol;
        Map[_posX + 1, _posY] = symbol;
        Map[_posX + 2, _posY] = symbol;

        Map[_posX - 2, _posY + 1] = symbol;
        Map[_posX - 1, _posY + 1] = symbol;
        Map[_posX, _posY + 1] = symbol;
        Map[_posX + 1, _posY + 1] = symbol;
        Map[_posX + 2, _posY + 1] = symbol;

        Map[_posX - 2, _posY + 2] = symbol;
        Map[_posX - 1, _posY + 2] = symbol;
        Map[_posX, _posY + 2] = symbol;
        Map[_posX + 1, _posY + 2] = symbol;
        Map[_posX + 2, _posY + 2] = symbol;
    }

    public void PlaceZelda()
    {
        Map[50, 14] = '=';
        Map[51, 14] = '<';
        Map[52, 14] = '>';
        Map[53, 14] = '=';

        Map[50, 15] = 's';
        Map[51, 15] = '^';
        Map[52, 15] = '^';
        Map[53, 15] = 's';

        Map[49, 16] = 's';
        Map[50, 16] = 's';
        Map[51, 16] = '~';
        Map[52, 16] = '~';
        Map[53, 16] = 's';
        Map[54, 16] = 's';

        Map[49, 16] = '~';
        Map[50, 16] = '~';
        Map[51, 16] = '~';
        Map[52, 16] = '~';
        Map[53, 16] = '~';
        Map[54, 16] = '~';
    }

    public void Hit()
    {
        if (iFrames <= 0)
        {
            iFrames = 6;

            if (HasFlag(GameFlag.HasArmor))
            {
                Health -= 0.5;
            }
            else
            {
                Health--;
            }
            SetFlag(GameFlag.Hit, true);

            StoreChar(_posX, _posY);

            PlayEffect('*');

            UpdateRow(_posY - 1);
            UpdateRow(_posY);
            UpdateRow(_posY + 1);
            UpdateRow(_posY + 2);
        }
    }

    public bool IsTouching(int posX, int posY, char symbol)
    {
        if (symbol == '/')
        {
            return Map[posX, posY - 1] == '/';
        }

        if (Map[posX - 2, posY - 1] == symbol
            || Map[posX - 1, posY - 1] == symbol
            || Map[posX, posY - 1] == symbol
            || Map[posX + 1, posY - 1] == symbol
            || Map[posX + 2, posY - 1] == symbol
            || Map[posX - 2, posY] == symbol
            || Map[posX - 1, posY] == symbol
            || Map[posX, posY] == symbol
            || Map[posX + 1, posY] == symbol
            || Map[posX + 2, posY] == symbol
            || Map[posX - 2, posY + 1] == symbol
            || Map[posX - 1, posY + 1] == symbol
            || Map[posX, posY + 1] == symbol
            || Map[posX + 1, posY + 1] == symbol
            || Map[posX + 2, posY + 1] == symbol
            || Map[posX - 2, posY + 2] == symbol
            || Map[posX - 1, posY + 2] == symbol
            || Map[posX, posY + 2] == symbol
            || Map[posX + 1, posY + 2] == symbol
            || Map[posX + 2, posY + 2] == symbol)
        {
            if (symbol is 'R' or 'r')
            {
                for (var i = 0; i < 4; i++)
                {
                    for (var j = 0; j < 5; j++)
                    {
                        if (Map[posX - 2 + j, posY - 1 + i] is 'R' or 'r')
                        {
                            MainProgram.EnemyMovement.RemoveRupee(posX - 2 + j, posY - 1 + i);
                        }
                    }
                }
            }
            return true;
        }
        return false;
    }
}
