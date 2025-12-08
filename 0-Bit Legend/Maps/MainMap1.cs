using _0_Bit_Legend.Entities.Enemies;

namespace _0_Bit_Legend.Maps;

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

    public override List<NewAreaInfo> AreaTransitions { get; } = [];
}
