using System.Collections.ObjectModel;
using System.ComponentModel;

namespace BitLegend.MapEditor.Model;

public class MapData : INotifyPropertyChanged
{
    private string _name;
    public string Name
    {
        get => _name;
        set
        {
            if (_name != value)
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
    }

    private List<string> _raw = [];
    public List<string> Raw
    {
        get => _raw;
        set
        {
            if (_raw != value)
            {
                _raw = value;
                OnPropertyChanged(nameof(Raw));
            }
        }
    }
    public ObservableCollection<EntityData> EntityLocations { get; set; } = [];
    public ObservableCollection<TransitionData> AreaTransitions { get; set; } = [];

    public MapData(string name, string[] raw)
    {
        _name = name;
        _raw = [.. raw];
    }

    public MapData() => _name = string.Empty;

    public event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
