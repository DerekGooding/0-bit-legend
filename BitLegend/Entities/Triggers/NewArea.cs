using BitLegend.Entities;
using BitLegend.Model;
using BitLegend.Model.Enums;
using System.Drawing;

namespace BitLegend.Entities.Triggers;

public class NewArea : IEntity, ICollider
{
    public Action OnContact { get; private set; } = () => LoadMap(0, Vector2.Zero, DirectionType.Up);

    public Vector2 Position { get; set; } = Vector2.Zero;
    public DirectionType Direction { get; set; }

    public Vector2 Size { get; private set; } = Vector2.Zero;

    private readonly string[] _spriteSheet = [ " " ];

    public void Draw()
    {
        //Nothing
    }
    public void HandleCollision() => OnContact.Invoke();

    public static NewArea Initialize(NewAreaInfo info) => new()
    {
        OnContact = () => LoadMap(info.MapId, info.StartPosition, info.StartDirection),
        Size = info.Size,
        Position = info.Position,
    };
}
