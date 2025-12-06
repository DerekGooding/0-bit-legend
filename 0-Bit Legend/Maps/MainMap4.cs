using _0_Bit_Legend.Entities.Triggers;

namespace _0_Bit_Legend.Maps;

public class MainMap4 : BaseMap
{
    public override string Name => "Main Map 4";
    public override string[] Raw =>[
"======================================================================================================",
"=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=",
"=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=",
"=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=",
"=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=",
"=XXXXXXXXXXXXXXXXXXXXXXXXXXXX====================================================XX       XXXXXXXXXXX=",
"=XXXXXXXXX====================                                                  =XX       XXXXXXXXXXX=",
"=XXXXXXXXX=   ~~~~~~~~~~~~~~                                                    ===       =XXXXXXXXXX=",
"=XXXXXXXXX=   ~~~~~~~~~~~~~~                                                              =XXXXXXXXXX=",
"===========   ~~~~~~~~~~~~~~                                                              ============",
"              ~~~~~~~~~~~~~~                                                                          ",
"              ~~~~~~~~~~~~~~           XXXXX         XXXXX         XXXXX                              ",
"              ~~~~~~~~~~~~~~           =XXX=         =XXX=         =XXX=                              ",
"              ~~~~~~~~~~~~~~           =====         =====         =====                              ",
"              ~~~~~~~~~~~~~~                                                                          ",
"              ~~~~~~~~~~~~~~                                                                          ",
"              ~~~~~~~~~~~~~~                                                                          ",
"              ~~~~~~~~~~~~~~                                                                          ",
"              ~~~~~~~~~~~~~~                                                                          ",
"              ~~~~~~~~~~~~~~           XXXXX         XXXXX         XXXXX                       =======",
"              ~~~~~~~~~~~~~~           =XXX=         =XXX=         =XXX=                       =XXXXX=",
"              ~~~~~~~~~~~~~~           =====         =====         =====                       =XXXXX=",
"              ~~~~~~~~~~~~~~                                                                   =XXXXX=",
"              ~~~~~~~~~~~~~~                                                               =====XXXXX=",
"              ~~~~~~~~~~~~~~                                                               =XXXXXXXXX=",
"              ~~~~~~~~~~~~~~                                                               =XXXXXXXXX=",
"=====         ~~~~~~~~~~~~~~                                                         =======XXXXXXXXX=",
"=XXX=         ~~~~~~~~~~~~~~ =====                                                   =XXXXXXXXXXXXXXX=",
"=XXX=         ~~~~~~~~~~~~~~ =XXX=                                 ===================XXXXXXXXXXXXXXX=",
"=XXX=         ~~~~~~~~~~~~~~ =XXX==========                        =XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=",
"=XXX=         ~~~~~~~~~~~~~~ =XXXXXXXXXXXX=                      ===XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=",
"=XXX=         ~~~~~~~~~~~~~~ =XXXXXXXXXXXX=                      =XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=",
"=====         ~~~~~~~~~~~~~~ ==============                      =====================================",
];
    public override List<EntityLocation> EntityLocations { get; } =
    [
        new(typeof(EnterCave1), new(84,4), () => true),
    ];
}
