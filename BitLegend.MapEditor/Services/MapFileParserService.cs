using BitLegend.MapEditor.Models;
using System.Text.RegularExpressions;

namespace BitLegend.MapEditor.Services;

[Singleton]
public class MapFileParserService
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
        var nameMatch = Regex.Match(fileContent, @"public override string Name => ""(?<name>[^""]+)"";");
        if (!nameMatch.Success)
        {
            return null; // Invalid map file: no name found
        }
        var name = nameMatch.Groups["name"].Value;

        // Extract Raw map data
        var rawMatch = Regex.Match(fileContent, @"public override string\[\] Raw =>\s*\[\s*(?<rawContent>[\s\S]*?)\s*\];");
        if (!rawMatch.Success)
        {
            return null; // Invalid map file: no raw data found
        }
        List<string> raw = [];
        var rawLines = Regex.Matches(rawMatch.Groups["rawContent"].Value, @"""(?<line>[^""]*)""");
        foreach (Match lineMatch in rawLines)
        {
            raw.Add(lineMatch.Groups["line"].Value);
        }

        MapData mapData = new(name, [.. raw]);

        // Extract EntityLocations
        var entityLocationsMatch = Regex.Match(fileContent, @"public override List<EntityLocation> EntityLocations { get; } =[\s\S]*?\[(?<entities>[\s\S]*?)\];");
        if (entityLocationsMatch.Success)
        {
            var entityMatches = Regex.Matches(entityLocationsMatch.Groups["entities"].Value, @"new\(typeof\((?<type>[^)]+)\),\s*new\((?<x>\d+),\s*(?<y>\d+)\),\s*(?<condition>.*?)\)"); // Updated regex
            foreach (Match entityMatch in entityMatches)
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
        var areaTransitionsMatch = Regex.Match(fileContent, @"public override List<NewAreaInfo> AreaTransitions { get; } =[\s\S]*?\[(?<transitions>[\s\S]*?)\];");
        if (areaTransitionsMatch.Success)
        {
            var transitionMatches = Regex.Matches(areaTransitionsMatch.Groups["transitions"].Value, @"new\(MapId:\s*WorldMap.MapName.(?<mapId>[^,]+),\s*StartPosition:\s*new\((?<startX>\d+),\s*(?<startY>\d+)\),\s*DirectionType.(?<direction>[^,]+),\s*Size:\s*new\((?<sizeX>\d+),\s*(?<sizeY>\d+)\),\s*Position:\s*new\((?<posX>\d+),\s*(?<posY>\d+)\)\)");
            foreach (Match transitionMatch in transitionMatches)
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
}
