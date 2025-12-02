using _0_Bit_Legend.Entities;

namespace _0_Bit_Legend.Managers;

public class PlayerController
{
    private Vector2 _preHitPosition = Vector2.Zero;
    private readonly Link _player = new();

    private readonly char[] _storage_map = new string(' ', 20).ToArray();
    private static readonly char[] _storage_sword = new char[6];
    private static readonly char[] _storage_detect_enemy = new char[6];

    private DirectionType _prev = DirectionType.Up;
    private DirectionType _prev2 = DirectionType.Left;

    private bool _debounce;
    private bool _spawnRupee;
    private bool _swingingSword;

    public Vector2 Position { get; private set; }
    public int MovementWait;

    public DirectionType GetPrev() => _prev;
    public DirectionType GetPrev2() => _prev2;
    public void SetPosition(Vector2 pos) => Position = pos;
    public void SetPreHitPosition(Vector2 pos) => _preHitPosition = pos;
    public void SetPrev(DirectionType prev) => _prev = prev;
    public void SetSpawnRupee(bool spawnRupee) => _spawnRupee = spawnRupee;

    //Temporary rename fix, TODO removed
    private int PosX => Position.X;
    private int PosY => Position.Y;
    private int PreHitPosX => _preHitPosition.X;
    private int PreHitPosY => _preHitPosition.Y;

    public void Attack()
    {
        switch (_prev)
        {
            case DirectionType.Up:
                HandleAttackUp();
                break;
            case DirectionType.Left:
                HandleAttackLeft();
                break;
            case DirectionType.Down:
                HandleAttackDown();
                break;
            case DirectionType.Right:
                HandleAttackRight();
                break;
        }
    }

    private void HandleAttackUp()
    {
        //if (PosY > 3)
        //    return;
        if (!_swingingSword)
        {
            _storage_sword[0] = Map[PosX - 1, PosY - 2];
            _storage_sword[1] = Map[PosX, PosY - 2];
            _storage_sword[2] = Map[PosX + 1, PosY - 2];
            _storage_sword[3] = Map[PosX, PosY - 3];
            _storage_sword[4] = Map[PosX, PosY - 4];

            Map[PosX - 1, PosY - 2] = '-';
            Map[PosX, PosY - 2] = '-';
            Map[PosX + 1, PosY - 2] = '-';
            Map[PosX, PosY - 3] = 'S';
            Map[PosX, PosY - 4] = 'S';

            _preHitPosition = new(PosX, PosY);
            _swingingSword = true;
        }
        else
        {
            _storage_detect_enemy[0] = Map[PreHitPosX - 1, PreHitPosY - 2];
            _storage_detect_enemy[1] = Map[PreHitPosX, PreHitPosY - 2];
            _storage_detect_enemy[2] = Map[PreHitPosX + 1, PreHitPosY - 2];
            _storage_detect_enemy[3] = Map[PreHitPosX, PreHitPosY - 3];
            _storage_detect_enemy[4] = Map[PreHitPosX, PreHitPosY - 4];

            var swordArr = new int[2, 5];
            swordArr[0, 0] = PreHitPosX - 1;
            swordArr[1, 0] = PreHitPosY - 2;
            swordArr[0, 1] = PreHitPosX;
            swordArr[1, 1] = PreHitPosY - 2;
            swordArr[0, 2] = PreHitPosX + 1;
            swordArr[1, 2] = PreHitPosY - 2;
            swordArr[0, 3] = PreHitPosX;
            swordArr[1, 3] = PreHitPosY - 3;
            swordArr[0, 4] = PreHitPosX;
            swordArr[1, 4] = PreHitPosY - 4;

            Stab(swordArr, DirectionType.Up, 5, 1);
        }

        UpdateRow(PreHitPosY - 2);
        UpdateRow(PreHitPosY - 3);
        UpdateRow(PreHitPosY - 4);
    }
    private void HandleAttackLeft()
    {
        //if (PosY > 4)
        //    return;
        if (!_swingingSword)
        {
            _storage_sword[0] = Map[PosX - 3, PosY];
            _storage_sword[1] = Map[PosX - 3, PosY + 1];
            _storage_sword[2] = Map[PosX - 3, PosY + 2];
            _storage_sword[3] = Map[PosX - 4, PosY + 1];
            _storage_sword[4] = Map[PosX - 5, PosY + 1];
            _storage_sword[5] = Map[PosX - 6, PosY + 1];

            Map[PosX - 3, PosY] = '-';
            Map[PosX - 3, PosY + 1] = '-';
            Map[PosX - 3, PosY + 2] = '-';
            Map[PosX - 4, PosY + 1] = 'S';
            Map[PosX - 5, PosY + 1] = 'S';
            Map[PosX - 6, PosY + 1] = 'S';

            _preHitPosition = new(PosX, PosY);
            _swingingSword = true;
        }
        else
        {
            _storage_detect_enemy[0] = Map[PreHitPosX - 3, PreHitPosY];
            _storage_detect_enemy[1] = Map[PreHitPosX - 3, PreHitPosY + 1];
            _storage_detect_enemy[2] = Map[PreHitPosX - 3, PreHitPosY + 2];
            _storage_detect_enemy[3] = Map[PreHitPosX - 4, PreHitPosY + 1];
            _storage_detect_enemy[4] = Map[PreHitPosX - 5, PreHitPosY + 1];
            _storage_detect_enemy[5] = Map[PreHitPosX - 6, PreHitPosY + 1];

            var swordArr = new int[2, 6];
            swordArr[0, 0] = PreHitPosX - 3;
            swordArr[1, 0] = PreHitPosY;
            swordArr[0, 1] = PreHitPosX - 3;
            swordArr[1, 1] = PreHitPosY + 1;
            swordArr[0, 2] = PreHitPosX - 3;
            swordArr[1, 2] = PreHitPosY + 2;
            swordArr[0, 3] = PreHitPosX - 4;
            swordArr[1, 3] = PreHitPosY + 1;
            swordArr[0, 4] = PreHitPosX - 5;
            swordArr[1, 4] = PreHitPosY + 1;
            swordArr[0, 5] = PreHitPosX - 6;
            swordArr[1, 5] = PreHitPosY + 1;

            Stab(swordArr, DirectionType.Left, 6, 1);
        }

        UpdateRow(PreHitPosY);
        UpdateRow(PreHitPosY + 1);
        UpdateRow(PreHitPosY + 2);
    }
    private void HandleAttackDown()
    {
        //if (PosY + 4 < 33)
        //    return;
        if (!_swingingSword)
        {
            _storage_sword[0] = Map[PosX - 1, PosY + 3];
            _storage_sword[1] = Map[PosX, PosY + 3];
            _storage_sword[2] = Map[PosX + 1, PosY + 3];
            _storage_sword[3] = Map[PosX, PosY + 4];
            _storage_sword[4] = Map[PosX, PosY + 5];

            Map[PosX - 1, PosY + 3] = '-';
            Map[PosX, PosY + 3] = '-';
            Map[PosX + 1, PosY + 3] = '-';
            Map[PosX, PosY + 4] = 'S';
            Map[PosX, PosY + 5] = 'S';

            _preHitPosition = new(PosX, PosY);
            _swingingSword = true;
        }
        else
        {
            _storage_detect_enemy[0] = Map[PreHitPosX - 1, PreHitPosY + 3];
            _storage_detect_enemy[1] = Map[PreHitPosX, PreHitPosY + 3];
            _storage_detect_enemy[2] = Map[PreHitPosX + 1, PreHitPosY + 3];
            _storage_detect_enemy[3] = Map[PreHitPosX, PreHitPosY + 4];
            _storage_detect_enemy[4] = Map[PreHitPosX, PreHitPosY + 5];

            var swordArr = new int[2, 5];
            swordArr[0, 0] = PreHitPosX - 1;
            swordArr[1, 0] = PreHitPosY + 3;
            swordArr[0, 1] = PreHitPosX;
            swordArr[1, 1] = PreHitPosY + 3;
            swordArr[0, 2] = PreHitPosX + 1;
            swordArr[1, 2] = PreHitPosY + 3;
            swordArr[0, 3] = PreHitPosX;
            swordArr[1, 3] = PreHitPosY + 4;
            swordArr[0, 4] = PreHitPosX;
            swordArr[1, 4] = PreHitPosY + 5;

            Stab(swordArr, DirectionType.Down, 5, 1);
        }

        UpdateRow(PreHitPosY + 3);
        UpdateRow(PreHitPosY + 4);
        UpdateRow(PreHitPosY + 5);
    }
    private void HandleAttackRight()
    {
        //if (PosY + 6 < 102)
        //    return;
        if (!_swingingSword)
        {
            _storage_sword[0] = Map[PosX + 3, PosY];
            _storage_sword[1] = Map[PosX + 3, PosY + 1];
            _storage_sword[2] = Map[PosX + 3, PosY + 2];
            _storage_sword[3] = Map[PosX + 4, PosY + 1];
            _storage_sword[4] = Map[PosX + 5, PosY + 1];
            _storage_sword[5] = Map[PosX + 6, PosY + 1];

            Map[PosX + 3, PosY] = '-';
            Map[PosX + 3, PosY + 1] = '-';
            Map[PosX + 3, PosY + 2] = '-';
            Map[PosX + 4, PosY + 1] = 'S';
            Map[PosX + 5, PosY + 1] = 'S';
            Map[PosX + 6, PosY + 1] = 'S';

            _preHitPosition = new(PosX, PosY);
            _swingingSword = true;
        }
        else
        {
            _storage_detect_enemy[0] = Map[PreHitPosX + 3, PreHitPosY];
            _storage_detect_enemy[1] = Map[PreHitPosX + 3, PreHitPosY + 1];
            _storage_detect_enemy[2] = Map[PreHitPosX + 3, PreHitPosY + 2];
            _storage_detect_enemy[3] = Map[PreHitPosX + 4, PreHitPosY + 1];
            _storage_detect_enemy[4] = Map[PreHitPosX + 5, PreHitPosY + 1];
            _storage_detect_enemy[5] = Map[PreHitPosX + 6, PreHitPosY + 1];

            var swordArr = new int[2, 6];
            swordArr[0, 0] = PreHitPosX + 3;
            swordArr[1, 0] = PreHitPosY;
            swordArr[0, 1] = PreHitPosX + 3;
            swordArr[1, 1] = PreHitPosY + 1;
            swordArr[0, 2] = PreHitPosX + 3;
            swordArr[1, 2] = PreHitPosY + 2;
            swordArr[0, 3] = PreHitPosX + 4;
            swordArr[1, 3] = PreHitPosY + 1;
            swordArr[0, 4] = PreHitPosX + 5;
            swordArr[1, 4] = PreHitPosY + 1;
            swordArr[0, 5] = PreHitPosX + 6;
            swordArr[1, 5] = PreHitPosY + 1;

            Stab(swordArr, DirectionType.Right, 6, 1);
        }

        UpdateRow(PreHitPosY);
        UpdateRow(PreHitPosY + 1);
        UpdateRow(PreHitPosY + 2);
    }

    public void Stab(int[,] swordArr, DirectionType prev, int amt, int dmg)
    {
        var hit = false;
        for (var i = 0; i < amt; i++)
        {
            char[] detect = ['t', 'n', 'B', '{', '}', 'F'];
            if (detect.Any(x => x == _storage_sword[i]) || detect.Any(x => x == _storage_detect_enemy[i]))
            {
                hit = true;
                if (MainProgram.EnemyManager.TakeDamage(swordArr[0, i], swordArr[1, i], prev, dmg) && _spawnRupee)
                {
                    _spawnRupee = false;
                    MainProgram.EnemyManager.SpawnRupee();
                }
                break;
            }
        }
        if (!hit)
        {
            StoreSword(prev);
        }
        SetGameState(GameState.Idle);
        _swingingSword = false;
    }

    public void StoreSword(DirectionType prev)
    {
        char[] convert = ['t', '^', 'n', '0', 'B', '{', '}', 'F', 'S', '>', '*'];
        for (var i = 0; i < 6; i++)
        {
            if (convert.Any(x => x == _storage_sword[i]))
            {
                _storage_sword[i] = ' ';
            }
        }

        if (prev == DirectionType.Up)
        {
            Map[PreHitPosX - 1, PreHitPosY - 2] = _storage_sword[0];
            Map[PreHitPosX, PreHitPosY - 2] = _storage_sword[1];
            Map[PreHitPosX + 1, PreHitPosY - 2] = _storage_sword[2];
            Map[PreHitPosX, PreHitPosY - 3] = _storage_sword[3];
            Map[PreHitPosX, PreHitPosY - 4] = _storage_sword[4];
        }
        else if (prev == DirectionType.Left)
        {
            Map[PreHitPosX - 3, PreHitPosY] = _storage_sword[0];
            Map[PreHitPosX - 3, PreHitPosY + 1] = _storage_sword[1];
            Map[PreHitPosX - 3, PreHitPosY + 2] = _storage_sword[2];
            Map[PreHitPosX - 4, PreHitPosY + 1] = _storage_sword[3];
            Map[PreHitPosX - 5, PreHitPosY + 1] = _storage_sword[4];
            Map[PreHitPosX - 6, PreHitPosY + 1] = _storage_sword[5];
        }
        else if (prev == DirectionType.Down)
        {
            Map[PreHitPosX - 1, PreHitPosY + 3] = _storage_sword[0];
            Map[PreHitPosX, PreHitPosY + 3] = _storage_sword[1];
            Map[PreHitPosX + 1, PreHitPosY + 3] = _storage_sword[2];
            Map[PreHitPosX, PreHitPosY + 4] = _storage_sword[3];
            Map[PreHitPosX, PreHitPosY + 5] = _storage_sword[4];
        }
        else if (prev == DirectionType.Right)
        {
            Map[PreHitPosX + 3, PreHitPosY] = _storage_sword[0];
            Map[PreHitPosX + 3, PreHitPosY + 1] = _storage_sword[1];
            Map[PreHitPosX + 3, PreHitPosY + 2] = _storage_sword[2];
            Map[PreHitPosX + 4, PreHitPosY + 1] = _storage_sword[3];
            Map[PreHitPosX + 5, PreHitPosY + 1] = _storage_sword[4];
            Map[PreHitPosX + 6, PreHitPosY + 1] = _storage_sword[5];
        }
    }

    public void MoveUp(int magnitude = 1)
    {
        _prev = DirectionType.Up;
        var posY = PosY - magnitude;
        var posX = PosX;

        if (PosX == 21 && ((CurrentMap == 4 && posY > 9) || CurrentMap == 2))
        {
            if (posY > 1)
            {
                for (var y = PosY - 2; y <= PosY + 3; y++)
                {
                    for (var x = PosX - 3; x <= PosX + 3; x++)
                    {
                        Map[x, y] = '~';
                    }
                }

                Position = new(Position.X, Position.Y - 1);
                DeployRaft(_prev2);

                UpdateRow(PosY + 4);
            }
            else
            {
                LoadMap(4, 21, 29, DirectionType.Up);
            }
        }
        else if (posY >= 1 && !(PosX == 21 && (CurrentMap == 4 || CurrentMap == 2)))
        {
            IsTouching(posX, posY, 'r');
            StoreChar(PosX, PosY);
            var inCave = false;

            if (CurrentMap == 6 && (IsTouching(posX, posY, '-') || IsTouching(posX, posY, 'S')))
            {
                SetFlag(GameFlag.HasSword, true);
                LoadMap(6, posX, posY, DirectionType.Up);
            }
            else if (CurrentMap == 7 && IsTouching(posX, posY, '*') && Rupees >= 35)
            {
                Rupees -= 35;

                SetFlag(GameFlag.HasRaft, true);
                LoadMap(7, posX, posY, DirectionType.Up);
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
                LoadMap(7, posX, posY, DirectionType.Up);
            }
            else if (CurrentMap == 9
                && IsTouching(posX, posY, 'X')
                && !IsTouching(posX, posY, '=')
                && HasFlags([GameFlag.Door1, GameFlag.Door2, GameFlag.Door3])
                && cEnemies1 <= 0
                && cEnemies2 <= 0
                && !_debounce)
            {
                LoadMap(12, 50, 24, DirectionType.Up);
            }
            else if (CurrentMap == 9 && PosX >= 48 && PosX <= 52 && PosY == 7 && !HasFlag(GameFlag.Door3) && Keys > 0)
            {
                _debounce = true;
                Keys--;

                SetFlag(GameFlag.Door3, true);
                LoadMap(9, PosX, PosY, DirectionType.Up);
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
                BuildChar(posX, posY, DirectionType.Up);

                UpdateRow(posY - 1);
                UpdateRow(posY);
                UpdateRow(posY + 1);
                UpdateRow(posY + 2);

                Position = new(posX, posY);
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
                BuildChar(PosX, PosY, DirectionType.Up);

                UpdateRow(PosY - 1);
                UpdateRow(PosY);
                UpdateRow(PosY + 1);
                UpdateRow(PosY + 2);

                Hit();
            }
            else if (CurrentMap == 13 && IsTouching(posX, posY, '~'))
            {
                BuildChar(PosX, PosY, DirectionType.Up);

                UpdateRow(PosY - 1);
                UpdateRow(PosY);
                UpdateRow(PosY + 1);
                UpdateRow(PosY + 2);

                LoadMap(13, 58, 15, DirectionType.Left);
                SetGameState(GameState.GameOver);
            }
            else
            {
                BuildChar(PosX, PosY, DirectionType.Up);

                UpdateRow(PosY - 1);
                UpdateRow(PosY);
                UpdateRow(PosY + 1);
                UpdateRow(PosY + 2);
            }

            if (inCave)
            {
                WaitForTransition();
            }
        }
        else
        {
            if (CurrentMap == 0)
            {
                LoadMap(1, 63, 29, DirectionType.Up);
            }
            else if (CurrentMap == 2)
            {
                if (posX > 29)
                {
                    LoadMap(4, 55, 30, DirectionType.Up);
                }
                else if (posX == 21)
                {
                    LoadMap(4, 21, 29, DirectionType.Up);
                }
                else
                {
                    LoadMap(4, 10, 29, DirectionType.Up);
                }
            }
            else if (CurrentMap == 3)
            {
                LoadMap(5, 49, 30, DirectionType.Up);
            }
        }
        if (HasFlag(GameFlag.Text))
        {
            SetFlag(GameFlag.Text, false);
            LoadMap(9, posX, posY, DirectionType.Up);
        }

        _debounce = false;
    }
    public void MoveLeft(int magnitude = 1)
    {
        _prev = DirectionType.Left;
        _prev2 = DirectionType.Left;
        var posY = PosY;
        var posX = PosX - (magnitude * 2);

        if (posX >= 2)
        {
            IsTouching(posX, posY, 'r');
            StoreChar(PosX, PosY);

            if (CurrentMap == 6 && (IsTouching(posX, posY, '-') || IsTouching(posX, posY, 'S')))
            {
                SetFlag(GameFlag.HasSword, true);
                LoadMap(6, posX, posY, DirectionType.Left);
            }
            else if (CurrentMap == 7 && IsTouching(posX, posY, '*') && Rupees >= 35)
            {
                Rupees -= 35;

                SetFlag(GameFlag.HasRaft, true);
                LoadMap(7, posX, posY, DirectionType.Left);
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
                LoadMap(7, posX, posY, DirectionType.Left);
            }
            else if (CurrentMap == 9
                && IsTouching(posX, posY, 'X')
                && !IsTouching(posX, posY, '=')
                && HasFlag(GameFlag.Door1)
                && !_debounce)
            {
                LoadMap(10, 87, 15, DirectionType.Left);
            }
            else if (CurrentMap == 9 && PosX == 14 && PosY >= 14 && PosY <= 16 && !HasFlag(GameFlag.Door1) && Keys > 0)
            {
                _debounce = true;
                Keys--;

                SetFlag(GameFlag.Door1, true);
                LoadMap(9, PosX, PosY, DirectionType.Left);
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
                BuildChar(posX, posY, DirectionType.Left);

                UpdateRow(posY - 1);
                UpdateRow(posY);
                UpdateRow(posY + 1);
                UpdateRow(posY + 2);

                Position = new(posX, posY); Position = new(posX, posY);
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
                BuildChar(PosX, PosY, DirectionType.Left);

                UpdateRow(PosY - 1);
                UpdateRow(PosY);
                UpdateRow(PosY + 1);
                UpdateRow(PosY + 2);

                Hit();
            }
            else if (IsTouching(posX, posY, '~')
                && PosX != 21
                && HasFlag(GameFlag.HasRaft)
                && !IsTouching(posX, posY, '=')
                && !IsTouching(posX, posY, 'X')
                && !IsTouching(posX, posY, 't')
                && !IsTouching(posX, posY, 'n'))
            {
                StoreChar(PosX, PosY);

                UpdateRow(PosY - 1);
                UpdateRow(PosY);
                UpdateRow(PosY + 1);
                UpdateRow(PosY + 2);

                Position = new(21, Position.Y);
                DeployRaft(DirectionType.Left);
                wait = 150;
            }
            else if (PosX == 21 && ((posY > 11 && CurrentMap == 4)
                || CurrentMap == 2) && ((CurrentMap == 2 && posY < 25) || CurrentMap == 4))
            {
                BuildChar(PosX, PosY, DirectionType.Left);

                UpdateRow(PosY - 1);
                UpdateRow(PosY);
                UpdateRow(PosY + 1);
                UpdateRow(PosY + 2);

                for (var y = PosY - 2; y <= PosY + 3; y++)
                {
                    for (var x = PosX - 3; x <= PosX + 3; x++)
                    {
                        Map[x, y] = '~';
                    }
                }

                Position = new(11, Position.Y);
                posX = 11;

                BuildChar(posX, posY, DirectionType.Left);

                UpdateRow(posY - 2);
                UpdateRow(posY - 1);
                UpdateRow(posY);
                UpdateRow(posY + 1);
                UpdateRow(posY + 2);
                UpdateRow(posY + 3);
            }
            else if (CurrentMap == 11 && IsTouching(posX, posY, 'X') && !IsTouching(posX, posY, '='))
            {
                LoadMap(9, 86, 15, DirectionType.Left);
            }
            else if (CurrentMap == 13 && IsTouching(posX, posY, 'X') && !IsTouching(posX, posY, '='))
            {
                LoadMap(12, 86, 15, DirectionType.Left);
            }
            else
            {
                BuildChar(PosX, PosY, DirectionType.Left);

                UpdateRow(PosY - 1);
                UpdateRow(PosY);
                UpdateRow(PosY + 1);
                UpdateRow(PosY + 2);
            }

        }
        else
        {
            if (CurrentMap == 0)
            {
                LoadMap(2, 99, 12, DirectionType.Left);
            }
            else if (CurrentMap == 3)
            {
                LoadMap(0, 98, 17, DirectionType.Left);
            }
            else if (CurrentMap == 1)
            {
                LoadMap(4, 98, 13, DirectionType.Left);
            }
            else if (CurrentMap == 5)
            {
                LoadMap(1, 98, 16, DirectionType.Left);
            }
            else if (CurrentMap == 4)
            {
                LoadMap(8, 99, 16, DirectionType.Left);
            }
        }
        if (HasFlag(GameFlag.Text))
        {
            SetFlag(GameFlag.Text, false);
            LoadMap(9, posX, posY, DirectionType.Up);
        }

        _debounce = false;
    }
    public void MoveDown(int magnitude = 1)
    {
        _prev = DirectionType.Down;
        var posY = PosY + magnitude;
        var posX = PosX;

        if ((CurrentMap == 2 || CurrentMap == 4) && posX == 21)
        {
            if ((posY < 30) && ((CurrentMap == 2 && posY < 27) || CurrentMap == 4))
            {
                for (var y = PosY - 2; y <= PosY + 3; y++)
                {
                    for (var x = PosX - 3; x <= PosX + 3; x++)
                    {
                        Map[x, y] = '~';
                    }
                }

                Position = new(Position.X, Position.Y + 1);
                DeployRaft(_prev2);

                UpdateRow(PosY - 3);
            }
            else if (CurrentMap == 4)
            {
                LoadMap(2, 21, 2, DirectionType.Down);
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
                BuildChar(posX, posY, DirectionType.Down);

                UpdateRow(posY - 1);
                UpdateRow(posY);
                UpdateRow(posY + 1);
                UpdateRow(posY + 2);

                Position = new(posX, posY);
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
                BuildChar(PosX, PosY, DirectionType.Down);

                UpdateRow(PosY - 1);
                UpdateRow(PosY);
                UpdateRow(PosY + 1);
                UpdateRow(PosY + 2);

                Hit();
            }
            else if (CurrentMap == 12 && IsTouching(posX, posY, 'X') && !IsTouching(posX, posY, '='))
            {
                BuildChar(PosX, PosY, DirectionType.Down);

                UpdateRow(PosY - 1);
                UpdateRow(PosY);
                UpdateRow(PosY + 1);
                UpdateRow(PosY + 2);

                LoadMap(9, 50, 9, DirectionType.Down);
            }
            else
            {
                BuildChar(PosX, PosY, DirectionType.Down);

                UpdateRow(PosY - 1);
                UpdateRow(PosY);
                UpdateRow(PosY + 1);
                UpdateRow(PosY + 2);
            }
        }
        else
        {
            if (CurrentMap == 1)
            {
                LoadMap(0, 63, 1, DirectionType.Down);
            }
            else if (CurrentMap == 4)
            {
                if (posX > 29)
                {
                    LoadMap(2, 55, 1, DirectionType.Down);
                }
                else if (posX == 21)
                {
                    LoadMap(2, 21, 2, DirectionType.Down);
                }
                else
                {
                    LoadMap(2, 10, 2, DirectionType.Down);
                }
            }
            else if (CurrentMap == 5)
            {
                LoadMap(3, 49, 2, DirectionType.Down);
            }
            else if (CurrentMap == 6)
            {
                LoadMap(0, 16, 6, DirectionType.Down);
                //WaitForTransition();
            }
            else if (CurrentMap == 7)
            {
                LoadMap(4, 86, 7, DirectionType.Down);
                //WaitForTransition();
            }
            else if (CurrentMap == 9)
            {
                LoadMap(8, 51, 17, DirectionType.Down);
                //WaitForTransition();
            }
        }
        SetFlag(GameFlag.Text, false);
        _debounce = false;
    }
    public void MoveRight(int magnitude = 1)
    {
        _prev = DirectionType.Right;
        _prev2 = DirectionType.Right;
        var posY = PosY;
        var posX = PosX + (magnitude * 2);

        if (posX <= 99)
        {
            IsTouching(posX, posY, 'r');
            StoreChar(PosX, PosY);

            var persist = true;
            if (CurrentMap == 6 && (IsTouching(posX, posY, '-') || IsTouching(posX, posY, 's')))
            {
                SetFlag(GameFlag.HasSword, true);
                LoadMap(6, posX, posY, DirectionType.Right);
            }
            else if (CurrentMap == 7 && IsTouching(posX, posY, '*') && Rupees >= 35)
            {
                Rupees -= 35;

                SetFlag(GameFlag.HasRaft, true);
                LoadMap(7, posX, posY, DirectionType.Right);
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
                LoadMap(7, posX, posY, DirectionType.Right);
            }
            else if (CurrentMap == 9
                && IsTouching(posX, posY, 'X')
                && !IsTouching(posX, posY, '=')
                && HasFlag(GameFlag.Door2)
                && !_debounce)
            {
                persist = false;
                LoadMap(11, 14, 15, DirectionType.Right);
            }
            else if (CurrentMap == 9 && PosX == 86 && PosY >= 14 && PosY <= 16 && !HasFlag(GameFlag.Door2) && Keys > 0)
            {
                _debounce = true;
                Keys--;

                SetFlag(GameFlag.Door2, true);
                LoadMap(9, PosX, PosY, DirectionType.Right);
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
                BuildChar(posX, posY, DirectionType.Right);

                UpdateRow(posY - 1);
                UpdateRow(posY);
                UpdateRow(posY + 1);
                UpdateRow(posY + 2);

                Position = new(posX, posY);
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
                BuildChar(PosX, PosY, DirectionType.Right);

                UpdateRow(PosY - 1);
                UpdateRow(PosY);
                UpdateRow(PosY + 1);
                UpdateRow(PosY + 2);

                Hit();
            }
            else if (IsTouching(posX, posY, '~')
                && PosX != 21
                && HasFlag(GameFlag.HasRaft)
                && !IsTouching(posX, posY, '=')
                && !IsTouching(posX, posY, 'X')
                && !IsTouching(posX, posY, 't')
                && !IsTouching(posX, posY, 'n'))
            {
                StoreChar(PosX, PosY);

                UpdateRow(PosY - 1);
                UpdateRow(PosY);
                UpdateRow(PosY + 1);
                UpdateRow(PosY + 2);

                Position = new(21, Position.Y);
                DeployRaft(DirectionType.Right);
                wait = 150;
            }
            else if (PosX == 21 && posY < 25 && ((posY > 3 && CurrentMap == 2) || (posY < 25 && CurrentMap == 4)))
            {
                BuildChar(PosX, PosY, DirectionType.Right);

                UpdateRow(PosY - 1);
                UpdateRow(PosY);
                UpdateRow(PosY + 1);
                UpdateRow(PosY + 2);

                for (var y = PosY - 2; y <= PosY + 3; y++)
                {
                    for (var x = PosX - 3; x <= PosX + 3; x++)
                    {
                        Map[x, y] = '~';
                    }
                }

                Position = new(30, Position.Y);
                posX = 30;

                BuildChar(posX, posY, DirectionType.Right);

                UpdateRow(posY - 2);
                UpdateRow(posY - 1);
                UpdateRow(posY);
                UpdateRow(posY + 1);
                UpdateRow(posY + 2);
                UpdateRow(posY + 3);
            }
            else if (CurrentMap == 10 && IsTouching(posX, posY, 'X') && !IsTouching(posX, posY, '='))
            {
                LoadMap(9, 14, 15, DirectionType.Right);
            }
            else if (CurrentMap == 12 && IsTouching(posX, posY, 'X') && !IsTouching(posX, posY, '='))
            {
                LoadMap(13, 15, 15, DirectionType.Right);
            }
            else
            {
                BuildChar(PosX, PosY, DirectionType.Right);

                UpdateRow(PosY - 1);
                UpdateRow(PosY);
                UpdateRow(PosY + 1);
                UpdateRow(PosY + 2);
            }
        }
        else
        {
            if (CurrentMap == 2)
            {
                LoadMap(0, 4, 12, DirectionType.Right);
            }
            else if (CurrentMap == 0)
            {
                LoadMap(3, 2, 18, DirectionType.Right);
            }
            else if (CurrentMap == 4)
            {
                LoadMap(1, 2, 13, DirectionType.Right);
            }
            else if (CurrentMap == 1)
            {
                LoadMap(5, 2, 15, DirectionType.Right);
            }
            else if (CurrentMap == 8)
            {
                LoadMap(4, 2, 16, DirectionType.Right);
            }
        }
        if (HasFlag(GameFlag.Text))
        {
            SetFlag(GameFlag.Text, false);
            LoadMap(9, posX, posY, DirectionType.Up);
        }
        _debounce = false;
    }

    public void SpawnLink(int posX, int posY, DirectionType direction)
    {
        Position = new(posX, posY);

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
            case DirectionType.Up:
                MoveUp(0);
                break;
            case DirectionType.Down:
                MoveDown(0);
                break;
            case DirectionType.Left:
                MoveLeft(0);
                break;
            case DirectionType.Right:
                MoveRight(0);
                break;
        }
    }

    public void BuildChar(int posX, int posY, DirectionType direction) => _player.Draw(posX, posY, direction);

    public void StoreChar(int posX, int posY)
    {
        Map[PosX - 2, PosY - 1] = _storage_map[0];
        Map[PosX - 1, PosY - 1] = _storage_map[1];
        Map[PosX, PosY - 1] = _storage_map[2];
        Map[PosX + 1, PosY - 1] = _storage_map[3];
        Map[PosX + 2, PosY - 1] = _storage_map[4];

        Map[PosX - 2, PosY] = _storage_map[5];
        Map[PosX - 1, PosY] = _storage_map[6];
        Map[PosX, PosY] = _storage_map[7];
        Map[PosX + 1, PosY] = _storage_map[8];
        Map[PosX + 2, PosY] = _storage_map[9];

        Map[PosX - 2, PosY + 1] = _storage_map[10];
        Map[PosX - 1, PosY + 1] = _storage_map[11];
        Map[PosX, PosY + 1] = _storage_map[12];
        Map[PosX + 1, PosY + 1] = _storage_map[13];
        Map[PosX + 2, PosY + 1] = _storage_map[14];

        Map[PosX - 2, PosY + 2] = _storage_map[15];
        Map[PosX - 1, PosY + 2] = _storage_map[16];
        Map[PosX, PosY + 2] = _storage_map[17];
        Map[PosX + 1, PosY + 2] = _storage_map[18];
        Map[PosX + 2, PosY + 2] = _storage_map[19];

        UpdateRow(PosY - 1);
        UpdateRow(PosY);
        UpdateRow(PosY + 1);
        UpdateRow(PosY + 2);

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

    public void DeployRaft(DirectionType direction)
    {
        var spaceslot = ' ';
        var underslot = '_';
        if (HasFlag(GameFlag.HasArmor))
        {
            spaceslot = '#';
            underslot = '#';
        }

        Map[PosX - 3, PosY - 2] = '*';
        Map[PosX - 2, PosY - 2] = '*';
        Map[PosX - 1, PosY - 2] = '*';
        Map[PosX, PosY - 2] = '*';
        Map[PosX + 1, PosY - 2] = '*';
        Map[PosX + 2, PosY - 2] = '*';
        Map[PosX + 3, PosY - 2] = '*';

        Map[PosX - 3, PosY - 1] = '=';
        Map[PosX, PosY - 1] = ' ';
        Map[PosX + 3, PosY - 1] = '=';

        Map[PosX - 3, PosY] = '*';
        Map[PosX - 2, PosY] = '|';
        Map[PosX + 2, PosY] = '|';
        Map[PosX + 3, PosY] = '*';

        Map[PosX - 3, PosY + 1] = '*';
        Map[PosX - 2, PosY + 1] = '|';
        Map[PosX - 1, PosY + 1] = underslot;
        Map[PosX, PosY + 1] = '=';
        Map[PosX + 1, PosY + 1] = underslot;
        Map[PosX + 2, PosY + 1] = '|';
        Map[PosX + 3, PosY + 1] = '*';

        Map[PosX - 3, PosY + 2] = '=';
        Map[PosX - 2, PosY + 2] = '=';
        Map[PosX - 1, PosY + 2] = '=';
        Map[PosX, PosY + 2] = '=';
        Map[PosX + 1, PosY + 2] = '=';
        Map[PosX + 2, PosY + 2] = '=';
        Map[PosX + 3, PosY + 2] = '=';

        Map[PosX - 3, PosY + 3] = '*';
        Map[PosX - 2, PosY + 3] = '*';
        Map[PosX - 1, PosY + 3] = '*';
        Map[PosX, PosY + 3] = '*';
        Map[PosX + 1, PosY + 3] = '*';
        Map[PosX + 2, PosY + 3] = '*';
        Map[PosX + 3, PosY + 3] = '*';

        if (direction == DirectionType.Left)
        {
            Map[PosX - 2, PosY - 1] = '=';
            Map[PosX - 1, PosY - 1] = '/';
            Map[PosX + 1, PosY - 1] = ' ';
            Map[PosX + 2, PosY - 1] = '|';

            Map[PosX - 1, PosY] = '^';
            Map[PosX, PosY] = spaceslot;
            Map[PosX + 1, PosY] = spaceslot;
        }
        else if (direction == DirectionType.Right)
        {
            Map[PosX - 2, PosY - 1] = '|';
            Map[PosX - 1, PosY - 1] = ' ';
            Map[PosX + 1, PosY - 1] = '\\';
            Map[PosX + 2, PosY - 1] = '=';

            Map[PosX - 1, PosY] = spaceslot;
            Map[PosX, PosY] = spaceslot;
            Map[PosX + 1, PosY] = '^';
        }

        UpdateRow(PosY - 2);
        UpdateRow(PosY - 1);
        UpdateRow(PosY);
        UpdateRow(PosY + 1);
        UpdateRow(PosY + 2);
        UpdateRow(PosY + 3);
    }

    public void PlayEffect(char symbol)
    {
        Map[PosX - 2, PosY - 1] = symbol;
        Map[PosX - 1, PosY - 1] = symbol;
        Map[PosX, PosY - 1] = symbol;
        Map[PosX + 1, PosY - 1] = symbol;
        Map[PosX + 2, PosY - 1] = symbol;

        Map[PosX - 2, PosY] = symbol;
        Map[PosX - 1, PosY] = symbol;
        Map[PosX, PosY] = symbol;
        Map[PosX + 1, PosY] = symbol;
        Map[PosX + 2, PosY] = symbol;

        Map[PosX - 2, PosY + 1] = symbol;
        Map[PosX - 1, PosY + 1] = symbol;
        Map[PosX, PosY + 1] = symbol;
        Map[PosX + 1, PosY + 1] = symbol;
        Map[PosX + 2, PosY + 1] = symbol;

        Map[PosX - 2, PosY + 2] = symbol;
        Map[PosX - 1, PosY + 2] = symbol;
        Map[PosX, PosY + 2] = symbol;
        Map[PosX + 1, PosY + 2] = symbol;
        Map[PosX + 2, PosY + 2] = symbol;
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
            SetGameState(GameState.Hit);

            StoreChar(PosX, PosY);

            PlayEffect('*');

            UpdateRow(PosY - 1);
            UpdateRow(PosY);
            UpdateRow(PosY + 1);
            UpdateRow(PosY + 2);
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
                            MainProgram.EnemyManager.RemoveRupee(posX - 2 + j, posY - 1 + i);
                        }
                    }
                }
            }
            return true;
        }
        return false;
    }
}
