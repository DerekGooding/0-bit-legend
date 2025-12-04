namespace _0_Bit_Legend.Entities.Pickups;

public class Raft : BasePickup, IPurchased
{
    public Raft() => OnPickup = () =>
    {
        Rupees -= Cost;
        SetFlag(GameFlag.HasRaft);
        PickupManager.Remove(this);
    };

    public int Cost { get; }

    public override string[] Image => _spriteSheet;

    private readonly string[] _spriteSheet =
[
        "=====",
        "*****",
        "=====",
        "*****",
        "RAFT ",
        " x35 ",
    ];

    public override Action OnPickup { get; }

    public override (Vector2 TopLeft, Vector2 BottomRight) BoundingBox { get; } = (new(-2, -2), new(2, 3));
}
