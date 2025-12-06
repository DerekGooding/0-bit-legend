using _0_Bit_Legend.Entities.Pickups;

namespace _0_Bit_Legend.Maps;

public class Cave0 : IMap
{
    public string Name => "Cave 0";
    public string[] Raw =>[
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
 "=XXXXXXXXXX=                                      S                                       =XXXXXXXXXX=",
 "=XXXXXXXXXX=                                      S                                       =XXXXXXXXXX=",
 "=XXXXXXXXXX=                                     ---                                      =XXXXXXXXXX=",
 "=XXXXXXXXXX=                                      -                                       =XXXXXXXXXX=",
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
    public string[] FlagAdjusted => Raw;

    public List<EntityLocation> EntityLocations { get; } =
    [
        new(typeof(Sword), new(X: 30, Y: 30), () => !HasFlag(GameFlag.HasSword)),
    ];

    public void Load() { }
}
