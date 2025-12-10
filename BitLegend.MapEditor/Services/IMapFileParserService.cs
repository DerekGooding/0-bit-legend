using BitLegend.MapEditor.Model;

namespace BitLegend.MapEditor.Services;

public interface IMapFileParserService
{
    public MapData? LoadMap(string filePath);
    public Task<List<MapData>> LoadMapsAsync();
}
