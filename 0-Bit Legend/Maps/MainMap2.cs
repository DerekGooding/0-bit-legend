using _0_Bit_Legend.Entities.Enemies;

namespace _0_Bit_Legend.Maps;

public class MainMap2 : BaseMap
{
    public override string Name => "Main Map 2";
    public override string[] Raw =>[
"=====         ~~~~~~~~~~~~~~ =============                       =====================================",
"=XXX=         ~~~~~~~~~~~~~~ =XXXXXXXXXXX=                       =XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=",
"=XXX=         ~~~~~~~~~~~~~~ =============                       =======XXXXXXXXXXXXXXXXXXXXXXXXXXXXX=",
"=XXX=         ~~~~~~~~~~~~~~                                           =XXXXXXXXXXXXXXXXXXXXXXXXXXXXX=",
"=XXX=         ~~~~~~~~~~~~~~                                           ============XXXXXXXXXXXXXXXXXX=",
"=XXX=         ~~~~~~~~~~~~~~                                                      =XXXXXXXXXXXXXXXXXX=",
"=XXX=         ~~~~~~~~~~~~~~                                                      =XXXXXXXXXXXXXXXXXX=",
"=XXX=         ~~~~~~~~~~~~~~                                                      =XXXXXXXXXXXXXXXXXX=",
"=XXX=         ~~~~~~~~~~~~~~                                                      ========XXXXXXXXXXX=",
"=XXX=         ~~~~~~~~~~~~~~                                                             =============",
"=XXX=         ~~~~~~~~~~~~~~                                                                          ",
"=XXX=         ~~~~~~~~~~~~~~           XXXXX         XXXXX         XXXXX                              ",
"=XXX=         ~~~~~~~~~~~~~~           XX=XX         XX=XX         XX=XX                              ",
"=XXX=         ~~~~~~~~~~~~~~             =             =             =                                ",
"=XXX=         ~~~~~~~~~~~~~~                                                                          ",
"=XXX=         ~~~~~~~~~~~~~~                                                                          ",
"=XXX=         ~~~~~~~~~~~~~~                                                                          ",
"=XXX=         ~~~~~~~~~~~~~~                                                                          ",
"=XXX=         ~~~~~~~~~~~~~~                                                                          ",
"=XXX=         ~~~~~~~~~~~~~~           XXXXX         XXXXX         XXXXX                       =======",
"=XXX=         ~~~~~~~~~~~~~~           XX=XX         XX=XX         XX=XX                       =XXXXX=",
"=XXX=         ~~~~~~~~~~~~~~             =             =             =                         =XXXXX=",
"=XXX=         ~~~~~~~~~~~~~~                                                                   =XXXXX=",
"=XXX=====     ~~~~~~~~~~~~~~                                                               =====XXXXX=",
"=XXXXXXX=     ~~~~~~~~~~~~~~                                                               =XXXXXXXXX=",
"=XXXXXXX=     ~~~~~~~~~~~~~~                                                               =XXXXXXXXX=",
"=XXXXXXX=     ~~~~~~~~~~~~~~                                                         =======XXXXXXXXX=",
"=XXXXXXX======~~~~~~~~~~~~~~=====                                                    =XXXXXXXXXXXXXXX=",
"=XXXXXXXXXXXX=~~~~~~~~~~~~~~=XXX=                                  ===================XXXXXXXXXXXXXXX=",
"=XXXXXXXXXXXX=~~~~~~~~~~~~~~=XXX==========                         =XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=",
"=XXXXXXXXXXXX================XXXXXXXXXXXX===========================XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=",
"=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=",
"======================================================================================================",
];
    public override List<EntityLocation> EntityLocations { get; } =
    [
        new(typeof(Octorok), new(59, 23), () => true),
        new(typeof(Spider), new(76, 6), () => true),
    ];

    public override List<NewAreaInfo> AreaTransitions { get; } =
    [
        new(MapId: 2, StartPosition: new(52, 18),
            DirectionType.Left, Size: new(3, 10), Position: new(0, 9)),
        new(MapId: 1, StartPosition: new(52, 18),
            DirectionType.Up,   Size: new(21, 1), Position: new(53, 0)),
        new(MapId: 0, StartPosition: new(52, 18),
            DirectionType.Right, Size: new(3, 9), Position: new(98, 10)),
    ];
}
