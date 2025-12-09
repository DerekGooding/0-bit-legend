using BitLegend.MapEditor.Models;
using System.Text.RegularExpressions;

namespace BitLegend.MapEditor.Services;

[Singleton]
public partial class MapFileParserService
{
    private const string _gameMapsSubPath = @"BitLegend\Maps";
    public static readonly string AbsoluteGameMapsPath
        = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", _gameMapsSubPath);

    public List<MapData> LoadMaps()
    {
        List<MapData> maps = [];
        // Debugging: Log the absolute path and files found
        var debugLogPath = Path.Combine(Path.GetTempPath(), "map_parser_debug.log");
        File.AppendAllText(debugLogPath, $"Timestamp: {DateTime.Now}\n");
        File.AppendAllText(debugLogPath, $"AbsoluteGameMapsPath: {AbsoluteGameMapsPath}\n");

        var mapFiles = Directory.GetFiles(AbsoluteGameMapsPath, "*.cs");
        File.AppendAllText(debugLogPath, "Files found by Directory.GetFiles:\n");
        foreach (var file in mapFiles)
        {
            File.AppendAllText(debugLogPath, $"- {file}\n");
        }
        File.AppendAllText(debugLogPath, "--- End of files ---\n\n");

        foreach (var filePath in mapFiles)
        {
            var content = File.ReadAllText(filePath);
            var map = ParseMapFile(content);
            if (map != null)
            {
                maps.Add(map);
            }
        }
        return maps;
    }

    private static MapData? ParseMapFile(string fileContent)
    {
        // Extract Name
        var nameMatch = ParseName().Match(fileContent);
        if (!nameMatch.Success)
        {
            return null; // Invalid map file: no name found
        }
        var name = nameMatch.Groups["name"].Value;

        // Extract Raw map data
        var rawMatch = ParseRaw().Match(fileContent);
        if (!rawMatch.Success)
        {
            return null; // Invalid map file: no raw data found
        }
        List<string> raw = [];
        foreach (Match lineMatch in ParseRawLines().Matches(rawMatch.Groups["rawContent"].Value))
        {
            raw.Add(lineMatch.Groups["line"].Value);
        }

        MapData mapData = new(name, [.. raw]);

        // Extract EntityLocations
        var entityLocationsMatch = ParseEntityLocations().Match(fileContent);
        if (entityLocationsMatch.Success)
        {
             // Updated regex
            foreach (Match entityMatch in ParseEntities().Matches(entityLocationsMatch.Groups["entities"].Value))
            {
                mapData.EntityLocations.Add(new EntityData(
                    entityMatch.Groups["type"].Value,
                    int.Parse(entityMatch.Groups["x"].Value),
                    int.Parse(entityMatch.Groups["y"].Value),
                    entityMatch.Groups["condition"].Value.Trim()
                ));
            }
        }

        // Extract AreaTransitions
        var areaTransitionsMatch = ParseAreaTransitions().Match(fileContent);
        if (areaTransitionsMatch.Success)
        {
            foreach (Match transitionMatch in ParseTransitions().Matches(areaTransitionsMatch.Groups["transitions"].Value))
            {
                var mapId = transitionMatch.Groups["mapId"].Value;
                // Remove "WorldMap.MapName." prefix
                if (mapId.StartsWith("WorldMap.MapName."))
                {
                    mapId = mapId.Replace("WorldMap.MapName.", "");
                }

                mapData.AreaTransitions.Add(new TransitionData(
                    mapId, // Use the cleaned mapId
                    int.Parse(transitionMatch.Groups["startX"].Value),
                    int.Parse(transitionMatch.Groups["startY"].Value),
                    transitionMatch.Groups["direction"].Value,
                    int.Parse(transitionMatch.Groups["sizeX"].Value),
                    int.Parse(transitionMatch.Groups["sizeY"].Value),
                    int.Parse(transitionMatch.Groups["posX"].Value),
                    int.Parse(transitionMatch.Groups["posY"].Value)
                ));
            }
        }

        return mapData;
    }

    [GeneratedRegex(@"public override string Name => ""(?<name>[^""]+)"";")]
    private static partial Regex ParseName();
    [GeneratedRegex(@"public override string\[\] Raw =>\s*\[\s*(?<rawContent>[\s\S]*?)\s*\];")]
    private static partial Regex ParseRaw();
    [GeneratedRegex(@"""(?<line>[^""]*)""")]
    private static partial Regex ParseRawLines();
    [GeneratedRegex(@"public override List<EntityLocation> EntityLocations { get; } =[\s\S]*?\[(?<entities>[\s\S]*?)\];")]
    private static partial Regex ParseEntityLocations();
    [GeneratedRegex(@"new\(typeof\((?<type>[^)]+)\),\s*new\((?<x>\d+),\s*(?<y>\d+)\),\s*(?<condition>.*?)\)")]
    private static partial Regex ParseEntities();
    [GeneratedRegex(@"public override List<NewAreaInfo> AreaTransitions { get; } =[\s\S]*?\[(?<transitions>[\s\S]*?)\];")]
    private static partial Regex ParseAreaTransitions();
    [GeneratedRegex(@"new\(MapId:\s*WorldMap.MapName.(?<mapId>[^,]+),\s*StartPosition:\s*new\((?<startX>\d+),\s*(?<startY>\d+)\),\s*DirectionType.(?<direction>[^,]+),\s*Size:\s*new\((?<sizeX>\d+),\s*(?<sizeY>\d+)\),\s*Position:\s*new\((?<posX>\d+),\s*(?<posY>\d+)\)\)")]
    private static partial Regex ParseTransitions();
}
