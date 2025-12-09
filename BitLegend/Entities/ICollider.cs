using BitLegend.Model;

namespace BitLegend.Entities;

public interface ICollider : IWorldSpace
{
    //Origin point is top left corner of object
    public Vector2 Size { get; }

    public void HandleCollision();
}
