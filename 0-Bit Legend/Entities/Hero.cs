namespace _0_Bit_Legend.Entities;

public class Hero : IEntity, IBoundingBox
{
    public double Hp { get; set; } = 3;
    private Vector2 _position = Vector2.Zero;
    public Vector2 Position
    {
        get => _position;
        set
        {
            if(_position == value) return;
            _position = value;
            RequiresRedraw = true;
        }
    }
    public DirectionType Direction { get; set; } = DirectionType.Up;

    public Vector2 Size { get; } = new(4, 3);

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
        DrawToScreen(image, Position);
    }
}
