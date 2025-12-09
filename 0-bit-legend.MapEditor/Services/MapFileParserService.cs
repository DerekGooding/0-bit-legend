using _0_bit_legend.MapEditor.Models;
using System.IO;
using System.Text.RegularExpressions;

namespace _0_bit_legend.MapEditor.Services;

public class MapFileParserService
{
    private const string GameMapsPath = @"0-Bit Legend\Maps";
    private static readonly string fourUp = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "..", "..", "..", ".."));
    public static readonly string AbsoluteGameMapsPath = Path.Join(fourUp, GameMapsPath);

    public List<MapData> LoadMaps()
    {
        List<MapData> maps = [];
        var mapFiles = Directory.GetFiles(AbsoluteGameMapsPath, "*.cs");

        foreach (var filePath in mapFiles)
        {
            var content = File.ReadAllText(filePath);
            MapData map = ParseMapFile(content);
            if (map != null)
            {
                maps.Add(map);
            }
        }
        return maps;
    }

    private MapData ParseMapFile(string fileContent)
    {
        // Extract Name
        var nameMatch = Regex.Match(fileContent, @"public override string Name => ""(?<name>[^""]+)"";");
        string name = nameMatch.Success ? nameMatch.Groups["name"].Value : "Unknown Map";

        // Extract Raw map data
        var rawMatch = Regex.Match(fileContent, @"public override string\[\] Raw => \[\s*(?<rawContent>[\s\S]*?)\];");
        List<string> raw = [];
        if (rawMatch.Success)
        {
            var rawLines = Regex.Matches(rawMatch.Groups["rawContent"].Value, @"""(?<line>[^""]*)""");
            foreach (Match lineMatch in rawLines)
            {
                raw.Add(lineMatch.Groups["line"].Value);
            }
        }

        MapData mapData = new(name, raw.ToArray());

        // Extract EntityLocations
        var entityLocationsMatch = Regex.Match(fileContent, @"public override List<EntityLocation> EntityLocations { get; } =[\s\S]*?\[(?<entities>[\s\S]*?)\];");
        if (entityLocationsMatch.Success)
        {
            var entityMatches = Regex.Matches(entityLocationsMatch.Groups["entities"].Value, @"new\(typeof\((?<type>[^)]+)\),\s*new\((?<x>\d+),\s*(?<y>\d+)\),\s*(?<condition>[^)]+)\)");
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
                mapData.AreaTransitions.Add(new TransitionData(
                    transitionMatch.Groups["mapId"].Value,
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
