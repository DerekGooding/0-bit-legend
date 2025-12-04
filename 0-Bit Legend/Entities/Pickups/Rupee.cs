namespace _0_Bit_Legend.Entities.Pickups;

public class Rupee : BasePickup
{
    public Rupee() => OnPickup = () =>
    {
        Rupees += 5;
        EnemyManager.RemoveRupee(Position);
    };

    public char[] MapStorage { get; } = new string(' ', 9).ToCharArray();

    public void Clear()
    {
        Map[Position.X - 1, Position.Y - 1] = MapStorage[0];
        Map[Position.X + 0, Position.Y - 1] = MapStorage[1];
        Map[Position.X + 1, Position.Y - 1] = MapStorage[2];

        Map[Position.X - 1, Position.Y] = MapStorage[3];
        Map[Position.X + 0, Position.Y] = MapStorage[4];
        Map[Position.X + 1, Position.Y] = MapStorage[5];

        Map[Position.X - 1, Position.Y + 1] = MapStorage[6];
        Map[Position.X + 0, Position.Y + 1] = MapStorage[7];
        Map[Position.X + 1, Position.Y + 1] = MapStorage[8];
    }

    public override string[] Image => _spriteSheet;

    private readonly string[] _spriteSheet =
[
        "FFF",
        "FFF",
    ];

    public override Action OnPickup { get; }

    public override (Vector2 TopLeft, Vector2 BottomRight) BoundingBox { get; } = (new(), new());
}
