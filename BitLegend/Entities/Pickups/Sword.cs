namespace BitLegend.Entities.Pickups;

public class Sword : BasePickup
{
    public Sword() => OnPickup = () =>
    {
        SetFlag(GameFlag.HasSword);
        EntityManager.Remove(this);
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

    public override Vector2 Size { get; } = new(2, 3);
}
