using System.ComponentModel;

namespace BitLegend.MapEditor.Model;

public class TransitionData : INotifyPropertyChanged
{
    private string _mapId;
    public string MapId { get => _mapId; set { _mapId = value; OnPropertyChanged(nameof(MapId)); } }

    private int _startPositionX;
    public int StartPositionX { get => _startPositionX; set { _startPositionX = value; OnPropertyChanged(nameof(StartPositionX)); } }

    private int _startPositionY;
    public int StartPositionY { get => _startPositionY; set { _startPositionY = value; OnPropertyChanged(nameof(StartPositionY)); } }

    private string _directionType;
    public string DirectionType { get => _directionType; set { _directionType = value; OnPropertyChanged(nameof(DirectionType)); } }

    private int _sizeX;
    public int SizeX { get => _sizeX; set { _sizeX = value; OnPropertyChanged(nameof(SizeX)); } }

    private int _sizeY;
    public int SizeY { get => _sizeY; set { _sizeY = value; OnPropertyChanged(nameof(SizeY)); } }

    private int _positionX;
    public int PositionX { get => _positionX; set { _positionX = value; OnPropertyChanged(nameof(PositionX)); } }

    private int _positionY;
    public int PositionY { get => _positionY; set { _positionY = value; OnPropertyChanged(nameof(PositionY)); } }


    public TransitionData(string mapId,
                          int startPositionX,
                          int startPositionY,
                          string directionType,
                          int sizeX,
                          int sizeY,
                          int positionX,
                          int positionY)
    {
        _mapId = mapId;
        _startPositionX = startPositionX;
        _startPositionY = startPositionY;
        _directionType = directionType;
        _sizeX = sizeX;
        _sizeY = sizeY;
        _positionX = positionX;
        _positionY = positionY;
    }

    public TransitionData()
    {
        _mapId = string.Empty;
        _directionType = string.Empty;
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
