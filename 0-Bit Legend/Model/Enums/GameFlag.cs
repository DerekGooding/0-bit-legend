namespace _0_Bit_Legend.Model.Enums;

[Flags]
public enum GameFlag
{
    None = 0,
    HasSword = 1 << 0,
    HasArmor = 1 << 1,
    HasRaft = 1 << 2,
    Door1 = 1 << 3,
    Door2 = 1 << 4,
    Door3 = 1 << 5,
    Text = 1 << 6,
    Dragon = 1 << 7,
}
