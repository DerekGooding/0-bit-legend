namespace _0_Bit_Legend.Model;

[Flags]
public enum GameFlag
{
    None = 0,
    HasSword = 1 << 0,
    HasArmor = 1 << 1,
    HasRaft = 1 << 2,
    GameOver = 1 << 3,
    Door1 = 1 << 4,
    Door2 = 1 << 5,
    Door3 = 1 << 6,
    Text = 1 << 7,
    Dragon = 1 << 8,
    Hit = 1 << 9
}
