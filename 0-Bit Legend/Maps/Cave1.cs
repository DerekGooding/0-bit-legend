using _0_Bit_Legend.Entities.Pickups;

namespace _0_Bit_Legend.Maps;

public class Cave1 : IMap
{
    public string Name => "Cave 1";
    public string[] Raw =>[
 "======================================================================================================#",
 "=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#",
 "=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#",
 "=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#",
 "=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#",
 "=XXXXXXXXXX================================================================================XXXXXXXXXX=#",
 "=XXXXXXXXXX=                                                                              =XXXXXXXXXX=#",
 "=XXXXXXXXXX=                                                                              =XXXXXXXXXX=#",
 "=XXXXXXXXXX=                         BUY   SOMETHIN'   WILL   YA!                         =XXXXXXXXXX=#",
 "=XXXXXXXXXX=                                                                              =XXXXXXXXXX=#",
 "=XXXXXXXXXX=                                                                              =XXXXXXXXXX=#",
 "=XXXXXXXXXX=                                   xxxxxxx                                    =XXXXXXXXXX=#",
 "=XXXXXXXXXX=                                   x^ x ^x                                    =XXXXXXXXXX=#",
@"=XXXXXXXXXX=                                   /**=**\                                    =XXXXXXXXXX=#",
 "=XXXXXXXXXX=                                  |_|***|_|                                   =XXXXXXXXXX=#",
 "=XXXXXXXXXX=                                   |__|__|                                    =XXXXXXXXXX=#",
 "=XXXXXXXXXx=                                                                              =XXXXXXXXXX=#",
 "=XXXXXXXXXX=                 =====                                                        =XXXXXXXXXX=#",
 "=XXXXXXXXXX=                 *****            =======               ## ##                 =XXXXXXXXXX=#",
 "=XXXXXXXXXX=                 =====            ==  = =               #####                 =XXXXXXXXXX=#",
 "=XXXXXXXXXX=                 *****                                   ###                  =XXXXXXXXXX=#",
 "=XXXXXXXXXX=                 RAFT              KEY                  ARMOR                 =XXXXXXXXXX=#",
 "=XXXXXXXXXX=                 35                10                   25                    =XXXXXXXXXX=#",
 "=XXXXXXXXXX=     r                                                                        =XXXXXXXXXX=#",
 "=XXXXXXXXXX=    RRR                                                                       =XXXXXXXXXX=#",
 "=XXXXXXXXXX=     r (X)                                                                    =XXXXXXXXXX=#",
 "=XXXXXXXXXX=                                                                              =XXXXXXXXXX=#",
 "=XXXXXXXXXX========================                                ========================XXXXXXXXXX=#",
 "=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=                                =XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#",
 "=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=                                =XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#",
 "=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=                                =XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#",
 "=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=                                =XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#",
 "===================================                                ===================================#",
];
    public string[] FlagAdjusted => Raw;

    public List<EntityLocation> EntityLocations { get; } =
    [
        new(typeof(Raft), new(x: 30, y: 30), () => !HasFlag(GameFlag.HasRaft)),
        new(typeof(Key), new(x: 50, y: 30), () => true),
        new(typeof(Armor), new(x: 70, y: 30), () => !HasFlag(GameFlag.HasArmor)),
    ];

    public void Load() { }
}
