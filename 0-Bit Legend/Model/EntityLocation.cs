using _0_Bit_Legend.Entities;

namespace _0_Bit_Legend.Model;

public readonly record struct EntityLocation
{
    public Type EntityType { get; }
    public Vector2 Position { get; }
    public Func<bool> IsActive { get; }

    public EntityLocation(Type entityType, Vector2 position, Func<bool> isActive)
    {
        if (!typeof(IEntity).IsAssignableFrom(entityType))
            throw new ArgumentException("Type must implement IEntity");
        EntityType = entityType;
        Position = position;
        IsActive = isActive;
    }
}