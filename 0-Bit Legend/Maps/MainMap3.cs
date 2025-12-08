using _0_Bit_Legend.Entities.Enemies;

namespace _0_Bit_Legend.Maps;

public class MainMap3 : BaseMap
{
    public override string Name => "Main Map 3";
    public override string[] Raw =>[
"=============================                                                  =======================",
"=XXXXXXXXXXXXXXXXXXXXXXXXXXX=                                                  =XXXXXXXXXXXXXXXXXXXXX=",
"=XXXXXXXXX===================                                                  ======XXXXXXXXXXXXXXXX=",
"=XXXXXXXXX= XXXXX                                                                   =XXXXXXXXXXXXXXXX=",
"=XXXXXXXXX= XX=XX                                                                   =XXXXXXXXXXXXXXXX=",
"=XXXXXXXXX=   =                                                                     ==========XXXXXXX=",
"=XXX=======                                                                            XXXXX =XXXXXXX=",
"=XXX=                                                                                  XX=XX =XXXXXXX=",
"=XXX=                                                                                    =   =XXXXXXX=",
"=XXX=                                                                                        =XXXXXXX=",
"=XXX=                  XXXXX       XXXXX       XXXXX       XXXXX       XXXXX                 =XXXXXXX=",
"=XXX=                  XX=XX       XX=XX       XX=XX       XX=XX       XX=XX                 ====XXXX=",
"=====                    =           =           =           =           =                      =XXXX=",
"                                                                                                =XXXX=",
"                                                                                                =XXXX=",
"                                                                                                =XXXX=",
"                                                                                                =XXXX=",
"                                                                                                =XXXX=",
"                                                                                                =XXXX=",
"                       XXXXX       XXXXX       XXXXX       XXXXX       XXXXX                    ==XXX=",
"                       XX=XX       XX=XX       XX=XX       XX=XX       XX=XX                     =XXX=",
"                         =           =           =           =           =                 XXXXX =XXX=",
"                                                                                           XX=XX =XXX=",
"                                                                                             =   =XXX=",
"      XXXXX                                                                          XXXXX XXXXX =XXX=",
"===== XX=XX                                                                          XX=XX XX=XX =XXX=",
"=XXX=   =                                                                              =     =   =XXX=",
"=XXX======= XXXXX XXXXX                                                        XXXXX XXXXX =======XXX=",
"=XXXXXXXXX= XX=XX XX=XX                                                        XX=XX XX=XX =XXXXXXXXX=",
"=XXXXXXXXX=   =     =                                                            =     =   =XXXXXXXXX=",
"=XXXXXXXXX==================================================================================XXXXXXXXX=",
"=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=",
"======================================================================================================",
];
    public override List<EntityLocation> EntityLocations { get; } =
    [
        new(typeof(Spider), new(44, 25), () => true),
        new(typeof(Octorok), new(38, 14), () => true),
        new(typeof(Octorok), new(83, 9), () => true),
    ];

    public override List<NewAreaInfo> AreaTransitions { get; } =
    [
        new(MapId: 2, StartPosition: new(52, 18),
            DirectionType.Left, Size: new(3, 10), Position: new(0, 9)),
        new(MapId: 1, StartPosition: new(52, 18),
            DirectionType.Up,   Size: new(21, 1), Position: new(53, 0)),
    ];
}
