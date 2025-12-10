using System.Windows;
using System.Windows.Controls;
using BitLegend.MapEditor.Model;

namespace BitLegend.MapEditor.Controls;

/// <summary>
/// Interaction logic for EntityControl.xaml
/// </summary>
public partial class EntityControl : UserControl
{
    public static readonly DependencyProperty EntityDataProperty =
        DependencyProperty.Register(nameof(EntityData), typeof(EntityData), typeof(EntityControl), new PropertyMetadata(null, OnEntityDataChanged));

    public EntityData EntityData
    {
        get => (EntityData)GetValue(EntityDataProperty);
        set => SetValue(EntityDataProperty, value);
    }

    public static readonly DependencyProperty CellSizeProperty =
        DependencyProperty.Register(nameof(CellSize), typeof(double), typeof(EntityControl), new PropertyMetadata(16.0, OnCellSizeChanged));

    public double CellSize
    {
        get => (double)GetValue(CellSizeProperty);
        set => SetValue(CellSizeProperty, value);
    }

    public EntityControl()
    {
        InitializeComponent();
        // Removed: this.DataContextChanged += EntityControl_DataContextChanged;
    }

    private static void OnEntityDataChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is EntityControl entityControl && e.NewValue is EntityData newData)
        {
            entityControl.DataContext = newData;
            entityControl.UpdateLayoutFromEntityData(newData);
        }
    }

    private static void OnCellSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is EntityControl entityControl && entityControl.EntityData != null)
        {
            entityControl.UpdateLayoutFromEntityData(entityControl.EntityData);
        }
    }

    private void UpdateLayoutFromEntityData(EntityData data)
    {
        if (data != null)
        {
            // Set Width and Height based on EntityData properties
            this.Width = data.Width * CellSize;
            this.Height = data.Height * CellSize;

            // Set Canvas.Left and Canvas.Top based on EntityData properties
            Canvas.SetLeft(this, data.X * CellSize);
            Canvas.SetTop(this, data.Y * CellSize);
        }
    }
}
