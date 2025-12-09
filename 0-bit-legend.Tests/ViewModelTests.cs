using Microsoft.VisualStudio.TestTools.UnitTesting;
using _0_bit_legend.MapEditor.ViewModels;
using _0_bit_legend.MapEditor.Models;
using _0_bit_legend.MapEditor.Services;
using System.Linq;
using System.Collections.Generic;

namespace _0_bit_legend.Tests;

[TestClass]
public class ViewModelTests
{
    private GameDataService _gameDataService;
    private const int MapWidth = 32;
    private const int MapHeight = 32;

    [TestInitialize]
    public void Setup()
    {
        _gameDataService = new GameDataService();
    }

    #region EntityEditorViewModel Tests

    [TestMethod]
    public void EntityEditorViewModel_ValidEntity_NoErrors()
    {
        var entity = new EntityData("Octorok", 10, 10, "true");
        var viewModel = new EntityEditorViewModel(entity, _gameDataService, MapWidth, MapHeight);
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
        var viewModel = new EntityEditorViewModel(entity, _gameDataService, MapWidth, MapHeight);
        Assert.IsTrue(viewModel.HasErrors);
        Assert.IsNotNull(viewModel[nameof(entity.EntityType)]);
        Assert.AreEqual("Invalid Entity Type.", viewModel[nameof(entity.EntityType)]);
    }

    [TestMethod]
    public void EntityEditorViewModel_XOutOfBounds_HasErrors()
    {
        var entity = new EntityData("Octorok", -1, 10, "true"); // X too low
        var viewModel = new EntityEditorViewModel(entity, _gameDataService, MapWidth, MapHeight);
        Assert.IsTrue(viewModel.HasErrors);
        Assert.IsNotNull(viewModel[nameof(entity.X)]);
        Assert.AreEqual($"X position must be between 0 and {MapWidth - 1}.", viewModel[nameof(entity.X)]);

        entity = new EntityData("Octorok", MapWidth, 10, "true"); // X too high
        viewModel = new EntityEditorViewModel(entity, _gameDataService, MapWidth, MapHeight);
        Assert.IsTrue(viewModel.HasErrors);
        Assert.IsNotNull(viewModel[nameof(entity.X)]);
        Assert.AreEqual($"X position must be between 0 and {MapWidth - 1}.", viewModel[nameof(entity.X)]);
    }

    [TestMethod]
    public void EntityEditorViewModel_YOutOfBounds_HasErrors()
    {
        var entity = new EntityData("Octorok", 10, -1, "true"); // Y too low
        var viewModel = new EntityEditorViewModel(entity, _gameDataService, MapWidth, MapHeight);
        Assert.IsTrue(viewModel.HasErrors);
        Assert.IsNotNull(viewModel[nameof(entity.Y)]);
        Assert.AreEqual($"Y position must be between 0 and {MapHeight - 1}.", viewModel[nameof(entity.Y)]);

        entity = new EntityData("Octorok", 10, MapHeight, "true"); // Y too high
        viewModel = new EntityEditorViewModel(entity, _gameDataService, MapWidth, MapHeight);
        Assert.IsTrue(viewModel.HasErrors);
        Assert.IsNotNull(viewModel[nameof(entity.Y)]);
        Assert.AreEqual($"Y position must be between 0 and {MapHeight - 1}.", viewModel[nameof(entity.Y)]);
    }

    [TestMethod]
    public void EntityEditorViewModel_EmptyCondition_HasErrors()
    {
        var entity = new EntityData("Octorok", 10, 10, "");
        var viewModel = new EntityEditorViewModel(entity, _gameDataService, MapWidth, MapHeight);
        Assert.IsTrue(viewModel.HasErrors);
        Assert.IsNotNull(viewModel[nameof(entity.Condition)]);
        Assert.AreEqual("Condition cannot be empty. Use 'true' for always active.", viewModel[nameof(entity.Condition)]);
    }

    [TestMethod]
    public void EntityEditorViewModel_InvalidConditionFormat_HasErrors()
    {
        var entity = new EntityData("Octorok", 10, 10, "invalid-expression!");
        var viewModel = new EntityEditorViewModel(entity, _gameDataService, MapWidth, MapHeight);
        Assert.IsTrue(viewModel.HasErrors);
        Assert.IsNotNull(viewModel[nameof(entity.Condition)]);
        StringAssert.Contains(viewModel[nameof(entity.Condition)], "Condition must be a valid C# boolean expression");
    }

    [TestMethod]
    public void EntityEditorViewModel_SaveCommand_CanExecuteAndExecute()
    {
        var entity = new EntityData("Octorok", 10, 10, "true");
        var viewModel = new EntityEditorViewModel(entity, _gameDataService, MapWidth, MapHeight);
        Assert.IsTrue(viewModel.SaveCommand.CanExecute(null));

        bool closed = false;
        viewModel.RequestClose += (s, e) => closed = true;
        viewModel.SaveCommand.Execute(null);
        Assert.IsTrue(closed);
    }

    [TestMethod]
    public void EntityEditorViewModel_CancelCommand_Execute()
    {
        var entity = new EntityData("Octorok", 10, 10, "true");
        var viewModel = new EntityEditorViewModel(entity, _gameDataService, MapWidth, MapHeight);

        bool closed = false;
        viewModel.RequestClose += (s, e) => closed = true;
        viewModel.CancelCommand.Execute(null);
        Assert.IsTrue(closed);
        Assert.IsNull(viewModel.Entity); // Check if entity is nulled on cancel
    }

    #endregion

    #region TransitionEditorViewModel Tests

    [TestMethod]
    public void TransitionEditorViewModel_ValidTransition_NoErrors()
    {
        var transition = new TransitionData("MainMap0", 5, 5, "Up", 1, 1, 10, 10);
        var viewModel = new TransitionEditorViewModel(transition, _gameDataService, MapWidth, MapHeight);
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
        var viewModel = new TransitionEditorViewModel(transition, _gameDataService, MapWidth, MapHeight);
        Assert.IsTrue(viewModel.HasErrors);
        Assert.IsNotNull(viewModel[nameof(transition.MapId)]);
        Assert.AreEqual("Invalid Map ID.", viewModel[nameof(transition.MapId)]);
    }

    [TestMethod]
    public void TransitionEditorViewModel_InvalidDirectionType_HasErrors()
    {
        var transition = new TransitionData("MainMap0", 5, 5, "InvalidDirection", 1, 1, 10, 10);
        var viewModel = new TransitionEditorViewModel(transition, _gameDataService, MapWidth, MapHeight);
        Assert.IsTrue(viewModel.HasErrors);
        Assert.IsNotNull(viewModel[nameof(transition.DirectionType)]);
        Assert.AreEqual("Invalid Direction Type.", viewModel[nameof(transition.DirectionType)]);
    }

    [TestMethod]
    public void TransitionEditorViewModel_PositionXOutOfBounds_HasErrors()
    {
        var transition = new TransitionData("MainMap0", 5, 5, "Up", 1, 1, -1, 10); // X too low
        var viewModel = new TransitionEditorViewModel(transition, _gameDataService, MapWidth, MapHeight);
        Assert.IsTrue(viewModel.HasErrors);
        Assert.IsNotNull(viewModel[nameof(transition.PositionX)]);
        Assert.AreEqual($"Position X must be between 0 and {MapWidth - 1}.", viewModel[nameof(transition.PositionX)]);

        transition = new TransitionData("MainMap0", 5, 5, "Up", 1, 1, MapWidth, 10); // X too high
        viewModel = new TransitionEditorViewModel(transition, _gameDataService, MapWidth, MapHeight);
        Assert.IsTrue(viewModel.HasErrors);
        Assert.IsNotNull(viewModel[nameof(transition.PositionX)]);
        Assert.AreEqual($"Position X must be between 0 and {MapWidth - 1}.", viewModel[nameof(transition.PositionX)]);
    }

    [TestMethod]
    public void TransitionEditorViewModel_SizeXExceedsBounds_HasErrors()
    {
        var transition = new TransitionData("MainMap0", 5, 5, "Up", 50, 1, 10, 10); // Size X too large
        var viewModel = new TransitionEditorViewModel(transition, _gameDataService, MapWidth, MapHeight);
        Assert.IsTrue(viewModel.HasErrors);
        Assert.IsNotNull(viewModel[nameof(transition.SizeX)]);
        Assert.AreEqual($"Size X must be positive and not exceed map bounds (max: {MapWidth - transition.PositionX}).", viewModel[nameof(transition.SizeX)]);

        transition = new TransitionData("MainMap0", 5, 5, "Up", 0, 1, 10, 10); // Size X zero
        viewModel = new TransitionEditorViewModel(transition, _gameDataService, MapWidth, MapHeight);
        Assert.IsTrue(viewModel.HasErrors);
        Assert.IsNotNull(viewModel[nameof(transition.SizeX)]);
        Assert.AreEqual($"Size X must be positive and not exceed map bounds (max: {MapWidth - transition.PositionX}).", viewModel[nameof(transition.SizeX)]);
    }

    [TestMethod]
    public void TransitionEditorViewModel_SaveCommand_CanExecuteAndExecute()
    {
        var transition = new TransitionData("MainMap0", 5, 5, "Up", 1, 1, 10, 10);
        var viewModel = new TransitionEditorViewModel(transition, _gameDataService, MapWidth, MapHeight);
        Assert.IsTrue(viewModel.SaveCommand.CanExecute(null));

        bool closed = false;
        viewModel.RequestClose += (s, e) => closed = true;
        viewModel.SaveCommand.Execute(null);
        Assert.IsTrue(closed);
    }

    [TestMethod]
    public void TransitionEditorViewModel_CancelCommand_Execute()
    {
        var transition = new TransitionData("MainMap0", 5, 5, "Up", 1, 1, 10, 10);
        var viewModel = new TransitionEditorViewModel(transition, _gameDataService, MapWidth, MapHeight);

        bool closed = false;
        viewModel.RequestClose += (s, e) => closed = true;
        viewModel.CancelCommand.Execute(null);
        Assert.IsTrue(closed);
        Assert.IsNull(viewModel.Transition); // Check if transition is nulled on cancel
    }

    #endregion
}