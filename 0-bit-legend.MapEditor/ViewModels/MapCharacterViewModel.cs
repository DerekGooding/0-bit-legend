using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace _0_bit_legend.MapEditor.ViewModels;

public class MapCharacterViewModel : INotifyPropertyChanged
{
    private char _character;
    public char Character
    {
        get => _character;
        set
        {
            if (_character != value)
            {
                _character = value;
                OnPropertyChanged();
            }
        }
    }

    public int X { get; }
    public int Y { get; }

    public MapCharacterViewModel(char character, int x, int y)
    {
        Character = character;
        X = x;
        Y = y;
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
