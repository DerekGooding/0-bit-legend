namespace BitLegend.MapEditor;

public static class ThemeManager
{
    private const string _lightThemeSource = "/BitLegend.MapEditor;component/Themes/LightTheme.xaml";
    private const string _darkThemeSource = "/BitLegend.MapEditor;component/Themes/DarkTheme.xaml";

    public static bool IsDarkMode
    {
        get;
        private set
        {
            if (field != value)
            {
                field = value;
            }
        }
    } = false;

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
            d => d.Source != null && (d.Source.OriginalString == _lightThemeSource || d.Source.OriginalString == _darkThemeSource));

        if (oldTheme != null)
        {
            dictionaries.Remove(oldTheme);
        }

        var newThemeSource = isDark ? _darkThemeSource : _lightThemeSource;
        dictionaries.Add(new ResourceDictionary() { Source = new Uri(newThemeSource, UriKind.RelativeOrAbsolute) });
    }
}


