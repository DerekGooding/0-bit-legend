namespace _0_Bit_Legend.Entities.Pickups;

public interface IPickup : IEntity, IBoundingBox
{
    public string[] Image { get; }

    public Action OnPickup { get; }
}
