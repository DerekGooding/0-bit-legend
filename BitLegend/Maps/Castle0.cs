using BitLegend.Entities.Triggers;

namespace BitLegend.Maps;

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
"=XXXXXXXXX=           =/X/=             =======         =======             =/X/=                     ",
"=XXXXXXXXX=           =====             ||||||=         =||||||             =====                     ",
"=XXXXXXXXX=                             =======         =======                                       ",
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
        new(typeof(EnterCastle), new(47,15), () => true),
    ];

    public override List<NewAreaInfo> AreaTransitions { get; } =
    [
        new(MapId: WorldMap.MapName.MainMap5, StartPosition: new(52, 18),
            DirectionType.Left, Size: new(3, 10), Position: new(0, 9)),
    ];
}
