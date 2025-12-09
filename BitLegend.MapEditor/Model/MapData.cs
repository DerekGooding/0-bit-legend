using System.Collections.ObjectModel;

namespace BitLegend.MapEditor.Model;

public class MapData
{
    public string Name { get; set; }
    public List<string> Raw { get; set; } = [];
    public ObservableCollection<EntityData> EntityLocations { get; set; } = [];
    public ObservableCollection<TransitionData> AreaTransitions { get; set; } = [];

    public MapData(string name, string[] raw)
    {
        Name = name;
        Raw = [.. raw];
    }

    public MapData() => Name = string.Empty;
}
