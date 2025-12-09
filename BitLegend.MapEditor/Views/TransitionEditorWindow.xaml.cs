using BitLegend.MapEditor.Models;
using BitLegend.MapEditor.Services;
using BitLegend.MapEditor.ViewModels;

namespace BitLegend.MapEditor.Views;

/// <summary>
/// Interaction logic for TransitionEditorWindow.xaml
/// </summary>
public partial class TransitionEditorWindow : Window
{
    public TransitionEditorWindow(TransitionData transition, GameDataService gameDataService, int mapWidth, int mapHeight)
    {
        InitializeComponent();
        DataContext = new TransitionEditorViewModel(transition, gameDataService, mapWidth, mapHeight);
        (DataContext as TransitionEditorViewModel).RequestClose += (s, e) => this.Close();
    }
}
