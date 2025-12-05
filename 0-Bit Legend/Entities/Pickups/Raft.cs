namespace _0_Bit_Legend.Entities.Pickups;

public class Raft : BasePickup, IPurchased
{
    public Raft() => OnPickup = () =>
    {
        Rupees -= Cost;
        SetFlag(GameFlag.HasRaft);
        EntityManager.Remove(this);
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

    public override Vector2 Size { get; } = new(4, 5);
}
