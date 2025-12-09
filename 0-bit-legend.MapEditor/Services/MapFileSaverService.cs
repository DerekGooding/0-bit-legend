using _0_bit_legend.MapEditor.Models;
using System.IO;
using System.Text;

namespace _0_bit_legend.MapEditor.Services;

public class MapFileSaverService(GameDataService gameDataService)
{
    private const string GameMapsSubPath = @"0-Bit Legend\Maps";
    public static readonly string AbsoluteGameMapsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", GameMapsSubPath);

    private readonly GameDataService _gameDataService = gameDataService;

    public void SaveMap(MapData mapData)
    {
        string fileName = $"{mapData.Name}.cs";
        string filePath = Path.Combine(AbsoluteGameMapsPath, fileName);

        string fileContent = GenerateMapFileContent(mapData);

        File.WriteAllText(filePath, fileContent);
    }

    private string GenerateMapFileContent(MapData mapData)
    {
        StringBuilder sb = new();

        sb.AppendLine("using _0_Bit_Legend.Content;");
        sb.AppendLine("using _0_Bit_Legend.Entities;");
        sb.AppendLine("using _0_Bit_Legend.Model;");
        sb.AppendLine("using _0_Bit_Legend.Model.Enums;");

        sb.AppendLine("");
        sb.AppendLine("namespace _0_Bit_Legend.Maps");
        sb.AppendLine("{");
        sb.AppendLine($"    public class {mapData.Name} : BaseMap");
        sb.AppendLine("    {");
        sb.AppendLine($"        public override string Name => \"{mapData.Name}\";");
        sb.AppendLine("");

        // Raw map data
        sb.AppendLine("        public override string[] Raw => [");
        sb.AppendLine("        {");
        foreach (string line in mapData.Raw)
        {
            sb.AppendLine($"            \"{line}\",");
        }
        sb.AppendLine("        ];");
        sb.AppendLine("");

        // Entity Locations
        sb.AppendLine("        public override List<EntityLocation> EntityLocations { get; } =");
        sb.AppendLine("        {");
        foreach (EntityData entity in mapData.EntityLocations)
        {
            string fullTypeName = _gameDataService.EntityTypeToFullTypeName[entity.EntityType]; // Get full type name
            sb.AppendLine($"            new(typeof({fullTypeName}), new({entity.X}, {entity.Y}), () => {entity.Condition}),");
        }
        sb.AppendLine("        }"); // Close EntityLocations list
        sb.AppendLine("");

        // Area Transitions
        sb.AppendLine("        public override List<NewAreaInfo> AreaTransitions { get; } =");
        sb.AppendLine("        {");
        foreach (TransitionData transition in mapData.AreaTransitions)
        {
            // Ensure MapId and DirectionType are correctly referenced as enums
            sb.AppendLine($"            new(MapId: WorldMap.MapName.{transition.MapId}, StartPosition: new({transition.StartPositionX}, {transition.StartPositionY}), Direction: DirectionType.{transition.DirectionType}, Size: new({transition.SizeX}, {transition.SizeY}), Position: new({transition.PositionX}, {transition.PositionY})),");
        }
        sb.AppendLine("        };"); // Close AreaTransitions list
        sb.AppendLine("    }"); // Close class
        sb.AppendLine("}"); // Close namespace
        return sb.ToString();
    }
}