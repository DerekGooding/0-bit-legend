using BitLegend.MapEditor.Model;
using BitLegend.MapEditor.ViewModels;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace BitLegend.MapEditor.Adorners;

public class ResizeAdorner : Adorner
{
    private readonly Thumb _topLeft, _top, _topRight, _left, _right, _bottomLeft, _bottom, _bottomRight, _move;
    private readonly VisualCollection _visuals;
    private readonly TransitionData _transition;
    private readonly MainWindowViewModel _viewModel; // Still needed for other commands if any, but not for map dims
    private readonly int _mapWidthInCells;
    private readonly int _mapHeightInCells;

    private (double Width, double Height) _cellSize;
    private (int X, int Y, int Width, int Height) _initialDragValues;

    public ResizeAdorner(UIElement adornedElement, MainWindowViewModel viewModel, double cellWidth, double cellHeight, int mapWidthInCells, int mapHeightInCells) : base(adornedElement)
    {
        if (adornedElement is not FrameworkElement adornedFrameworkElement || adornedFrameworkElement.DataContext is not TransitionData transition)
        {
            throw new ArgumentException("Adorned element must be a FrameworkElement with a TransitionData DataContext.", nameof(adornedElement));
        }

        _transition = transition;
        _viewModel = viewModel;
        _mapWidthInCells = mapWidthInCells;
        _mapHeightInCells = mapHeightInCells;
        _visuals = new VisualCollection(this);

        _cellSize = (cellWidth, cellHeight);

        _topLeft = CreateThumb(Cursors.SizeNWSE);
        _top = CreateThumb(Cursors.SizeNS);
        _topRight = CreateThumb(Cursors.SizeNESW);
        _left = CreateThumb(Cursors.SizeWE);
        _right = CreateThumb(Cursors.SizeWE);
        _bottomLeft = CreateThumb(Cursors.SizeNESW);
        _bottom = CreateThumb(Cursors.SizeNS);
        _bottomRight = CreateThumb(Cursors.SizeNWSE);
        _move = CreateThumb(Cursors.SizeAll, Brushes.Transparent);

        // Attach drag handlers
        _topLeft.DragDelta += TopLeft_DragDelta;
        _top.DragDelta += Top_DragDelta;
        _topRight.DragDelta += TopRight_DragDelta;
        _left.DragDelta += Left_DragDelta;
        _right.DragDelta += Right_DragDelta;
        _bottomLeft.DragDelta += BottomLeft_DragDelta;
        _bottom.DragDelta += Bottom_DragDelta;
        _bottomRight.DragDelta += BottomRight_DragDelta;
        _move.DragDelta += Move_DragDelta;

        // Attach DragStarted handlers
        _topLeft.DragStarted += Thumb_DragStarted;
        _top.DragStarted += Thumb_DragStarted;
        _topRight.DragStarted += Thumb_DragStarted;
        _left.DragStarted += Thumb_DragStarted;
        _right.DragStarted += Thumb_DragStarted;
        _bottomLeft.DragStarted += Thumb_DragStarted;
        _bottom.DragStarted += Thumb_DragStarted;
        _bottomRight.DragStarted += Thumb_DragStarted;
        _move.DragStarted += Thumb_DragStarted;
    }

    private void Thumb_DragStarted(object sender, DragStartedEventArgs e)
        => _initialDragValues = (_transition.PositionX, _transition.PositionY, _transition.SizeX, _transition.SizeY);

    private Thumb CreateThumb(Cursor cursor, Brush? background = null)
    {
        var thumb = new Thumb
        {
            Cursor = cursor,
            Width = 10,
            Height = 10,
            Background = background ?? Brushes.DodgerBlue,
            BorderBrush = Brushes.White,
            BorderThickness = new Thickness(1)
        };
        _visuals.Add(thumb);
        return thumb;
    }

    private void HandleDrag(double horizontalChange, double verticalChange, Action<int, int> updateAction)
    {
        if (_cellSize.Width == 0 || _cellSize.Height == 0) return;

        var dx = (int)Math.Round(horizontalChange / _cellSize.Width);
        var dy = (int)Math.Round(verticalChange / _cellSize.Height);

        updateAction(dx, dy);
    }


    private void TopLeft_DragDelta(object sender, DragDeltaEventArgs e) => HandleDrag(e.HorizontalChange, e.VerticalChange, (dx, dy) =>
    {
        // Clamp position to >= 0, which limits how much we can expand
        var newPosX = Math.Max(0, _initialDragValues.X + dx);
        var newPosY = Math.Max(0, _initialDragValues.Y + dy);

        // Calculate how much position actually changed (may be clamped)
        var actualDx = newPosX - _initialDragValues.X;
        var actualDy = newPosY - _initialDragValues.Y;

        // Size changes by opposite amount to maintain far edge position
        _transition.SizeX = Math.Max(1, _initialDragValues.Width - actualDx);
        _transition.SizeY = Math.Max(1, _initialDragValues.Height - actualDy);
        _transition.PositionX = newPosX;
        _transition.PositionY = newPosY;
    });

    private void Top_DragDelta(object sender, DragDeltaEventArgs e) => HandleDrag(0, e.VerticalChange, (_, dy) =>
    {
        var newPosY = Math.Max(0, _initialDragValues.Y + dy);
        var actualDy = newPosY - _initialDragValues.Y;

        _transition.SizeY = Math.Max(1, _initialDragValues.Height + actualDy);
        _transition.PositionY = newPosY;
    });

    private void TopRight_DragDelta(object sender, DragDeltaEventArgs e) => HandleDrag(e.HorizontalChange, e.VerticalChange, (dx, dy) =>
    {
        // Top edge
        var newPosY = Math.Max(0, _initialDragValues.Y + dy);
        var actualDy = newPosY - _initialDragValues.Y;
        _transition.SizeY = Math.Max(1, _initialDragValues.Height - actualDy);
        _transition.PositionY = newPosY;

        // Right edge - bounded by map width
        var maxWidth = _mapWidthInCells - _initialDragValues.X;
        _transition.SizeX = Math.Min(Math.Max(1, _initialDragValues.Width + dx), maxWidth);
    });

    private void Left_DragDelta(object sender, DragDeltaEventArgs e) => HandleDrag(e.HorizontalChange, 0, (dx, _) =>
    {
        var newPosX = Math.Max(0, _initialDragValues.X + dx);
        var actualDx = newPosX - _initialDragValues.X;

        _transition.SizeX = Math.Max(1, _initialDragValues.Width + actualDx);
        _transition.PositionX = newPosX;
    });

    private void Right_DragDelta(object sender, DragDeltaEventArgs e) => HandleDrag(e.HorizontalChange, 0, (dx, _) =>
    {
        var maxWidth = _mapWidthInCells - _initialDragValues.X;

        _transition.SizeX = Math.Min(Math.Max(1, _initialDragValues.Width + dx), maxWidth);
    });

    private void BottomLeft_DragDelta(object sender, DragDeltaEventArgs e) => HandleDrag(e.HorizontalChange, e.VerticalChange, (dx, dy) =>
    {
        var newPosX = Math.Max(0, _initialDragValues.X + dx);
        var actualDx = newPosX - _initialDragValues.X;
        _transition.SizeX = Math.Max(1, _initialDragValues.Width - actualDx);
        _transition.PositionX = newPosX;

        var maxHeight = _mapHeightInCells - _initialDragValues.Y;
        _transition.SizeY = Math.Min(Math.Max(1, _initialDragValues.Height + dy), maxHeight);
    });

    private void Bottom_DragDelta(object sender, DragDeltaEventArgs e) => HandleDrag(0, e.VerticalChange, (_, dy) =>
    {
        var maxHeight = _mapHeightInCells - _initialDragValues.Y;

        _transition.SizeY = Math.Min(Math.Max(1, _initialDragValues.Height + dy), maxHeight);
    });

    private void BottomRight_DragDelta(object sender, DragDeltaEventArgs e) => HandleDrag(e.HorizontalChange, e.VerticalChange, (dx, dy) =>
    {
        var maxWidth = _mapWidthInCells - _initialDragValues.X;
        var maxHeight = _mapHeightInCells - _initialDragValues.Y;

        _transition.SizeX = Math.Min(Math.Max(1, _initialDragValues.Width + dx), maxWidth);
        _transition.SizeY = Math.Min(Math.Max(1, _initialDragValues.Height + dy), maxHeight);
    });


    private void Move_DragDelta(object sender, DragDeltaEventArgs e) => HandleDrag(e.HorizontalChange, e.VerticalChange, (dx, dy) =>
    {
        _transition.PositionX = Math.Clamp(_initialDragValues.X + dx, 0, _mapWidthInCells - _initialDragValues.Width);
        _transition.PositionY = Math.Clamp(_initialDragValues.Y + dy, 0, _mapHeightInCells - _initialDragValues.Height);
    });

    protected override int VisualChildrenCount => _visuals.Count;
    protected override Visual GetVisualChild(int index) => _visuals[index];

    protected override Size ArrangeOverride(Size finalSize)
    {
        if (AdornedElement is not FrameworkElement adornedElement) return finalSize;

        var adornedWidth = adornedElement.ActualWidth;
        var adornedHeight = adornedElement.ActualHeight;
        if (adornedWidth == 0 || adornedHeight == 0) return finalSize;

        const double halfThumb = 5.0;

        _topLeft.Arrange(new Rect(-halfThumb, -halfThumb, 10, 10));
        _top.Arrange(new Rect((adornedWidth / 2) - halfThumb, -halfThumb, 10, 10));
        _topRight.Arrange(new Rect(adornedWidth - halfThumb, -halfThumb, 10, 10));
        _left.Arrange(new Rect(-halfThumb, (adornedHeight / 2) - halfThumb, 10, 10));
        _right.Arrange(new Rect(adornedWidth - halfThumb, (adornedHeight / 2) - halfThumb, 10, 10));
        _bottomLeft.Arrange(new Rect(-halfThumb, adornedHeight - halfThumb, 10, 10));
        _bottom.Arrange(new Rect((adornedWidth / 2) - halfThumb, adornedHeight - halfThumb, 10, 10));
        _bottomRight.Arrange(new Rect(adornedWidth - halfThumb, adornedHeight - halfThumb, 10, 10));
        _move.Arrange(new Rect(0, 0, adornedWidth, adornedHeight));

        return finalSize;
    }

}
