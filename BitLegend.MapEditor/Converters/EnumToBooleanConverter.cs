using System.Globalization;
using System.Windows.Data;

namespace BitLegend.MapEditor.Converters;

public class EnumToBooleanConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null || parameter == null)
        {
            return false;
        }

        var enumValue = value.ToString();
        var parameterValue = parameter.ToString();

        return enumValue.Equals(parameterValue, StringComparison.InvariantCultureIgnoreCase);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => value == null || parameter == null
            ? Binding.DoNothing
            : value is bool boolValue && boolValue ? Enum.Parse(targetType, parameter.ToString()) : Binding.DoNothing;
}
