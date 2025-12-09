using System.Windows;
using _0_bit_legend.MapEditor.ViewModels;
using _0_bit_legend.MapEditor.Models;
using _0_bit_legend.MapEditor.Services;

namespace _0_bit_legend.MapEditor.Views;

/// <summary>
/// Interaction logic for EntityEditorWindow.xaml
/// </summary>
public partial class EntityEditorWindow : Window
{
    public EntityEditorWindow(EntityData entity, GameDataService gameDataService, int mapWidth, int mapHeight)
    {
        InitializeComponent();
        DataContext = new EntityEditorViewModel(entity, gameDataService, mapWidth, mapHeight);
        (DataContext as EntityEditorViewModel).RequestClose += (s, e) => this.Close();
    }
}
