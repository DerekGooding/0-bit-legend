using _0_Bit_Legend.Entities;
using _0_Bit_Legend.Maps;
using _0_Bit_Legend.Model;
using System.Reflection;

namespace _0_bit_legend.Tests;

[TestClass]
public sealed class MapTests
{
    public static IEnumerable<object?[]> AllImplementations()
    {
        var interfaceType = typeof(IMap);

        var types = AppDomain.CurrentDomain
            .GetAssemblies()
            .SelectMany(a =>
            {
                try
                {
                    return a.GetTypes();
                }
                catch (ReflectionTypeLoadException e)
                {
                    return e.Types.Where(t => t is not null)!;
                }
            })
            .Where(t =>
                interfaceType.IsAssignableFrom(t) &&
                t is { IsInterface: false, IsAbstract: false });

        foreach (var type in types)
        {
            yield return [type];
        }
    }

    [TestMethod]
    [DynamicData(nameof(AllImplementations))]
    public void Map_Integrety(Type implementationType)
    {
        var instance = (IMap)Activator.CreateInstance(implementationType)!;
        var width = instance.Raw[0].Length;
        var height = instance.Raw.Length;
        var incoherantLine = instance.Raw.FirstOrDefault(x => x.Length != width);

        var index = instance.Raw.IndexOf(incoherantLine ?? instance.Raw[0]);

        var isCoherant = incoherantLine is null;
        var isWidth = width == 102;
        var isHeight = height == 33;

        Assert.IsTrue(isCoherant, $"{implementationType.Name} does not have equal width on every line. @ line {index}");
        Assert.IsTrue(isWidth, $"{implementationType.Name} does not have proper width. Width: {width} instead of 102");
        Assert.IsTrue(isHeight, $"{implementationType.Name} does not have proper height. Height: {height} instead of 33");
    }

    [TestMethod]
    [DynamicData(nameof(AllImplementations))]
    public void Map_EntityPlacement(Type implementationType)
    {
        var instance = (IMap)Activator.CreateInstance(implementationType)!;
        var map = instance.Raw;

        foreach(var entityLocation in instance.EntityLocations)
        {
            var entity = (IEntity?)Activator.CreateInstance(entityLocation.EntityType);
            if (entity is not ICollider collider)
                return;
            foreach (var point in PointsFromSize(collider.Position, collider.Size))
            {
                Assert.IsFalse(CollidsInMap(point, map));
            }
        }


    }

    [TestMethod]
    [DynamicData(nameof(AllImplementations))]
    public void Map_Transitions(Type implementationType)
    {
        var instance = (IMap)Activator.CreateInstance(implementationType)!;
        var map = instance.Raw;

        foreach (var areaInfo in instance.AreaTransitions)
        {
            foreach(var point in PointsFromSize(areaInfo.Position, areaInfo.Size))
            {
                Assert.IsFalse(CollidsInMap(point, map));
            }
        }
    }

    private static bool CollidsInMap(Vector2 point, string[] map) => map[point.X][point.Y] == ' ';

    private List<Vector2> PointsFromSize(Vector2 Position, Vector2 Size)
    {
        List<Vector2> points = [];
        for(var x = 0; x < Size.X; x++)
        {
            for (var y = 0; y < Size.Y; y++)
                points.Add(Position.Offset(x, y));
        }

        return points;
    }
}
