using _0_Bit_Legend.Entities.Pickups;

namespace _0_Bit_Legend.Maps;

public class Cave0 : BaseMap
{
    public override string Name => "Cave 0";
    public override string[] Raw =>[
 "======================================================================================================",
 "=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=",
 "=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=",
 "=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=",
 "=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=",
 "=XXXXXXXXXX================================================================================XXXXXXXXXX=",
 "=XXXXXXXXXX=                                                                              =XXXXXXXXXX=",
 "=XXXXXXXXXX=                                                                              =XXXXXXXXXX=",
 "=XXXXXXXXXX=                     IT'S   DANGEROUS   TO   GO   ALONE!                      =XXXXXXXXXX=",
 "=XXXXXXXXXX=                                TAKE   THIS.                                  =XXXXXXXXXX=",
 "=XXXXXXXXXX=                                                                              =XXXXXXXXXX=",
 "=XXXXXXXXXX=                                   xxxxxxx                                    =XXXXXXXXXX=",
 "=XXXXXXXXXX=              //////               x^ x ^x                //////              =XXXXXXXXXX=",
@"=XXXXXXXXXX=              //////               /**=**\                //////              =XXXXXXXXXX=",
 "=XXXXXXXXXX=              ======              |_|***|_|               ======              =XXXXXXXXXX=",
 "=XXXXXXXXXX=                                   |__|__|                                    =XXXXXXXXXX=",
 "=XXXXXXXXXx=                                                                              =XXXXXXXXXX=",
 "=XXXXXXXXXX=                                                                              =XXXXXXXXXX=",
 "=XXXXXXXXXX=                                                                              =XXXXXXXXXX=",
 "=XXXXXXXXXX=                                                                              =XXXXXXXXXX=",
 "=XXXXXXXXXX=                                                                              =XXXXXXXXXX=",
 "=XXXXXXXXXX=                                                                              =XXXXXXXXXX=",
 "=XXXXXXXXXX=                                                                              =XXXXXXXXXX=",
 "=XXXXXXXXXX=                                                                              =XXXXXXXXXX=",
 "=XXXXXXXXXX=                                                                              =XXXXXXXXXX=",
 "=XXXXXXXXXX=                                                                              =XXXXXXXXXX=",
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
        new(typeof(Sword), new(X: 49, Y: 19), () => !HasFlag(GameFlag.HasSword)),
    ];

}
