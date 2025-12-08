
using _0_Bit_Legend.Entities.Triggers;

namespace _0_Bit_Legend.Maps;

public class Castle0 : BaseMap
{
    public override string Name => "Castle 0";
    public override string[] Raw =>[
"======================================================================================================",
"=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=",
"=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=",
"=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=",
"=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=",
"=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXX==========================================XXXXXXXXXXXXXXXXXXXXXXXXXXXX=",
"=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=                                        =XXXXXXXXXXXXXXXXXXXXXXXXXXXX=",
"=XXXXXXXXXXXXXXXXXXXXXXXX=======                                        =============XXXXXXXXXXXXXXXX=",
"=XXXXXXXXXXXXXXXXXXXXXXXX=                             |***                         =XXXXXXXXXXXXXXXX=",
"=XXXXXXXXX================                             |******                      ==================",
"=XXXXXXXXX=                                            |  ****                                        ",
"=XXXXXXXXX=                                            =                                              ",
"=XXXXXXXXX=                             = == =         = = == =                                       ",
"=XXXXXXXXX=                             ======         = ======                                       ",
"=XXXXXXXXX=           XXXXX             ||||||===========||||||             XXXXX                     ",
"=XXXXXXXXX=           =/X/=             =======/////////=======             =/X/=                     ",
"=XXXXXXXXX=           =====             ||||||=/////////=||||||             =====                     ",
"=XXXXXXXXX=                             =======/////////=======                                       ",
"=XXXXXXXXX=                                                                                           ",
"=XXXXXXXXX=                                                                                           ",
"=XXXXXXXXX=                                                                                           ",
"=XXXXXXXXX=                                                                                           ",
"=XXXXXXXXX=====                                                                                       ",
"=XXXXXXXXXXXXX=                                                                                       ",
"=XXXXXXXXXXXXX=                                                                                       ",
"=XXXXXXXXXXXXX=                              ==============                                           ",
"=XXXXXXXXXXXXX================================XXXXXXXXXXXX=                        ===================",
"XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX==========================XXXXXXXXXXXXXXXXX=",
"XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=",
"=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=",
"=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=",
"=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=",
"======================================================================================================",
];

    public override List<EntityLocation> EntityLocations { get; } =
    [
        new(typeof(EnterCave0), new(47,15), () => true),
    ];

    public override List<NewAreaInfo> AreaTransitions { get; } =
    [
        new(MapId: 2, StartPosition: new(52, 18),
            DirectionType.Left, Size: new(3, 10), Position: new(0, 9)),
    ];
}
