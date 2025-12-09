using System.Windows.Controls;
using System.Windows.Input;
using System.Text.RegularExpressions;

namespace BitLegend.MapEditor.Controls;

/// <summary>
/// Interaction logic for NumericUpDown.xaml
/// </summary>
public partial class NumericUpDown : UserControl
{
    public static readonly DependencyProperty ValueProperty =
        DependencyProperty.Register("Value", typeof(int), typeof(NumericUpDown),
            new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnValueChanged));

    public static readonly DependencyProperty MinimumProperty =
        DependencyProperty.Register("Minimum", typeof(int), typeof(NumericUpDown),
            new FrameworkPropertyMetadata(int.MinValue, OnMinimumChanged));

    public static readonly DependencyProperty MaximumProperty =
        DependencyProperty.Register("Maximum", typeof(int), typeof(NumericUpDown),
            new FrameworkPropertyMetadata(int.MaxValue, OnMaximumChanged));

    public int Value
    {
        get => (int)GetValue(ValueProperty); set => SetValue(ValueProperty, value);
    }

    public int Minimum
    {
        get => (int)GetValue(MinimumProperty); set => SetValue(MinimumProperty, value);
    }

    public int Maximum
    {
        get => (int)GetValue(MaximumProperty); set => SetValue(MaximumProperty, value);
    }

    public NumericUpDown()
    {
        InitializeComponent();
        ValueTextBox.Text = Value.ToString(); // Initialize text box with default value
    }

    private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var control = (NumericUpDown)d;
        control.ValueTextBox.Text = control.Value.ToString();
        control.CoerceValue(MinimumProperty);
        control.CoerceValue(MaximumProperty);
    }

    private static void OnMinimumChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        => d.CoerceValue(ValueProperty);

    private static void OnMaximumChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        => d.CoerceValue(ValueProperty);

    private void IncreaseButton_Click(object sender, RoutedEventArgs e)
    {
        if (Value < Maximum)
        {
            Value++;
        }
    }

    private void DecreaseButton_Click(object sender, RoutedEventArgs e)
    {
        if (Value > Minimum)
        {
            Value--;
        }
    }

    private void ValueTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e) =>
        e.Handled = !IsTextAllowed(e.Text);

    private static bool IsTextAllowed(string text) => IsText().IsMatch(text);

    private void ValueTextBox_LostFocus(object sender, RoutedEventArgs e)
    {
        if (int.TryParse(ValueTextBox.Text, out var parsedValue))
        {
            Value = Math.Clamp(parsedValue, Minimum, Maximum);
        }
        else
        {
            Value = Math.Clamp(Value, Minimum, Maximum); // Revert to current valid value
            ValueTextBox.Text = Value.ToString();
        }
    }

    [GeneratedRegex(@"^\d*$")]
    private static partial Regex IsText();
}
