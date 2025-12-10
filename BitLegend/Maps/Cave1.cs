using BitLegend.Entities.Pickups;

namespace BitLegend.Maps;

public class Cave1 : BaseMap
{
    public override string Name => "Cave1";
    public override string[] Raw =>[
 "======================================================================================================",
 "=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=",
 "=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=",
 "=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=",
 "=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=",
 "=XXXXXXXXXX================================================================================XXXXXXXXXX=",
 "=XXXXXXXXXX=                                                                              =XXXXXXXXXX=",
 "=XXXXXXXXXX=                                                                              =XXXXXXXXXX=",
 "=XXXXXXXXXX=                         BUY   SOMETHIN'   WILL   YA!                         =XXXXXXXXXX=",
 "=XXXXXXXXXX=                                                                              =XXXXXXXXXX=",
 "=XXXXXXXXXX=                                                                              =XXXXXXXXXX=",
 "=XXXXXXXXXX=                                   xxxxxxx                                    =XXXXXXXXXX=",
 "=XXXXXXXXXX=                                   x^ x ^x                                    =XXXXXXXXXX=",
@"=XXXXXXXXXX=                                   /**=**\                                    =XXXXXXXXXX=",
 "=XXXXXXXXXX=                                  |_|***|_|                                   =XXXXXXXXXX=",
 "=XXXXXXXXXX=                                   |__|__|                                    =XXXXXXXXXX=",
 "=XXXXXXXXXx=                                                                              =XXXXXXXXXX=",
 "=XXXXXXXXXX=                 =====                                                        =XXXXXXXXXX=",
 "=XXXXXXXXXX=                 *****            =======               ## ##                 =XXXXXXXXXX=",
 "=XXXXXXXXXX=                 =====            ==  = =               #####                 =XXXXXXXXXX=",
 "=XXXXXXXXXX=                 *****                                   ###                  =XXXXXXXXXX=",
 "=XXXXXXXXXX=                 RAFT              KEY                  ARMOR                 =XXXXXXXXXX=",
 "=XXXXXXXXXX=                 35                10                   25                    =XXXXXXXXXX=",
 "=XXXXXXXXXX=     r                                                                        =XXXXXXXXXX=",
 "=XXXXXXXXXX=    RRR                                                                       =XXXXXXXXXX=",
 "=XXXXXXXXXX=     r (X)                                                                    =XXXXXXXXXX=",
 "=XXXXXXXXXX=                                                                              =XXXXXXXXXX=",
 "=XXXXXXXXXX========================                                ========================XXXXXXXXXX=",
 "=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=                                =XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=",
 "=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=                                =XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=",
 "=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=                                =XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=",
 "=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=                                =XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=",
 "===================================                                ===================================",
];


    public override List<EntityLocation> EntityLocations { get; } =
    [
        new(typeof(Raft), new(X: 30, Y: 30), () => !HasFlag(GameFlag.HasRaft)),
        new(typeof(Key), new(X: 50, Y: 30), () => true),
        new(typeof(Armor), new(X: 70, Y: 30), () => !HasFlag(GameFlag.HasArmor)),
    ];

    public override List<NewAreaInfo> AreaTransitions { get; } = [];
}
