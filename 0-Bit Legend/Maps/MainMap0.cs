using _0_Bit_Legend.Entities.Triggers;

namespace _0_Bit_Legend.Maps;

public class MainMap0 : BaseMap
{
    public override string Name => "Main Map 0";
    public override string[] Raw => [
"======================================================                    ============================",
"=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=                    =XXXXXXXXXXXXXXXXXXXXXXXXXX=",
"=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=                    =XXXXXXXXXXXXXXXXXXXXXXXXXX=",
"=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX====================                    =XXXXXXXXXXXXXXXXXXXXXXXXXX=",
"=XXXXXXXXXXXX       XXXXXXXXXXXXXX=                                       =========XXXXXXXXXXXXXXXXXX=",
"=XXXXXXXXXXXX       XXXXXXXXX======                                               =XXXXXXXXXXXXXXXXXX=",
"=XXXXXXXXX===       ==========                                                    =XXXXXXXXXXXXXXXXXX=",
"=XXXXXXXXX=                                                                       =XXXXXXXXXXXXXXXXXX=",
"=XXXXXXXXX=                                                                       =XXXXXXXXXXXXXXXXXX=",
"===========                                                                       =XXXXXXXXXXXXXXXXXX=",
"                                                                                  =====XXXXXXXXXXXXXX=",
"                                                                                      ======XXXXXXXXX=",
"                                                                                           ===========",
"                                                                                                      ",
"                                                                                                      ",
"                                                                                                      ",
"                                                                                                      ",
"                                                                                                      ",
"                                                                                                      ",
"====                                                                                                  ",
"=XX=                                                                                                  ",
"=XX=                                                                                                  ",
"=XX=                                                                                                  ",
"=XX=                                                                                                  ",
"=XX=                                                                                                  ",
"=XX=                                                                                            ======",
"=XX=======                                                                                      =XXXX=",
"=XXXXXXXX=                                                                                 ======XXXX=",
"=XXXXXXXX=                                      ============================================XXXXXXXXX=",
"=XXXXXXXX=========                              =XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=",
"=XXXXXXXXXXXXXXXX================================XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=",
"=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=",
"======================================================================================================",
];

    public override List<EntityLocation> EntityLocations { get; } =
    [
        new(typeof(EnterCave0), new(13,4), () => true),
    ];

    public override List<NewAreaInfo> AreaTransitions { get; } =
    [
        new(1, new(52, 18), DirectionType.Left, new(0, 10), new(2, 11)),
    ];
}
