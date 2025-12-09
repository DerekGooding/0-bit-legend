using _0_bit_legend.MapEditor.Models;
using _0_bit_legend.MapEditor.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows;
using System.IO;

namespace _0_bit_legend.MapEditor.ViewModels;

/// <summary>
/// ViewModel for the main application window, managing map data, entities, and transitions.
/// </summary>
public class MainWindowViewModel : INotifyPropertyChanged
{
    private ObservableCollection<MapData> _maps = [];
    public ObservableCollection<MapData> Maps
    {
        get => _maps;
        set
        {
            _maps = value;
            OnPropertyChanged();
        }
    }

    private MapData? _selectedMap; // Made nullable
    /// <summary>
    /// Gets or sets the currently selected map.
    /// </summary>
    public MapData? SelectedMap
    {
        get => _selectedMap;
        set
        {
            if (!Equals(_selectedMap, value)) // Use Equals for object comparison
            {
                _selectedMap = value;
                OnPropertyChanged();
                PopulateDisplayMapCharacters();
                // Update entity and transition collections when map changes
                OnPropertyChanged(nameof(SelectedMap.EntityLocations));
                OnPropertyChanged(nameof(SelectedMap.AreaTransitions));
            }
        }
    }

    private ObservableCollection<ObservableCollection<MapCharacterViewModel>> _displayMapCharacters = [];
    /// <summary>
    /// Gets or sets the 2D collection of <see cref="MapCharacterViewModel"/> for display.
    /// </summary>
    public ObservableCollection<ObservableCollection<MapCharacterViewModel>> DisplayMapCharacters
    {
        get => _displayMapCharacters;
        set
        {
            _displayMapCharacters = value;
            OnPropertyChanged();
        }
    }

    private EntityData? _selectedEntity;
    /// <summary>
    /// Gets or sets the currently selected entity.
    /// </summary>
    public EntityData? SelectedEntity
    {
        get => _selectedEntity;
        set
        {
            _selectedEntity = value;
            OnPropertyChanged();
        }
    }

    private TransitionData? _selectedTransition;
    /// <summary>
    /// Gets or sets the currently selected transition.
    /// </summary>
    public TransitionData? SelectedTransition
    {
        get => _selectedTransition;
        set
        {
            _selectedTransition = value;
            OnPropertyChanged();
        }
    }

    private char _currentDrawingCharacter = 'X'; // Default drawing character
    /// <summary>
    /// Gets or sets the character used for drawing on the map.
    /// </summary>
    public char CurrentDrawingCharacter
    {
        get => _currentDrawingCharacter;
        set
        {
            if (_currentDrawingCharacter != value)
            {
                _currentDrawingCharacter = value;
                OnPropertyChanged();
            }
        }
    }

    private double _cellSize = 16; // Default cell size
    /// <summary>
    /// Gets or sets the size of each cell in the map display.
    /// </summary>
    public double CellSize
    {
        get => _cellSize;
        set
        {
            if (_cellSize != value)
            {
                _cellSize = value;
                OnPropertyChanged();
            }
        }
    }

    // Removed AvailableEntityTypes property as it will be accessed via GameDataService
    // private ObservableCollection<string> _availableEntityTypes = [];
    // public ObservableCollection<string> AvailableEntityTypes
    // {
    //     get => _availableEntityTypes;
    //     set
    //     {
    //         _availableEntityTypes = value;
    //         OnPropertyChanged();
    //     }
    // }

    private readonly MapFileParserService _mapFileParserService;
    private readonly MapFileSaverService _mapFileSaverService;
    private readonly GameDataService _gameDataService; // Added

    public ICommand SaveMapCommand { get; }
    public ICommand NewMapCommand { get; }
    public ICommand DeleteMapCommand { get; }
    public ICommand ToggleThemeCommand { get; }
    public ICommand AddEntityCommand { get; }
    public ICommand EditEntityCommand { get; }
    public ICommand DeleteEntityCommand { get; }
    public ICommand AddTransitionCommand { get; }
    public ICommand EditTransitionCommand { get; }
    public ICommand DeleteTransitionCommand { get; }

    public MainWindowViewModel()
    {
        _mapFileParserService = new MapFileParserService();
        _gameDataService = new GameDataService(); // Initialized
        _mapFileSaverService = new MapFileSaverService(_gameDataService); // Modified here

        LoadMaps();
        if (Maps.Count > 0)
        {
            SelectedMap = Maps[0];
        }
        _selectedEntity = new EntityData();
        _selectedTransition = new TransitionData();

        // Removed PopulateAvailableEntityTypes();
        // PopulateAvailableEntityTypes();

        SaveMapCommand = new RelayCommand(ExecuteSaveMap, CanExecuteSaveMap);
        NewMapCommand = new RelayCommand(ExecuteNewMap, CanExecuteNewMap);
        DeleteMapCommand = new RelayCommand(ExecuteDeleteMap, CanExecuteDeleteMap);
        ToggleThemeCommand = new RelayCommand(ExecuteToggleTheme);
        AddEntityCommand = new RelayCommand(ExecuteAddEntity, CanExecuteAddEntity);
        EditEntityCommand = new RelayCommand(ExecuteEditEntity, CanExecuteEditEntity);
        DeleteEntityCommand = new RelayCommand(ExecuteDeleteEntity, CanExecuteDeleteEntity);
        AddTransitionCommand = new RelayCommand(ExecuteAddTransition, CanExecuteAddTransition);
        EditTransitionCommand = new RelayCommand(ExecuteEditTransition, CanExecuteEditTransition);
        DeleteTransitionCommand = new RelayCommand(ExecuteDeleteTransition, CanExecuteDeleteTransition);
    }

    private void ExecuteToggleTheme(object parameter) => ThemeManager.ToggleTheme();

    // Removed PopulateAvailableEntityTypes method
    // private void PopulateAvailableEntityTypes()
    // {
    //     AvailableEntityTypes.Add("Door");
    //     AvailableEntityTypes.Add("Hero");
    //     AvailableEntityTypes.Add("Princess");
    //     AvailableEntityTypes.Add("RaftInUse");
    //     AvailableEntityTypes.Add("SwordInUse");
    //     AvailableEntityTypes.Add("Bat");
    //     AvailableEntityTypes.Add("Dragon");
    //     AvailableEntityTypes.Add("Fireball");
    //     AvailableEntityTypes.Add("Octorok");
    //     AvailableEntityTypes.Add("Spider");
    //     AvailableEntityTypes.Add("Armor");
    //     AvailableEntityTypes.Add("Key");
    //     AvailableEntityTypes.Add("Raft");
    //     AvailableEntityTypes.Add("Rupee");
    //     AvailableEntityTypes.Add("Sword");
    //     AvailableEntityTypes.Add("EnterCastle");
    //     AvailableEntityTypes.Add("EnterCave0");
    //     AvailableEntityTypes.Add("EnterCave1");
    //     AvailableEntityTypes.Add("NewArea");
    //     AvailableEntityTypes.Add("Water");
    // }

    public void AddEntityFromDragDrop(string entityType, int x, int y)
    {
        if (SelectedMap != null)
        {
            // Ensure the condition is always "true" when adding via drag and drop
            SelectedMap.EntityLocations.Add(new EntityData(entityType, x, y, "true"));
            OnPropertyChanged(nameof(SelectedMap.EntityLocations));
        }
    }

    private void LoadMaps() => Maps = new ObservableCollection<MapData>(_mapFileParserService.LoadMaps());

    private bool CanExecuteNewMap(object parameter) => true;

    private void ExecuteNewMap(object parameter)
    {
        // For simplicity, using MessageBox.Show for input. In a real app, use a custom dialog.
        string newMapName = Microsoft.VisualBasic.Interaction.InputBox("Enter new map name:", "New Map", "NewMap");

        if (!string.IsNullOrWhiteSpace(newMapName) && newMapName != "NewMap")
        {
            // Check for duplicate name
            if (Maps.Any(m => m.Name == newMapName))
            {
                MessageBox.Show("A map with this name already exists.", "New Map Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Create a blank map (e.g., 10x10 with '.' characters)
            List<string> rawMap = [];
            for (int i = 0; i < 10; i++) // Default 10 rows
            {
                rawMap.Add(new string('.', 10)); // Default 10 columns
            }

            MapData newMap = new(newMapName, [.. rawMap]);
            Maps.Add(newMap);
            SelectedMap = newMap; // Select the new map

            try
            {
                _mapFileSaverService.SaveMap(newMap);
                MessageBox.Show($"Map '{newMapName}' created successfully!", "New Map", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Error creating map '{newMapName}':\n{ex.Message}", "New Map Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        else if (newMapName == "NewMap")
        {
            MessageBox.Show("Map creation cancelled.", "New Map", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }

    private bool CanExecuteDeleteMap(object parameter) => SelectedMap != null;

    private void ExecuteDeleteMap(object parameter)
    {
        if (SelectedMap != null)
        {
            MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete map '{SelectedMap.Name}'?", "Delete Map", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    string fileName = $"{SelectedMap.Name}.cs";
                    string filePath = Path.Combine(MapFileSaverService.AbsoluteGameMapsPath, fileName);

                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }

                    Maps.Remove(SelectedMap);
                    SelectedMap = Maps.FirstOrDefault(); // Select the first map, or null if no maps remain
                    MessageBox.Show($"Map '{fileName}' deleted successfully!", "Delete Map", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show($"Error deleting map '{SelectedMap?.Name}':\n{ex.Message}", "Delete Map Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }

    private void PopulateDisplayMapCharacters()
    {
        if (SelectedMap?.Raw != null)
        {
            var tempRows = new ObservableCollection<ObservableCollection<MapCharacterViewModel>>();
            for (int y = 0; y < SelectedMap.Raw.Count; y++)
            {
                string line = SelectedMap.Raw[y];
                var tempRow = new ObservableCollection<MapCharacterViewModel>();
                for (int x = 0; x < line.Length; x++)
                {
                    tempRow.Add(new MapCharacterViewModel(line[x], x, y));
                }
                tempRows.Add(tempRow);
            }
            DisplayMapCharacters = tempRows;
        }
        else
        {
            DisplayMapCharacters = [];
        }
    }

    private bool CanExecuteSaveMap(object parameter) => SelectedMap != null;

    private void ExecuteSaveMap(object parameter)
    {
        if (SelectedMap != null)
        {
            try
            {
                SelectedMap.Raw.Clear();
                foreach (var row in DisplayMapCharacters)
                {
                    string line = new([.. row.OrderBy(mc => mc.X).Select(mc => mc.Character)]);
                    SelectedMap.Raw.Add(line);
                }

                _mapFileSaverService.SaveMap(SelectedMap);
                MessageBox.Show($"Map '{SelectedMap.Name}' saved successfully!", "Save Map", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Error saving map '{SelectedMap.Name}':\n{ex.Message}", "Save Map Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    // Entity Commands
    private bool CanExecuteAddEntity(object parameter) => SelectedMap != null;
    private void ExecuteAddEntity(object parameter)
    {
        if (SelectedMap != null)
        {
            // Placeholder map dimensions for now
            const int MapWidth = 32;
            const int MapHeight = 32;

            EntityData newEntity = new("NewEntity", 0, 0, "true");
            Views.EntityEditorWindow editorWindow = new(newEntity, _gameDataService, MapWidth, MapHeight);
            editorWindow.ShowDialog();

            if (editorWindow.DataContext is EntityEditorViewModel viewModel && viewModel.Entity != null)
            {
                SelectedMap.EntityLocations.Add(viewModel.Entity);
                OnPropertyChanged(nameof(SelectedMap.EntityLocations)); // Notify UI
            }
        }
    }

    private bool CanExecuteEditEntity(object parameter) => SelectedEntity != null;
    private void ExecuteEditEntity(object parameter)
    {
        if (SelectedMap != null && SelectedEntity != null)
        {
            // Placeholder map dimensions for now
            const int MapWidth = 32;
            const int MapHeight = 32;

            // Create a clone for editing to allow cancellation
            EntityData originalEntity = SelectedEntity;
            EntityData clonedEntity = new(originalEntity.EntityType, originalEntity.X, originalEntity.Y, originalEntity.Condition);

            Views.EntityEditorWindow editorWindow = new(clonedEntity, _gameDataService, MapWidth, MapHeight);
            editorWindow.ShowDialog();

            if (editorWindow.DataContext is EntityEditorViewModel viewModel && viewModel.Entity != null)
            {
                // Update original entity with changes from the cloned one
                originalEntity.EntityType = viewModel.Entity.EntityType;
                originalEntity.X = viewModel.Entity.X;
                originalEntity.Y = viewModel.Entity.Y;
                originalEntity.Condition = viewModel.Entity.Condition;

                // Notify UI that the collection item has changed
                OnPropertyChanged(nameof(SelectedMap.EntityLocations));
            }
        }
    }

    private bool CanExecuteDeleteEntity(object parameter) => SelectedEntity != null && SelectedMap != null;
    private void ExecuteDeleteEntity(object parameter)
    {
        if (SelectedMap != null && SelectedEntity != null)
        {
            SelectedMap.EntityLocations.Remove(SelectedEntity);
            OnPropertyChanged(nameof(SelectedMap.EntityLocations)); // Notify UI
            SelectedEntity = null; // Clear selection after deletion
        }
    }

    // Transition Commands
    private bool CanExecuteAddTransition(object parameter) => SelectedMap != null;
    private void ExecuteAddTransition(object parameter)
    {
        if (SelectedMap != null)
        {
            // Placeholder map dimensions for now
            const int MapWidth = 32;
            const int MapHeight = 32;

            TransitionData newTransition = new("MainMap0", 0, 0, "Up", 1, 1, 0, 0);
            Views.TransitionEditorWindow editorWindow = new(newTransition, _gameDataService, MapWidth, MapHeight);
            editorWindow.ShowDialog();

            if (editorWindow.DataContext is TransitionEditorViewModel viewModel && viewModel.Transition != null)
            {
                SelectedMap.AreaTransitions.Add(viewModel.Transition);
                OnPropertyChanged(nameof(SelectedMap.AreaTransitions)); // Notify UI
            }
        }
    }

    private bool CanExecuteEditTransition(object parameter) => SelectedTransition != null;
    private void ExecuteEditTransition(object parameter)
    {
        if (SelectedMap != null && SelectedTransition != null)
        {
            // Placeholder map dimensions for now
            const int MapWidth = 32;
            const int MapHeight = 32;

            // Create a clone for editing to allow cancellation
            TransitionData originalTransition = SelectedTransition;
            TransitionData clonedTransition = new(
                originalTransition.MapId,
                originalTransition.StartPositionX,
                originalTransition.StartPositionY,
                originalTransition.DirectionType,
                originalTransition.SizeX,
                originalTransition.SizeY,
                originalTransition.PositionX,
                originalTransition.PositionY
            );

            Views.TransitionEditorWindow editorWindow = new(clonedTransition, _gameDataService, MapWidth, MapHeight);
            editorWindow.ShowDialog();

            if (editorWindow.DataContext is TransitionEditorViewModel viewModel && viewModel.Transition != null)
            {
                // Update original transition with changes from the cloned one
                originalTransition.MapId = viewModel.Transition.MapId;
                originalTransition.StartPositionX = viewModel.Transition.StartPositionX;
                originalTransition.StartPositionY = viewModel.Transition.StartPositionY;
                originalTransition.DirectionType = viewModel.Transition.DirectionType;
                originalTransition.SizeX = viewModel.Transition.SizeX;
                originalTransition.SizeY = viewModel.Transition.SizeY;
                originalTransition.PositionX = viewModel.Transition.PositionX;
                originalTransition.PositionY = viewModel.Transition.PositionY;

                // Notify UI that the collection item has changed
                OnPropertyChanged(nameof(SelectedMap.AreaTransitions));
            }
        }
    }

    private bool CanExecuteDeleteTransition(object parameter) => SelectedTransition != null && SelectedMap != null;
    private void ExecuteDeleteTransition(object parameter)
    {
        if (SelectedMap != null && SelectedTransition != null)
        {
            SelectedMap.AreaTransitions.Remove(SelectedTransition);
            OnPropertyChanged(nameof(SelectedMap.AreaTransitions)); // Notify UI
            SelectedTransition = null; // Clear selection after deletion
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}


