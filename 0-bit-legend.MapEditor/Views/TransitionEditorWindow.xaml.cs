using System.Windows;
using _0_bit_legend.MapEditor.ViewModels;
using _0_bit_legend.MapEditor.Models;

namespace _0_bit_legend.MapEditor.Views;

/// <summary>
/// Interaction logic for TransitionEditorWindow.xaml
/// </summary>
public partial class TransitionEditorWindow : Window
{
    public TransitionEditorWindow(TransitionData transition)
    {
        InitializeComponent();
        DataContext = new TransitionEditorViewModel(transition);
        (DataContext as TransitionEditorViewModel).RequestClose += (s, e) => this.Close();
    }
}
