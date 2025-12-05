using _0_Bit_Legend.Entities;
using _0_Bit_Legend.Model.Enums;
using System.Reflection;

namespace _0_bit_legend.Tests;

[TestClass]
public sealed class SpriteSheetTests
{
    public static IEnumerable<object?[]> AllImplementations()
    {
        var interfaceType = typeof(ICollider);

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
    public void SpirtSheet_Integrety(Type implementationType)
    {
        var instance = (ICollider)Activator.CreateInstance(implementationType)!;

        var type = instance.GetType();
        var spriteSheet = type.GetField("_spriteSheet", BindingFlags.Instance | BindingFlags.NonPublic);

        if (spriteSheet is null)
        {
            Assert.IsNotNull(spriteSheet);
            return;
        }
        var expectedLineCount = instance.Size.Y + 1;
        var expectedLineLength = instance.Size.X + 1;

        if (spriteSheet.FieldType == typeof(Dictionary<DirectionType, string[]>))
        {
            var value = (Dictionary<DirectionType, string[]>)spriteSheet.GetValue(instance)!;
            foreach(var dir in value)
            {
                var sheet = dir.Value;

                var resultLineCount = sheet.Length;

                Assert.AreEqual(expectedLineCount, resultLineCount);
                foreach (var line in sheet)
                    Assert.AreEqual(expectedLineLength, line.Length);
            }
        }
        else if(spriteSheet.FieldType == typeof(string[]))
        {
            var value = (string[])spriteSheet.GetValue(instance)!;
            var resultLineCount = value.Length;

            Assert.AreEqual(expectedLineCount, resultLineCount);
            foreach(var line in value)
                Assert.AreEqual(expectedLineLength, line.Length);
        }
    }
}
