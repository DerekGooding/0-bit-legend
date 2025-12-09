namespace _0_bit_legend.MapEditor.Models;

public class TransitionData
{
    public string MapId { get; set; } // Store as string (e.g., "MainMap2")
    public int StartPositionX { get; set; }
    public int StartPositionY { get; set; }
    public string DirectionType { get; set; } // Store as string (e.g., "Left")
    public int SizeX { get; set; }
    public int SizeY { get; set; }
    public int PositionX { get; set; }
    public int PositionY { get; set; }

    public TransitionData(string mapId, int startPositionX, int startPositionY, string directionType, int sizeX, int sizeY, int positionX, int positionY)
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

    public TransitionData() { }
}
