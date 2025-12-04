
namespace _0_Bit_Legend.Entities.Pickups;

public class Armor : BasePickup, IPurchased
{
    public Armor() => OnPickup = () =>
    {
        Rupees -= Cost;
        SetFlag(GameFlag.HasArmor);
        PickupManager.Remove(this);
    };

    public int Cost { get; }

    public override string[] Image => _spriteSheet;

    private readonly string[] _spriteSheet =
[
        "FFF",
        "FFF",
    ];

    public override Action OnPickup { get; }

    public override (Vector2 TopLeft, Vector2 BottomRight) BoundingBox { get; } = (new(), new());
}
