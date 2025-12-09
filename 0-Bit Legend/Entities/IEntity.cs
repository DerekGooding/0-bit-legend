namespace _0_bit_Legend.Entities;

public interface IEntity : IWorldSpace
{
    public DirectionType Direction { get; set; }

    public void Draw();
}
