namespace _0_Bit_Legend.Entities;

public interface IBoundingBox
{
    public (Vector2 TopLeft, Vector2 BottomRight) BoundingBox { get; }

    public bool InsideBoundingBox(char symbol);

    public bool InsideBoundingBox(char[] symbols);
}