using _0_Bit_Legend;
using _0_Bit_Legend.Maps;
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
        var isWidth = width == 103;
        var isHeight = height == 33;

        Assert.IsTrue(isCoherant, $"{implementationType.Name} does not have equal width on every line. @ line {index}");
        Assert.IsTrue(isWidth, $"{implementationType.Name} does not have proper width. Width: {width} instead of 103");
        Assert.IsTrue(isHeight, $"{implementationType.Name} does not have proper height. Height: {height} instead of 33");
    }

    [TestMethod]
    [DynamicData(nameof(AllImplementations))]
    public void Map_Loads(Type implementationType)
    {
        var instance = (IMap)Activator.CreateInstance(implementationType)!;
        var width = instance.Raw[0].Length;
        var height = instance.Raw.Length;
        var incoherantLine = instance.Raw.FirstOrDefault(x => x.Length != width);
        MainProgram.SetMapZero(instance);


        using var _ = new ConsoleSilencer();


        MainProgram.LoadMap(0, new(50, 29), _0_Bit_Legend.Model.Enums.DirectionType.Up);
    }


    public sealed class ConsoleSilencer : IDisposable
    {
        private readonly TextWriter _original;

        public ConsoleSilencer()
        {
            _original = Console.Out;
            Console.SetOut(TextWriter.Null);
        }

        public void Dispose() => Console.SetOut(_original);
    }
}
