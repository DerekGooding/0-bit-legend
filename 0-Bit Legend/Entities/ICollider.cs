namespace _0_Bit_Legend.Entities;

public interface ICollider : IWorldSpace
{
    //Origin point is top left corner of object
    public Vector2 Size { get; }

    public void HandleCollision();
}
