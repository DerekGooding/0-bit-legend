using _0_Bit_Legend.Animations;
using _0_Bit_Legend.Content;

namespace _0_Bit_Legend.Entities.Triggers;

public class EnterCave0 : IEntity, ICollider
{
    public Action OnContact { get; } = () =>
    {
        new CaveTransition().Call();
        LoadMap(WorldMap.MapName.Cave0, new(48, 27), DirectionType.Up);
    };

    public Vector2 Position { get; set; }
    public DirectionType Direction { get; set; }

    public Vector2 Size { get; } = new Vector2(6, 2);

    private readonly string[] _spriteSheet =
    [
        "///////",
        "///////",
        "///////",
    ];

    public void Draw() => DrawToScreen(_spriteSheet, Position);

    public void HandleCollision() => OnContact.Invoke();
}
