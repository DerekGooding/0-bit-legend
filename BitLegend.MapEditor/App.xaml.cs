using BitLegend.MapEditor;
using System.Windows.Threading;


namespace _0_bit_legend.MapEditor;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    //private static readonly Host _host = Host.Initialize();
    //private T Get<T>() where T : class => _host.Get<T>();

    public App()
    {
        DispatcherUnhandledException += App_DispatcherUnhandledException;
        AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
    }

    private void Application_Startup(object sender, StartupEventArgs e) => ThemeManager.ApplyTheme(true);

    private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
    {
        var errorMessage = $"An unhandled exception occurred (UI Thread): {e.Exception.Message}\n\n" +
                              $"Please contact support with the following details:\n{e.Exception.ToString()}";
        MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);

        e.Handled = true;
    }

    private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        if (e.ExceptionObject is Exception ex)
        {
            // Log the exception
            var errorMessage = $"An unhandled exception occurred (Non-UI Thread): {ex.Message}\n\n" +
                                  $"Please contact support with the following details:\n{ex.ToString()}";
            MessageBox.Show(errorMessage, "Fatal Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        else
        {
            MessageBox.Show("An unknown fatal error occurred.", "Fatal Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}


