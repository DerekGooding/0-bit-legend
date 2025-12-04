namespace _0_Bit_Legend.Entities.Pickups;

public class Key : BasePickup, IPurchased
{
    public Key() => OnPickup = () =>
    {
        Rupees -= Cost;
        Keys++;
    };

    public int Cost { get; }

    public override string[] Image => _spriteSheet;

    private readonly string[] _spriteSheet =
[
        "=======",
        "==  = =",
        "       ",
        " Key   ",
        " x10   ",
    ];

    public override Action OnPickup { get; }

    public override (Vector2 TopLeft, Vector2 BottomRight) BoundingBox { get; } = (new(-2, -2), new(4, 2));
}
