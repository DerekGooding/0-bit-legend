namespace _0_bit_Legend.Maps;

public interface IMap
{
    public string Name { get; }
    public string[] Raw { get; }

    public char[][] RawChars { get; }

    public List<EntityLocation> EntityLocations { get; }

    public List<NewAreaInfo> AreaTransitions { get; }
}
