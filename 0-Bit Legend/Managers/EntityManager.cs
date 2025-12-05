using _0_Bit_Legend.Entities;
using _0_Bit_Legend.Entities.Enemies;

namespace _0_Bit_Legend.Managers;

public class EntityManager
{
    private readonly List<IEntity> _entities = [];

    public List<ICollider> GetCollisions(Vector2 Position, Vector2 Size)
    {
        var ax = Position.X;
        var ay = Position.Y;
        var aw = Size.X;
        var ah = Size.Y;

        return [.. _entities.Where(b => b is ICollider c
        && Overlaps(ax, ay, aw, ah, b.Position.X, b.Position.Y, c.Size.X, c.Size.Y)).OfType<ICollider>()];
    }

    public bool TakeDamage(Vector2 target, DirectionType prev)
    {
        //var enemy = GetEnemyAt(target);
        //TODO => Trigger sword collapse assuming that's in the future system
        //MainProgram.PlayerController.StoreSword(prev);

        //enemy.TakeDamage();

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

        _entities.Add(enemy);
    }

    public void RemoveAll() => _entities.Clear();
    public void MoveAll()
    {
        var enemies = _entities.OfType<IEnemy>().ToList();
        for(var i = enemies.Count - 1; i >= 0; i--)
        {
            var enemy = enemies[i];
            enemy.Move();
        }
    }

    public void Remove(IEntity entity)
    {
        if(entity is  Bat)
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

        _entities.Remove(entity);
    }

    public void Add(IEntity entity) => _entities.Add(entity);

    internal void Draw()
    {
        foreach (var entity in _entities)
            entity.Draw();
    }
}