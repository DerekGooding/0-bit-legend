using BitLegend.MapEditor.Model;
using System.Text;

namespace BitLegend.MapEditor.Services;

[Singleton]
public class MapFileSaverService(GameDataService gameDataService) : IMapFileSaverService
{
    private const string _gameMapsSubPath = @"BitLegend\Maps";
    public static readonly string AbsoluteGameMapsPath
        = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", _gameMapsSubPath);

    private readonly GameDataService _gameDataService = gameDataService;

    public void SaveMap(MapData mapData)
    {
        var fileName = $"{mapData.Name}.cs";
        var filePath = Path.Combine(AbsoluteGameMapsPath, fileName);

        var fileContent = GenerateMapFileContent(mapData);

        File.WriteAllText(filePath, fileContent);
    }

    private string GenerateMapFileContent(MapData mapData)
    {
        StringBuilder sb = new();
        sb.AppendLine("using BitLegend.Entities.Triggers;");
        sb.AppendLine("using BitLegend.Entities.Enemies;");
        sb.AppendLine("using BitLegend.Entities.Pickups;");
        sb.AppendLine("");

        sb.AppendLine("namespace BitLegend.Maps;");
        sb.AppendLine($"public class {mapData.Name} : BaseMap");
        sb.AppendLine(" {");
        sb.AppendLine($"    public override string Name => \"{mapData.Name}\";");
        sb.AppendLine("");

        // Raw map data
        sb.AppendLine("     public override string[] Raw => [");
        foreach (var line in mapData.Raw)
        {
            sb.AppendLine($"        \"{line}\",");
        }
        sb.AppendLine("     ];");
        sb.AppendLine("");

        // Entity Locations
        sb.AppendLine("     public override List<EntityLocation> EntityLocations { get; } =");
        sb.AppendLine("     [");
        foreach (var entity in mapData.EntityLocations)
        {
            sb.AppendLine($"        new(typeof({entity.EntityType}), new({entity.X}, {entity.Y}), {entity.Condition}),");
        }
        sb.AppendLine("     ];"); // Close EntityLocations list
        sb.AppendLine("");

        // Area Transitions
        sb.AppendLine("     public override List<NewAreaInfo> AreaTransitions { get; } =");
        sb.AppendLine("     [");
        foreach (var transition in mapData.AreaTransitions)
        {
            // Ensure MapId and DirectionType are correctly referenced as enums
            sb.AppendLine($"        new(MapId: WorldMap.MapName.{transition.MapId}, StartPosition: new({transition.StartPositionX}, {transition.StartPositionY}), DirectionType.{transition.DirectionType}, Size: new({transition.SizeX}, {transition.SizeY}), Position: new({transition.PositionX}, {transition.PositionY})),");
        }
        sb.AppendLine("     ];"); // Close AreaTransitions list
        sb.AppendLine(" }"); // Close class
        return sb.ToString();
    }
}