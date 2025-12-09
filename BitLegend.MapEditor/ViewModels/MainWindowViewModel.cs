using BitLegend.MapEditor.Model;
using BitLegend.MapEditor.Model.Enums;
using BitLegend.MapEditor.Services;
using System.Collections.ObjectModel;

namespace BitLegend.MapEditor.ViewModels;

/// <summary>
/// ViewModel for the main application window, managing map data, entities, and transitions.
/// </summary>
[Singleton, ViewModel]
public partial class MainWindowViewModel
{
    [Bind] private ObservableCollection<MapData> _maps = [];

    [Bind(OnChangeMethodName = nameof(OnMapDataChange))] private MapData? _selectedMap;
    public void OnMapDataChange() => PopulateDisplayMapCharacters();

    [Bind] private ObservableCollection<ObservableCollection<MapCharacterViewModel>> _displayMapCharacters
        = [];

    [Bind] private EntityData? _selectedEntity;
    [Bind] private TransitionData? _selectedTransition;
    [Bind] private char _currentDrawingCharacter = 'X';
    [Bind] private double _cellSize = 16;
    [Bind] private PaintingMode _paintingMode = PaintingMode.Brush;

    private ObservableCollection<ObservableCollection<char>>? _selectedCharacterBrush;
    public ObservableCollection<ObservableCollection<char>>? SelectedCharacterBrush
    {
        get => _selectedCharacterBrush;
        set => SetProperty(ref _selectedCharacterBrush, value);
    }

    private readonly MapFileParserService _mapFileParserService;
    private readonly MapFileSaverService _mapFileSaverService;
    private readonly GameDataService _gameDataService;

    public MainWindowViewModel(
        MapFileParserService mapFileParserService,
        GameDataService gameDataService,
        MapFileSaverService mapFileSaverService)
    {
        _mapFileParserService = mapFileParserService;
        _gameDataService = gameDataService;
        _mapFileSaverService = mapFileSaverService;

        LoadMaps();
        if (Maps.Count > 0)
        {
            SelectedMap = Maps[0];
        }
    }

    [Command] public void ToggleTheme() => ThemeManager.ToggleTheme();
    [Command] public void NewMap()
    {
        // For simplicity, using MessageBox.Show for input. In a real app, use a custom dialog.
        var newMapName = Microsoft.VisualBasic.Interaction.InputBox("Enter new map name:", "New Map", "NewMap");

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
            for (var i = 0; i < 10; i++) // Default 10 rows
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

    public bool IsSelectedMap() => SelectedMap != null;
    public bool IsSelectedEntity() => SelectedEntity != null;
    public bool IsSelectedMapAndEntity() => SelectedEntity != null && SelectedMap != null;
    public bool IsSelectedTransition() => SelectedTransition != null;
    public bool IsSelectedMapAndTransition() => SelectedTransition != null && SelectedMap != null;

    [Command(CanExecuteMethodName = nameof(IsSelectedMap))] public void DeleteMap()
    {
        if (SelectedMap != null)
        {
            var result = MessageBox.Show($"Are you sure you want to delete map '{SelectedMap.Name}'?", "Delete Map", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    var fileName = $"{SelectedMap.Name}.cs";
                    var filePath = Path.Combine(MapFileSaverService.AbsoluteGameMapsPath, fileName);

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
    [Command(CanExecuteMethodName = nameof(IsSelectedMap))] public void SaveMap()
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
    [Command(CanExecuteMethodName = nameof(IsSelectedMap))] public void AddEntity()
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
                //OnPropertyChanged(nameof(SelectedMap.EntityLocations)); // Notify UI
            }
        }
    }
    [Command(CanExecuteMethodName = nameof(IsSelectedEntity))] public void EditEntity()
    {
        if (SelectedMap != null && SelectedEntity != null)
        {
            // Placeholder map dimensions for now
            const int MapWidth = 32;
            const int MapHeight = 32;

            // Create a clone for editing to allow cancellation
            var originalEntity = SelectedEntity;
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
                //OnPropertyChanged(nameof(SelectedMap.EntityLocations));
            }
        }
    }
    [Command(CanExecuteMethodName = nameof(IsSelectedMapAndEntity))] public void DeleteEntity()
    {
        if (SelectedMap != null && SelectedEntity != null)
        {
            SelectedMap.EntityLocations.Remove(SelectedEntity);
            //OnPropertyChanged(nameof(SelectedMap.EntityLocations)); // Notify UI
            SelectedEntity = null; // Clear selection after deletion
        }
    }
    [Command(CanExecuteMethodName = nameof(IsSelectedMap))] public void AddTransition()
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
                //OnPropertyChanged(nameof(SelectedMap.AreaTransitions)); // Notify UI
            }
        }
    }
    [Command(CanExecuteMethodName = nameof(IsSelectedTransition))] public void EditTransition()
    {
        if (SelectedMap != null && SelectedTransition != null)
        {
            // Placeholder map dimensions for now
            const int MapWidth = 32;
            const int MapHeight = 32;

            // Create a clone for editing to allow cancellation
            var originalTransition = SelectedTransition;
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
                //OnPropertyChanged(nameof(SelectedMap.AreaTransitions));
            }
        }
    }
    [Command(CanExecuteMethodName = nameof(IsSelectedMapAndTransition))] public void DeleteTransition()
    {
        if (SelectedMap != null && SelectedTransition != null)
        {
            SelectedMap.AreaTransitions.Remove(SelectedTransition);
            //OnPropertyChanged(nameof(SelectedMap.AreaTransitions)); // Notify UI
            SelectedTransition = null; // Clear selection after deletion
        }
    }

    private void PopulateDisplayMapCharacters()
    {
        if (SelectedMap?.Raw != null)
        {
            var tempRows = new ObservableCollection<ObservableCollection<MapCharacterViewModel>>();
            for (var y = 0; y < SelectedMap.Raw.Count; y++)
            {
                var line = SelectedMap.Raw[y];
                var tempRow = new ObservableCollection<MapCharacterViewModel>();
                for (var x = 0; x < line.Length; x++)
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

    public void AddEntityFromDragDrop(string entityType, int x, int y)
    {
        if (SelectedMap != null)
        {
            // Ensure the condition is always "true" when adding via drag and drop
            SelectedMap.EntityLocations.Add(new EntityData(entityType, x, y, "true"));
        }
    }

    public void SetSelectedCharacterBrush(int startGridX, int startGridY, int endGridX, int endGridY)
    {
        if (SelectedMap == null || DisplayMapCharacters == null || DisplayMapCharacters.Count == 0)
        {
            SelectedCharacterBrush = null;
            return;
        }

        var brush = new ObservableCollection<ObservableCollection<char>>();
        for (int y = startGridY; y <= endGridY; y++)
        {
            var row = new ObservableCollection<char>();
            for (int x = startGridX; x <= endGridX; x++)
            {
                if (y >= 0 && y < DisplayMapCharacters.Count &&
                    x >= 0 && x < DisplayMapCharacters[y].Count)
                {
                    row.Add(DisplayMapCharacters[y][x].Character);
                }
                else
                {
                    row.Add(' '); // Add empty character for out-of-bounds selection
                }
            }
            brush.Add(row);
        }
        SelectedCharacterBrush = brush;
    }

    private void LoadMaps() => Maps = new ObservableCollection<MapData>(_mapFileParserService.LoadMaps());
}