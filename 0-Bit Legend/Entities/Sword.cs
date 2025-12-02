using _0_Bit_Legend.Model.Enums;
using static _0_Bit_Legend.MainProgram;

namespace _0_Bit_Legend.Enemies;

public class Sword : IEntity
{
    public void Draw(int posX, int posY, Direction direction) => throw new NotImplementedException();
    public bool IsTouching(int posX, int posY, char[] symbols) => throw new NotImplementedException();
    public bool IsTouching(int posX, int posY, char symbol) => throw new NotImplementedException();
}
