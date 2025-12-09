namespace BitLegend.Entities.Triggers;

public interface ITrigger : IEntity, ICollider
{
    public Action OnContact { get; }
}
