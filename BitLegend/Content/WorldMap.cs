using BitLegend.Maps;

namespace BitLegend.Content;

public static class WorldMap
{
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

    public static IMap GetMap(MapName mapName) => Maps[(int)mapName];

    public static MapName Transition(MapName index, DirectionType direction)
    {
        var grid = _gridLookup[index];

        var rows = grid.GetLength(0);
        var cols = grid.GetLength(1);

        for (var y = 0; y < rows; y++)
        {
            for (var x = 0; x < cols; x++)
            {
                if (grid[y, x] != index)
                    continue;

                var nx = x;
                var ny = y;

                switch (direction)
                {
                    case DirectionType.Up:
                        ny--;
                        break;
                    case DirectionType.Down:
                        ny++;
                        break;
                    case DirectionType.Left:
                        nx--;
                        break;
                    case DirectionType.Right:
                        nx++;
                        break;
                }

                return nx < 0 || ny < 0 || nx >= cols || ny >= rows ? MapName.XXXXXXXX : grid[ny, nx];
            }
        }

        return MapName.XXXXXXXX;
    }

    public static MapName ParTransition(MapName index)
    {
        foreach((var item1, var item2) in _entrancePairs)
        {
            if (item1 == index)
                return item2;
            if (item2 == index)
                return item1;
        }
        return MapName.XXXXXXXX;
    }

    public enum MapName
    {
        XXXXXXXX = -1, //No map tile
        MainMap0,
        MainMap1,
        MainMap2,
        MainMap3,
        MainMap4,
        MainMap5,
        Cave0,
        Cave1,
        Castle0,
        Castle1,
        Castle2,
        Castle3,
        Castle4,
        Castle5,
    }

    //Enters at MainMap0
    private readonly static MapName[,] _mainMap =
    {
        { MapName.Castle0 , MapName.MainMap4, MapName.MainMap1, MapName.MainMap5 },
        { MapName.XXXXXXXX, MapName.MainMap2, MapName.MainMap0, MapName.MainMap3 }
    };

    //Enters at Castle1
    private readonly static MapName[,] _castle =
    {
        { MapName.XXXXXXXX, MapName.Castle4, MapName.Castle5  },
        { MapName.Castle2 , MapName.Castle1, MapName.Castle3  }
    };

    //Entrance pair
    private readonly static List<(MapName, MapName)> _entrancePairs =
    [
            (MapName.MainMap0, MapName.Cave0 ),
            (MapName.MainMap4, MapName.Cave1 ),
            (MapName.Castle0, MapName.Castle1 ),
    ];

    private static readonly Dictionary<MapName, MapName[,]> _gridLookup = new()
    {

        { MapName.Castle1, _castle },
        { MapName.Castle2, _castle },
        { MapName.Castle3, _castle },
        { MapName.Castle4, _castle },
        { MapName.Castle5, _castle },

        { MapName.MainMap0, _mainMap },
        { MapName.MainMap1, _mainMap },
        { MapName.MainMap2, _mainMap },
        { MapName.MainMap3, _mainMap },
        { MapName.MainMap4, _mainMap },
        { MapName.MainMap5, _mainMap },
        { MapName.Castle0, _mainMap },
    };
}