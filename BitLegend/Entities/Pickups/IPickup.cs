namespace BitLegend.Entities.Pickups;

public interface IPickup : IEntity, ICollider
{
    public string[] Image { get; }

    public Action OnPickup { get; }
}
