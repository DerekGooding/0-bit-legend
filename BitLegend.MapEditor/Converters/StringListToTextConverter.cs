using System.Globalization;
using System.Windows.Data;

namespace BitLegend.MapEditor.Converters;

public class StringListToTextConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        => value is IEnumerable<string> stringList ? string.Join(Environment.NewLine, stringList) : value;

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
