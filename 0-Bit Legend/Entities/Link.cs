namespace _0_Bit_Legend.Entities;

public class Link : IEntity, IBoundingBox
{
    public int Hp { get; set; } = 3;
    public Vector2 Position { get; set; } = Vector2.Zero;
    public DirectionType Direction { get; set; } = DirectionType.Up;

    public (Vector2 TopLeft, Vector2 BottomRight) BoundingBox { get; } = (new(-2, -1), new(2, 2));

    private readonly Dictionary<DirectionType, string[]> _spriteSheet = new()
    {
        { DirectionType.Up,
        [
            " ___ ",
            "| = |",
            "|^ ^|",
           @" \_/ ",
        ]},
        { DirectionType.Left,
        [
           @"  /\ ",
            " /  |",
            "|^  |",
            "|_=_|",
        ]},
        { DirectionType.Down,
        [
            "  _  ",
           @" / \ ",
            "|^ ^|",
            "| = |",
        ]},
        { DirectionType.Right,
        [
           @" /\  ",
           @"|  \ ",
            "|  ^|",
            "|_=_|",
        ] },
    };

    private readonly Dictionary<DirectionType, string[]> _spriteSheetArmor = new()
    {
        { DirectionType.Up,
        [
            " ___ ",
            "|#=#|",
            "|^#^|",
           @" \_/ ",
        ]},
        { DirectionType.Left,
        [
           @"  /\ ",
            " /  |",
            "|^##|",
            "|#=#|",
        ]},
        { DirectionType.Down,
        [
            "  _  ",
           @" / \ ",
            "|^#^|",
            "|#=#|",
        ]},
        { DirectionType.Right,
        [
           @" /\  ",
           @"|  \ ",
            "|##^|",
            "|#=#|",
        ] },
    };

    public void Draw()
    {
        var image = HasFlag(GameFlag.HasArmor) ? _spriteSheetArmor[Direction] : _spriteSheet[Direction];

        for (var x = BoundingBox.TopLeft.X; x <= BoundingBox.BottomRight.X; x++)
        {
            for (var y = BoundingBox.TopLeft.Y; y <= BoundingBox.BottomRight.Y; y++)
            {
                Map[Position.X + x, Position.Y + y] = image[y + 1][x + 2];
            }
        }
    }

    public bool IsTouching(char symbol)
    {
        var posX = Position.X;
        var posY = Position.Y;

        if (symbol == '/')
        {
            return Map[posX, posY - 1] == '/';
        }

        if (InsideBoundingBox(symbol))
        {
            if (symbol is 'R' or 'r')
            {
                for (var i = 0; i < 4; i++)
                {
                    for (var j = 0; j < 5; j++)
                    {
                        if (Map[posX - 2 + j, posY - 1 + i] is 'R' or 'r')
                        {
                            EnemyManager.RemoveRupee(new(posX - 2 + j, posY - 1 + i));
                        }
                    }
                }
            }
            return true;
        }
        return false;
    }
    public bool IsTouching(char[] symbols) => throw new NotImplementedException();

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
}
