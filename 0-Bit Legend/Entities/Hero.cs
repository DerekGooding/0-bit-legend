namespace _0_bit_Legend.Entities;

public class Hero : IEntity, ICollider
{
    public bool IsTakingDamaged { get; set; }
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

    private readonly string[] _spriteSheetDamaged =
    [
        "*****",
        "*****",
        "*****",
        "*****",
    ];

    public void HandleCollision()
    {
        //TODO
    }
    public void Draw()
    {
        var image = IsTakingDamaged ? _spriteSheetDamaged :
                            HasFlag(GameFlag.HasArmor) ? _spriteSheetArmor[Direction] :
                            _spriteSheet[Direction];
        DrawToScreen(image, Position);
    }
}
