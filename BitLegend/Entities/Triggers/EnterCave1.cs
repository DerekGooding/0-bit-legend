using _0_bit_Legend.Animations;
using BitLegend.Content;
using BitLegend.Entities;
using BitLegend.Model;
using BitLegend.Model.Enums;

namespace BitLegend.Entities.Triggers;

public class EnterCave1 : IEntity, ICollider
{
    public Action OnContact { get; } = () =>
    {
        new CaveTransition().Call();
        LoadMap(WorldMap.MapName.Cave1, new(48, 27), DirectionType.Up);
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