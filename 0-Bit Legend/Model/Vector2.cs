namespace _0_bit_Legend.Model;

public record struct Vector2(int X, int Y)
{
    public static Vector2 Zero => new(0, 0);

    public readonly Vector2 Offset(int x = 0, int y = 0) => new(X + x, Y + y);
}
