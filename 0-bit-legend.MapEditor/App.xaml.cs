using System.Windows;

namespace _0_bit_legend.MapEditor;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{

    public App()
    {
        // ThemeManager.ApplyTheme(true); // Moved to Startup event
    }

    private void Application_Startup(object sender, StartupEventArgs e)
    {
        ThemeManager.ApplyTheme(true); // Apply dark theme at startup
    }
}


