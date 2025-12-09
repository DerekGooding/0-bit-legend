namespace BitLegend.MapEditor.Model;

[ViewModel]
public partial class TransitionData
{
    [Bind] private string _mapId;
    [Bind] private int _startPositionX;
    [Bind] private int _startPositionY;
    [Bind] private string _directionType;
    [Bind] private int _sizeX;
    [Bind] private int _sizeY;
    [Bind] private int _positionX;
    [Bind] private int _positionY;

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
}
