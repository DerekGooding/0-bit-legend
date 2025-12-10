using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Windows.Controls.Primitives;
using System.Windows.Shapes;
using System.Windows;
using BitLegend.MapEditor.Adorners;
using BitLegend.MapEditor.Model;
using BitLegend.MapEditor.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using BitLegend.MapEditor.Services;

namespace BitLegend.Tests;

[TestClass]
public class ResizeAdornerTests
{
    private Mock<MainWindowViewModel>? _mockViewModel;
    private TransitionData? _transition;
    private FrameworkElement? _adornedElement;
    private ResizeAdorner? _resizeAdorner;
    private const double CellWidth = 16.0;
    private const double CellHeight = 16.0;
    private const int MapWidthInCells = 10;
    private const int MapHeightInCells = 10;

    [TestInitialize]
    public void Setup()
    {
        // Setup TransitionData
        _transition = new TransitionData("TestMap", 0, 0, "Up", 1, 1, 5, 5); // Initial position 5,5 size 1,1

        // Mock dependencies of MainWindowViewModel
        var mockMapFileParserService = new Mock<IMapFileParserService>();
        var mockMapFileSaverService = new Mock<IMapFileSaverService>();
        var mockGameDataService = new Mock<GameDataService>(); // GameDataService is now mockable

        // Pass the mocked dependencies to the MainWindowViewModel constructor
        _mockViewModel = new Mock<MainWindowViewModel>(
            mockMapFileParserService.Object,
            mockGameDataService.Object,
            mockMapFileSaverService.Object);

        // Setup AdornedElement (a simple Rectangle for testing)
        _adornedElement = new Rectangle
        {
            Width = _transition.SizeX * CellWidth,
            Height = _transition.SizeY * CellHeight,
            DataContext = _transition // Important for adorner to get TransitionData
        };

        // Instantiate ResizeAdorner, passing map dimensions directly
        _resizeAdorner = new ResizeAdorner(_adornedElement, _mockViewModel.Object, CellWidth, CellHeight, MapWidthInCells, MapHeightInCells);
    }

    [STATestMethod]
    public void Move_DragDelta_UpdatesTransitionDataPositionCorrectly()
    {
        // Arrange
        if (_resizeAdorner == null || _transition == null)
        {
            Assert.Fail("Setup failed, objects are null.");
        }

        // Simulate DragStarted to capture initial values
        // This is normally called by the Thumb, so we have to simulate it for the _move thumb
        // Accessing private fields is generally not good practice, but for testing internal logic it's sometimes necessary.
        // A more robust solution would be to make a protected virtual method that Thumb_DragStarted calls,
        // or a public method on the adorner to start a drag.
        var moveThumb = (Thumb)typeof(ResizeAdorner)
            .GetField("_move", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!
            .GetValue(_resizeAdorner)!;

        // Manually invoke the DragStarted handler attached to _moveThumb
        typeof(ResizeAdorner)
            .GetMethod("Thumb_DragStarted", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!
            .Invoke(_resizeAdorner, [moveThumb, new DragStartedEventArgs(0, 0)]);

        // Initial values
        var initialPositionX = _transition.PositionX;
        var initialPositionY = _transition.PositionY;

        // Simulate a drag movement of 2 cells to the right and 3 cells down
        var pixelChangeX = 2 * CellWidth;
        var pixelChangeY = 3 * CellHeight;

        // Act
        // Create DragDeltaEventArgs and invoke the _move thumb's DragDelta handler
        var dragDeltaEventArgs = new DragDeltaEventArgs(pixelChangeX, pixelChangeY);

        // Manually invoke the DragDelta handler attached to _moveThumb
        var moveDragDeltaMethod = typeof(ResizeAdorner)
            .GetMethod("Move_DragDelta", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!;
        moveDragDeltaMethod.Invoke(_resizeAdorner, [moveThumb, dragDeltaEventArgs]);

        // Assert
        // Expected changes should be 2 cells in X and 3 cells in Y
        Assert.AreEqual(initialPositionX + 2, _transition.PositionX, "PositionX should be updated by 2 cells.");
        Assert.AreEqual(initialPositionY + 3, _transition.PositionY, "PositionY should be updated by 3 cells.");

        // Simulate another drag movement, this time attempting to go out of bounds
        pixelChangeX = 10 * CellWidth; // Try to move far right
        pixelChangeY = 10 * CellHeight; // Try to move far down

        // Reset initial drag values for the new drag operation
        typeof(ResizeAdorner)
            .GetMethod("Thumb_DragStarted", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!
            .Invoke(_resizeAdorner, [moveThumb, new DragStartedEventArgs(0, 0)]);

        // Act again
        dragDeltaEventArgs = new DragDeltaEventArgs(pixelChangeX, pixelChangeY);
        moveDragDeltaMethod.Invoke(_resizeAdorner, [moveThumb, dragDeltaEventArgs]);

        // Assert clamping
        // Max X position without going out of bounds is MapWidthInCells - _transition.SizeX
        var expectedMaxX = MapWidthInCells - _transition.SizeX;
        var expectedMaxY = MapHeightInCells - _transition.SizeY;
        Assert.AreEqual(expectedMaxX, _transition.PositionX, "PositionX should be clamped to map boundary.");
        Assert.AreEqual(expectedMaxY, _transition.PositionY, "PositionY should be clamped to map boundary.");
    }
}