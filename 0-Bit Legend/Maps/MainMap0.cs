using _0_Bit_Legend.Entities.Triggers;

namespace _0_Bit_Legend.Maps;

public class MainMap0 : IMap
{
    public string Name => "Main Map 0";
    public string[] Raw =>[
"======================================================                    ============================#",
"=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=                    =XXXXXXXXXXXXXXXXXXXXXXXXXX=#",
"=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=                    =XXXXXXXXXXXXXXXXXXXXXXXXXX=#",
"=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX====================                    =XXXXXXXXXXXXXXXXXXXXXXXXXX=#",
"=XXXXXXXXXXXX///////XXXXXXXXXXXXXX=                                       =========XXXXXXXXXXXXXXXXXX=#",
"=XXXXXXXXXXXX///////XXXXXXXXX======                                               =XXXXXXXXXXXXXXXXXX=#",
"=XXXXXXXXX===///////==========                                                    =XXXXXXXXXXXXXXXXXX=#",
"=XXXXXXXXX=                                                                       =XXXXXXXXXXXXXXXXXX=#",
"=XXXXXXXXX=                                                                       =XXXXXXXXXXXXXXXXXX=#",
"===========                                                                       =XXXXXXXXXXXXXXXXXX=#",
"                                                                                  =====XXXXXXXXXXXXXX=#",
"                                                                                      ======XXXXXXXXX=#",
"                                                                                           ===========#",
"                                                                                                      #",
"                                                                                                      #",
"                                                                                                      #",
"                                                                                                      #",
"                                                                                                      #",
"                                                                                                      #",
"====                                                                                                  #",
"=XX=                                                                                                  #",
"=XX=                                                                                                  #",
"=XX=                                                                                                  #",
"=XX=                                                                                                  #",
"=XX=                                                                                                  #",
"=XX=                                                                                            ======#",
"=XX=======                                                                                      =XXXX=#",
"=XXXXXXXX=                                                                                 ======XXXX=#",
"=XXXXXXXX=                                      ============================================XXXXXXXXX=#",
"=XXXXXXXX=========                              =XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#",
"=XXXXXXXXXXXXXXXX================================XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#",
"=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#",
"======================================================================================================#",
];
    public List<EntityLocation> EntityLocations { get; } =
    [
        new(typeof(EnterCave0), new(13,5), () => true),
    ];
    public void Load() { }
}
