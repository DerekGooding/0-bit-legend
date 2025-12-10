using BitLegend.MapEditor.Model;
using System.Text.Json;

namespace BitLegend.MapEditor.Services;

[Singleton]
public class MapFileSaverService : IMapFileSaverService
{
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

    public async Task SaveMapAsync(MapData mapData) => await Task.Run(() => SaveMap(mapData));
}