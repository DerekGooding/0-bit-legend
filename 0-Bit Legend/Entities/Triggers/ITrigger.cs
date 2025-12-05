namespace _0_Bit_Legend.Entities.Triggers;

public interface ITrigger : IEntity, ICollider
{
    public Action OnContact { get; }
}
