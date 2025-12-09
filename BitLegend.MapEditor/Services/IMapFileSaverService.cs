using BitLegend.MapEditor.Model;

namespace BitLegend.MapEditor.Services;

public interface IMapFileSaverService
{
    public void SaveMap(MapData mapData);
}
