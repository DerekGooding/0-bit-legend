using BitLegend.Model;
using BitLegend.Model.Enums;

namespace BitLegend.Entities;

public class Princess : IEntity, ICollider
{
    public int Hp { get; set; } = 3;
    public Vector2 Position { get; set; } = Vector2.Zero;
    public DirectionType Direction { get; set; } = DirectionType.Up;

    public Vector2 Size { get; } = new(5, 3);

    private readonly string[] _spriteSheet =
    [
        " =<>= ",
        " s^^s ",
        "ss~~ss",
        "~~~~~~",
    ];

    public void Draw() => DrawToScreen(_spriteSheet, Position);
    public void HandleCollision()
    {
        //TODO
    }
}
