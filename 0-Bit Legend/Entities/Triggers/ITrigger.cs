namespace _0_bit_Legend.Entities.Triggers;

public interface ITrigger : IEntity, ICollider
{
    public Action OnContact { get; }
}
