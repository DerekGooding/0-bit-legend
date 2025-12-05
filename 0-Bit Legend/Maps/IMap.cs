namespace _0_Bit_Legend.Maps;

public interface IMap
{
    public string Name { get; }
    public string[] Raw { get; }

    public void Load() { }

    public List<EntityLocation> EntityLocations { get; }
}
