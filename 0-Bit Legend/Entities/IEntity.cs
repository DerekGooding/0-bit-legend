using _0_Bit_Legend.Model.Enums;

namespace _0_Bit_Legend.Entities;

public interface IEntity
{
    public Vector2 Position { get; set; }
    public DirectionType Direction { get; set; }

    public void Draw(int posX, int posY, DirectionType previousIndex);

    public bool IsTouching(int posX, int posY, char symbol);

    public bool IsTouching(int posX, int posY, char[] symbols);
}