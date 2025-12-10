using BitLegend.MapEditor.Model;
using System.Text.RegularExpressions;

namespace BitLegend.MapEditor.Services;

[Singleton]
public partial class MapFileParserService : IMapFileParserService
{
    private const string _gameMapsSubPath = @"BitLegend\Maps";
    public static readonly string AbsoluteGameMapsPath
        = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", _gameMapsSubPath);

using BitLegend.MapEditor.Model;
using System.Text.Json; // Added for JSON serialization
using System.IO;
using System.Threading.Tasks; // Added for async operations
using System.Collections.Generic; // Make sure this is present
using System; // For Exception
using System.Reflection; // Added for Assembly.GetExecutingAssembly()

namespace BitLegend.MapEditor.Services;

[Singleton]
public partial class MapFileParserService : IMapFileParserService
{
    private const string _gameMapsRelativePath = @"BitLegend\Maps";
    private static string? _cachedAbsoluteGameMapsPath;

    public static string GetAbsoluteGameMapsPath()
    {
        if (_cachedAbsoluteGameMapsPath != null)
        {
            return _cachedAbsoluteGameMapsPath;
        }

        string currentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
        DirectoryInfo? projectRoot = null;

        // Traverse up until a known marker (e.g., solution file or .git folder) is found
        while (currentDirectory != null && Directory.GetParent(currentDirectory) != null)
        {
            if (File.Exists(Path.Combine(currentDirectory, "BitLegend.sln")) || Directory.Exists(Path.Combine(currentDirectory, ".git")))
            {
                projectRoot = new DirectoryInfo(currentDirectory);
                break;
            }
            currentDirectory = Directory.GetParent(currentDirectory)?.FullName;
        }

        if (projectRoot == null)
        {
            throw new InvalidOperationException("Could not determine project root directory.");
        }

        _cachedAbsoluteGameMapsPath = Path.Combine(projectRoot.FullName, _gameMapsRelativePath);
        return _cachedAbsoluteGameMapsPath;
    }

    public MapData? LoadMap(string filePath)
    {
        try
        {
            var jsonContent = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<MapData>(jsonContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
        catch (Exception ex)
        {
            // Log the error or handle it as appropriate for your application
            Console.WriteLine($"Error loading map from {filePath}: {ex.Message}");
            return null;
        }
    }

    public async Task<List<MapData>> LoadMapsAsync()
    {
        List<MapData> maps = [];

        // Change to look for .json files
        var mapFiles = Directory.GetFiles(GetAbsoluteGameMapsPath(), "*.json");

        foreach (var filePath in mapFiles)
        {
            var map = await Task.Run(() => LoadMap(filePath)); // Offload synchronous file read and JSON parsing
            if (map != null)
            {
                maps.Add(map);
            }
        }
        return maps;
    }
}