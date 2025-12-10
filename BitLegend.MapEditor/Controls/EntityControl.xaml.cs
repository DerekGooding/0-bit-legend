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

    public EntityControl()
    {
        InitializeComponent();
        this.DataContextChanged += EntityControl_DataContextChanged;
    }

    private static void OnEntityDataChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is EntityControl entityControl && e.NewValue is EntityData newData)
        {
            entityControl.DataContext = newData;
            entityControl.UpdateLayoutFromEntityData(newData);
        }
    }

    private void EntityControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        if (e.NewValue is EntityData newData)
        {
            UpdateLayoutFromEntityData(newData);
        }
    }

    private void UpdateLayoutFromEntityData(EntityData data)
    {
        if (data != null)
        {
            // Set Width and Height based on EntityData properties
            this.Width = data.Width * 30; // Assuming 30 pixels per cell
            this.Height = data.Height * 30; // Assuming 30 pixels per cell

            // Set Canvas.Left and Canvas.Top based on EntityData properties
            Canvas.SetLeft(this, data.X * 30);
            Canvas.SetTop(this, data.Y * 30);
        }
    }
}
