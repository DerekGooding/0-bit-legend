using System.Windows;

namespace _0_bit_legend.MapEditor;

public static class ThemeManager
{
    private const string LightThemeSource = "/0-bit-legend.MapEditor;component/Themes/LightTheme.xaml";
    private const string DarkThemeSource = "/0-bit-legend.MapEditor;component/Themes/DarkTheme.xaml";

    private static bool _isDarkMode = false;
    public static bool IsDarkMode
    {
        get => _isDarkMode;
        private set
        {
            if (_isDarkMode != value)
            {
                _isDarkMode = value;
            }
        }
    }

    public static void ToggleTheme()
    {
        IsDarkMode = !IsDarkMode;
        ApplyTheme(IsDarkMode);
    }

    public static void ApplyTheme(bool isDark)
    {
        IsDarkMode = isDark;
        var dictionaries = Application.Current.Resources.MergedDictionaries;
        var oldTheme = dictionaries.FirstOrDefault(
            d => d.Source != null && (d.Source.OriginalString == LightThemeSource || d.Source.OriginalString == DarkThemeSource));

        if (oldTheme != null)
        {
            dictionaries.Remove(oldTheme);
        }

        var newThemeSource = isDark ? DarkThemeSource : LightThemeSource;
        dictionaries.Add(new ResourceDictionary() { Source = new System.Uri(newThemeSource, System.UriKind.RelativeOrAbsolute) });
    }
}


