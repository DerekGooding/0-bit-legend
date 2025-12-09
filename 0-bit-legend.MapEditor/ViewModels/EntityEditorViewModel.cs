using _0_bit_legend.MapEditor.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Text.RegularExpressions; // Added for Regex validation

namespace _0_bit_legend.MapEditor.ViewModels;

public class EntityEditorViewModel : INotifyPropertyChanged, IDataErrorInfo
{
    public event EventHandler RequestClose;

    private EntityData _entity;
    public EntityData Entity
    {
        get => _entity;
        set
        {
            _entity = value;
            OnPropertyChanged();
        }
    }

    public ICommand SaveCommand { get; }
    public ICommand CancelCommand { get; }

    public EntityEditorViewModel(EntityData entity)
    {
        Entity = entity;
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
        Entity = null; // Indicate cancellation by nulling the entity
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
                case nameof(Entity.EntityType):
                    if (string.IsNullOrWhiteSpace(Entity.EntityType))
                    {
                        result = "Entity Type cannot be empty.";
                    }
                    // Optionally, check if it matches a known entity type
                    // For now, allow any string as we don't have a comprehensive list here
                    break;
                case nameof(Entity.X):
                    if (Entity.X < 0)
                    {
                        result = "X position cannot be negative.";
                    }
                    // Further validation can be added here (e.g., within map bounds)
                    break;
                case nameof(Entity.Y):
                    if (Entity.Y < 0)
                    {
                        result = "Y position cannot be negative.";
                    }
                    // Further validation can be added here (e.g., within map bounds)
                    break;
                case nameof(Entity.Condition):
                    if (string.IsNullOrWhiteSpace(Entity.Condition))
                    {
                        result = "Condition cannot be empty. Use 'true' for always active.";
                    }
                    // Basic check for valid C# boolean expression format
                    // This is a more robust check allowing for identifiers, dot notation, operators, and parentheses.
                    // It's still a heuristic and not a full C# parser.
                    else if (!Regex.IsMatch(Entity.Condition.Trim(), @"^[\w\d\s\.\(\)\=\!\<\>\&\|]+$"))
                    {
                         result = "Condition must be a valid C# boolean expression (e.g., 'true', 'Hero.HasSword', 'GameFlag.KeyCollected == true', 'GameManager.IsFlagTrue(GameFlag.VisitedCave0)').";
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
            // Check all properties for errors
            return !string.IsNullOrEmpty(this[nameof(Entity.EntityType)]) ||
                   !string.IsNullOrEmpty(this[nameof(Entity.X)]) ||
                   !string.IsNullOrEmpty(this[nameof(Entity.Y)]) ||
                   !string.IsNullOrEmpty(this[nameof(Entity.Condition)]);
        }
    }
    #endregion
}
