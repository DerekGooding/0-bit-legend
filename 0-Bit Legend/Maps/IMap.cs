namespace _0_Bit_Legend.Maps;

public interface IMap
{
    public string Name { get; }
    public string[] Raw { get; }
    public string[] FlagAdjusted { get; }

    public void Load() { }
}
