using BitLegend.MapEditor.Adorners;
using BitLegend.MapEditor.Model;
using BitLegend.MapEditor.Model.Enums;
using BitLegend.MapEditor.ViewModels;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace BitLegend.MapEditor;

[Singleton]
public partial class MainWindow : Window
{
    private Point _dragStartPoint;
    private bool _isBrushDrawing;
    private bool _isSelecting;
    private MapCharacterViewModel? _lastPaintedCell;
    private readonly Dictionary<TransitionData, Rectangle> _transitionRectangles = [];
    private Adorner? _currentAdorner;
    private SelectionAdorner? _selectionAdorner;
    private Point _selectionStartPoint;

    public MainWindow(MainWindowViewModel mainWindowViewModel)
    {
        InitializeComponent();
        DataContext = mainWindowViewModel;
    }
    private void EntityPalette_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        => _dragStartPoint = e.GetPosition(null);

    private void EntityPalette_MouseMove(object sender, MouseEventArgs e)
    {
        var mousePos = e.GetPosition(null);
        var diff = _dragStartPoint - mousePos;

        if (e.LeftButton == MouseButtonState.Pressed &&
            (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
             Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
        {
            var listBox = sender as ListBox;
            var entityType = (string?)listBox?.SelectedItem;

            if (entityType != null)
            {
                DataObject dragData = new("entityType", entityType);
                DragDrop.DoDragDrop(listBox, dragData, DragDropEffects.Move);
            }
        }
    }

    private void MapArea_DragOver(object sender, DragEventArgs e)
    {
        e.Effects = e.Data.GetDataPresent("entityType") ? DragDropEffects.Move : DragDropEffects.None;
        e.Handled = true;
    }

    private void MapArea_Drop(object sender, DragEventArgs e)
    {
        if (e.Data.GetDataPresent("entityType"))
        {
            var entityType = (string)e.Data.GetData("entityType");
            if (DataContext is MainWindowViewModel viewModel)
            {
                var dropTarget = e.OriginalSource as UIElement;
                if (dropTarget != null)
                {
                    while (dropTarget is not null and not TextBox)
                    {
                        dropTarget = VisualTreeHelper.GetParent(dropTarget) as UIElement;
                    }
                    var targetTextBox = dropTarget as TextBox;

                    if (targetTextBox?.DataContext is MapCharacterViewModel mapCharViewModel)
                    {
                        viewModel.AddEntityFromDragDrop(entityType, mapCharViewModel.X, mapCharViewModel.Y);
                    }
                }
            }
        }
        e.Handled = true;
    }

    private void MapDisplay_PreviewMouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed && DataContext is MainWindowViewModel viewModel && viewModel.SelectedMap != null)
        {
            var mapItemsControl = sender as ItemsControl;
            if (mapItemsControl == null) return;

            (mapItemsControl as UIElement)?.CaptureMouse();
            e.Handled = true;

            switch (viewModel.PaintingMode)
            {
                case PaintingMode.Brush:
                    _isBrushDrawing = true;
                    ProcessBrushDrawing(mapItemsControl, e.GetPosition(mapItemsControl));
                    break;
                case PaintingMode.Selection:
                    _isSelecting = true;
                    _selectionStartPoint = e.GetPosition(mapItemsControl);

                    var adornerLayer = AdornerLayer.GetAdornerLayer(mapItemsControl);
                    if (adornerLayer != null)
                    {
                        _selectionAdorner = new SelectionAdorner(mapItemsControl);
                        adornerLayer.Add(_selectionAdorner);
                        _selectionAdorner.UpdateSelection(_selectionStartPoint, _selectionStartPoint);
                    }
                    break;
            }
        }
    }

    private void MapDisplay_PreviewMouseMove(object sender, MouseEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed && DataContext is MainWindowViewModel viewModel && viewModel.SelectedMap != null)
        {
            var mapItemsControl = sender as ItemsControl;
            if (mapItemsControl == null) return;

            e.Handled = true;

            switch (viewModel.PaintingMode)
            {
                case PaintingMode.Brush:
                    if (_isBrushDrawing)
                    {
                        ProcessBrushDrawing(mapItemsControl, e.GetPosition(mapItemsControl));
                    }
                    break;
                case PaintingMode.Selection:
                    if (_isSelecting && _selectionAdorner != null)
                    {
                        _selectionAdorner.UpdateSelection(_selectionStartPoint, e.GetPosition(mapItemsControl));
                    }
                    break;
            }
        }
    }

    private void MapDisplay_PreviewMouseUp(object sender, MouseButtonEventArgs e)
    {
        var mapItemsControl = sender as ItemsControl;
        if (mapItemsControl == null) return;

        (mapItemsControl as UIElement)?.ReleaseMouseCapture();
        e.Handled = true;

        if (DataContext is not MainWindowViewModel viewModel) return;

        switch (viewModel.PaintingMode)
        {
            case PaintingMode.Brush:
                _isBrushDrawing = false;
                _lastPaintedCell = null;
                break;
            case PaintingMode.Selection:
                _isSelecting = false;
                if (_selectionAdorner != null)
                {
                    var adornerLayer = AdornerLayer.GetAdornerLayer(mapItemsControl);
                    adornerLayer?.Remove(_selectionAdorner);
                    _selectionAdorner.ClearSelection();
                    _selectionAdorner = null;
                }
                ProcessSelectionEnd(mapItemsControl, _selectionStartPoint, e.GetPosition(mapItemsControl));
                break;
        }
    }

    private void ProcessBrushDrawing(ItemsControl mapItemsControl, Point mousePosition)
    {
        // Get the current view model from the DataContext
        if (DataContext is not MainWindowViewModel viewModel) return;

        // Perform a hit test to find the TextBox at the current mouse position
        VisualTreeHelper.HitTest(mapItemsControl, null,
            new HitTestResultCallback(result =>
            {
                var visual = result.VisualHit;
                while (visual is not null and not TextBox)
                {
                    visual = VisualTreeHelper.GetParent(visual);
                }

                if (visual is TextBox hitTextBox)
                {
                    if (hitTextBox.DataContext is MapCharacterViewModel currentCell && currentCell != _lastPaintedCell)
                    {
                        if (viewModel.SelectedCharacterBrush != null && viewModel.SelectedCharacterBrush.Count > 0)
                        {
                            // Apply the selected brush
                            for (int y = 0; y < viewModel.SelectedCharacterBrush.Count; y++)
                            {
                                var brushRow = viewModel.SelectedCharacterBrush[y];
                                for (int x = 0; x < brushRow.Count; x++)
                                {
                                    int targetX = currentCell.X + x;
                                    int targetY = currentCell.Y + y;

                                    if (targetY >= 0 && targetY < viewModel.DisplayMapCharacters.Count &&
                                        targetX >= 0 && targetX < viewModel.DisplayMapCharacters[targetY].Count)
                                    {
                                        viewModel.DisplayMapCharacters[targetY][targetX].Character = brushRow[x];
                                    }
                                }
                            }
                        }
                        else
                        {
                            // Fallback to single character drawing
                            currentCell.Character = viewModel.CurrentDrawingCharacter;
                        }
                        _lastPaintedCell = currentCell; // Mark this cell as painted
                    }
                }
                return HitTestResultBehavior.Continue; // Continue hit testing
            }),
            new PointHitTestParameters(mousePosition));
    }

    private void ProcessSelectionDrawing(ItemsControl mapItemsControl, Point mousePosition)
    {
        // Implement selection rectangle drawing here
    }

    private void ProcessSelectionEnd(ItemsControl mapItemsControl, Point startPoint, Point endPoint)
    {
        if (DataContext is not MainWindowViewModel viewModel) return;

        // Determine the actual pixel size of one character cell
        if (viewModel.DisplayMapCharacters.Count == 0 || viewModel.DisplayMapCharacters[0].Count == 0) return;

        var firstCell = FindVisualChild<TextBox>(mapItemsControl);
        if (firstCell == null) return;

        var cellWidth = firstCell.ActualWidth;
        var cellHeight = firstCell.ActualHeight;

        if (cellWidth == 0 || cellHeight == 0) return;

        // Convert pixel coordinates to grid coordinates
        var startGridX = (int)Math.Floor(Math.Min(startPoint.X, endPoint.X) / cellWidth);
        var startGridY = (int)Math.Floor(Math.Min(startPoint.Y, endPoint.Y) / cellHeight);
        var endGridX = (int)Math.Floor(Math.Max(startPoint.X, endPoint.X) / cellWidth);
        var endGridY = (int)Math.Floor(Math.Max(startPoint.Y, endPoint.Y) / cellHeight);

        // Ensure selection is within map bounds
        var mapWidth = viewModel.SelectedMap!.Raw[0].Length;
        var mapHeight = viewModel.SelectedMap.Raw.Count;

        startGridX = Math.Clamp(startGridX, 0, mapWidth - 1);
        startGridY = Math.Clamp(startGridY, 0, mapHeight - 1);
        endGridX = Math.Clamp(endGridX, 0, mapWidth - 1);
        endGridY = Math.Clamp(endGridY, 0, mapHeight - 1);
        
        viewModel.SetSelectedCharacterBrush(startGridX, startGridY, endGridX, endGridY);
    }

    private static T? FindVisualChild<T>(DependencyObject parent) where T : DependencyObject
    {
        for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
        {
            DependencyObject child = VisualTreeHelper.GetChild(parent, i);
            if (child is T typedChild)
            {
                return typedChild;
            }
            else
            {
                T? result = FindVisualChild<T>(child);
                if (result != null)
                    return result;
            }
        }
        return null;
    }

    private void TransitionRectangle_Loaded(object sender, RoutedEventArgs e)
    {
        if (sender is Rectangle rectangle && rectangle.DataContext is TransitionData transition)
        {
            _transitionRectangles[transition] = rectangle;
        }
    }

    private void TransitionsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var adornerLayer = AdornerLayer.GetAdornerLayer(MapAdornerDecorator);
        if (adornerLayer == null) return;

        // Remove the old adorner
        if (_currentAdorner != null)
        {
            adornerLayer.Remove(_currentAdorner);
            _currentAdorner = null;
        }

        // Add a new adorner to the selected transition's rectangle
        if (TransitionsDataGrid.SelectedItem is TransitionData selectedTransition &&
            _transitionRectangles.TryGetValue(selectedTransition, out var rectangle) &&
            DataContext is MainWindowViewModel viewModel) // Get the view model
        {
            _currentAdorner = new ResizeAdorner(rectangle, selectedTransition, viewModel);
            adornerLayer.Add(_currentAdorner);
        }
    }
}




