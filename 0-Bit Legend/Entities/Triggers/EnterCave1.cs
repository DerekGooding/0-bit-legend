using _0_Bit_Legend.Animations;

namespace _0_Bit_Legend.Entities.Triggers;

public class EnterCave1 : IEntity, ICollider
{
    public Action OnContact { get; } = () =>
    {
        new CaveTransition().Call();
        LoadMap(7, new(50, 18), DirectionType.Up);
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
}