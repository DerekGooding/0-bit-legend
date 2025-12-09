using System.Collections.ObjectModel;

namespace BitLegend.MapEditor.Model;

[ViewModel]
public partial class MapData
{
    [Bind] private string _name;
    [Bind] private List<string> _raw = [];

    public ObservableCollection<EntityData> EntityLocations { get; set; } = [];
    public ObservableCollection<TransitionData> AreaTransitions { get; set; } = [];

    public MapData(string name, string[] raw)
    {
        _name = name;
        _raw = [.. raw];
    }

    public MapData() => _name = string.Empty;
}
