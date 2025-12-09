namespace _0_bit_legend.MapEditor.Models;

public class MapData
{
    public string Name { get; set; }
    public List<string> Raw { get; set; } = [];
    public List<EntityData> EntityLocations { get; set; } = [];
    public List<TransitionData> AreaTransitions { get; set; } = [];

    // Constructor for easy initialization
    public MapData(string name, string[] raw)
    {
        Name = name;
        Raw = [.. raw];
    }

    public MapData() { } // Parameterless constructor for serialization
}
