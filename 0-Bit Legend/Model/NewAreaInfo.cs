using _0_bit_Legend.Content;

namespace _0_bit_Legend.Model;

public readonly record struct NewAreaInfo(WorldMap.MapName MapId, Vector2 StartPosition, DirectionType StartDirection, Vector2 Size, Vector2 Position);