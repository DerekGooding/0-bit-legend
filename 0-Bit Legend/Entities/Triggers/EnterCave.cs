using _0_Bit_Legend.Animations;

namespace _0_Bit_Legend.Entities.Triggers;

public class EnterCave0 : IEntity, ICollider
{
    public Action OnContact { get; } = () =>
    {
        new CaveTransition().Call();
        LoadMap(6, new(50, 18), DirectionType.Up);
    };

    public Vector2 Position { get; set; }
    public DirectionType Direction { get; set; }

    public Vector2 Size { get; }

    public void Draw()
    {

    }
}
