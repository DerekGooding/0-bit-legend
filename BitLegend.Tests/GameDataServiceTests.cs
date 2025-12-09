using BitLegend.MapEditor.Services;

namespace BitLegend.Tests;

[TestClass]
public class GameDataServiceTests
{
    private GameDataService? _gameDataService;

    [TestInitialize]
    public void Setup() => _gameDataService = new GameDataService();

    [TestMethod]
    public void LoadValidEntityTypes_PopulatesCorrectly()
    {
        Assert.IsNotNull(_gameDataService.ValidEntityTypes);
        Assert.IsNotEmpty(_gameDataService.ValidEntityTypes, "ValidEntityTypes should not be empty.");
        Assert.IsNotNull(_gameDataService.EntityTypeToFullTypeName);
        Assert.IsNotEmpty(_gameDataService.EntityTypeToFullTypeName, "EntityTypeToFullTypeName should not be empty.");

        // Check for some expected types (based on prior inspection)
        Assert.Contains("Door", _gameDataService.ValidEntityTypes);
        Assert.Contains("Octorok", _gameDataService.ValidEntityTypes);
        Assert.Contains("Princess", _gameDataService.ValidEntityTypes);
        Assert.Contains("Sword", _gameDataService.ValidEntityTypes); // Pickup
        Assert.Contains("EnterCastle", _gameDataService.ValidEntityTypes); // Trigger

        // Check for full type names
        Assert.IsTrue(_gameDataService.EntityTypeToFullTypeName.ContainsKey("Octorok"));
        Assert.Contains("BitLegend.Entities.Enemies.Octorok", _gameDataService.EntityTypeToFullTypeName["Octorok"]);
        Assert.IsTrue(_gameDataService.EntityTypeToFullTypeName.ContainsKey("Princess"));
        Assert.Contains("BitLegend.Entities.Princess", _gameDataService.EntityTypeToFullTypeName["Princess"]);
    }

    [TestMethod]
    public void LoadValidEntityTypes_ContainsNoDuplicates()
    {
        var duplicates = _gameDataService.ValidEntityTypes.GroupBy(x => x)
                                         .Where(g => g.Count() > 1)
                                         .Select(y => y.Key)
                                         .ToList();
        Assert.IsEmpty(duplicates, $"Duplicate entity types found: {string.Join(", ", duplicates)}");
    }

    [TestMethod]
    public void LoadValidMapIds_PopulatesCorrectly()
    {
        Assert.IsNotNull(_gameDataService.ValidMapIds);
        Assert.IsNotEmpty(_gameDataService.ValidMapIds, "ValidMapIds should not be empty.");

        // Check for some expected map IDs
        Assert.Contains("MainMap0", _gameDataService.ValidMapIds);
        Assert.Contains("Castle0", _gameDataService.ValidMapIds);
        Assert.Contains("Cave1", _gameDataService.ValidMapIds);
    }

    [TestMethod]
    public void LoadValidMapIds_ContainsNoDuplicates()
    {
        var duplicates = _gameDataService.ValidMapIds.GroupBy(x => x)
                                        .Where(g => g.Count() > 1)
                                        .Select(y => y.Key)
                                        .ToList();
        Assert.IsEmpty(duplicates, $"Duplicate map IDs found: {string.Join(", ", duplicates)}");
    }

    [TestMethod]
    public void LoadValidDirectionTypes_PopulatesCorrectly()
    {
        Assert.IsNotNull(_gameDataService.ValidDirectionTypes);
        Assert.HasCount(4, _gameDataService.ValidDirectionTypes); // Up, Down, Left, Right
        Assert.Contains("Up", _gameDataService.ValidDirectionTypes);
        Assert.Contains("Down", _gameDataService.ValidDirectionTypes);
        Assert.Contains("Left", _gameDataService.ValidDirectionTypes);
        Assert.Contains("Right", _gameDataService.ValidDirectionTypes);
    }

    [TestMethod]
    public void LoadValidDirectionTypes_ContainsNoDuplicates()
    {
        var duplicates = _gameDataService.ValidDirectionTypes.GroupBy(x => x)
                                            .Where(g => g.Count() > 1)
                                            .Select(y => y.Key)
                                            .ToList();
        Assert.IsEmpty(duplicates, $"Duplicate direction types found: {string.Join(", ", duplicates)}");
    }

    [TestMethod]
    public void LoadValidGameFlags_PopulatesCorrectly()
    {
        Assert.IsNotNull(_gameDataService.ValidGameFlags);
        Assert.IsNotEmpty(_gameDataService.ValidGameFlags, "ValidGameFlags should not be empty.");
        Assert.DoesNotContain("None", _gameDataService.ValidGameFlags, "ValidGameFlags should not contain 'None'.");

        // Check for some expected game flags
        Assert.Contains("HasSword", _gameDataService.ValidGameFlags);
        Assert.Contains("Door1", _gameDataService.ValidGameFlags);
        Assert.Contains("Dragon", _gameDataService.ValidGameFlags);
    }

    [TestMethod]
    public void LoadValidGameFlags_ContainsNoDuplicates()
    {
        var duplicates = _gameDataService.ValidGameFlags.GroupBy(x => x)
                                         .Where(g => g.Count() > 1)
                                         .Select(y => y.Key)
                                         .ToList();
        Assert.IsEmpty(duplicates, $"Duplicate game flags found: {string.Join(", ", duplicates)}");
    }
}
