namespace BitLegend.Maps;

public abstract class BaseMap : IMap
{
    public char[][] RawChars
    {
        get
        {
            field ??= [.. Raw.Select(line => line.ToCharArray())];

            return field;
        }
    }

    public abstract string Name { get; }
    public abstract string[] Raw { get; }
    public abstract List<EntityLocation> EntityLocations { get; }
    public abstract List<NewAreaInfo> AreaTransitions { get; }
}
