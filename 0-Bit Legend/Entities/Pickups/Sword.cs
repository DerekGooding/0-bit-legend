namespace _0_Bit_Legend.Entities.Pickups;

public class Sword : BasePickup
{
    public override string[] Image => _spriteSheet;

    private readonly string[] _spriteSheet =
[
        "FFF",
        "FFF",
    ];

    public override Action OnPickup { get; } = () =>
    {
        SetFlag(GameFlag.HasSword);
        //PickupManager.Remove();
    };

    public override (Vector2 TopLeft, Vector2 BottomRight) BoundingBox { get; } = (new(), new());
}
