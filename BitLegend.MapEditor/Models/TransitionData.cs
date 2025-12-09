namespace BitLegend.MapEditor.Models;

public class TransitionData
{
    public string MapId { get; set; }
    public int StartPositionX { get; set; }
    public int StartPositionY { get; set; }
    public string DirectionType { get; set; }
    public int SizeX { get; set; }
    public int SizeY { get; set; }
    public int PositionX { get; set; }
    public int PositionY { get; set; }

    public TransitionData(string mapId,
                          int startPositionX,
                          int startPositionY,
                          string directionType,
                          int sizeX,
                          int sizeY,
                          int positionX,
                          int positionY)
    {
        MapId = mapId;
        StartPositionX = startPositionX;
        StartPositionY = startPositionY;
        DirectionType = directionType;
        SizeX = sizeX;
        SizeY = sizeY;
        PositionX = positionX;
        PositionY = positionY;
    }

    public TransitionData()
    {
        MapId = string.Empty;
        DirectionType = string.Empty;
    }
}
