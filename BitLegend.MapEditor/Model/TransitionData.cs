namespace BitLegend.MapEditor.Model;

[ViewModel]
public partial class TransitionData : IResizableAndMovable
{
    [Bind] private string _mapId;
    [Bind] private double _startPositionX;
    [Bind] private double _startPositionY;
    [Bind] private string _directionType;
    [Bind] private double _sizeX;
    [Bind] private double _sizeY;
    [Bind] private double _positionX;
    [Bind] private double _positionY;

    public double X { get => _positionX; set => _positionX = value; }
    public double Y { get => _positionY; set => _positionY = value; }
    public double Width { get => _sizeX; set => _sizeX = value; }
    public double Height { get => _sizeY; set => _sizeY = value; }


    public TransitionData(string mapId,
                          double startPositionX,
                          double startPositionY,
                          string directionType,
                          double sizeX,
                          double sizeY,
                          double positionX,
                          double positionY)
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
}
