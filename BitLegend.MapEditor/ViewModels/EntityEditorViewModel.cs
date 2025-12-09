using System.ComponentModel;
using System.Text.RegularExpressions;
using BitLegend.MapEditor.Models;
using BitLegend.MapEditor.Services;

namespace BitLegend.MapEditor.ViewModels;

/// <summary>
/// ViewModel for the Entity Editor window, handling logic and validation for <see cref="EntityData"/>.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="EntityEditorViewModel"/> class.
/// </remarks>
/// <param name="entity">The <see cref="EntityData"/> to edit.</param>
/// <param name="gameDataService">The game data service for validation lookups.</param>
/// <param name="mapWidth">The width of the map for position validation.</param>
/// <param name="mapHeight">The height of the map for position validation.</param>
[ViewModel]
public partial class EntityEditorViewModel(EntityData entity,
                                           GameDataService gameDataService,
                                           int mapWidth,
                                           int mapHeight) : IDataErrorInfo
{
    private readonly GameDataService _gameDataService = gameDataService ?? throw new ArgumentNullException(nameof(gameDataService));
    private readonly int _mapWidth = mapWidth;
    private readonly int _mapHeight = mapHeight;

    /// <summary>
    /// Event to request the closing of the associated view.
    /// </summary>
    public event EventHandler? RequestClose;

    /// <summary>
    /// Gets or sets the <see cref="EntityData"/> being edited.
    /// </summary>
    [Bind] private EntityData? _entity = entity ?? throw new ArgumentNullException(nameof(entity));

    /// <summary>
    /// Gets a list of valid entity type names from the game data service.
    /// </summary>
    public List<string> ValidEntityTypes => _gameDataService.ValidEntityTypes;

    public bool CanExecuteSave() => !HasErrors;
    [Command(CanExecuteMethodName = nameof(CanExecuteSave))] public void EntitySave()
    {
        if (!HasErrors)
        {
            RequestClose?.Invoke(this, EventArgs.Empty);
        }
    }

    [Command] public void EntityCancel()
    {
        Entity = null;
        RequestClose?.Invoke(this, EventArgs.Empty);
    }

    #region IDataErrorInfo Implementation
    /// <summary>
    /// Gets an error message indicating what is wrong with this object.
    /// </summary>
    public string? Error => null;

    /// <summary>
    /// Gets the error message for the property with the given name.
    /// </summary>
    /// <param name="columnName">The name of the property to validate.</param>
    /// <returns>The error message, or null if there is no error.</returns>
    public string? this[string? columnName] // Made parameter nullable
    {
        get
        {
            string? result = null;
            if (Entity == null) return null;

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
                    else if (!ParseFlages().IsMatch(Entity.Condition.Trim()))
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
    public bool HasErrors => Entity != null
                && (!string.IsNullOrEmpty(this[nameof(Entity.EntityType)]) ||
                   !string.IsNullOrEmpty(this[nameof(Entity.X)]) ||
                   !string.IsNullOrEmpty(this[nameof(Entity.Y)]) ||
                   !string.IsNullOrEmpty(this[nameof(Entity.Condition)]));

    [GeneratedRegex(@"^[\w\d\s\.\(\)\=\!\<\>\&\|]+$")]
    private static partial Regex ParseFlages();
    #endregion
}
