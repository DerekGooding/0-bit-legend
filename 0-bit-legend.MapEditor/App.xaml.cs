using System.Windows;
using System.Windows.Threading; // Added for DispatcherUnhandledException
using System; // Added for AppDomain

namespace _0_bit_legend.MapEditor;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{

    public App()
    {
        // ThemeManager.ApplyTheme(true); // Moved to Startup event
        this.DispatcherUnhandledException += App_DispatcherUnhandledException;
        AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
    }

    private void Application_Startup(object sender, StartupEventArgs e)
    {
        ThemeManager.ApplyTheme(true); // Apply dark theme at startup
    }

    private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
    {
        // Log the exception (e.g., to a file, console, or a logging service)
        // For demonstration, we'll just show a MessageBox
        string errorMessage = $"An unhandled exception occurred (UI Thread): {e.Exception.Message}\n\n" +
                              $"Please contact support with the following details:\n{e.Exception.ToString()}";
        MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);

        // Prevent the application from crashing
        e.Handled = true;
    }

    private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        Exception ex = e.ExceptionObject as Exception;
        if (ex != null)
        {
            // Log the exception
            string errorMessage = $"An unhandled exception occurred (Non-UI Thread): {ex.Message}\n\n" +
                                  $"Please contact support with the following details:\n{ex.ToString()}";
            MessageBox.Show(errorMessage, "Fatal Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        else
        {
            MessageBox.Show("An unknown fatal error occurred.", "Fatal Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        // It is generally not recommended to continue application execution after a non-UI thread unhandled exception.
        // The application will terminate shortly after this handler runs.
    }
}


