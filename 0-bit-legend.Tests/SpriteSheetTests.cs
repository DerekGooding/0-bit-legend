using _0_Bit_Legend.Entities;
using _0_Bit_Legend.Model.Enums;
using System.Reflection;

namespace _0_bit_legend.Tests;

[TestClass]
public sealed class SpriteSheetTests
{
    public static IEnumerable<object?[]> AllImplementations()
    {
        var interfaceType = typeof(IBoundingBox);

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
        var instance = (IBoundingBox)Activator.CreateInstance(implementationType)!;

        var type = instance.GetType();
        var spriteSheet = type.GetField("_spriteSheet", BindingFlags.Instance | BindingFlags.NonPublic);

        if (spriteSheet is null)
        {
            Assert.IsNotNull(spriteSheet);
            return;
        }
        var expectedLineCount = BoundingY(instance);
        var expectedLineLength = BoundingX(instance);

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

    private int BoundingX(IBoundingBox boundingBox) => boundingBox.BoundingBox.BottomRight.X - boundingBox.BoundingBox.TopLeft.X + 1;
    private int BoundingY(IBoundingBox boundingBox) => boundingBox.BoundingBox.BottomRight.Y - boundingBox.BoundingBox.TopLeft.Y + 1;
}
