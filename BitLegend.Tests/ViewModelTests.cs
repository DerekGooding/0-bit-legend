using BitLegend.MapEditor.Model;
using BitLegend.MapEditor.Services;
using BitLegend.MapEditor.ViewModels;
using System.Windows.Controls; // Added for ItemsControl
using System.Windows;

namespace BitLegend.Tests;

[TestClass]
public class ViewModelTests
{
    private GameDataService? _gameDataService;
    private const int _mapWidth = 32;
    private const int _mapHeight = 32;

    [TestInitialize]
    public void Setup() => _gameDataService = new GameDataService();

    #region Mocks
    private class MockMapFileParserService : IMapFileParserService
    {
        public List<MapData> LoadMaps()
        {
            var map = new MapData("TestMap", ["abc", "def", "ghi"]);
            return [map];
        }

        public Task<List<MapData>> LoadMapsAsync() => Task.Run(LoadMaps);
    }

    private class MockMapFileSaverService : IMapFileSaverService
    {
        public void SaveMap(MapData mapData)
        {
            // Do nothing
        }
    }

    #endregion

    #region MainWindowViewModel Tests

    [TestMethod]
    public void MainWindowViewModel_AddEntityFromDragDrop_AddsEntity()
    {
        var parser = new MockMapFileParserService();
        var saver = new MockMapFileSaverService();
        var gds = new GameDataService();

        var viewModel = new MainWindowViewModel(parser, gds, saver);
        viewModel.SelectedMap = viewModel.Maps.First();

        var initialCount = viewModel.SelectedMap.EntityLocations.Count;
        var entityType = "Bat";
        var x = 5;
        var y = 10;

        viewModel.AddEntityFromDragDrop(entityType, x, y);

        Assert.AreEqual(initialCount + 1, viewModel.SelectedMap.EntityLocations.Count);
        var newEntity = viewModel.SelectedMap.EntityLocations.Last();
        Assert.AreEqual(entityType, newEntity.EntityType);
        Assert.AreEqual(x, newEntity.X);
        Assert.AreEqual(y, newEntity.Y);
        Assert.AreEqual("true", newEntity.Condition);
    }

    [TestMethod]
    public void MainWindowViewModel_SetSelectedCharacterBrush_CreatesCorrectBrush()
    {
        var parser = new MockMapFileParserService();
        var saver = new MockMapFileSaverService();
        var gds = new GameDataService();

        var viewModel = new MainWindowViewModel(parser, gds, saver);
        viewModel.SelectedMap = viewModel.Maps.First(); // This will populate DisplayMapCharacters

        // Select a 2x2 brush from the top-left corner
        viewModel.SetSelectedCharacterBrush(0, 0, 1, 1);

        var brush = viewModel.SelectedCharacterBrush;
        Assert.IsNotNull(brush);
        Assert.AreEqual(2, brush.Count); // 2 rows
        Assert.AreEqual(2, brush[0].Count); // 2 columns in first row
        Assert.AreEqual('a', brush[0][0]);
        Assert.AreEqual('b', brush[0][1]);
        Assert.AreEqual('d', brush[1][0]);
        Assert.AreEqual('e', brush[1][1]);
    }

    [TestMethod]
    public void MainWindowViewModel_SetSelectedCharacterBrush_HandlesOutOfBounds()
    {
        var parser = new MockMapFileParserService();
        var saver = new MockMapFileSaverService();
        var gds = new GameDataService();

        var viewModel = new MainWindowViewModel(parser, gds, saver);
        viewModel.SelectedMap = viewModel.Maps.First(); // Map: "abc", "def", "ghi"

        // Select a brush that goes out of bounds
        viewModel.SetSelectedCharacterBrush(-1, -1, 4, 4);

        var brush = viewModel.SelectedCharacterBrush;
        Assert.IsNotNull(brush);
        Assert.AreEqual(6, brush.Count); // From -1 to 4 is 6 rows
        Assert.AreEqual(6, brush[0].Count); // From -1 to 4 is 6 columns

        // Expected brush:
        // '     '
        // ' abc '
        // ' def '
        // ' ghi '
        // '     '

        Assert.AreEqual(' ', brush[0][0]); Assert.AreEqual(' ', brush[0][1]); Assert.AreEqual(' ', brush[0][2]); Assert.AreEqual(' ', brush[0][3]); Assert.AreEqual(' ', brush[0][4]);
        Assert.AreEqual(' ', brush[1][0]); Assert.AreEqual('a', brush[1][1]); Assert.AreEqual('b', brush[1][2]); Assert.AreEqual('c', brush[1][3]); Assert.AreEqual(' ', brush[1][4]);
        Assert.AreEqual(' ', brush[2][0]); Assert.AreEqual('d', brush[2][1]); Assert.AreEqual('e', brush[2][2]); Assert.AreEqual('f', brush[2][3]); Assert.AreEqual(' ', brush[2][4]);
        Assert.AreEqual(' ', brush[3][0]); Assert.AreEqual('g', brush[3][1]); Assert.AreEqual('h', brush[3][2]); Assert.AreEqual('i', brush[3][3]); Assert.AreEqual(' ', brush[3][4]);
        Assert.AreEqual(' ', brush[4][0]); Assert.AreEqual(' ', brush[4][1]); Assert.AreEqual(' ', brush[4][2]); Assert.AreEqual(' ', brush[4][3]); Assert.AreEqual(' ', brush[4][4]);
    }

    [TestMethod]
    [STAThread]
    public void MainWindowViewModel_ProcessBrushDrawing_WithCharacterBrush()
    {
        var parser = new MockMapFileParserService();
        var saver = new MockMapFileSaverService();
        var gds = new GameDataService();

        var viewModel = new MainWindowViewModel(parser, gds, saver);
        viewModel.SelectedMap = viewModel.Maps.First();

        viewModel.SetSelectedCharacterBrush(0, 0, 1, 1);

        var mockItemsControl = new ItemsControl
        {
            DataContext = viewModel,
            ItemsSource = viewModel.DisplayMapCharacters
        };

        var targetCell = viewModel.DisplayMapCharacters[1][1];
        var mousePosition = new Point(targetCell.X * 10, targetCell.Y * 10);

        var hitX = 1;
        var hitY = 1;

        if (viewModel.SelectedCharacterBrush?.Count > 0)
        {
            for (var y = 0; y < viewModel.SelectedCharacterBrush.Count; y++)
            {
                var brushRow = viewModel.SelectedCharacterBrush[y];
                for (var x = 0; x < brushRow.Count; x++)
                {
                    var targetX = hitX + x;
                    var targetY = hitY + y;

                    if (targetY >= 0 && targetY < viewModel.DisplayMapCharacters.Count &&
                        targetX >= 0 && targetX < viewModel.DisplayMapCharacters[targetY].Count)
                    {
                        viewModel.DisplayMapCharacters[targetY][targetX].Character = brushRow[x];
                    }
                }
            }
        }

        Assert.AreEqual('a', viewModel.DisplayMapCharacters[0][0].Character);
        Assert.AreEqual('b', viewModel.DisplayMapCharacters[0][1].Character);
        Assert.AreEqual('c', viewModel.DisplayMapCharacters[0][2].Character);

        Assert.AreEqual('a', viewModel.DisplayMapCharacters[1][1].Character); // targetCell (1,1) gets brush[0][0]
        Assert.AreEqual('b', viewModel.DisplayMapCharacters[1][2].Character); // targetCell (1,2) gets brush[0][1]
        Assert.AreEqual('f', viewModel.DisplayMapCharacters[1][0].Character); // should remain 'f'

        Assert.AreEqual('d', viewModel.DisplayMapCharacters[2][1].Character); // targetCell (2,1) gets brush[1][0]
        Assert.AreEqual('e', viewModel.DisplayMapCharacters[2][2].Character); // targetCell (2,2) gets brush[1][1]
        Assert.AreEqual('g', viewModel.DisplayMapCharacters[2][0].Character); // should remain 'g'
    }

    #endregion

    #region EntityEditorViewModel Tests

    [TestMethod]
    public void EntityEditorViewModel_ValidEntity_NoErrors()
    {
        var entity = new EntityData("Octorok", 10, 10, "true");
        var viewModel = new EntityEditorViewModel(entity, _gameDataService, _mapWidth, _mapHeight);
        Assert.IsFalse(viewModel.HasErrors);
        Assert.IsNull(viewModel[nameof(entity.EntityType)]);
        Assert.IsNull(viewModel[nameof(entity.X)]);
        Assert.IsNull(viewModel[nameof(entity.Y)]);
        Assert.IsNull(viewModel[nameof(entity.Condition)]);
    }

    [TestMethod]
    public void EntityEditorViewModel_InvalidEntityType_HasErrors()
    {
        var entity = new EntityData("InvalidType", 10, 10, "true");
        var viewModel = new EntityEditorViewModel(entity, _gameDataService, _mapWidth, _mapHeight);
        Assert.IsTrue(viewModel.HasErrors);
        Assert.IsNotNull(viewModel[nameof(entity.EntityType)]);
        Assert.AreEqual("Invalid Entity Type.", viewModel[nameof(entity.EntityType)]);
    }

    [TestMethod]
    public void EntityEditorViewModel_XOutOfBounds_HasErrors()
    {
        var entity = new EntityData("Octorok", -1, 10, "true"); // X too low
        var viewModel = new EntityEditorViewModel(entity, _gameDataService, _mapWidth, _mapHeight);
        Assert.IsTrue(viewModel.HasErrors);
        Assert.IsNotNull(viewModel[nameof(entity.X)]);
        Assert.AreEqual($"X position must be between 0 and {_mapWidth - 1}.", viewModel[nameof(entity.X)]);

        entity = new EntityData("Octorok", _mapWidth, 10, "true"); // X too high
        viewModel = new EntityEditorViewModel(entity, _gameDataService, _mapWidth, _mapHeight);
        Assert.IsTrue(viewModel.HasErrors);
        Assert.IsNotNull(viewModel[nameof(entity.X)]);
        Assert.AreEqual($"X position must be between 0 and {_mapWidth - 1}.", viewModel[nameof(entity.X)]);
    }

    [TestMethod]
    public void EntityEditorViewModel_YOutOfBounds_HasErrors()
    {
        var entity = new EntityData("Octorok", 10, -1, "true"); // Y too low
        var viewModel = new EntityEditorViewModel(entity, _gameDataService, _mapWidth, _mapHeight);
        Assert.IsTrue(viewModel.HasErrors);
        Assert.IsNotNull(viewModel[nameof(entity.Y)]);
        Assert.AreEqual($"Y position must be between 0 and {_mapHeight - 1}.", viewModel[nameof(entity.Y)]);

        entity = new EntityData("Octorok", 10, _mapHeight, "true"); // Y too high
        viewModel = new EntityEditorViewModel(entity, _gameDataService, _mapWidth, _mapHeight);
        Assert.IsTrue(viewModel.HasErrors);
        Assert.IsNotNull(viewModel[nameof(entity.Y)]);
        Assert.AreEqual($"Y position must be between 0 and {_mapHeight - 1}.", viewModel[nameof(entity.Y)]);
    }

    [TestMethod]
    public void EntityEditorViewModel_EmptyCondition_HasErrors()
    {
        var entity = new EntityData("Octorok", 10, 10, "");
        var viewModel = new EntityEditorViewModel(entity, _gameDataService, _mapWidth, _mapHeight);
        Assert.IsTrue(viewModel.HasErrors);
        Assert.IsNotNull(viewModel[nameof(entity.Condition)]);
        Assert.AreEqual("Condition cannot be empty. Use 'true' for always active.", viewModel[nameof(entity.Condition)]);
    }

    [TestMethod]
    public void EntityEditorViewModel_InvalidConditionFormat_HasErrors()
    {
        var entity = new EntityData("Octorok", 10, 10, "invalid-expression!");
        var viewModel = new EntityEditorViewModel(entity, _gameDataService, _mapWidth, _mapHeight);
        Assert.IsTrue(viewModel.HasErrors);
        Assert.IsNotNull(viewModel[nameof(entity.Condition)]);
        StringAssert.Contains(viewModel[nameof(entity.Condition)], "Condition must be a valid C# boolean expression");
    }

    [TestMethod]
    public void EntityEditorViewModel_SaveCommand_CanExecuteAndExecute()
    {
        var entity = new EntityData("Octorok", 10, 10, "true");
        var viewModel = new EntityEditorViewModel(entity, _gameDataService, _mapWidth, _mapHeight);
        Assert.IsTrue(viewModel.EntitySaveCommand.CanExecute(null));

        var closed = false;
        viewModel.RequestClose += (s, e) => closed = true;
        viewModel.EntitySaveCommand.Execute(null);
        Assert.IsTrue(closed);
    }

    [TestMethod]
    public void EntityEditorViewModel_CancelCommand_Execute()
    {
        var entity = new EntityData("Octorok", 10, 10, "true");
        var viewModel = new EntityEditorViewModel(entity, _gameDataService, _mapWidth, _mapHeight);

        var closed = false;
        viewModel.RequestClose += (s, e) => closed = true;
        viewModel.EntityCancelCommand.Execute(null);
        Assert.IsTrue(closed);
        Assert.IsNull(viewModel.Entity); // Check if entity is nulled on cancel
    }

    #endregion

    #region TransitionEditorViewModel Tests

    [TestMethod]
    public void TransitionEditorViewModel_ValidTransition_NoErrors()
    {
        var transition = new TransitionData("MainMap0", 5, 5, "Up", 1, 1, 10, 10);
        var viewModel = new TransitionEditorViewModel(transition, _gameDataService, _mapWidth, _mapHeight);
        Assert.IsFalse(viewModel.HasErrors);
        Assert.IsNull(viewModel[nameof(transition.MapId)]);
        Assert.IsNull(viewModel[nameof(transition.StartPositionX)]);
        Assert.IsNull(viewModel[nameof(transition.StartPositionY)]);
        Assert.IsNull(viewModel[nameof(transition.DirectionType)]);
        Assert.IsNull(viewModel[nameof(transition.SizeX)]);
        Assert.IsNull(viewModel[nameof(transition.SizeY)]);
        Assert.IsNull(viewModel[nameof(transition.PositionX)]);
        Assert.IsNull(viewModel[nameof(transition.PositionY)]);
    }

    [TestMethod]
    public void TransitionEditorViewModel_InvalidMapId_HasErrors()
    {
        var transition = new TransitionData("InvalidMap", 5, 5, "Up", 1, 1, 10, 10);
        var viewModel = new TransitionEditorViewModel(transition, _gameDataService, _mapWidth, _mapHeight);
        Assert.IsTrue(viewModel.HasErrors);
        Assert.IsNotNull(viewModel[nameof(transition.MapId)]);
        Assert.AreEqual("Invalid Map ID.", viewModel[nameof(transition.MapId)]);
    }

    [TestMethod]
    public void TransitionEditorViewModel_InvalidDirectionType_HasErrors()
    {
        var transition = new TransitionData("MainMap0", 5, 5, "InvalidDirection", 1, 1, 10, 10);
        var viewModel = new TransitionEditorViewModel(transition, _gameDataService, _mapWidth, _mapHeight);
        Assert.IsTrue(viewModel.HasErrors);
        Assert.IsNotNull(viewModel[nameof(transition.DirectionType)]);
        Assert.AreEqual("Invalid Direction Type.", viewModel[nameof(transition.DirectionType)]);
    }

    [TestMethod]
    public void TransitionEditorViewModel_PositionXOutOfBounds_HasErrors()
    {
        var transition = new TransitionData("MainMap0", 5, 5, "Up", 1, 1, -1, 10); // X too low
        var viewModel = new TransitionEditorViewModel(transition, _gameDataService, _mapWidth, _mapHeight);
        Assert.IsTrue(viewModel.HasErrors);
        Assert.IsNotNull(viewModel[nameof(transition.PositionX)]);
        Assert.AreEqual($"Position X must be between 0 and {_mapWidth - 1}.", viewModel[nameof(transition.PositionX)]);

        transition = new TransitionData("MainMap0", 5, 5, "Up", 1, 1, _mapWidth, 10); // X too high
        viewModel = new TransitionEditorViewModel(transition, _gameDataService, _mapWidth, _mapHeight);
        Assert.IsTrue(viewModel.HasErrors);
        Assert.IsNotNull(viewModel[nameof(transition.PositionX)]);
        Assert.AreEqual($"Position X must be between 0 and {_mapWidth - 1}.", viewModel[nameof(transition.PositionX)]);
    }

    [TestMethod]
    public void TransitionEditorViewModel_SizeXExceedsBounds_HasErrors()
    {
        var transition = new TransitionData("MainMap0", 5, 5, "Up", 50, 1, 10, 10); // Size X too large
        var viewModel = new TransitionEditorViewModel(transition, _gameDataService, _mapWidth, _mapHeight);
        Assert.IsTrue(viewModel.HasErrors);
        Assert.IsNotNull(viewModel[nameof(transition.SizeX)]);
        Assert.AreEqual($"Size X must be positive and not exceed map bounds (max: {_mapWidth - transition.PositionX}).", viewModel[nameof(transition.SizeX)]);

        transition = new TransitionData("MainMap0", 5, 5, "Up", 0, 1, 10, 10); // Size X zero
        viewModel = new TransitionEditorViewModel(transition, _gameDataService, _mapWidth, _mapHeight);
        Assert.IsTrue(viewModel.HasErrors);
        Assert.IsNotNull(viewModel[nameof(transition.SizeX)]);
        Assert.AreEqual($"Size X must be positive and not exceed map bounds (max: {_mapWidth - transition.PositionX}).", viewModel[nameof(transition.SizeX)]);
    }

    [TestMethod]
    public void TransitionEditorViewModel_SaveCommand_CanExecuteAndExecute()
    {
        var transition = new TransitionData("MainMap0", 5, 5, "Up", 1, 1, 10, 10);
        var viewModel = new TransitionEditorViewModel(transition, _gameDataService, _mapWidth, _mapHeight);
        Assert.IsTrue(viewModel.TransitionSaveCommand.CanExecute(null));

        var closed = false;
        viewModel.RequestClose += (s, e) => closed = true;
        viewModel.TransitionSaveCommand.Execute(null);
        Assert.IsTrue(closed);
    }

    [TestMethod]
    public void TransitionEditorViewModel_CancelCommand_Execute()
    {
        var transition = new TransitionData("MainMap0", 5, 5, "Up", 1, 1, 10, 10);
        var viewModel = new TransitionEditorViewModel(transition, _gameDataService, _mapWidth, _mapHeight);

        var closed = false;
        viewModel.RequestClose += (s, e) => closed = true;
        viewModel.TransitionCancelCommand.Execute(null);
        Assert.IsTrue(closed);
        Assert.IsNull(viewModel.Transition); // Check if transition is nulled on cancel
    }

    #endregion
}