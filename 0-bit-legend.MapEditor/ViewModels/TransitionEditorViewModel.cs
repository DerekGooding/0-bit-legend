using _0_bit_legend.MapEditor.Models;
using _0_bit_legend.MapEditor.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace _0_bit_legend.MapEditor.ViewModels;

/// <summary>
/// ViewModel for the Transition Editor window, handling logic and validation for <see cref="TransitionData"/>.
/// </summary>
public class TransitionEditorViewModel : INotifyPropertyChanged, IDataErrorInfo
{
    private readonly GameDataService _gameDataService;
    private readonly int _mapWidth;
    private readonly int _mapHeight;

    /// <summary>
    /// Event to request the closing of the associated view.
    /// </summary>
    public event EventHandler? RequestClose; // Made nullable

    private TransitionData? _transition; // Made nullable
    /// <summary>
    /// Gets or sets the <see cref="TransitionData"/> being edited.
    /// </summary>
    public TransitionData? Transition
    {
        get => _transition;
        set
        {
            _transition = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Gets a list of valid map IDs from the game data service.
    /// </summary>
    public List<string> ValidMapIds => _gameDataService.ValidMapIds;

    /// <summary>
    /// Gets a list of valid direction types from the game data service.
    /// </summary>
    public List<string> ValidDirectionTypes => _gameDataService.ValidDirectionTypes;

    /// <summary>
    /// Command to save the transition data.
    /// </summary>
    public ICommand SaveCommand { get; }

    /// <summary>
    /// Command to cancel the editing process.
    /// </summary>
    public ICommand CancelCommand { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="TransitionEditorViewModel"/> class.
    /// </summary>
    /// <param name="transition">The <see cref="TransitionData"/> to edit.</param>
    /// <param name="gameDataService">The game data service for validation lookups.</param>
    /// <param name="mapWidth">The width of the map for position and size validation.</param>
    /// <param name="mapHeight">The height of the map for position and size validation.</param>
    public TransitionEditorViewModel(TransitionData transition, GameDataService gameDataService, int mapWidth, int mapHeight)
    {
        _transition = transition ?? throw new ArgumentNullException(nameof(transition)); // Null check
        _gameDataService = gameDataService ?? throw new ArgumentNullException(nameof(gameDataService)); // Null check
        _mapWidth = mapWidth;
        _mapHeight = mapHeight;
        SaveCommand = new RelayCommand(ExecuteSave, CanExecuteSave);
        CancelCommand = new RelayCommand(ExecuteCancel);
    }

    private bool CanExecuteSave(object? parameter) => !HasErrors; // Made parameter nullable

    private void ExecuteSave(object? parameter) // Made parameter nullable
    {
        if (!HasErrors)
        {
            RequestClose?.Invoke(this, EventArgs.Empty);
        }
    }

    private void ExecuteCancel(object? parameter) // Made parameter nullable
    {
        Transition = null; // Indicate cancellation by nulling the transition
        RequestClose?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Event that is raised when a property value changes.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged; // Made nullable

    /// <summary>
    /// Raises the <see cref="PropertyChanged"/> event.
    /// </summary>
    /// <param name="propertyName">The name of the property that changed.</param>
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); // Made parameter nullable

    #region IDataErrorInfo Implementation
    /// <summary>
    /// Gets an error message indicating what is wrong with this object.
    /// </summary>
    public string? Error => null; // Made nullable

    /// <summary>
    /// Gets the error message for the property with the given name.
    /// </summary>
    /// <param name="columnName">The name of the property to validate.</param>
    /// <returns>The error message, or null if there is no error.</returns>
    public string? this[string? columnName] // Made parameter nullable
    {
        get
        {
            string? result = null; // Made nullable
            if (Transition == null) return null; // Defensive check if transition is null (e.g., after cancel)

            switch (columnName)
            {
                case nameof(Transition.MapId):
                    if (string.IsNullOrWhiteSpace(Transition.MapId))
                    {
                        result = "Map ID cannot be empty.";
                    }
                    else if (!_gameDataService.ValidMapIds.Contains(Transition.MapId))
                    {
                        result = "Invalid Map ID.";
                    }
                    break;
                case nameof(Transition.StartPositionX):
                    if (Transition.StartPositionX < 0 || Transition.StartPositionX >= _mapWidth)
                    {
                        result = $"Start Position X must be between 0 and {_mapWidth - 1}.";
                    }
                    break;
                case nameof(Transition.StartPositionY):
                    if (Transition.StartPositionY < 0 || Transition.StartPositionY >= _mapHeight)
                    {
                        result = $"Start Position Y must be between 0 and {_mapHeight - 1}.";
                    }
                    break;
                case nameof(Transition.DirectionType):
                    if (string.IsNullOrWhiteSpace(Transition.DirectionType))
                    {
                        result = "Direction Type cannot be empty.";
                    }
                    else if (!_gameDataService.ValidDirectionTypes.Contains(Transition.DirectionType))
                    {
                        result = "Invalid Direction Type.";
                    }
                    break;
                case nameof(Transition.SizeX):
                    if (Transition.SizeX <= 0 || (Transition.PositionX + Transition.SizeX > _mapWidth && Transition.PositionX < _mapWidth))
                    {
                        // Calculate max allowed size for informative message, ensuring PositionX is within bounds
                        int maxAllowedSizeX = (Transition.PositionX >= 0 && Transition.PositionX < _mapWidth)
                            ? _mapWidth - Transition.PositionX : 0;
                        
                        result = $"Size X must be positive and not exceed map bounds (max: {maxAllowedSizeX}).";
                    }
                    break;
                case nameof(Transition.SizeY):
                    if (Transition.SizeY <= 0 || (Transition.PositionY + Transition.SizeY > _mapHeight && Transition.PositionY < _mapHeight))
                    {
                        // Calculate max allowed size for informative message, ensuring PositionY is within bounds
                        int maxAllowedSizeY = (Transition.PositionY >= 0 && Transition.PositionY < _mapHeight)
                            ? _mapHeight - Transition.PositionY : 0;
                        result = $"Size Y must be positive and not exceed map bounds (max: {maxAllowedSizeY}).";
                    }
                    break;
                case nameof(Transition.PositionX):
                    if (Transition.PositionX < 0 || Transition.PositionX >= _mapWidth)
                    {
                        result = $"Position X must be between 0 and {_mapWidth - 1}.";
                    }
                    break;
                case nameof(Transition.PositionY):
                    if (Transition.PositionY < 0 || Transition.PositionY >= _mapHeight)
                    {
                        result = $"Position Y must be between 0 and {_mapHeight - 1}.";
                    }
                    break;
            }
            return result;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the object has validation errors.
    /// </summary>
    public bool HasErrors
    {
        get
        {
            // Explicitly check for null transition
            if (Transition == null) return false;

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
