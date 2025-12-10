using BitLegend.MapEditor.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;

namespace BitLegend.Tests
{
    [TestClass]
    public class MapFileParserServiceTests
    {
        private MethodInfo _parseMapFileMethod;

        public MapFileParserServiceTests()
        {
            _parseMapFileMethod = typeof(MapFileParserService).GetMethod("ParseMapFile", BindingFlags.NonPublic | BindingFlags.Static);
        }

        [TestMethod]
        public void ParseMapFile_WithValidContent_ShouldParseCorrectly()
        {
            // Arrange
            var mockMapContent = @"
public class MainMap0 : BaseMap
{
    public override string Name => ""Main Map 0"";
    public override string[] Raw => [
""======================================================"",
""=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX="",
];

    public override List<EntityLocation> EntityLocations { get; } =
    [
        new(typeof(EnterCave0), new(13,4), () => true),
    ];

    public override List<NewAreaInfo> AreaTransitions { get; } =
    [
        new(MapId: WorldMap.MapName.MainMap2, StartPosition: new(52, 18),
            DirectionType.Left, Size: new(3, 10), Position: new(0, 9)),
        new(MapId: WorldMap.MapName.MainMap1, StartPosition: new(52, 18),
            DirectionType.Up,   Size: new(21, 1), Position: new(53, 0)),
    ];
}
";

            // Act
            var mapData = _parseMapFileMethod.Invoke(null, [mockMapContent]) as BitLegend.MapEditor.Model.MapData;

            // Assert
            Assert.IsNotNull(mapData);
            Assert.AreEqual("Main Map 0", mapData.Name);
            Assert.AreEqual(2, mapData.Raw.Count);
            Assert.AreEqual("======================================================", mapData.Raw[0]);
            Assert.AreEqual(1, mapData.EntityLocations.Count);
            Assert.AreEqual("EnterCave0", mapData.EntityLocations[0].EntityType);
            Assert.AreEqual(13, mapData.EntityLocations[0].X);
            Assert.AreEqual(4, mapData.EntityLocations[0].Y);
            Assert.AreEqual("() => true", mapData.EntityLocations[0].Condition);
            Assert.AreEqual(2, mapData.AreaTransitions.Count);
            Assert.AreEqual("MainMap2", mapData.AreaTransitions[0].MapId);
            Assert.AreEqual(52, mapData.AreaTransitions[0].StartPositionX);
            Assert.AreEqual(18, mapData.AreaTransitions[0].StartPositionY);
            Assert.AreEqual("Left", mapData.AreaTransitions[0].DirectionType);
        }

        [TestMethod]
        public void ParseMapFile_WithNoEntities_ShouldParseCorrectly()
        {
            // Arrange
            var mockMapContent = @"
public class MainMap1 : BaseMap
{
    public override string Name => ""Main Map 1"";
    public override string[] Raw => [ ""line1"", ""line2"" ];

    public override List<EntityLocation> EntityLocations { get; } = new();

    public override List<NewAreaInfo> AreaTransitions { get; } =
    [
        new(MapId: WorldMap.MapName.MainMap2, StartPosition: new(1, 1),
            DirectionType.Right, Size: new(1, 1), Position: new(1, 1)),
    ];
}
";

            // Act
            var mapData = _parseMapFileMethod.Invoke(null, [mockMapContent]) as BitLegend.MapEditor.Model.MapData;

            // Assert
            Assert.IsNotNull(mapData);
            Assert.AreEqual("Main Map 1", mapData.Name);
            Assert.AreEqual(2, mapData.Raw.Count);
            Assert.AreEqual(0, mapData.EntityLocations.Count);
            Assert.AreEqual(1, mapData.AreaTransitions.Count);
        }

        [TestMethod]
        public void ParseMapFile_WithNoTransitions_ShouldParseCorrectly()
        {
            // Arrange
            var mockMapContent = @"
public class MainMap2 : BaseMap
{
    public override string Name => ""Main Map 2"";
    public override string[] Raw => [ ""line1"" ];

    public override List<EntityLocation> EntityLocations { get; } =
    [
        new(typeof(SomeEntity), new(10, 20), () => false),
    ];

    public override List<NewAreaInfo> AreaTransitions { get; } = new();
}
";

            // Act
            var mapData = _parseMapFileMethod.Invoke(null, [mockMapContent]) as BitLegend.MapEditor.Model.MapData;

            // Assert
            Assert.IsNotNull(mapData);
            Assert.AreEqual("Main Map 2", mapData.Name);
            Assert.AreEqual(1, mapData.Raw.Count);
            Assert.AreEqual(1, mapData.EntityLocations.Count);
            Assert.AreEqual(0, mapData.AreaTransitions.Count);
        }

        [TestMethod]
        public void ParseMapFile_WithNoEntitiesAndNoTransitions_ShouldParseCorrectly()
        {
            // Arrange
            var mockMapContent = @"
public class MainMap3 : BaseMap
{
    public override string Name => ""Main Map 3"";
    public override string[] Raw => [ ""only line"" ];

    public override List<EntityLocation> EntityLocations { get; } = new();
    public override List<NewAreaInfo> AreaTransitions { get; } = new();
}
";
            
            // Act
            var mapData = _parseMapFileMethod.Invoke(null, [mockMapContent]) as BitLegend.MapEditor.Model.MapData;

            // Assert
            Assert.IsNotNull(mapData);
            Assert.AreEqual("Main Map 3", mapData.Name);
            Assert.AreEqual(1, mapData.Raw.Count);
            Assert.AreEqual(0, mapData.EntityLocations.Count);
            Assert.AreEqual(0, mapData.AreaTransitions.Count);
        }

        [TestMethod]
        public void ParseMapFile_InvalidContent_ShouldReturnNull()
        {
            // Arrange
            var invalidContent = @"
// This file is missing the Name property.
public class InvalidMap : BaseMap
{
    public override string[] Raw => [ ""data"" ];
}
";

            // Act
            var mapData = _parseMapFileMethod.Invoke(null, [invalidContent]);

            // Assert
            Assert.IsNull(mapData);
        }
    }
}
