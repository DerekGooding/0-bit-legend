using _0_Bit_Legend.Model.Enums;

namespace _0_Bit_Legend.Enemies;

public interface IEntity
{
    public void Draw(int posX, int posY, Direction previousIndex);

    public bool IsTouching(int posX, int posY, char symbol);

    public bool IsTouching(int posX, int posY, char[] symbols);
}