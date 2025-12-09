using BitLegend.Content;
using BitLegend.Model.Enums;

namespace BitLegend.Model;

public readonly record struct NewAreaInfo(WorldMap.MapName MapId, Vector2 StartPosition, DirectionType StartDirection, Vector2 Size, Vector2 Position);