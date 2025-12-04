
namespace _0_Bit_Legend.Entities.Pickups;

public abstract class BasePickup : IPickup
{
    public Vector2 Position { get; set; } = Vector2.Zero;
    public DirectionType Direction { get; set; }

    public abstract string[] Image { get; }
    public abstract Action OnPickup { get; }

    public abstract (Vector2 TopLeft, Vector2 BottomRight) BoundingBox { get; }

    public void Draw()
    {
        for (var x = BoundingBox.TopLeft.X; x <= BoundingBox.BottomRight.X; x++)
        {
            for (var y = BoundingBox.TopLeft.Y; y <= BoundingBox.BottomRight.Y; y++)
            {
                Map[Position.X + x, Position.Y + y] = Image[y + 1][x + 2];
            }
        }
    }
    public bool InsideBoundingBox(char symbol)
    {
        for (var x = BoundingBox.TopLeft.X; x <= BoundingBox.BottomRight.X; x++)
        {
            for (var y = BoundingBox.TopLeft.Y; y <= BoundingBox.BottomRight.Y; y++)
            {
                if (Map[Position.X + x, Position.Y + y] == symbol)
                    return true;
            }
        }
        return false;
    }
    public bool InsideBoundingBox(char[] symbols)
    {
        for (var x = BoundingBox.TopLeft.X; x <= BoundingBox.BottomRight.X; x++)
        {
            for (var y = BoundingBox.TopLeft.Y; y <= BoundingBox.BottomRight.Y; y++)
            {
                if (symbols.Any(x => x == Map[Position.X + x, Position.Y + y]))
                    return true;
            }
        }
        return false;
    }
    public virtual bool IsTouching(char symbol) => InsideBoundingBox(symbol);
    public virtual bool IsTouching(char[] symbols) => InsideBoundingBox(symbols);
}