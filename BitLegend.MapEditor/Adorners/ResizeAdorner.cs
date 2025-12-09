using BitLegend.MapEditor.Model;
using BitLegend.MapEditor.ViewModels;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace BitLegend.MapEditor.Adorners;

public class ResizeAdorner : Adorner
{
    private readonly Thumb _topLeft, _topRight, _bottomLeft, _bottomRight;
    private readonly VisualCollection _visuals;
    private readonly TransitionData _transition;
    private readonly MainWindowViewModel _viewModel;
    private readonly Rectangle _adornedRectangle;

    public ResizeAdorner(Rectangle adornedRectangle, TransitionData transition, MainWindowViewModel viewModel) : base(adornedRectangle)
    {
        _adornedRectangle = adornedRectangle;
        _transition = transition;
        _viewModel = viewModel;

        _visuals = new VisualCollection(this);
        _topLeft = CreateThumb(Cursors.SizeNWSE);
        _topRight = CreateThumb(Cursors.SizeNESW);
        _bottomLeft = CreateThumb(Cursors.SizeNESW);
        _bottomRight = CreateThumb(Cursors.SizeNWSE);

        _topLeft.DragDelta += TopLeft_DragDelta;
        _topRight.DragDelta += TopRight_DragDelta;
        _bottomLeft.DragDelta += BottomLeft_DragDelta;
        _bottomRight.DragDelta += BottomRight_DragDelta;
    }

    private Thumb CreateThumb(Cursor cursor)
    {
        var thumb = new Thumb
        {
            Cursor = cursor,
            Width = 10,
            Height = 10,
            Background = Brushes.DodgerBlue
        };
        _visuals.Add(thumb);
        return thumb;
    }

    private void TopLeft_DragDelta(object sender, DragDeltaEventArgs e)
    {
        var (gridWidth, gridHeight) = GetGridSize();
        if (gridWidth == 0 || gridHeight == 0) return;

        var dx = (int)(e.HorizontalChange / gridWidth);
        var dy = (int)(e.VerticalChange / gridHeight);

        if (_transition.SizeX > dx)
        {
            _transition.PositionX += dx;
            _transition.SizeX -= dx;
        }

        if (_transition.SizeY > dy)
        {
            _transition.PositionY += dy;
            _transition.SizeY -= dy;
        }
    }

    private void TopRight_DragDelta(object sender, DragDeltaEventArgs e)
    {
        var (gridWidth, gridHeight) = GetGridSize();
        if (gridWidth == 0 || gridHeight == 0) return;

        var dx = (int)(e.HorizontalChange / gridWidth);
        var dy = (int)(e.VerticalChange / gridHeight);

        if (_transition.SizeX + dx > 0)
        {
            _transition.SizeX += dx;
        }

        if (_transition.SizeY > dy)
        {
            _transition.PositionY += dy;
            _transition.SizeY -= dy;
        }
    }

    private void BottomLeft_DragDelta(object sender, DragDeltaEventArgs e)
    {
        var (gridWidth, gridHeight) = GetGridSize();
        if (gridWidth == 0 || gridHeight == 0) return;

        var dx = (int)(e.HorizontalChange / gridWidth);
        var dy = (int)(e.VerticalChange / gridHeight);

        if (_transition.SizeX > dx)
        {
            _transition.PositionX += dx;
            _transition.SizeX -= dx;
        }

        if (_transition.SizeY + dy > 0)
        {
            _transition.SizeY += dy;
        }
    }

    private void BottomRight_DragDelta(object sender, DragDeltaEventArgs e)
    {
        var (gridWidth, gridHeight) = GetGridSize();
        if (gridWidth == 0 || gridHeight == 0) return;

        var dx = (int)(e.HorizontalChange / gridWidth);
        var dy = (int)(e.VerticalChange / gridHeight);

        if (_transition.SizeX + dx > 0)
        {
            _transition.SizeX += dx;
        }

        if (_transition.SizeY + dy > 0)
        {
            _transition.SizeY += dy;
        }
    }

    private (double, double) GetGridSize()
    {
        if (_viewModel.SelectedMap == null || _viewModel.SelectedMap.Raw.Count == 0 || _viewModel.SelectedMap.Raw[0].Length == 0)
            return (0, 0);

        var mapWidth = _adornedRectangle.ActualWidth / _transition.SizeX;
        var mapHeight = _adornedRectangle.ActualHeight / _transition.SizeY;

        return (mapWidth, mapHeight);
    }


    protected override int VisualChildrenCount => _visuals.Count;

    protected override Visual GetVisualChild(int index) => _visuals[index];

    protected override Size ArrangeOverride(Size finalSize)
    {
        if (AdornedElement is not FrameworkElement adornedElement) return finalSize;

        var adornedWidth = adornedElement.ActualWidth;
        var adornedHeight = adornedElement.ActualHeight;

        _topLeft.Arrange(new Rect(-5, -5, 10, 10));
        _topRight.Arrange(new Rect(adornedWidth - 5, -5, 10, 10));
        _bottomLeft.Arrange(new Rect(-5, adornedHeight - 5, 10, 10));
        _bottomRight.Arrange(new Rect(adornedWidth - 5, adornedHeight - 5, 10, 10));

        return finalSize;
    }
}
