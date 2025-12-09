using BitLegend.Entities.Enemies;
using BitLegend.Entities;

namespace BitLegend.Managers;

public class EntityManager
{
    private readonly List<IEntity> _entities = [];
    private readonly List<IEnemy> _enemies = [];

    public List<ICollider> GetCollisions(CollisionBox collisionBox)
    {
        var ax = collisionBox.Position.X;
        var ay = collisionBox.Position.Y;
        var aw = collisionBox.Size.X;
        var ah = collisionBox.Size.Y;

        return
        [.. _entities.Where(b => b is ICollider c
        && Overlaps(ax, ay, aw, ah, c.Position.X, c.Position.Y, c.Size.X, c.Size.Y)).OfType<ICollider>()];
    }

    public List<IEnemy> GetEnemyCollisions(CollisionBox collisionBox)
    {
        var ax = collisionBox.Position.X;
        var ay = collisionBox.Position.Y;
        var aw = collisionBox.Size.X;
        var ah = collisionBox.Size.Y;

        return
        [.. _enemies.Where(b => b is ICollider c
        && Overlaps(ax, ay, aw, ah, c.Position.X, c.Position.Y, c.Size.X, c.Size.Y))];
    }


    public List<(Vector2 Position, Vector2 Size)> GetPositionalData()
    {
        List<(Vector2 Position, Vector2 Size)> result = [];
        foreach(var entity in _entities)
        {
            if (entity is ICollider c)
                result.Add((entity.Position, c.Size));
        }
        return result;
    }

    public bool TakeDamage(SwordInUse sword)
    {
        var box = new CollisionBox(sword.Position, sword.Size);
        foreach (var enemy in GetEnemyCollisions(box))
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

        _entities.Add(enemy);
        _enemies.Add(enemy);
    }

    public void RemoveAll()
    {
        _entities.Clear();
        _enemies.Clear();
    }

    public void MoveAll()
    {
        for(var i = _enemies.Count - 1; i >= 0; i--)
        {
            var enemy = _enemies[i];
            enemy.Move();
        }
    }

    public void Remove(IEntity entity)
    {
        _entities.Remove(entity);
        if (entity is not IEnemy enemy) return;

        _enemies.Remove(enemy);
        if(enemy.Type != EnemyType.Bat) return;

        if (CurrentMap == WorldMap.MapName.Castle2)
        {
            cEnemies1--;
        }
        else if (CurrentMap == WorldMap.MapName.Castle3)
        {
            cEnemies2--;
        }
    }

    public void Add(IEntity entity)
    {
        if(entity is IEnemy enemy)
            _enemies.Add(enemy);
        _entities.Add(entity);
    }

    internal void Draw()
    {
        foreach (var entity in _entities)
            entity.Draw();
    }
}