namespace _0_Bit_Legend.Maps;

public class MainMap3 : IMap
{
    public string Name => "Main Map 3";
    public string[] Raw =>[
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
    public string[] FlagAdjusted => Raw;

    public List<EntityLocation> EntityLocations { get; } = [];
    public void Load() { }
}
