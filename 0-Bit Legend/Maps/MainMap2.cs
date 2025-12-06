namespace _0_Bit_Legend.Maps;

public class MainMap2 : IMap
{
    public string Name => "Main Map 2";
    public string[] Raw =>[
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
    public string[] FlagAdjusted => Raw;

    public List<EntityLocation> EntityLocations { get; } = [];
    public void Load() { }
}
