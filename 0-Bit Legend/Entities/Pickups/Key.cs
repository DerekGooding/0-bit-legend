namespace _0_Bit_Legend.Entities.Pickups;

public class Key : BasePickup, IPurchased
{
    public int Cost { get; }

    public override string[] Image => _spriteSheet;

    private readonly string[] _spriteSheet =
[
        "FFF",
        "FFF",
    ];

    public override Action OnPickup { get; } = () =>
    {

    };

    public override (Vector2 TopLeft, Vector2 BottomRight) BoundingBox { get; } = (new(), new());
}
