using System.Windows.Documents;
using System.Windows.Media;

namespace BitLegend.MapEditor.Adorners;

public class SelectionAdorner : Adorner
{
    private Point? _startPoint;
    private Point? _endPoint;
    private readonly Pen _pen;
    private readonly Brush _fillBrush;

    public SelectionAdorner(UIElement adornedElement) : base(adornedElement)
    {
        _pen = new Pen(Brushes.Red, 1.5) { DashStyle = DashStyles.Dash };
        _fillBrush = new SolidColorBrush(Colors.LightBlue) { Opacity = 0.3 };
    }

    public void UpdateSelection(Point startPoint, Point endPoint)
    {
        _startPoint = startPoint;
        _endPoint = endPoint;
        InvalidateVisual(); // Redraw the adorner
    }

    public void ClearSelection()
    {
        _startPoint = null;
        _endPoint = null;
        InvalidateVisual();
    }

    protected override void OnRender(DrawingContext drawingContext)
    {
        base.OnRender(drawingContext);

        if (_startPoint.HasValue && _endPoint.HasValue)
        {
            var selectionRect = new Rect(_startPoint.Value, _endPoint.Value);
            drawingContext.DrawRectangle(_fillBrush, _pen, selectionRect);
        }
    }
}