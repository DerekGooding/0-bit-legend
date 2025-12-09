using System.Globalization;
using System.Windows.Data;

namespace BitLegend.MapEditor.Converters;

public class PositionConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        => values.Length < 3 ||
            !double.TryParse(values[0].ToString(), out var pos) ||
            !double.TryParse(values[1].ToString(), out var totalSize) ||
            !int.TryParse(values[2].ToString(), out var count)
            ? 0
            : count == 0 ? 0 : (object)((pos * totalSize) / count);

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
