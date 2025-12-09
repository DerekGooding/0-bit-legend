using _0_bit_legend.MapEditor.ViewModels;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls; // Added for ListBox
using System.Windows.Media; // Added for VisualTreeHelper

namespace _0_bit_legend.MapEditor;

public partial class MainWindow : Window
{
    private bool _isDrawing = false;
    private Point _dragStartPoint;

    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel();
    }

    private void MapCharacter_PreviewMouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed)
        {
            _isDrawing = true;
            UpdateMapCharacter(sender);
        }
    }

    private void MapCharacter_PreviewMouseMove(object sender, MouseEventArgs e)
    {
        if (_isDrawing && e.LeftButton == MouseButtonState.Pressed)
        {
            UpdateMapCharacter(sender);
        }
    }

    protected override void OnMouseUp(MouseButtonEventArgs e)
    {
        base.OnMouseUp(e);
        _isDrawing = false;
    }

    private void UpdateMapCharacter(object sender)
    {
        if (sender is System.Windows.Controls.TextBox textBox && textBox.DataContext is MapCharacterViewModel mapCharViewModel)
        {
            if (DataContext is MainWindowViewModel viewModel)
            {
                viewModel.UpdateMapCharacter(mapCharViewModel);
            }
        }
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
            string entityType = (string)listBox.SelectedItem;

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
            string entityType = (string)e.Data.GetData("entityType");
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
}


