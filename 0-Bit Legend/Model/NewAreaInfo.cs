using _0_Bit_Legend.Content;

namespace _0_Bit_Legend.Model;

public readonly record struct NewAreaInfo(WorldMap.MapName MapId, Vector2 StartPosition, DirectionType StartDirection, Vector2 Size, Vector2 Position);