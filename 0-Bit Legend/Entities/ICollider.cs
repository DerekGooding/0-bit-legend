namespace _0_bit_Legend.Entities;

public interface ICollider : IWorldSpace
{
    //Origin point is top left corner of object
    public Vector2 Size { get; }

    public void HandleCollision();
}
