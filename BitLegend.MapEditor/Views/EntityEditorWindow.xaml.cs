using BitLegend.MapEditor.Models;
using BitLegend.MapEditor.Services;
using BitLegend.MapEditor.ViewModels;

namespace BitLegend.MapEditor.Views;

/// <summary>
/// Interaction logic for EntityEditorWindow.xaml
/// </summary>
public partial class EntityEditorWindow : Window
{
    public EntityEditorWindow(EntityData entity, GameDataService gameDataService, int mapWidth, int mapHeight)
    {
        InitializeComponent();
        DataContext = new EntityEditorViewModel(entity, gameDataService, mapWidth, mapHeight);
        (DataContext as EntityEditorViewModel)?.RequestClose += (s, e) => Close();
    }
}
