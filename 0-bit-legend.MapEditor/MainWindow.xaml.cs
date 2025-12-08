using _0_bit_legend.MapEditor.ViewModels;
using System.Windows;

namespace _0_bit_legend.MapEditor
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }
    }
}

