using BitLegend.MapEditor.Model;

namespace BitLegend.MapEditor.Services;

public interface IMapFileParserService
{
    public List<MapData> LoadMaps();
    public Task<List<MapData>> LoadMapsAsync();
}
