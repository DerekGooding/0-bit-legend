namespace BitLegend.MapEditor.ViewModels;

[ViewModel]
public partial class MapCharacterViewModel(char character, int x, int y)
{
    [Bind] private char _character = character;

    public int X { get; } = x;
    public int Y { get; } = y;
}
