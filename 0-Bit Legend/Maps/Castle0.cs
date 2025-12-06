
namespace _0_Bit_Legend.Maps;

public class Castle0 : IMap
{
    public string Name => "Castle 0";
    public string[] Raw =>[
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
    public string[] FlagAdjusted => Raw;

    public List<EntityLocation> EntityLocations { get; } = [];

    public void Load() { }
}
