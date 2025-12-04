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
    public string[] FlagAdjusted => Raw;

    public List<EntityLocation> EntityLocations { get; } = [];
    public void Load() { }
}
