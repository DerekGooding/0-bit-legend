using BitLegend.Content;
using BitLegend.Entities.Enemies;
using BitLegend.Model;
using BitLegend.Model.Enums;

namespace BitLegend.Maps;

public class MainMap1 : BaseMap
{
    public override string Name => "Main Map 1";
    public override string[] Raw =>[
"======================================================================================================",
"=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=",
"=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=",
"=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=",
"=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=",
"=XXXXXXXXXXXXXXXXXXXXXXXXXXXX======================================================XXXXXXXXXXXXXXXXXX=",
"=XXXXXXXXX====================                                                    =XXXXXXXXXXXXXXXXXX=",
"=XXXXXXXXX=                                                                       =XXXXXXXXXXXXXXXXXX=",
"=XXXXXXXXX=                                                                       =XXXXXXXXXXXXXXXXXX=",
"===========                                                                       =XXXXXXXXXXXXXXXXXX=",
"                                                                                  =====XXXXXXXXXXXXXX=",
"                       XXXXX               XXXXX               XXXXX                  ======XXXXXXXXX=",
"                       =XXX=               =XXX=               =XXX=                       =XXXXXXXXX=",
"                       =====               =====               =====                       ===========",
"                                                                                                      ",
"                                                                                                      ",
"                                                                                                      ",
"                                                                                                      ",
"                                                                                                      ",
"====                                                                                                  ",
"=XX=                   XXXXX               XXXXX               XXXXX                            ======",
"=XX=                   =XXX=               =XXX=               =XXX=                     ========XXXX=",
"=XX=                   =====               =====               =====                     =XXXXXXXXXXX=",
"=XX=                                                                                     =XXXXXXXXXXX=",
"=XX======                                                                                =XXXXXXXXXXX=",
"=XXXXXXX=                                                                                =XXXXXXXXXXX=",
"=XXXXXXX==                                                                         =======XXXXXXXXXXX=",
"=XXXXXXXX=                                                                      ====XXXXXXXXXXXXXXXXX=",
"=XXXXXXXX=                                                                      =XXXXXXXXXXXXXXXXXXXX=",
"=XXXXXXXX=========                                                              =XXXXXXXXXXXXXXXXXXXX=",
"=XXXXXXXXXXXXXXXX=====================================                    =======XXXXXXXXXXXXXXXXXXXX=",
"=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=                    =XXXXXXXXXXXXXXXXXXXXXXXXXX=",
"======================================================                    ============================",
];
    public override List<EntityLocation> EntityLocations { get; } =
    [
        new(typeof(Octorok), new(75, 13), () => true),
        new(typeof(Octorok), new(9, 12), () => true),
        new(typeof(Octorok), new(23, 26), () => true),
    ];

    public override List<NewAreaInfo> AreaTransitions { get; } =
    [
        new(MapId: WorldMap.MapName.MainMap5, StartPosition: new(52, 18),
            DirectionType.Left, Size: new(3, 10), Position: new(0, 9)),
        new(MapId: WorldMap.MapName.MainMap0, StartPosition: new(52, 18),
            DirectionType.Up,   Size: new(21, 1), Position: new(53, 0)),
        new(MapId: WorldMap.MapName.MainMap4, StartPosition: new(52, 18),
            DirectionType.Right, Size: new(3, 13), Position: new(98, 12)),
    ];
}
