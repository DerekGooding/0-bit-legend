namespace _0_Bit_Legend.Entities;

public class Rupee : IEntity
{
    public Vector2 Position { get; set; } = Vector2.Zero;
    public DirectionType Direction { get; set; }
    public char[] MapStorage { get; } = new string(' ', 9).ToCharArray();
    public void Draw() => throw new NotImplementedException();

    public void Clear()
    {
        Map[Position.X - 1, Position.Y - 1] = MapStorage[0];
        Map[Position.X + 0, Position.Y - 1] = MapStorage[1];
        Map[Position.X + 1, Position.Y - 1] = MapStorage[2];

        Map[Position.X - 1, Position.Y] = MapStorage[3];
        Map[Position.X + 0, Position.Y] = MapStorage[4];
        Map[Position.X + 1, Position.Y] = MapStorage[5];

        Map[Position.X - 1, Position.Y + 1] = MapStorage[6];
        Map[Position.X + 0, Position.Y + 1] = MapStorage[7];
        Map[Position.X + 1, Position.Y + 1] = MapStorage[8];
    }

    public bool IsTouching(char[] symbols) => throw new NotImplementedException();
    public bool IsTouching(char symbol) => throw new NotImplementedException();
}
