namespace BitLegend.Maps;

public abstract class BaseMap : IMap
{
    private char[][]? _rawChars;
    public char[][] RawChars
    {
        get
        {
            _rawChars ??= [.. Raw.Select(line => line.ToCharArray())];

            return _rawChars;
        }
    }

    public abstract string Name { get; }
    public abstract string[] Raw { get; }
    public abstract List<EntityLocation> EntityLocations { get; }
    public abstract List<NewAreaInfo> AreaTransitions { get; }
}
