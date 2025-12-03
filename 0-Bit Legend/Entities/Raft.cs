namespace _0_Bit_Legend.Entities;

public class Raft : IEntity
{
    public Vector2 Position { get; set; } = Vector2.Zero;
    public DirectionType Direction { get; set; }
    public void Draw() => throw new NotImplementedException();
    public bool IsTouching(char[] symbols) => throw new NotImplementedException();
    public bool IsTouching(char symbol) => throw new NotImplementedException();
}
