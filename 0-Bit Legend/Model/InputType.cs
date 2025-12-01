namespace _0_Bit_Legend.Model;

[Flags]
public enum InputType
{
    None = 0,
    Up = 1 << 0,
    Down = 1 << 1,
    Left = 1 << 2,
    Right = 1 << 3,
    Attack = 1 << 4,
}