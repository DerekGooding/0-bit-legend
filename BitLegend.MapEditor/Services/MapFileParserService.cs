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

        var mapFiles = Directory.GetFiles(AbsoluteGameMapsPath, "*.cs");

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
        // --- Extract Name ---
        var nameMatch = ParseName().Match(fileContent);
        if (!nameMatch.Success)
        {
            return null; // Invalid map file: no name found
        }
        var name = nameMatch.Groups["name"].Value;

        // --- Extract Raw map data ---
        var rawMatch = ParseRaw().Match(fileContent);
        if (!rawMatch.Success)
        {
            return null; // Invalid map file: no raw data found
        }
        List<string> raw = [];
        var rawContent = rawMatch.Groups["rawContent"].Value;

        foreach (Match lineMatch in ParseRawLines().Matches(rawContent))
        {
            raw.Add(lineMatch.Groups["line"].Value);
        }


        MapData mapData = new(name, [.. raw]);

        // --- Extract EntityLocations ---
        var entityLocationsMatch = ParseEntityLocations().Match(fileContent);
        if (entityLocationsMatch.Success)
        {
            var entitiesContent = entityLocationsMatch.Groups["entities_content"].Value;
            if (entitiesContent.Contains("["))
            {
                var entities = entitiesContent.Substring(entitiesContent.IndexOf('[') + 1);
                entities = entities.Substring(0, entities.LastIndexOf(']'));

                foreach (Match entityMatch in ParseEntities().Matches(entities))
                {
                    mapData.EntityLocations.Add(new EntityData(
                        entityMatch.Groups["type"].Value,
                        int.Parse(entityMatch.Groups["x"].Value),
                        int.Parse(entityMatch.Groups["y"].Value),
                        entityMatch.Groups["condition"].Value.Trim()
                    ));
                }
            }
        }


        // --- Extract AreaTransitions ---
        var areaTransitionsMatch = ParseAreaTransitions().Match(fileContent);
        if (areaTransitionsMatch.Success)
        {
            var transitionsContent = areaTransitionsMatch.Groups["transitions_content"].Value;
            if (transitionsContent.Contains("["))
            {
                var transitions = transitionsContent.Substring(transitionsContent.IndexOf('[') + 1);
                transitions = transitions.Substring(0, transitions.LastIndexOf(']'));

                foreach (Match transitionMatch in ParseTransitions().Matches(transitions))
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
        }

        return mapData;
    }

    [GeneratedRegex(@"public\s+override\s+string\s+Name\s*=>\s*""(?<name>[^""]+)"";")]
    private static partial Regex ParseName();

    [GeneratedRegex(@"public override string\[\] Raw =>\s*\[\s*(?<rawContent>[\s\S]*?)\s*\];")]
    private static partial Regex ParseRaw();

    [GeneratedRegex(@"""(?<line>[^""]*)""")]
    private static partial Regex ParseRawLines();

    [GeneratedRegex(@"public override List<EntityLocation> EntityLocations { get; } = (?<entities_content>.*?);", RegexOptions.Singleline)]
    private static partial Regex ParseEntityLocations();

    [GeneratedRegex(@"new\(typeof\((?<type>[^)]+)\),\s*new\((?<x>\d+),\s*(?<y>\d+)\),\s*(?<condition>.*?)\)")]
    private static partial Regex ParseEntities();

    [GeneratedRegex(@"public override List<NewAreaInfo> AreaTransitions { get; } = (?<transitions_content>.*?);", RegexOptions.Singleline)]
    private static partial Regex ParseAreaTransitions();

    [GeneratedRegex(@"new\(MapId:\s*WorldMap.MapName.(?<mapId>[^,]+),\s*StartPosition:\s*new\((?<startX>\d+),\s*(?<startY>\d+)\),\s*DirectionType.(?<direction>[^,]+),\s*Size:\s*new\((?<sizeX>\d+),\s*(?<sizeY>\d+)\),\s*Position:\s*new\((?<posX>\d+),\s*(?<posY>\d+)\)")]
    private static partial Regex ParseTransitions();
}
