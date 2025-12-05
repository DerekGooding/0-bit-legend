namespace _0_Bit_Legend.Entities;

public interface IEntity
{
    public Vector2 Position { get; set; }
    public DirectionType Direction { get; set; }

    public void Draw();
}
