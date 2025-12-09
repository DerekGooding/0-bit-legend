using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace BitLegend.MapEditor.Converters;

public class ValidationErrorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var errors = value as System.Collections.ObjectModel.ReadOnlyObservableCollection<ValidationError>;
        return errors?.Count > 0 ? errors[0].ErrorContent : string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}