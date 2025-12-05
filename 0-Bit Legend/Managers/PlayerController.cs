using _0_Bit_Legend.Entities;

namespace _0_Bit_Legend.Managers;

public class PlayerController
{
    private readonly Hero _player = new();

    private static readonly char[] _storage_sword = new char[6];
    private static readonly char[] _storage_detect_enemy = new char[6];

    private DirectionType _prev = DirectionType.Up;
    private DirectionType _prev2 = DirectionType.Left;

    //private bool _debounce;
    //private bool _swingingSword;

    public int MovementWait;
    public double Health { get => _player.Hp; set => _player.Hp = value;  }

    public DirectionType GetPrev() => _prev;
    public DirectionType GetPrev2() => _prev2;
    public void SetPosition(Vector2 pos) => _player.Position = pos;

    public (Vector2 Position, DirectionType Prev1) GetPlayerInfo() => (_player.Position, _prev);

    public void Attack()
    {
        //TODO => Sword attack system. Likely needs a new bounding object type

        //switch (_prev)
        //{
        //    case DirectionType.Up:
        //        HandleAttackUp();
        //        break;
        //    case DirectionType.Left:
        //        HandleAttackLeft();
        //        break;
        //    case DirectionType.Down:
        //        HandleAttackDown();
        //        break;
        //    case DirectionType.Right:
        //        HandleAttackRight();
        //        break;
        //}
    }


    //private void HandleAttackUp()
    //{
    //    if (!_swingingSword)
    //    {
    //        _storage_sword[0] = Map[PosX - 1, PosY - 2];
    //        _storage_sword[1] = Map[PosX, PosY - 2];
    //        _storage_sword[2] = Map[PosX + 1, PosY - 2];
    //        _storage_sword[3] = Map[PosX, PosY - 3];
    //        _storage_sword[4] = Map[PosX, PosY - 4];

    //        Map[PosX - 1, PosY - 2] = '-';
    //        Map[PosX, PosY - 2] = '-';
    //        Map[PosX + 1, PosY - 2] = '-';
    //        Map[PosX, PosY - 3] = 'S';
    //        Map[PosX, PosY - 4] = 'S';

    //        _preHitPosition = new(PosX, PosY);
    //        _swingingSword = true;
    //    }
    //    else
    //    {
    //        _storage_detect_enemy[0] = Map[PreHitPosX - 1, PreHitPosY - 2];
    //        _storage_detect_enemy[1] = Map[PreHitPosX, PreHitPosY - 2];
    //        _storage_detect_enemy[2] = Map[PreHitPosX + 1, PreHitPosY - 2];
    //        _storage_detect_enemy[3] = Map[PreHitPosX, PreHitPosY - 3];
    //        _storage_detect_enemy[4] = Map[PreHitPosX, PreHitPosY - 4];

    //        var swordArr = new int[2, 5];
    //        swordArr[0, 0] = PreHitPosX - 1;
    //        swordArr[1, 0] = PreHitPosY - 2;
    //        swordArr[0, 1] = PreHitPosX;
    //        swordArr[1, 1] = PreHitPosY - 2;
    //        swordArr[0, 2] = PreHitPosX + 1;
    //        swordArr[1, 2] = PreHitPosY - 2;
    //        swordArr[0, 3] = PreHitPosX;
    //        swordArr[1, 3] = PreHitPosY - 3;
    //        swordArr[0, 4] = PreHitPosX;
    //        swordArr[1, 4] = PreHitPosY - 4;

    //        Stab(swordArr, DirectionType.Up, 5);
    //    }
    //}
    //private void HandleAttackLeft()
    //{
    //    if (!_swingingSword)
    //    {
    //        _storage_sword[0] = Map[PosX - 3, PosY];
    //        _storage_sword[1] = Map[PosX - 3, PosY + 1];
    //        _storage_sword[2] = Map[PosX - 3, PosY + 2];
    //        _storage_sword[3] = Map[PosX - 4, PosY + 1];
    //        _storage_sword[4] = Map[PosX - 5, PosY + 1];
    //        _storage_sword[5] = Map[PosX - 6, PosY + 1];

    //        Map[PosX - 3, PosY] = '-';
    //        Map[PosX - 3, PosY + 1] = '-';
    //        Map[PosX - 3, PosY + 2] = '-';
    //        Map[PosX - 4, PosY + 1] = 'S';
    //        Map[PosX - 5, PosY + 1] = 'S';
    //        Map[PosX - 6, PosY + 1] = 'S';

    //        _preHitPosition = new(PosX, PosY);
    //        _swingingSword = true;
    //    }
    //    else
    //    {
    //        _storage_detect_enemy[0] = Map[PreHitPosX - 3, PreHitPosY];
    //        _storage_detect_enemy[1] = Map[PreHitPosX - 3, PreHitPosY + 1];
    //        _storage_detect_enemy[2] = Map[PreHitPosX - 3, PreHitPosY + 2];
    //        _storage_detect_enemy[3] = Map[PreHitPosX - 4, PreHitPosY + 1];
    //        _storage_detect_enemy[4] = Map[PreHitPosX - 5, PreHitPosY + 1];
    //        _storage_detect_enemy[5] = Map[PreHitPosX - 6, PreHitPosY + 1];

    //        var swordArr = new int[2, 6];
    //        swordArr[0, 0] = PreHitPosX - 3;
    //        swordArr[1, 0] = PreHitPosY;
    //        swordArr[0, 1] = PreHitPosX - 3;
    //        swordArr[1, 1] = PreHitPosY + 1;
    //        swordArr[0, 2] = PreHitPosX - 3;
    //        swordArr[1, 2] = PreHitPosY + 2;
    //        swordArr[0, 3] = PreHitPosX - 4;
    //        swordArr[1, 3] = PreHitPosY + 1;
    //        swordArr[0, 4] = PreHitPosX - 5;
    //        swordArr[1, 4] = PreHitPosY + 1;
    //        swordArr[0, 5] = PreHitPosX - 6;
    //        swordArr[1, 5] = PreHitPosY + 1;

    //        Stab(swordArr, DirectionType.Left, 6);
    //    }
    //}
    //private void HandleAttackDown()
    //{
    //    if (!_swingingSword)
    //    {
    //        _storage_sword[0] = Map[PosX - 1, PosY + 3];
    //        _storage_sword[1] = Map[PosX, PosY + 3];
    //        _storage_sword[2] = Map[PosX + 1, PosY + 3];
    //        _storage_sword[3] = Map[PosX, PosY + 4];
    //        _storage_sword[4] = Map[PosX, PosY + 5];

    //        Map[PosX - 1, PosY + 3] = '-';
    //        Map[PosX, PosY + 3] = '-';
    //        Map[PosX + 1, PosY + 3] = '-';
    //        Map[PosX, PosY + 4] = 'S';
    //        Map[PosX, PosY + 5] = 'S';

    //        _preHitPosition = new(PosX, PosY);
    //        _swingingSword = true;
    //    }
    //    else
    //    {
    //        _storage_detect_enemy[0] = Map[PreHitPosX - 1, PreHitPosY + 3];
    //        _storage_detect_enemy[1] = Map[PreHitPosX, PreHitPosY + 3];
    //        _storage_detect_enemy[2] = Map[PreHitPosX + 1, PreHitPosY + 3];
    //        _storage_detect_enemy[3] = Map[PreHitPosX, PreHitPosY + 4];
    //        _storage_detect_enemy[4] = Map[PreHitPosX, PreHitPosY + 5];

    //        var swordArr = new int[2, 5];
    //        swordArr[0, 0] = PreHitPosX - 1;
    //        swordArr[1, 0] = PreHitPosY + 3;
    //        swordArr[0, 1] = PreHitPosX;
    //        swordArr[1, 1] = PreHitPosY + 3;
    //        swordArr[0, 2] = PreHitPosX + 1;
    //        swordArr[1, 2] = PreHitPosY + 3;
    //        swordArr[0, 3] = PreHitPosX;
    //        swordArr[1, 3] = PreHitPosY + 4;
    //        swordArr[0, 4] = PreHitPosX;
    //        swordArr[1, 4] = PreHitPosY + 5;

    //        Stab(swordArr, DirectionType.Down, 5);
    //    }
    //}
    //private void HandleAttackRight()
    //{
    //    if (!_swingingSword)
    //    {
    //        _storage_sword[0] = Map[PosX + 3, PosY];
    //        _storage_sword[1] = Map[PosX + 3, PosY + 1];
    //        _storage_sword[2] = Map[PosX + 3, PosY + 2];
    //        _storage_sword[3] = Map[PosX + 4, PosY + 1];
    //        _storage_sword[4] = Map[PosX + 5, PosY + 1];
    //        _storage_sword[5] = Map[PosX + 6, PosY + 1];

    //        Map[PosX + 3, PosY] = '-';
    //        Map[PosX + 3, PosY + 1] = '-';
    //        Map[PosX + 3, PosY + 2] = '-';
    //        Map[PosX + 4, PosY + 1] = 'S';
    //        Map[PosX + 5, PosY + 1] = 'S';
    //        Map[PosX + 6, PosY + 1] = 'S';

    //        _preHitPosition = new(PosX, PosY);
    //        _swingingSword = true;
    //    }
    //    else
    //    {
    //        _storage_detect_enemy[0] = Map[PreHitPosX + 3, PreHitPosY];
    //        _storage_detect_enemy[1] = Map[PreHitPosX + 3, PreHitPosY + 1];
    //        _storage_detect_enemy[2] = Map[PreHitPosX + 3, PreHitPosY + 2];
    //        _storage_detect_enemy[3] = Map[PreHitPosX + 4, PreHitPosY + 1];
    //        _storage_detect_enemy[4] = Map[PreHitPosX + 5, PreHitPosY + 1];
    //        _storage_detect_enemy[5] = Map[PreHitPosX + 6, PreHitPosY + 1];

    //        var swordArr = new int[2, 6];
    //        swordArr[0, 0] = PreHitPosX + 3;
    //        swordArr[1, 0] = PreHitPosY;
    //        swordArr[0, 1] = PreHitPosX + 3;
    //        swordArr[1, 1] = PreHitPosY + 1;
    //        swordArr[0, 2] = PreHitPosX + 3;
    //        swordArr[1, 2] = PreHitPosY + 2;
    //        swordArr[0, 3] = PreHitPosX + 4;
    //        swordArr[1, 3] = PreHitPosY + 1;
    //        swordArr[0, 4] = PreHitPosX + 5;
    //        swordArr[1, 4] = PreHitPosY + 1;
    //        swordArr[0, 5] = PreHitPosX + 6;
    //        swordArr[1, 5] = PreHitPosY + 1;

    //        Stab(swordArr, DirectionType.Right, 6);
    //    }
    //}

    public void Stab(int[,] swordArr, DirectionType prev, int amt)
    {
        var hit = false;
        for (var i = 0; i < amt; i++)
        {
            char[] detect = ['t', 'n', 'B', '{', '}', 'F'];
            if (detect.Any(x => x == _storage_sword[i]) || detect.Any(x => x == _storage_detect_enemy[i]))
            {
                hit = true;
                MainProgram.EntityManager.TakeDamage(new(swordArr[0, i], swordArr[1, i]), prev);
                break;
            }
        }
        if (!hit)
        {
            //StoreSword(prev);
        }
        SetGameState(GameState.Idle);
        //_swingingSword = false;
    }

    public void MoveUp(int magnitude = 1)
    {
        _player.Direction = DirectionType.Up;

        _player.Position = _player.Position.Offset(0, -magnitude);
        //if (PosX == 21 && ((CurrentMap == 4 && posY > 9) || CurrentMap == 2))
        //{
        //    if (posY > 1)
        //    {
        //        for (var y = PosY - 2; y <= PosY + 3; y++)
        //        {
        //            for (var x = PosX - 3; x <= PosX + 3; x++)
        //            {
        //                Map[x, y] = '~';
        //            }
        //        }

        //        _player.Position = new(_player.Position.X, _player.Position.Y - 1);
        //        DeployRaft(_prev2);
        //    }
        //    else
        //    {
        //        LoadMap(4, new(21, 29), DirectionType.Up);
        //    }
        //}
        //else if (posY >= 1 && !(PosX == 21 && (CurrentMap == 4 || CurrentMap == 2)))
        //{
        //    _player.IsTouching('r');
        //    StoreChar(new(PosX, PosY));
        //    var inCave = false;

        //    if (CurrentMap == 6 && (_player.IsTouching('-') || _player.IsTouching('S')))
        //    {
        //        SetFlag(GameFlag.HasSword);
        //        LoadMap(6, new(posX, posY), DirectionType.Up);
        //    }
        //    else if (CurrentMap == 7 && _player.IsTouching('*') && Rupees >= 35)
        //    {
        //        Rupees -= 35;

        //        SetFlag(GameFlag.HasRaft);
        //        LoadMap(7, new(posX, posY), DirectionType.Up);
        //    }
        //    else if (CurrentMap == 7 && _player.IsTouching('Y') && Rupees >= 5)
        //    {
        //        Rupees -= 10;
        //        Keys++;
        //    }
        //    else if (CurrentMap == 7 && _player.IsTouching('#') && Rupees >= 15)
        //    {
        //        Rupees -= 25;

        //        SetFlag(GameFlag.HasArmor);
        //        LoadMap(7, new(posX, posY), DirectionType.Up);
        //    }
        //    else if (CurrentMap == 9
        //        && _player.IsTouching('X')
        //        && !_player.IsTouching('=')
        //        && HasFlags([GameFlag.Door1, GameFlag.Door2, GameFlag.Door3])
        //        && cEnemies1 <= 0
        //        && cEnemies2 <= 0
        //        && !_debounce)
        //    {
        //        LoadMap(12, new(50, 24), DirectionType.Up);
        //    }
        //    else if (CurrentMap == 9 && PosX >= 48 && PosX <= 52 && PosY == 7 && !HasFlag(GameFlag.Door3) && Keys > 0)
        //    {
        //        _debounce = true;
        //        Keys--;

        //        SetFlag(GameFlag.Door3, true);
        //        LoadMap(9, new(PosX, PosY), DirectionType.Up);
        //    }

        //    if (!_player.IsTouching('=')
        //        && !_player.IsTouching('X')
        //        && !_player.IsTouching('t')
        //        && !_player.IsTouching('n')
        //        && !_player.IsTouching('B')
        //        && !_player.IsTouching('{')
        //        && !_player.IsTouching('}')
        //        && !_player.IsTouching('S')
        //        && !_player.IsTouching('<')
        //        && !_player.IsTouching('>')
        //        && !_player.IsTouching('*')
        //        && !(_player.IsTouching('F') && CurrentMap != 7)
        //        && !_player.IsTouching('~')
        //        && !((CurrentMap == 6 || CurrentMap == 7) && posY < 17)
        //        && ((CurrentMap >= 9 && !_player.IsTouching('/')) || CurrentMap < 9))
        //    {
        //        if (_player.IsTouching('/'))
        //        {
        //            inCave = true;
        //        }

        //        StoreChar(new(posX, posY));
        //        _player.Direction = DirectionType.Up;
        //        _player.Draw();

        //        _player.Position = new(posX, posY);
        //    }
        //    else if (_player.IsTouching('t')
        //        || _player.IsTouching('n')
        //        || _player.IsTouching('B')
        //        || _player.IsTouching('{')
        //        || _player.IsTouching('}')
        //        || _player.IsTouching('S')
        //        || _player.IsTouching('<')
        //        || (_player.IsTouching('F') && CurrentMap != 7))
        //    {
        //        _player.Direction = DirectionType.Up;
        //        _player.Draw();

        //        Hit();
        //    }
        //    else if (CurrentMap == 13 && _player.IsTouching('~'))
        //    {
        //        _player.Direction = DirectionType.Up;
        //        _player.Draw();

        //        LoadMap(13, new(58, 15), DirectionType.Left);
        //        SetGameState(GameState.GameOver);
        //    }
        //    else
        //    {
        //        _player.Direction = DirectionType.Up;
        //        _player.Draw();
        //    }

        //    if (inCave)
        //    {
        //        WaitForTransition();
        //    }
        //}
        //else
        //{
        //    if (CurrentMap == 0)
        //    {
        //        LoadMap(1, new(63, 29), DirectionType.Up);
        //    }
        //    else if (CurrentMap == 2)
        //    {
        //        if (posX > 29)
        //        {
        //            LoadMap(4, new(55, 30), DirectionType.Up);
        //        }
        //        else if (posX == 21)
        //        {
        //            LoadMap(4, new(21, 29), DirectionType.Up);
        //        }
        //        else
        //        {
        //            LoadMap(4, new(10, 29), DirectionType.Up);
        //        }
        //    }
        //    else if (CurrentMap == 3)
        //    {
        //        LoadMap(5, new(49, 30), DirectionType.Up);
        //    }
        //}
        //if (HasFlag(GameFlag.Text))
        //{
        //    SetFlag(GameFlag.Text, false);
        //    LoadMap(9, new(posX, posY), DirectionType.Up);
        //}

        //_debounce = false;
    }
    public void MoveLeft(int magnitude = 1)
    {
        _player.Direction = DirectionType.Left;
        _player.Position = _player.Position.Offset(-magnitude*2, 0);
    }
    public void MoveDown(int magnitude = 1)
    {
        _player.Direction = DirectionType.Down;
        _player.Position = _player.Position.Offset(0, magnitude);
    }
    public void MoveRight(int magnitude = 1)
    {
        _player.Direction = DirectionType.Right;
        _player.Position = _player.Position.Offset(magnitude * 2, 0);
    }

    public void SpawnLink(Vector2 position, DirectionType direction)
    {
        _player.Position = position;

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

    public void DeployRaft(DirectionType direction)
    {

        //TODO => Draw something

        //var spaceslot = ' ';
        //var underslot = '_';
        //if (HasFlag(GameFlag.HasArmor))
        //{
        //    spaceslot = '#';
        //    underslot = '#';
        //}

        //Map[PosX - 3, PosY - 2] = '*';
        //Map[PosX - 2, PosY - 2] = '*';
        //Map[PosX - 1, PosY - 2] = '*';
        //Map[PosX, PosY - 2] = '*';
        //Map[PosX + 1, PosY - 2] = '*';
        //Map[PosX + 2, PosY - 2] = '*';
        //Map[PosX + 3, PosY - 2] = '*';

        //Map[PosX - 3, PosY - 1] = '=';
        //Map[PosX, PosY - 1] = ' ';
        //Map[PosX + 3, PosY - 1] = '=';

        //Map[PosX - 3, PosY] = '*';
        //Map[PosX - 2, PosY] = '|';
        //Map[PosX + 2, PosY] = '|';
        //Map[PosX + 3, PosY] = '*';

        //Map[PosX - 3, PosY + 1] = '*';
        //Map[PosX - 2, PosY + 1] = '|';
        //Map[PosX - 1, PosY + 1] = underslot;
        //Map[PosX, PosY + 1] = '=';
        //Map[PosX + 1, PosY + 1] = underslot;
        //Map[PosX + 2, PosY + 1] = '|';
        //Map[PosX + 3, PosY + 1] = '*';

        //Map[PosX - 3, PosY + 2] = '=';
        //Map[PosX - 2, PosY + 2] = '=';
        //Map[PosX - 1, PosY + 2] = '=';
        //Map[PosX, PosY + 2] = '=';
        //Map[PosX + 1, PosY + 2] = '=';
        //Map[PosX + 2, PosY + 2] = '=';
        //Map[PosX + 3, PosY + 2] = '=';

        //Map[PosX - 3, PosY + 3] = '*';
        //Map[PosX - 2, PosY + 3] = '*';
        //Map[PosX - 1, PosY + 3] = '*';
        //Map[PosX, PosY + 3] = '*';
        //Map[PosX + 1, PosY + 3] = '*';
        //Map[PosX + 2, PosY + 3] = '*';
        //Map[PosX + 3, PosY + 3] = '*';

        //if (direction == DirectionType.Left)
        //{
        //    Map[PosX - 2, PosY - 1] = '=';
        //    Map[PosX - 1, PosY - 1] = '/';
        //    Map[PosX + 1, PosY - 1] = ' ';
        //    Map[PosX + 2, PosY - 1] = '|';

        //    Map[PosX - 1, PosY] = '^';
        //    Map[PosX, PosY] = spaceslot;
        //    Map[PosX + 1, PosY] = spaceslot;
        //}
        //else if (direction == DirectionType.Right)
        //{
        //    Map[PosX - 2, PosY - 1] = '|';
        //    Map[PosX - 1, PosY - 1] = ' ';
        //    Map[PosX + 1, PosY - 1] = '\\';
        //    Map[PosX + 2, PosY - 1] = '=';

        //    Map[PosX - 1, PosY] = spaceslot;
        //    Map[PosX, PosY] = spaceslot;
        //    Map[PosX + 1, PosY] = '^';
        //}
    }

    public void PlayEffect(char symbol)
    {
        //TODO => Turn this into a drawn object

        //Map[PosX - 2, PosY - 1] = symbol;
        //Map[PosX - 1, PosY - 1] = symbol;
        //Map[PosX, PosY - 1] = symbol;
        //Map[PosX + 1, PosY - 1] = symbol;
        //Map[PosX + 2, PosY - 1] = symbol;

        //Map[PosX - 2, PosY] = symbol;
        //Map[PosX - 1, PosY] = symbol;
        //Map[PosX, PosY] = symbol;
        //Map[PosX + 1, PosY] = symbol;
        //Map[PosX + 2, PosY] = symbol;

        //Map[PosX - 2, PosY + 1] = symbol;
        //Map[PosX - 1, PosY + 1] = symbol;
        //Map[PosX, PosY + 1] = symbol;
        //Map[PosX + 1, PosY + 1] = symbol;
        //Map[PosX + 2, PosY + 1] = symbol;

        //Map[PosX - 2, PosY + 2] = symbol;
        //Map[PosX - 1, PosY + 2] = symbol;
        //Map[PosX, PosY + 2] = symbol;
        //Map[PosX + 1, PosY + 2] = symbol;
        //Map[PosX + 2, PosY + 2] = symbol;
    }

    public void PlacePrincess()
    {
        var princess = new Princess
        {
            Position = new(52, 15)
        };
        MainProgram.EntityManager.Add(princess);
        RequiresRedraw = true;
    }

    public void Hit()
    {
        if (HasFlag(GameFlag.HasArmor))
        {
            Health -= 0.5;
        }
        else
        {
            Health--;
        }
        SetGameState(GameState.Hit);

        PlayEffect('*');
        if(_player.Hp <= 0)
            SetGameState(GameState.Dead);
    }

    internal void Draw() => _player.Draw();

    private bool InBounds()
    {
        return true;
    }
}
