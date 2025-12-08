using _0_bit_legend.MapEditor.Models;
using _0_bit_legend.MapEditor.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows;

namespace _0_bit_legend.MapEditor.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<MapData> _maps = new ObservableCollection<MapData>();
        public ObservableCollection<MapData> Maps
        {
            get => _maps;
            set
            {
                _maps = value;
                OnPropertyChanged();
            }
        }

        private MapData _selectedMap;
        public MapData SelectedMap
        {
            get => _selectedMap;
            set
            {
                if (_selectedMap != value)
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

        private ObservableCollection<MapCharacterViewModel> _displayMapCharacters = new ObservableCollection<MapCharacterViewModel>();
        public ObservableCollection<MapCharacterViewModel> DisplayMapCharacters
        {
            get => _displayMapCharacters;
            set
            {
                _displayMapCharacters = value;
                OnPropertyChanged();
            }
        }

        private EntityData _selectedEntity;
        public EntityData SelectedEntity
        {
            get => _selectedEntity;
            set
            {
                _selectedEntity = value;
                OnPropertyChanged();
            }
        }

        private TransitionData _selectedTransition;
        public TransitionData SelectedTransition
        {
            get => _selectedTransition;
            set
            {
                _selectedTransition = value;
                OnPropertyChanged();
            }
        }

        private double _cellSize = 16; // Default cell size
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

        private readonly MapFileParserService _mapFileParserService;
        private readonly MapFileSaverService _mapFileSaverService;

        public ICommand SaveMapCommand { get; }
        public ICommand AddEntityCommand { get; }
        public ICommand EditEntityCommand { get; }
        public ICommand DeleteEntityCommand { get; }
        public ICommand AddTransitionCommand { get; }
        public ICommand EditTransitionCommand { get; }
        public ICommand DeleteTransitionCommand { get; }

        public MainWindowViewModel()
        {
            _mapFileParserService = new MapFileParserService();
            _mapFileSaverService = new MapFileSaverService();

            _maps = new ObservableCollection<MapData>();
            LoadMaps();
            if (_maps.Count > 0)
            {
                _selectedMap = _maps[0];
                PopulateDisplayMapCharacters();
            }
            _selectedEntity = new EntityData(); // Initialize to prevent nullable warning
            _selectedTransition = new TransitionData(); // Initialize to prevent nullable warning


            SaveMapCommand = new RelayCommand(ExecuteSaveMap, CanExecuteSaveMap);
            AddEntityCommand = new RelayCommand(ExecuteAddEntity, CanExecuteAddEntity);
            EditEntityCommand = new RelayCommand(ExecuteEditEntity, CanExecuteEditEntity);
            DeleteEntityCommand = new RelayCommand(ExecuteDeleteEntity, CanExecuteDeleteEntity);
            AddTransitionCommand = new RelayCommand(ExecuteAddTransition, CanExecuteAddTransition);
            EditTransitionCommand = new RelayCommand(ExecuteEditTransition, CanExecuteEditTransition);
            DeleteTransitionCommand = new RelayCommand(ExecuteDeleteTransition, CanExecuteDeleteTransition);
        }

        private void LoadMaps()
        {
            Maps = new ObservableCollection<MapData>(_mapFileParserService.LoadMaps());
        }

        private void PopulateDisplayMapCharacters()
        {
            if (SelectedMap != null && SelectedMap.Raw != null)
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
                DisplayMapCharacters = new ObservableCollection<ObservableCollection<MapCharacterViewModel>>();
            }
        }

        private bool CanExecuteSaveMap(object parameter)
        {
            return SelectedMap != null;
        }

        private void ExecuteSaveMap(object parameter)
        {
            if (SelectedMap != null)
            {
                try
                {
                    SelectedMap.Raw.Clear();
                    foreach (var row in DisplayMapCharacters)
                    {
                        string line = new string(row.OrderBy(mc => mc.X).Select(mc => mc.Character).ToArray());
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
                SelectedMap.EntityLocations.Add(new EntityData("NewEntity", 0, 0, "true"));
                OnPropertyChanged(nameof(SelectedMap.EntityLocations)); // Notify UI
            }
        }

        private bool CanExecuteEditEntity(object parameter) => SelectedEntity != null;
        private void ExecuteEditEntity(object parameter)
        {
            MessageBox.Show("Edit Entity functionality to be implemented.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
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
                SelectedMap.AreaTransitions.Add(new TransitionData("MainMap0", 0, 0, "Up", 1, 1, 0, 0));
                OnPropertyChanged(nameof(SelectedMap.AreaTransitions)); // Notify UI
            }
        }

        private bool CanExecuteEditTransition(object parameter) => SelectedTransition != null;
        private void ExecuteEditTransition(object parameter)
        {
            MessageBox.Show("Edit Transition functionality to be implemented.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}


