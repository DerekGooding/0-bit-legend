using BitLegend.Entities.Enemies;

namespace BitLegend.Maps;

public class MainMap5 : BaseMap
{
    public override string Name => "MainMap5";
    public override string[] Raw =>[
"======================================================================================================",
"=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=",
"=XXXXXXXXX===========================================================================XXXXXXXXXXXXXXXX=",
"=XXXXXXXXX= XXXXX                                                                   =XXXXXXXXXXXXXXXX=",
"=XXXXXXXXX= XXXXX                                                                   =XXXXXXXXXXXXXXXX=",
"=XXXXXXXXX=   =                                                                     ==========XXXXXXX=",
"=XXXXXXXXX=                                                                            XXXXX =XXXXXXX=",
"=XXX=======                                                                            XXXXX =XXXXXXX=",
"=XXX= XXXXX                                                                              =   =XXXXXXX=",
"=XXX= XXXXX                                                                                  =XXXXXXX=",
"=XXX=   =              XXXXX       XXXXX       XXXXX       XXXXX       XXXXX                 =XXXXXXX=",
"=XXX=                  =XXX=       =XXX=       =XXX=       =XXX=       =XXX=                 ====XXXX=",
"=XXX=                  =====       =====       =====       =====       =====                    =XXXX=",
"=====                                                                                           =XXXX=",
"                                                                                                =XXXX=",
"                                                                                                =XXXX=",
"                                                                                                =XXXX=",
"                                                                                                =XXXX=",
"                                                                                                =XXXX=",
"                       XXXXX       XXXXX       XXXXX       XXXXX       XXXXX                    ==XXX=",
"=====                  =XXX=       =XXX=       =XXX=       =XXX=       =XXX=                     =XXX=",
"=XXX=                  =====       =====       =====       =====       =====               XXXXX =XXX=",
"=XXX====                                                                                   XXXXX =XXX=",
"=XXXXXX=                                                                                     =   =XXX=",
"=XXXXXX= XXXXX                                                                       XXXXX XXXXX =XXX=",
"=XXXXXX= XXXXX                                                                       XXXXX XXXXX =XXX=",
"=XXXXXX=   =                                                                           =     =   =XXX=",
"=XXXXXX========== XXXXX                                                        XXXXX XXXXX =======XXX=",
"=XXXXXXXXXXXXXXX= XXXXX                                                        XXXXX XXXXX =XXXXXXXXX=",
"=XXXXXXXXXXXXXXX=   =                                                            =     =   =XXXXXXXXX=",
"=XXXXXXXXXXXXXXX=============                                                  =============XXXXXXXXX=",
"=XXXXXXXXXXXXXXXXXXXXXXXXXXX=                                                  =XXXXXXXXXXXXXXXXXXXXX=",
"=============================                                                  =======================",
];
    public override List<EntityLocation> EntityLocations { get; } =
    [
        new(typeof(Spider), new(81, 9), () => true),
        new(typeof(Spider), new(32, 5), () => true),
    ];

    public override List<NewAreaInfo> AreaTransitions { get; } =
    [
        new(MapId: WorldMap.MapName.MainMap1, StartPosition: new(52, 18),
            DirectionType.Left, Size: new(3, 10), Position: new(0, 9)),
        new(MapId: WorldMap.MapName.MainMap3, StartPosition: new(52, 18),
            DirectionType.Down,   Size: new(21, 1), Position: new(53, 0)),
    ];
}
