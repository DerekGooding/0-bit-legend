using _0_Bit_Legend.Entities;
using _0_Bit_Legend.Entities.Enemies;
using _0_Bit_Legend.Entities.Pickups;
using _0_Bit_Legend.Entities.Triggers;

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
    public double Health
    {
        get => _player.Hp;
        set
        {
            if(_player.Hp == value) return;
            _player.Hp = value;
            RequireHudDraw = true;
        }
    }

    public DirectionType GetPrev() => _prev;
    public DirectionType GetPrev2() => _prev2;
    public void SetPosition(Vector2 pos) => _player.Position = pos;

    public (Vector2 Position, DirectionType Prev1) GetPlayerInfo() => (_player.Position, _prev);

    public void Attack()
    {


        Stab();
    }

    public void Stab()
    {
        //MainProgram.EntityManager.TakeDamage(new(swordArr[0, i], swordArr[1, i]), prev);
        SetGameState(GameState.Idle);
    }

    public void MoveUp(int magnitude = 1) => Move(DirectionType.Up, magnitude);
    public void MoveLeft(int magnitude = 1) => Move(DirectionType.Left, magnitude);
    public void MoveDown(int magnitude = 1) => Move(DirectionType.Down, magnitude);
    public void MoveRight(int magnitude = 1) => Move(DirectionType.Right, magnitude);

    private void Move(DirectionType direction, int magnitude = 1)
    {
        var points = GetMovePoints(direction);
        if (!CanMove(points))
        {
            HandleDebugDraw(points);
            return;
        }
        _player.Direction = direction;
        _player.Position = direction switch
        {
            DirectionType.Up => _player.Position.Offset(0, -magnitude),
            DirectionType.Left => _player.Position.Offset(-magnitude * 2, 0),
            DirectionType.Down => _player.Position.Offset(0, magnitude),
            DirectionType.Right => _player.Position.Offset(magnitude * 2, 0),
            _ => throw new Exception(),
        };

        var box = GetMoveBox(direction);
        HandleCollisions(MainProgram.EntityManager.GetCollisions(box));
    }

    public void SpawnLink(Vector2 position, DirectionType direction)
    {
        _player.Position = position;
        _player.Direction = direction;
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

    private List<Vector2> GetMovePoints(DirectionType direction)
    {
        var bottom = _player.Position.Y + _player.Size.Y;
        var right = _player.Position.X + _player.Size.X;
        return direction switch
        {
            DirectionType.Up =>
            [.. Enumerable.Range(_player.Position.X, _player.Size.X + 1).Select(p => new Vector2(p, _player.Position.Y - 1))],
            DirectionType.Down =>
            [.. Enumerable.Range(_player.Position.X, _player.Size.X + 1).Select(p => new Vector2(p, bottom + 1))],
            DirectionType.Left =>
            [.. Enumerable.Range(_player.Position.Y, _player.Size.Y + 1).Select(p => new Vector2(_player.Position.X - 2, p))],
            DirectionType.Right =>
            [.. Enumerable.Range(_player.Position.Y, _player.Size.Y + 1).Select(p => new Vector2(right + 2, p))],
            _ => throw new Exception()
        };
    }

    private CollisionBox GetMoveBox(DirectionType direction)
    {
        var width = _player.Size.X + 1;
        var height = _player.Size.Y + 1;

        return direction switch
        {
            DirectionType.Up =>    new(_player.Position.Offset(0, -1), new(_player.Size.X, 0)),
            DirectionType.Down =>  new(_player.Position.Offset(0, height + 1), new(_player.Size.X, 0)),
            DirectionType.Left =>  new(_player.Position.Offset(-2, 0), new(0, _player.Size.Y)),
            DirectionType.Right => new(_player.Position.Offset(width + 2, 0), new(0, _player.Size.Y)),
            _ => throw new Exception()
        };
    }

    private void HandleCollisions(List<ICollider> colliders)
    {
        if(colliders.Count == 0) return;
        foreach (var entity in colliders)
        {
            entity.HandleCollision();
        }
    }

    private bool CanMove(List<Vector2> points) => !points.Any(OutsideGameSpace) && !points.Any(IsBlocking);

    private bool IsBlocking(Vector2 point) => WallMap[point.X, point.Y];

    private bool OutsideGameSpace(Vector2 point)
    => point.X < 0
    || point.X >= GlobalSize.X
    || point.Y < 0
    || point.Y >= GlobalSize.Y;


}
