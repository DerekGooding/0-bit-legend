using BitLegend.MapEditor.Services;

namespace BitLegend.Tests;

[TestClass]
public class GameDataServiceTests
{
    private GameDataService _gameDataService;

    [TestInitialize]
    public void Setup()
    {
        _gameDataService = new GameDataService();
    }

    [TestMethod]
    public void LoadValidEntityTypes_PopulatesCorrectly()
    {
        Assert.IsNotNull(_gameDataService.ValidEntityTypes);
        Assert.IsTrue(_gameDataService.ValidEntityTypes.Any(), "ValidEntityTypes should not be empty.");
        Assert.IsNotNull(_gameDataService.EntityTypeToFullTypeName);
        Assert.IsTrue(_gameDataService.EntityTypeToFullTypeName.Any(), "EntityTypeToFullTypeName should not be empty.");

        // Check for some expected types (based on prior inspection)
        Assert.IsTrue(_gameDataService.ValidEntityTypes.Contains("Door"));
        Assert.IsTrue(_gameDataService.ValidEntityTypes.Contains("Octorok"));
        Assert.IsTrue(_gameDataService.ValidEntityTypes.Contains("Princess"));
        Assert.IsTrue(_gameDataService.ValidEntityTypes.Contains("Sword")); // Pickup
        Assert.IsTrue(_gameDataService.ValidEntityTypes.Contains("EnterCastle")); // Trigger

        // Check for full type names
        Assert.IsTrue(_gameDataService.EntityTypeToFullTypeName.ContainsKey("Octorok"));
        Assert.IsTrue(_gameDataService.EntityTypeToFullTypeName["Octorok"].Contains("BitLegend.Entities.Enemies.Octorok"));
        Assert.IsTrue(_gameDataService.EntityTypeToFullTypeName.ContainsKey("Princess"));
        Assert.IsTrue(_gameDataService.EntityTypeToFullTypeName["Princess"].Contains("BitLegend.Entities.Princess"));
    }

    [TestMethod]
    public void LoadValidEntityTypes_ContainsNoDuplicates()
    {
        var duplicates = _gameDataService.ValidEntityTypes.GroupBy(x => x)
                                         .Where(g => g.Count() > 1)
                                         .Select(y => y.Key)
                                         .ToList();
        Assert.IsFalse(duplicates.Any(), $"Duplicate entity types found: {string.Join(", ", duplicates)}");
    }

    [TestMethod]
    public void LoadValidMapIds_PopulatesCorrectly()
    {
        Assert.IsNotNull(_gameDataService.ValidMapIds);
        Assert.IsTrue(_gameDataService.ValidMapIds.Any(), "ValidMapIds should not be empty.");

        // Check for some expected map IDs
        Assert.IsTrue(_gameDataService.ValidMapIds.Contains("MainMap0"));
        Assert.IsTrue(_gameDataService.ValidMapIds.Contains("Castle0"));
        Assert.IsTrue(_gameDataService.ValidMapIds.Contains("Cave1"));
    }

    [TestMethod]
    public void LoadValidMapIds_ContainsNoDuplicates()
    {
        var duplicates = _gameDataService.ValidMapIds.GroupBy(x => x)
                                        .Where(g => g.Count() > 1)
                                        .Select(y => y.Key)
                                        .ToList();
        Assert.IsFalse(duplicates.Any(), $"Duplicate map IDs found: {string.Join(", ", duplicates)}");
    }

    [TestMethod]
    public void LoadValidDirectionTypes_PopulatesCorrectly()
    {
        Assert.IsNotNull(_gameDataService.ValidDirectionTypes);
        Assert.AreEqual(4, _gameDataService.ValidDirectionTypes.Count); // Up, Down, Left, Right
        Assert.IsTrue(_gameDataService.ValidDirectionTypes.Contains("Up"));
        Assert.IsTrue(_gameDataService.ValidDirectionTypes.Contains("Down"));
        Assert.IsTrue(_gameDataService.ValidDirectionTypes.Contains("Left"));
        Assert.IsTrue(_gameDataService.ValidDirectionTypes.Contains("Right"));
    }

    [TestMethod]
    public void LoadValidDirectionTypes_ContainsNoDuplicates()
    {
        var duplicates = _gameDataService.ValidDirectionTypes.GroupBy(x => x)
                                            .Where(g => g.Count() > 1)
                                            .Select(y => y.Key)
                                            .ToList();
        Assert.IsFalse(duplicates.Any(), $"Duplicate direction types found: {string.Join(", ", duplicates)}");
    }

    [TestMethod]
    public void LoadValidGameFlags_PopulatesCorrectly()
    {
        Assert.IsNotNull(_gameDataService.ValidGameFlags);
        Assert.IsTrue(_gameDataService.ValidGameFlags.Any(), "ValidGameFlags should not be empty.");
        Assert.IsFalse(_gameDataService.ValidGameFlags.Contains("None"), "ValidGameFlags should not contain 'None'.");

        // Check for some expected game flags
        Assert.IsTrue(_gameDataService.ValidGameFlags.Contains("HasSword"));
        Assert.IsTrue(_gameDataService.ValidGameFlags.Contains("Door1"));
        Assert.IsTrue(_gameDataService.ValidGameFlags.Contains("Dragon"));
    }

    [TestMethod]
    public void LoadValidGameFlags_ContainsNoDuplicates()
    {
        var duplicates = _gameDataService.ValidGameFlags.GroupBy(x => x)
                                         .Where(g => g.Count() > 1)
                                         .Select(y => y.Key)
                                         .ToList();
        Assert.IsFalse(duplicates.Any(), $"Duplicate game flags found: {string.Join(", ", duplicates)}");
    }
}
