using _0_Bit_Legend.Maps;

namespace _0_Bit_Legend.Content;

public static class WorldMap
{
    //Enters at MainMap0
    public readonly static string[,] MainMap =
    {
        { "Castle0", "MainMap4", "MainMap1", "MainMap5" },
        { "XXXXXXX", "MainMap2", "MainMap0", "MainMap3"}
    };

    //Enters at Castle1
    public readonly static string[,] Castle =
    {
        { "XXXXXXX", "Castle4", "Castle5"  },
        { "Castle2", "Castle1", "Castle3"  }
    };


    public readonly static IMap[] Maps =
    [
        new MainMap0(),
        new MainMap1(),
        new MainMap2(),
        new MainMap3(),
        new MainMap4(),
        new MainMap5(),
        new Cave0(),
        new Cave1(),
        new Castle0(),
        new Castle1(),
        new Castle2(),
        new Castle3(),
        new Castle4(),
        new Castle5(),
    ];

}
