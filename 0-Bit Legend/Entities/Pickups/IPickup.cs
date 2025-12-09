namespace _0_bit_Legend.Entities.Pickups;

public interface IPickup : IEntity, ICollider
{
    public string[] Image { get; }

    public Action OnPickup { get; }
}
