using _0_bit_legend.MapEditor.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace _0_bit_legend.MapEditor.ViewModels;

public class TransitionEditorViewModel : INotifyPropertyChanged, IDataErrorInfo
{
    public event EventHandler RequestClose;

    private TransitionData _transition;
    public TransitionData Transition
    {
        get => _transition;
        set
        {
            _transition = value;
            OnPropertyChanged();
        }
    }

    public ICommand SaveCommand { get; }
    public ICommand CancelCommand { get; }

    public TransitionEditorViewModel(TransitionData transition)
    {
        Transition = transition;
        SaveCommand = new RelayCommand(ExecuteSave, CanExecuteSave);
        CancelCommand = new RelayCommand(ExecuteCancel);
    }

    private bool CanExecuteSave(object parameter) => !HasErrors;

    private void ExecuteSave(object parameter)
    {
        if (!HasErrors)
        {
            RequestClose?.Invoke(this, EventArgs.Empty);
        }
    }

    private void ExecuteCancel(object parameter)
    {
        Transition = null; // Indicate cancellation by nulling the transition
        RequestClose?.Invoke(this, EventArgs.Empty);
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    #region IDataErrorInfo Implementation
    public string Error => null; // Class-level error, not used in this scenario

    public string this[string columnName]
    {
        get
        {
            string result = null;
            switch (columnName)
            {
                case nameof(Transition.MapId):
                    if (string.IsNullOrWhiteSpace(Transition.MapId))
                    {
                        result = "Map ID cannot be empty.";
                    }
                    // Further validation can be added (e.g., check if MapId exists)
                    break;
                case nameof(Transition.StartPositionX):
                    if (Transition.StartPositionX < 0)
                    {
                        result = "Start Position X cannot be negative.";
                    }
                    break;
                case nameof(Transition.StartPositionY):
                    if (Transition.StartPositionY < 0)
                    {
                        result = "Start Position Y cannot be negative.";
                    }
                    break;
                case nameof(Transition.DirectionType):
                    if (string.IsNullOrWhiteSpace(Transition.DirectionType))
                    {
                        result = "Direction Type cannot be empty.";
                    }
                    // Further validation (e.g., check if it's a valid DirectionType enum value)
                    break;
                case nameof(Transition.SizeX):
                    if (Transition.SizeX <= 0)
                    {
                        result = "Size X must be positive.";
                    }
                    break;
                case nameof(Transition.SizeY):
                    if (Transition.SizeY <= 0)
                    {
                        result = "Size Y must be positive.";
                    }
                    break;
                case nameof(Transition.PositionX):
                    if (Transition.PositionX < 0)
                    {
                        result = "Position X cannot be negative.";
                    }
                    break;
                case nameof(Transition.PositionY):
                    if (Transition.PositionY < 0)
                    {
                        result = "Position Y cannot be negative.";
                    }
                    break;
            }
            return result;
        }
    }

    public bool HasErrors
    {
        get
        {
            return !string.IsNullOrEmpty(this[nameof(Transition.MapId)]) ||
                   !string.IsNullOrEmpty(this[nameof(Transition.StartPositionX)]) ||
                   !string.IsNullOrEmpty(this[nameof(Transition.StartPositionY)]) ||
                   !string.IsNullOrEmpty(this[nameof(Transition.DirectionType)]) ||
                   !string.IsNullOrEmpty(this[nameof(Transition.SizeX)]) ||
                   !string.IsNullOrEmpty(this[nameof(Transition.SizeY)]) ||
                   !string.IsNullOrEmpty(this[nameof(Transition.PositionX)]) ||
                   !string.IsNullOrEmpty(this[nameof(Transition.PositionY)]);
        }
    }
    #endregion
}
