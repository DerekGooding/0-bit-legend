using BitLegend.MapEditor.Model;
using System.Text.RegularExpressions;

namespace BitLegend.MapEditor.Services;

[Singleton]
public partial class MapFileParserService : IMapFileParserService
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

    public Task<List<MapData>> LoadMapsAsync() => Task.Run(LoadMaps);

    private static MapData? ParseMapFile(string fileContent)
    {
        var debugLogPath = Path.Combine(Path.GetTempPath(), "map_parser_debug.log");
        File.AppendAllText(debugLogPath, "\n--- Parsing New Map File ---\n");
        File.AppendAllText(debugLogPath, $"File Content:\n{fileContent}\n");

        // --- Extract Name ---
        var nameMatch = ParseName().Match(fileContent);
        if (!nameMatch.Success)
        {
            File.AppendAllText(debugLogPath, "ParseName: FAILED - No name found.\n");
            return null; // Invalid map file: no name found
        }
        var name = nameMatch.Groups["name"].Value;
        File.AppendAllText(debugLogPath, $"ParseName: SUCCESS - Name: '{name}'\n");

        // --- Extract Raw map data ---
        var rawMatch = ParseRaw().Match(fileContent);
        if (!rawMatch.Success)
        {
            File.AppendAllText(debugLogPath, "ParseRaw: FAILED - No raw map data block found.\n");
            return null; // Invalid map file: no raw data found
        }
        List<string> raw = [];
        var rawContent = rawMatch.Groups["rawContent"].Value;
        File.AppendAllText(debugLogPath, $"ParseRaw: SUCCESS - Raw Content Block:\n{rawContent}\n");

        foreach (Match lineMatch in ParseRawLines().Matches(rawContent))
        {
            raw.Add(lineMatch.Groups["line"].Value);
            File.AppendAllText(debugLogPath, $"ParseRawLines: Extracted Line: '{lineMatch.Groups["line"].Value}'\n");
        }
        File.AppendAllText(debugLogPath, $"ParseRawLines: Total {raw.Count} lines extracted.\n");


        MapData mapData = new(name, [.. raw]);

        // --- Extract EntityLocations ---
        var entityLocationsMatch = ParseEntityLocations().Match(fileContent);
        if (entityLocationsMatch.Success)
        {
            var entitiesContent = entityLocationsMatch.Groups["entities"].Value;
            File.AppendAllText(debugLogPath, $"ParseEntityLocations: SUCCESS - Entities Block:\n{entitiesContent}\n");

            foreach (Match entityMatch in ParseEntities().Matches(entitiesContent))
            {
                mapData.EntityLocations.Add(new EntityData(
                    entityMatch.Groups["type"].Value,
                    int.Parse(entityMatch.Groups["x"].Value),
                    int.Parse(entityMatch.Groups["y"].Value),
                    entityMatch.Groups["condition"].Value.Trim()
                ));
                File.AppendAllText(debugLogPath, $"ParseEntities: Extracted Entity - Type: '{entityMatch.Groups["type"].Value}', X: {entityMatch.Groups["x"].Value}, Y: {entityMatch.Groups["y"].Value}, Condition: '{entityMatch.Groups["condition"].Value.Trim()}'\n");
            }
            File.AppendAllText(debugLogPath, $"ParseEntities: Total {mapData.EntityLocations.Count} entities extracted.\n");
        }
        else
        {
            File.AppendAllText(debugLogPath, "ParseEntityLocations: FAILED - No entity locations block found.\n");
        }


        // --- Extract AreaTransitions ---
        var areaTransitionsMatch = ParseAreaTransitions().Match(fileContent);
        if (areaTransitionsMatch.Success)
        {
            var transitionsContent = areaTransitionsMatch.Groups["transitions"].Value;
            File.AppendAllText(debugLogPath, $"ParseAreaTransitions: SUCCESS - Transitions Block:\n{transitionsContent}\n");

            foreach (Match transitionMatch in ParseTransitions().Matches(transitionsContent))
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
                File.AppendAllText(debugLogPath, $"ParseTransitions: Extracted Transition - MapId: '{mapId}', StartX: {transitionMatch.Groups["startX"].Value}, StartY: {transitionMatch.Groups["startY"].Value}, Dir: '{transitionMatch.Groups["direction"].Value}', SizeX: {transitionMatch.Groups["sizeX"].Value}, SizeY: {transitionMatch.Groups["sizeY"].Value}, PosX: {transitionMatch.Groups["posX"].Value}, PosY: {transitionMatch.Groups["posY"].Value}\n");
            }
            File.AppendAllText(debugLogPath, $"ParseTransitions: Total {mapData.AreaTransitions.Count} transitions extracted.\n");
        }
        else
        {
            File.AppendAllText(debugLogPath, "ParseAreaTransitions: FAILED - No area transitions block found.\n");
        }
        File.AppendAllText(debugLogPath, "--- End Parsing Map File ---\n\n");

        return mapData;
    }

    [GeneratedRegex(@"public\s+override\s+string\s+Name\s*=>\s*""(?<name>[^""]+)"";")]
    private static partial Regex ParseName();

    [GeneratedRegex(@"public override string\[\] Raw =>\s*\[\s*(?<rawContent>[\s\S]*?)\s*\];")]
    private static partial Regex ParseRaw();

    [GeneratedRegex(@"""(?<line>[^""]*)""")]
    private static partial Regex ParseRawLines();

    [GeneratedRegex(@"public override List<EntityLocation> EntityLocations \{ get; \} = new\(\) \{\s*\[(?<entities>[\s\S]*?)\]\};")]
    private static partial Regex ParseEntityLocations();

    [GeneratedRegex(@"new\(typeof\((?<type>[^)]+)\),\s*new\((?<x>\d+),\s*(?<y>\d+)\),\s*(?<condition>.*?)\)")]
    private static partial Regex ParseEntities();

    [GeneratedRegex(@"public override List<NewAreaInfo> AreaTransitions \{ get; \} = new\(\) \{\s*\[(?<transitions>[\s\S]*?)\]\};")]
    private static partial Regex ParseAreaTransitions();

    [GeneratedRegex(@"new\(MapId:\s*WorldMap.MapName.(?<mapId>[^,]+),\s*StartPosition:\s*new\((?<startX>\d+),\s*(?<startY>\d+)\),\s*DirectionType.(?<direction>[^,]+),\s*Size:\s*new\((?<sizeX>\d+),\s*(?<sizeY>\d+)\),\s*Position:\s*new\((?<posX>\d+),\s*(?<posY>\d+)\)")]
    private static partial Regex ParseTransitions();
}
