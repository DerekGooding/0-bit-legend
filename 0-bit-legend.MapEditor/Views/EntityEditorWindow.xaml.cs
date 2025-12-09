using System.Windows;
using _0_bit_legend.MapEditor.ViewModels;
using _0_bit_legend.MapEditor.Models;

namespace _0_bit_legend.MapEditor.Views;

/// <summary>
/// Interaction logic for EntityEditorWindow.xaml
/// </summary>
public partial class EntityEditorWindow : Window
{
    public EntityEditorWindow(EntityData entity)
    {
        InitializeComponent();
        DataContext = new EntityEditorViewModel(entity);
        (DataContext as EntityEditorViewModel).RequestClose += (s, e) => this.Close();
    }
}
