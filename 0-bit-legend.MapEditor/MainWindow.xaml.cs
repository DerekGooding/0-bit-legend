using _0_bit_legend.MapEditor.ViewModels;
using System.Windows.Controls; // Added for ItemsControl and TextBox
using System.Windows.Input;
using System.Windows.Media;

namespace _0_bit_legend.MapEditor;

public partial class MainWindow : Window
{
    private Point _dragStartPoint;
    private bool _isDrawing; // Flag to indicate if drawing is in progress
    private MapCharacterViewModel _lastPaintedCell; // To prevent repainting the same cell

    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel();
    }
    private void EntityPalette_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e) => _dragStartPoint = e.GetPosition(null);

    private void EntityPalette_MouseMove(object sender, MouseEventArgs e)
    {
        Point mousePos = e.GetPosition(null);
        Vector diff = _dragStartPoint - mousePos;

        if (e.LeftButton == MouseButtonState.Pressed &&
            (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
             Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
        {
            // Get the dragged ListViewItem
            ListBox listBox = sender as ListBox;
            var entityType = (string)listBox.SelectedItem;

            if (entityType != null)
            {
                // Initialize the drag-and-drop operation
                DataObject dragData = new("entityType", entityType);
                DragDrop.DoDragDrop(listBox, dragData, DragDropEffects.Move);
            }
        }
    }

    private void MapArea_DragOver(object sender, DragEventArgs e)
    {
        if (e.Data.GetDataPresent("entityType"))
        {
            e.Effects = DragDropEffects.Move;
        }
        else
        {
            e.Effects = DragDropEffects.None;
        }
        e.Handled = true;
    }

    private void MapArea_Drop(object sender, DragEventArgs e)
    {
        if (e.Data.GetDataPresent("entityType"))
        {
            var entityType = (string)e.Data.GetData("entityType");
            if (DataContext is MainWindowViewModel viewModel)
            {
                // Get the drop position relative to the ItemsControl (map)
                // The map is displayed using nested ItemsControls, where each inner ItemsControl represents a row
                // and contains TextBoxes for each character.
                // We need to find which TextBox was dropped on to get accurate X, Y coordinates.

                // Find the visual element that was dropped on
                var dropTarget = e.OriginalSource as UIElement;
                if (dropTarget != null)
                {
                    // Traverse up the visual tree to find the TextBox
                    TextBox targetTextBox = null;
                    while (dropTarget != null && !(dropTarget is TextBox))
                    {
                        dropTarget = VisualTreeHelper.GetParent(dropTarget) as UIElement;
                    }
                    targetTextBox = dropTarget as TextBox;

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
            _isDrawing = true;
            ProcessDrawing(sender as ItemsControl, e.GetPosition(sender as IInputElement));
            (sender as UIElement).CaptureMouse();
            e.Handled = true;
        }
    }

    private void MapDisplay_PreviewMouseMove(object sender, MouseEventArgs e)
    {
        if (_isDrawing && e.LeftButton == MouseButtonState.Pressed && DataContext is MainWindowViewModel viewModel && viewModel.SelectedMap != null)
        {
            ProcessDrawing(sender as ItemsControl, e.GetPosition(sender as IInputElement));
            e.Handled = true;
        }
    }

    private void MapDisplay_PreviewMouseUp(object sender, MouseButtonEventArgs e)
    {
        if (_isDrawing)
        {
            _isDrawing = false;
            _lastPaintedCell = null;
            (sender as UIElement).ReleaseMouseCapture();
            e.Handled = true;
        }
    }

    private void ProcessDrawing(ItemsControl mapItemsControl, Point mousePosition)
    {
        // Get the current view model from the DataContext
        if (DataContext is not MainWindowViewModel viewModel) return;

        // Perform a hit test to find the TextBox at the current mouse position
        VisualTreeHelper.HitTest(mapItemsControl, null,
            new HitTestResultCallback(result =>
            {
                var visual = result.VisualHit;
                while (visual != null && !(visual is TextBox))
                {
                    visual = VisualTreeHelper.GetParent(visual);
                }

                if (visual is TextBox hitTextBox)
                {
                    if (hitTextBox.DataContext is MapCharacterViewModel currentCell && currentCell != _lastPaintedCell)
                    {
                        // Update the character of the hit cell
                        currentCell.Character = viewModel.CurrentDrawingCharacter;
                        _lastPaintedCell = currentCell; // Mark this cell as painted
                    }
                }
                return HitTestResultBehavior.Continue; // Continue hit testing
            }),
            new PointHitTestParameters(mousePosition));
    }
}



