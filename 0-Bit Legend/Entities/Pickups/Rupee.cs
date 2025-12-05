namespace _0_Bit_Legend.Entities.Pickups;

public class Rupee : BasePickup
{
    public Rupee() => OnPickup = () =>
    {
        Rupees += 5;
        EnemyManager.RemoveRupee(Position);
    };

    public char[] MapStorage { get; } = new string(' ', 9).ToCharArray();

    public override string[] Image => _spriteSheet;

    private readonly string[] _spriteSheet =
[
        " r ",
        "RRR",
        " r "
    ];

    public override Action OnPickup { get; }

    public override (Vector2 TopLeft, Vector2 BottomRight) BoundingBox { get; } = (new(-1, -1), new(1, 1));
}
