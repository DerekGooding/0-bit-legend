namespace _0_Bit_Legend.Entities;

public class Link : IEntity
{
    public int Hp { get; set; } = 3;
    public Vector2 Position { get; set; } = Vector2.Zero;
    public DirectionType Direction { get; set; } = DirectionType.Up;

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

        for (var x = -2; x <= 2; x++)
        {
            for (var y = -1; y <= 2; y++)
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

        if (Map[posX - 2, posY - 1] == symbol
            || Map[posX - 1, posY - 1] == symbol
            || Map[posX, posY - 1] == symbol
            || Map[posX + 1, posY - 1] == symbol
            || Map[posX + 2, posY - 1] == symbol
            || Map[posX - 2, posY] == symbol
            || Map[posX - 1, posY] == symbol
            || Map[posX, posY] == symbol
            || Map[posX + 1, posY] == symbol
            || Map[posX + 2, posY] == symbol
            || Map[posX - 2, posY + 1] == symbol
            || Map[posX - 1, posY + 1] == symbol
            || Map[posX, posY + 1] == symbol
            || Map[posX + 1, posY + 1] == symbol
            || Map[posX + 2, posY + 1] == symbol
            || Map[posX - 2, posY + 2] == symbol
            || Map[posX - 1, posY + 2] == symbol
            || Map[posX, posY + 2] == symbol
            || Map[posX + 1, posY + 2] == symbol
            || Map[posX + 2, posY + 2] == symbol)
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
}
