using _0_bit_legend.MapEditor.Models;
using _0_bit_legend.MapEditor.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Text.RegularExpressions;

namespace _0_bit_legend.MapEditor.ViewModels;

/// <summary>
/// ViewModel for the Entity Editor window, handling logic and validation for <see cref="EntityData"/>.
/// </summary>
public class EntityEditorViewModel : INotifyPropertyChanged, IDataErrorInfo
{
    private readonly GameDataService _gameDataService;
    private readonly int _mapWidth;
    private readonly int _mapHeight;

    /// <summary>
    /// Event to request the closing of the associated view.
    /// </summary>
    public event EventHandler? RequestClose; // Made nullable

    private EntityData? _entity; // Made nullable
    /// <summary>
    /// Gets or sets the <see cref="EntityData"/> being edited.
    /// </summary>
    public EntityData? Entity
    {
        get => _entity;
        set
        {
            _entity = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Gets a list of valid entity type names from the game data service.
    /// </summary>
    public List<string> ValidEntityTypes => _gameDataService.ValidEntityTypes;

    /// <summary>
    /// Command to save the entity data.
    /// </summary>
    public ICommand SaveCommand { get; }

    /// <summary>
    /// Command to cancel the editing process.
    /// </summary>
    public ICommand CancelCommand { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="EntityEditorViewModel"/> class.
    /// </summary>
    /// <param name="entity">The <see cref="EntityData"/> to edit.</param>
    /// <param name="gameDataService">The game data service for validation lookups.</param>
    /// <param name="mapWidth">The width of the map for position validation.</param>
    /// <param name="mapHeight">The height of the map for position validation.</param>
    public EntityEditorViewModel(EntityData entity, GameDataService gameDataService, int mapWidth, int mapHeight)
    {
        _entity = entity ?? throw new ArgumentNullException(nameof(entity)); // Null check
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
        Entity = null; // Indicate cancellation by nulling the entity
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
            if (Entity == null) return null; // Defensive check if entity is null (e.g., after cancel)

            switch (columnName)
            {
                case nameof(Entity.EntityType):
                    if (string.IsNullOrWhiteSpace(Entity.EntityType))
                    {
                        result = "Entity Type cannot be empty.";
                    }
                    else if (!_gameDataService.ValidEntityTypes.Contains(Entity.EntityType))
                    {
                        result = "Invalid Entity Type.";
                    }
                    break;
                case nameof(Entity.X):
                    if (Entity.X < 0 || Entity.X >= _mapWidth)
                    {
                        result = $"X position must be between 0 and {_mapWidth - 1}.";
                    }
                    break;
                case nameof(Entity.Y):
                    if (Entity.Y < 0 || Entity.Y >= _mapHeight)
                    {
                        result = $"Y position must be between 0 and {_mapHeight - 1}.";
                    }
                    break;
                case nameof(Entity.Condition):
                    if (string.IsNullOrWhiteSpace(Entity.Condition))
                    {
                        result = "Condition cannot be empty. Use 'true' for always active.";
                    }
                    else if (!Regex.IsMatch(Entity.Condition.Trim(), @"^[\w\d\s\.\(\)\=\!\<\>\&\|]+$"))
                    {
                         result = "Condition must be a valid C# boolean expression (e.g., 'true', 'Hero.HasSword', 'GameFlag.KeyCollected == true', 'GameManager.IsFlagTrue(GameFlag.VisitedCave0)').";
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
            // Explicitly check for null entity
            if (Entity == null) return false;

            return !string.IsNullOrEmpty(this[nameof(Entity.EntityType)]) ||
                   !string.IsNullOrEmpty(this[nameof(Entity.X)]) ||
                   !string.IsNullOrEmpty(this[nameof(Entity.Y)]) ||
                   !string.IsNullOrEmpty(this[nameof(Entity.Condition)]);
        }
    }
    #endregion
}
