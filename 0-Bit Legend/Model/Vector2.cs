namespace _0_Bit_Legend.Model;

public struct Vector2(int x, int y)
{
    public int X = x;
    public int Y = y;

    public static Vector2 Zero => new(0, 0);
}
