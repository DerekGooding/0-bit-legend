namespace _0_Bit_Legend.Entities.Pickups;

public class Sword : BasePickup
{
    public Sword() => OnPickup = () =>
    {
        SetFlag(GameFlag.HasSword);
        PickupManager.Remove(this);
    };

    public override string[] Image => _spriteSheet;

    private readonly string[] _spriteSheet =
[
        " S ",
        " S ",
        "---",
        " - ",
    ];

    public override Action OnPickup { get; }

    public override (Vector2 TopLeft, Vector2 BottomRight) BoundingBox { get; } = (new(-1, -2), new(1, 1));
}
