using _0_Bit_Legend.Entities;
using _0_Bit_Legend.Entities.Enemies;
using _0_Bit_Legend.Entities.Pickups;

namespace _0_Bit_Legend.Managers;

public class EntityManager
{
    private readonly List<IEnemy> _enemies = [];
    private readonly List<Rupee> _rupees = [];
    private readonly List<IEntity> _entities = new List<IEntity>();

    public List<IEnemy> GetCollisions(Vector2 Position, Vector2 Size)
    {
        var ax = Position.X;
        var ay = Position.Y;
        var aw = Size.X;
        var ah = Size.Y;


        return [.. _enemies.Where(b => Overlaps(ax, ay, aw, ah, b.Position.X, b.Position.Y, b.Size.X, b.Size.Y))];
    }

    public bool TakeDamage(Vector2 target, DirectionType prev)
    {
        var enemy = GetEnemyAt(target);
        //TODO => Trigger sword collapse assuming that's in the future system
        //MainProgram.PlayerController.StoreSword(prev);

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
        for(var i = _enemies.Count - 1; i >= 0; i--)
        {
            var enemy = _enemies[i];
            enemy.Move();
        }
    }

    public void Remove(IEnemy enemy)
    {
        _enemies.Remove(enemy);
        var type = enemy.Type;

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

    public void RemoveRupee(Vector2 target)
    {
        var posX = target.X;
        var posY = target.Y;

        //Always reverse order for removing from lists.
        //Bottom up so when the reorganize, you don't break things in the middle of a loop.
        for (var i = _rupees.Count - 1; i >= 0; i--)
        {
            var rupee = _rupees[i];
            var position = rupee.Position;
            var X = position.X;
            var Y = position.Y;
            if (posX >= X - 1 && posX <= X + 1 && posY >= Y - 1 && posY <= Y + 1)
            {
                Rupees += 5;

                _rupees.Remove(rupee);
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

    public void AddRupee(Rupee rupee) => _rupees.Add(rupee);

    internal void Draw()
    {
        foreach (var enemy in _enemies)
            enemy.Draw();
    }
}