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
        var entitiesContent = ParseListContent(fileContent, "EntityLocations");
        if (!string.IsNullOrEmpty(entitiesContent))
        {
            var entityDefinitions = SplitListItems(entitiesContent);
            foreach (var entityDef in entityDefinitions)
            {
                var entityData = ParseEntityData(entityDef);
                if (entityData != null)
                {
                    mapData.EntityLocations.Add(entityData);
                }
            }
        }

        // --- Extract AreaTransitions ---
        var transitionsContent = ParseListContent(fileContent, "AreaTransitions");
        if (!string.IsNullOrEmpty(transitionsContent))
        {
            var transitionDefinitions = SplitListItems(transitionsContent);
            foreach (var transitionDef in transitionDefinitions)
            {
                var transitionData = ParseTransitionData(transitionDef);
                if (transitionData != null)
                {
                    mapData.AreaTransitions.Add(transitionData);
                }
            }
        }

        return mapData;
    }

    private static string ParseListContent(string fileContent, string listName)
    {
        string listType = listName == "EntityLocations" ? "EntityLocation" : "NewAreaInfo";
        var pattern = @$"public override List<{listType}> {listName} {{ get; }} =\s*(?<content>.*?);";
        var match = Regex.Match(fileContent, pattern, RegexOptions.Singleline);

        if (match.Success)
        {
            var content = match.Groups["content"].Value.Trim();
            if (content.StartsWith("["))
            {
                var startIndex = content.IndexOf('[') + 1;
                var endIndex = content.LastIndexOf(']');
                if (endIndex > startIndex)
                {
                    return content.Substring(startIndex, endIndex - startIndex);
                }
            }
        }
        return "";
    }

    private static List<string> SplitListItems(string listContent)
    {
        var items = new List<string>();
        int balance = 0;
        int lastSplitIndex = 0;

        for (int i = 0; i < listContent.Length; i++)
        {
            if (listContent[i] == '(') balance++;
            else if (listContent[i] == ')') balance--;
            else if (listContent[i] == ',' && balance == 0)
            {
                items.Add(listContent.Substring(lastSplitIndex, i - lastSplitIndex).Trim());
                lastSplitIndex = i + 1;
            }
        }
        if (lastSplitIndex < listContent.Length)
        {
            items.Add(listContent.Substring(lastSplitIndex).Trim());
        }
        return items;
    }

    private static EntityData? ParseEntityData(string entityDefinition)
    {
        // Example: new(typeof(EnterCave0), new(13,4), () => true)
        // This regex specifically parses the components of an EntityData from its string definition.
        // It captures the entity type, its X and Y coordinates, and its condition.
        var match = Regex.Match(entityDefinition, @"new\(typeof\((?<type>[^)]+)\),\s*new\((?<x>\d+),\s*(?<y>\d+)\),\s*(?<condition>[\s\S]*)\)");
        if (match.Success)
        {
            return new EntityData(
                match.Groups["type"].Value,
                int.Parse(match.Groups["x"].Value),
                int.Parse(match.Groups["y"].Value),
                match.Groups["condition"].Value.Trim()
            );
        }
        return null;
    }

    private static TransitionData? ParseTransitionData(string transitionDefinition)
    {
        // Example: new(MapId: WorldMap.MapName.MainMap2, StartPosition: new(52, 18), DirectionType.Left, Size: new(3, 10), Position: new(0, 9))
        // This regex parses the components of a TransitionData from its string definition.
        // It captures the MapId, StartPosition X and Y, DirectionType, Size X and Y, and Position X and Y.
        var match = Regex.Match(transitionDefinition, @"new\(MapId:\s*WorldMap.MapName.(?<mapId>[^,]+),\s*StartPosition:\s*new\((?<startX>\d+),\s*(?<startY>\d+)\),\s*DirectionType.(?<direction>[^,]+),\s*Size:\s*new\((?<sizeX>\d+),\s*(?<sizeY>\d+)\),\s*Position:\s*new\((?<posX>\d+),\s*(?<posY>\d+)\)\)");
        if (match.Success)
        {
            var mapId = match.Groups["mapId"].Value;
            if (mapId.StartsWith("WorldMap.MapName."))
            {
                mapId = mapId.Replace("WorldMap.MapName.", "");
            }

            return new TransitionData(
                mapId,
                int.Parse(match.Groups["startX"].Value),
                int.Parse(match.Groups["startY"].Value),
                match.Groups["direction"].Value,
                int.Parse(match.Groups["sizeX"].Value),
                int.Parse(match.Groups["sizeY"].Value),
                int.Parse(match.Groups["posX"].Value),
                int.Parse(match.Groups["posY"].Value)
            );
        }
        return null;
    }

    [GeneratedRegex(@"public\s+override\s+string\s+Name\s*=>\s*""(?<name>[^""]+)"";")]
    private static partial Regex ParseName();

    [GeneratedRegex(@"public override string\[\] Raw =>\s*\[\s*(?<rawContent>[\s\S]*?)\s*\];")]
    private static partial Regex ParseRaw();

    [GeneratedRegex(@"""(?<line>[^""]*)""")]
    private static partial Regex ParseRawLines();
}