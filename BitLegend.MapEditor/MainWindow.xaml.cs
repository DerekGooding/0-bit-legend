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
    private IResizableAndMovable? _selectedResizableAndMovable;
    private double _cachedCellWidth;
    private double _cachedCellHeight;


    public MainWindow(MainWindowViewModel mainWindowViewModel)
    {
        InitializeComponent();
        DataContext = mainWindowViewModel;
        this.Loaded += MainWindow_Loaded;
    }

    private void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        CacheCellDimensions();
    }

    private void MapItemsControl_Loaded(object sender, RoutedEventArgs e)
    {
        CacheCellDimensions(); // Ensure dimensions are cached when the control is loaded or reloaded
    }

    private void MapItemsControl_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        CacheCellDimensions(); // Recache dimensions if the size of the control changes
    }

    private void CacheCellDimensions()
    {
        // Try to find the TextBlock inside the Grid within the ItemsControl
        // This relies on the structure from Task 1.2
        var firstCellTextBlock = FindVisualChild<TextBlock>(MapItemsControl);
        if (firstCellTextBlock != null)
        {
            _cachedCellWidth = firstCellTextBlock.ActualWidth;
            _cachedCellHeight = firstCellTextBlock.ActualHeight;
        }
        else
        {
            // Fallback for when no map is loaded yet or structure is different
            // Could use a default value or try to find a Grid directly and infer
            _cachedCellWidth = 12; // Default reasonable value
            _cachedCellHeight = 12; // Default reasonable value
        }
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
        
            private void SetActiveAdorner(FrameworkElement? element, IResizableAndMovable? data)
            {
                var adornerLayer = AdornerLayer.GetAdornerLayer(MapAdornerDecorator);
                if (adornerLayer == null) return;
        
                // Remove the old adorner
                if (_currentAdorner != null)
                {
                    adornerLayer.Remove(_currentAdorner);
                    _currentAdorner = null;
                }
        
                _selectedResizableAndMovable = data;
        
                // Add a new adorner if an element and data are provided
                if (element != null && data != null && DataContext is MainWindowViewModel viewModel)
                {
                    // Use cached cell dimensions
                    double cellWidth = _cachedCellWidth;
                    double cellHeight = _cachedCellHeight;
        
                    // Get map dimensions in cells
                    int mapWidthInCells = viewModel.SelectedMap?.Raw[0]?.Length ?? 0;
                    int mapHeightInCells = viewModel.SelectedMap?.Raw?.Count ?? 0;
        
                    if (cellWidth > 0 && cellHeight > 0 && mapWidthInCells > 0 && mapHeightInCells > 0)
                    {
                        _currentAdorner = new ResizeAdorner(element, viewModel, cellWidth, cellHeight, mapWidthInCells, mapHeightInCells);
                        adornerLayer.Add(_currentAdorner);
                    }
                }
                else
                {
                    // If no element or data, ensure SelectedEntity and SelectedTransition are null in ViewModel
                    if (DataContext is MainWindowViewModel vm)
                    {
                        vm.SelectedEntity = null;
                        vm.SelectedTransition = null;
                    }
                }
            }
        
                        private void MapDisplay_PreviewMouseDown(object sender, MouseButtonEventArgs e)
                        {
                            if (e.LeftButton == MouseButtonState.Pressed && DataContext is MainWindowViewModel viewModel && viewModel.SelectedMap != null)
                            {
                                if (sender is not ItemsControl mapItemsControl) return;
                    
                                _dragStartPoint = e.GetPosition(mapItemsControl); // Capture drag start point
                    
                                // Clear any active adorner when clicking on the map background
                                SetActiveAdorner(null, null);
                    
                                var originalSource = e.OriginalSource as FrameworkElement;
                                if (originalSource != null)
                                {
                                    // Check if an EntityControl was clicked
                                    var entityControl = VisualTreeHelper.GetParent(originalSource) as Controls.EntityControl;
                                    if (entityControl != null && entityControl.EntityData != null)
                                    {
                                        viewModel.SelectedEntity = entityControl.EntityData;
                                        SetActiveAdorner(entityControl, entityControl.EntityData);
                                        e.Handled = true;
                                        return;
                                    }
                    
                                    // Check if a Rectangle representing a TransitionData was clicked
                                    var clickedRectangle = VisualTreeHelper.GetParent(originalSource) as Rectangle;
                                    if (clickedRectangle != null && clickedRectangle.DataContext is TransitionData transition)
                                    {
                                        viewModel.SelectedTransition = transition;
                                        SetActiveAdorner(clickedRectangle, transition);
                                        e.Handled = true;
                                        return;
                                    }
                                }
                    
                                // If no entity or transition was clicked, capture mouse for potential brush/selection drag
                                (mapItemsControl as UIElement)?.CaptureMouse();
                                e.Handled = true;

                                // Additionally, calculate grid coordinates for the click point,
                                // which can be used by subsequent logic if needed for single cell interaction.
                                if (_cachedCellWidth > 0 && _cachedCellHeight > 0)
                                {
                                    int clickedGridX = (int)(_dragStartPoint.X / _cachedCellWidth);
                                    int clickedGridY = (int)(_dragStartPoint.Y / _cachedCellHeight);
                                    // These clickedGridX, clickedGridY can be used here or passed to ViewModel
                                    // For now, just calculating them.
                                }
                            }
                        }
                    
                        private void MapDisplay_PreviewMouseMove(object sender, MouseEventArgs e)
                        {
                            if (e.LeftButton == MouseButtonState.Pressed && DataContext is MainWindowViewModel viewModel && viewModel.SelectedMap != null)
                            {
                                if (sender is not ItemsControl mapItemsControl) return;
                    
                                var mousePos = e.GetPosition(mapItemsControl);
                                var diff = _dragStartPoint - mousePos;
                    
                                // Check for drag threshold
                                if (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                                    Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance)
                                {
                                    e.Handled = true;
                                    switch (viewModel.PaintingMode)
                                    {
                                        case PaintingMode.Brush:
                                            _isBrushDrawing = true;
                                            ProcessBrushDrawing(mapItemsControl, mousePos);
                                            break;
                                        case PaintingMode.Selection:
                                            _isSelecting = true;
                                            // If selection hasn't started yet, initialize _selectionStartPoint
                                            if (_selectionAdorner == null)
                                            {
                                                _selectionStartPoint = _dragStartPoint;
                                                var adornerLayer = AdornerLayer.GetAdornerLayer(mapItemsControl);
                                                if (adornerLayer != null)
                                                {
                                                    _selectionAdorner = new SelectionAdorner(mapItemsControl);
                                                    adornerLayer.Add(_selectionAdorner);
                                                    _selectionAdorner.UpdateSelection(_selectionStartPoint, _selectionStartPoint);
                                                }
                                            }
                                            _selectionAdorner?.UpdateSelection(_selectionStartPoint, mousePos);
                                            break;
                                    }
                                }
                                else if (_isBrushDrawing || _isSelecting) // Continue drawing if already started
                                {
                                     e.Handled = true;
                                     switch (viewModel.PaintingMode)
                                     {
                                         case PaintingMode.Brush:
                                             ProcessBrushDrawing(mapItemsControl, mousePos);
                                             break;
                                         case PaintingMode.Selection:
                                             _selectionAdorner?.UpdateSelection(_selectionStartPoint, mousePos);
                                             break;
                                     }
                                }
                            }
                        }
    private void MapDisplay_PreviewMouseUp(object sender, MouseButtonEventArgs e)
    {
        if (sender is not ItemsControl mapItemsControl) return;

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
        if (DataContext is not MainWindowViewModel viewModel) return;

        if (_cachedCellWidth == 0 || _cachedCellHeight == 0)
        {
            CacheCellDimensions();
            if (_cachedCellWidth == 0 || _cachedCellHeight == 0) return; // Still zero, cannot proceed
        }

        int gridX = (int)(mousePosition.X / _cachedCellWidth);
        int gridY = (int)(mousePosition.Y / _cachedCellHeight);

        // Boundary checks
        if (gridY < 0 || gridY >= viewModel.DisplayMapCharacters.Count ||
            gridX < 0 || gridX >= viewModel.DisplayMapCharacters[gridY].Count)
        {
            return;
        }

        var currentCell = viewModel.DisplayMapCharacters[gridY][gridX];

        if (currentCell != null && currentCell != _lastPaintedCell)
        {
            if (viewModel.SelectedCharacterBrush?.Count > 0)
            {
                // Apply the selected brush
                for (var y = 0; y < viewModel.SelectedCharacterBrush.Count; y++)
                {
                    var brushRow = viewModel.SelectedCharacterBrush[y];
                    for (var x = 0; x < brushRow.Count; x++)
                    {
                        var targetX = currentCell.X + x;
                        var targetY = currentCell.Y + y;

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

    private void ProcessSelectionDrawing(ItemsControl mapItemsControl, Point mousePosition)
    {
        // Implement selection rectangle drawing here
    }

    private void ProcessSelectionEnd(ItemsControl mapItemsControl, Point startPoint, Point endPoint)
    {
        if (DataContext is not MainWindowViewModel viewModel) return;

        // Determine the actual pixel size of one character cell using cached values
        var cellWidth = _cachedCellWidth;
        var cellHeight = _cachedCellHeight;

        if (cellWidth == 0 || cellHeight == 0)
        {
            CacheCellDimensions(); // Recache if somehow zero
            cellWidth = _cachedCellWidth;
            cellHeight = _cachedCellHeight;
            if (cellWidth == 0 || cellHeight == 0) return; // Still zero, something is wrong
        }

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
        for (var i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
        {
            var child = VisualTreeHelper.GetChild(parent, i);
            if (child is T typedChild)
            {
                return typedChild;
            }
            else
            {
                var result = FindVisualChild<T>(child);
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
        if (DataContext is MainWindowViewModel viewModel)
        {
            // The ViewModel's SelectedTransition is already updated via TwoWay binding from DataGrid.SelectedItem.
            // We just need to ensure the adorner is set for the currently selected item.
            if (viewModel.SelectedTransition != null &&
                _transitionRectangles.TryGetValue(viewModel.SelectedTransition, out var rectangle))
            {
                SetActiveAdorner(rectangle, viewModel.SelectedTransition);
            }
            else
            {
                SetActiveAdorner(null, null); // Clear adorner if no transition is selected
            }
        }
    }

    private void CharacterTextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (sender is TextBlock textBlock)
        {
            var parentGrid = VisualTreeHelper.GetParent(textBlock) as Grid;
            if (parentGrid != null)
            {
                var editTextBox = parentGrid.FindName("CharacterEditTextBox") as TextBox;
                if (editTextBox != null)
                {
                    textBlock.Visibility = Visibility.Collapsed;
                    editTextBox.Visibility = Visibility.Visible;
                    editTextBox.Focus();
                    editTextBox.SelectAll();
                }
            }
        }
        e.Handled = true;
    }

    private void CharacterEditTextBox_LostFocus(object sender, RoutedEventArgs e)
    {
        if (sender is TextBox editTextBox)
        {
            var parentGrid = VisualTreeHelper.GetParent(editTextBox) as Grid;
            if (parentGrid != null)
            {
                var textBlock = parentGrid.FindName("CharacterTextBlock") as TextBlock;
                if (textBlock != null)
                {
                    editTextBox.Visibility = Visibility.Collapsed;
                    textBlock.Visibility = Visibility.Visible;
                }
            }
        }
    }

    private void CharacterEditTextBox_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            // Lose focus to commit the change and hide the TextBox
            var textBox = sender as TextBox;
            var scope = FocusManager.GetFocusScope(textBox);
            FocusManager.SetFocusedElement(scope, null); // This will cause LostFocus
        }
    }
}




