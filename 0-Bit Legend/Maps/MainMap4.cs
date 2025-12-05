using _0_Bit_Legend.Entities.Triggers;

namespace _0_Bit_Legend.Maps;

public class MainMap4 : IMap
{
    public string Name => "Main Map 4";
    public string[] Raw =>[
"======================================================================================================#",
"=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#",
"=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#",
"=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#",
"=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#",
"=XXXXXXXXXXXXXXXXXXXXXXXXXXXX====================================================XX       XXXXXXXXXXX=#",
"=XXXXXXXXX====================                                                  =XX       XXXXXXXXXXX=#",
"=XXXXXXXXX=   ~~~~~~~~~~~~~~                                                    ===       =XXXXXXXXXX=#",
"=XXXXXXXXX=   ~~~~~~~~~~~~~~                                                              =XXXXXXXXXX=#",
"===========   ~~~~~~~~~~~~~~                                                              ============#",
"              ~~~~~~~~~~~~~~                                                                          #",
"              ~~~~~~~~~~~~~~           XXXXX         XXXXX         XXXXX                              #",
"              ~~~~~~~~~~~~~~           =XXX=         =XXX=         =XXX=                              #",
"              ~~~~~~~~~~~~~~           =====         =====         =====                              #",
"              ~~~~~~~~~~~~~~                                                                          #",
"              ~~~~~~~~~~~~~~                                                                          #",
"              ~~~~~~~~~~~~~~                                                                          #",
"              ~~~~~~~~~~~~~~                                                                          #",
"              ~~~~~~~~~~~~~~                                                                          #",
"              ~~~~~~~~~~~~~~           XXXXX         XXXXX         XXXXX                       =======#",
"              ~~~~~~~~~~~~~~           =XXX=         =XXX=         =XXX=                       =XXXXX=#",
"              ~~~~~~~~~~~~~~           =====         =====         =====                       =XXXXX=#",
"              ~~~~~~~~~~~~~~                                                                   =XXXXX=#",
"              ~~~~~~~~~~~~~~                                                               =====XXXXX=#",
"              ~~~~~~~~~~~~~~                                                               =XXXXXXXXX=#",
"              ~~~~~~~~~~~~~~                                                               =XXXXXXXXX=#",
"=====         ~~~~~~~~~~~~~~                                                         =======XXXXXXXXX=#",
"=XXX=         ~~~~~~~~~~~~~~ =====                                                   =XXXXXXXXXXXXXXX=#",
"=XXX=         ~~~~~~~~~~~~~~ =XXX=                                 ===================XXXXXXXXXXXXXXX=#",
"=XXX=         ~~~~~~~~~~~~~~ =XXX==========                        =XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#",
"=XXX=         ~~~~~~~~~~~~~~ =XXXXXXXXXXXX=                      ===XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#",
"=XXX=         ~~~~~~~~~~~~~~ =XXXXXXXXXXXX=                      =XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#",
"=====         ~~~~~~~~~~~~~~ ==============                      =====================================#",
];
    public List<EntityLocation> EntityLocations { get; } =
    [
        new(typeof(EnterCave1), new(84,4), () => true),
    ];
    public void Load() { }
}
