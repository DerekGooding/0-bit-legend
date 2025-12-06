namespace _0_Bit_Legend.Maps;

public class MainMap5 : IMap
{
    public string Name => "Main Map 5";
    public string[] Raw =>[
"======================================================================================================",
"=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=",
"=XXXXXXXXX===========================================================================XXXXXXXXXXXXXXXX=",
"=XXXXXXXXX= XXXXX                                                                   =XXXXXXXXXXXXXXXX=",
"=XXXXXXXXX= XXXXX                                                                   =XXXXXXXXXXXXXXXX=",
"=XXXXXXXXX=   =                                                                     ==========XXXXXXX=",
"=XXXXXXXXX=                                                                            XXXXX =XXXXXXX=",
"=XXX=======                                                                            XXXXX =XXXXXXX=",
"=XXX= XXXXX                                                                              =   =XXXXXXX=",
"=XXX= XXXXX                                                                                  =XXXXXXX=",
"=XXX=   =              XXXXX       XXXXX       XXXXX       XXXXX       XXXXX                 =XXXXXXX=",
"=XXX=                  =XXX=       =XXX=       =XXX=       =XXX=       =XXX=                 ====XXXX=",
"=XXX=                  =====       =====       =====       =====       =====                    =XXXX=",
"=====                                                                                           =XXXX=",
"                                                                                                =XXXX=",
"                                                                                                =XXXX=",
"                                                                                                =XXXX=",
"                                                                                                =XXXX=",
"                                                                                                =XXXX=",
"                       XXXXX       XXXXX       XXXXX       XXXXX       XXXXX                    ==XXX=",
"=====                  =XXX=       =XXX=       =XXX=       =XXX=       =XXX=                     =XXX=",
"=XXX=                  =====       =====       =====       =====       =====               XXXXX =XXX=",
"=XXX====                                                                                   XXXXX =XXX=",
"=XXXXXX=                                                                                     =   =XXX=",
"=XXXXXX= XXXXX                                                                       XXXXX XXXXX =XXX=",
"=XXXXXX= XXXXX                                                                       XXXXX XXXXX =XXX=",
"=XXXXXX=   =                                                                           =     =   =XXX=",
"=XXXXXX========== XXXXX                                                        XXXXX XXXXX =======XXX=",
"=XXXXXXXXXXXXXXX= XXXXX                                                        XXXXX XXXXX =XXXXXXXXX=",
"=XXXXXXXXXXXXXXX=   =                                                            =     =   =XXXXXXXXX=",
"=XXXXXXXXXXXXXXX=============                                                  =============XXXXXXXXX=",
"=XXXXXXXXXXXXXXXXXXXXXXXXXXX=                                                  =XXXXXXXXXXXXXXXXXXXXX=",
"=============================                                                  =======================",
];
    public string[] FlagAdjusted => Raw;
    public List<EntityLocation> EntityLocations { get; } = [];
    public void Load() { }
}
