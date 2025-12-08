using _0_Bit_Legend.Entities.Enemies;
using _0_Bit_Legend.Entities.Triggers;

namespace _0_Bit_Legend.Maps;

public class MainMap4 : BaseMap
{
    public override string Name => "Main Map 4";
    public override string[] Raw =>[
"======================================================================================================",
"=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=",
"=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=",
"=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=",
"=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=",
"=XXXXXXXXXXXXXXXXXXXXXXXXXXXX====================================================XX       XXXXXXXXXXX=",
"=XXXXXXXXX====================                                                  =XX       XXXXXXXXXXX=",
"=XXXXXXXXX=   ~~~~~~~~~~~~~~                                                    ===       =XXXXXXXXXX=",
"=XXXXXXXXX=   ~~~~~~~~~~~~~~                                                              =XXXXXXXXXX=",
"===========   ~~~~~~~~~~~~~~                                                              ============",
"              ~~~~~~~~~~~~~~                                                                          ",
"              ~~~~~~~~~~~~~~           XXXXX         XXXXX         XXXXX                              ",
"              ~~~~~~~~~~~~~~           =XXX=         =XXX=         =XXX=                              ",
"              ~~~~~~~~~~~~~~           =====         =====         =====                              ",
"              ~~~~~~~~~~~~~~                                                                          ",
"              ~~~~~~~~~~~~~~                                                                          ",
"              ~~~~~~~~~~~~~~                                                                          ",
"              ~~~~~~~~~~~~~~                                                                          ",
"              ~~~~~~~~~~~~~~                                                                          ",
"              ~~~~~~~~~~~~~~           XXXXX         XXXXX         XXXXX                       =======",
"              ~~~~~~~~~~~~~~           =XXX=         =XXX=         =XXX=                       =XXXXX=",
"              ~~~~~~~~~~~~~~           =====         =====         =====                       =XXXXX=",
"              ~~~~~~~~~~~~~~                                                                   =XXXXX=",
"              ~~~~~~~~~~~~~~                                                               =====XXXXX=",
"              ~~~~~~~~~~~~~~                                                               =XXXXXXXXX=",
"              ~~~~~~~~~~~~~~                                                               =XXXXXXXXX=",
"=====         ~~~~~~~~~~~~~~                                                         =======XXXXXXXXX=",
"=XXX=         ~~~~~~~~~~~~~~ =====                                                   =XXXXXXXXXXXXXXX=",
"=XXX=         ~~~~~~~~~~~~~~ =XXX=                                 ===================XXXXXXXXXXXXXXX=",
"=XXX=         ~~~~~~~~~~~~~~ =XXX==========                        =XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=",
"=XXX=         ~~~~~~~~~~~~~~ =XXXXXXXXXXXX=                      ===XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=",
"=XXX=         ~~~~~~~~~~~~~~ =XXXXXXXXXXXX=                      =XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=",
"=====         ~~~~~~~~~~~~~~ ==============                      =====================================",
];
    public override List<EntityLocation> EntityLocations { get; } =
    [
        new(typeof(EnterCastle), new(84,4), () => true),
        new(typeof(Octorok), new(23, 23), () => true),
        new(typeof(Octorok), new(69, 6), () => true),
    ];

    public override List<NewAreaInfo> AreaTransitions { get; } =
    [
        new(MapId: 2, StartPosition: new(52, 18),
            DirectionType.Left, Size: new(3, 10), Position: new(0, 9)),
        new(MapId: 1, StartPosition: new(52, 18),
            DirectionType.Up,   Size: new(21, 1), Position: new(53, 0)),
        new(MapId: 3, StartPosition: new(52, 18),
            DirectionType.Right, Size: new(3, 13), Position: new(98, 12)),
    ];
}
