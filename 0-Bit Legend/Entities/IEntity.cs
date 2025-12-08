namespace _0_Bit_Legend.Entities;

public interface IEntity : IWorldSpace
{
    public DirectionType Direction { get; set; }

    public void Draw();
}
