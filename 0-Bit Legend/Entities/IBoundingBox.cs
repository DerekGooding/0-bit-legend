namespace _0_Bit_Legend.Entities;

public interface IBoundingBox
{
    //Origin point is top left corner of object
    public Vector2 Size { get; }

    public bool InsideBoundingBox(char symbol);

    public bool InsideBoundingBox(char[] symbols);
}