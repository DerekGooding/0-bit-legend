namespace BitLegend.Entities.Pickups;

public class Armor : BasePickup, IPurchased
{
    public Armor() => OnPickup = () =>
    {
        Rupees -= Cost;
        SetFlag(GameFlag.HasArmor);
        EntityManager.Remove(this);
    };

    public int Cost { get; }

    public override string[] Image => _spriteSheet;

    private readonly string[] _spriteSheet =
[
        "## ##",
        "#####",
        " ### ",
        "ARMOR",
        " x25 ",
    ];

    public override Action OnPickup { get; }

    public override Vector2 Size { get; } = new(4, 4);
}
