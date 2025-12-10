using BitLegend.MapEditor.Model;
using BitLegend.MapEditor.Model;
using System.IO;
using System.Text.Json; // Added for JSON serialization
using System.Threading.Tasks; // Added for async operations
using System; // For Exception
// using System.Reflection; // No need to add here, using static method from ParserService

namespace BitLegend.MapEditor.Services;

[Singleton]
public class MapFileSaverService(GameDataService gameDataService) : IMapFileSaverService
{
    private readonly GameDataService _gameDataService = gameDataService;

    public void SaveMap(MapData mapData)
    {
        var fileName = $"{mapData.Name}.json"; // Change extension
        var filePath = Path.Combine(MapFileParserService.GetAbsoluteGameMapsPath(), fileName); // Use robust path resolver

        try
        {
            var jsonContent = JsonSerializer.Serialize(mapData, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, jsonContent);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving map '{mapData.Name}' to {filePath}: {ex.Message}");
            throw; // Re-throw to propagate the error
        }
    }

    public async Task SaveMapAsync(MapData mapData)
    {
        await Task.Run(() => SaveMap(mapData)); // Offload synchronous save to a background thread
    }
}