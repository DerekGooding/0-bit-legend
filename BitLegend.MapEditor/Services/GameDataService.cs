using System.Reflection;
using BitLegend.Model.Enums;
using BitLegend.Entities;
using System.Text.Json; // Added

namespace BitLegend.MapEditor.Services;

/// <summary>
/// Provides game-specific data for the map editor, loaded via reflection from the game's assembly.
/// </summary>
[Singleton]
public class GameDataService
{
    private const string GameAssemblyFileName = "BitLegend.dll";
    private const string CacheFileName = "GameDataCache.json";
    private readonly string _gameAssemblyPath;
    private readonly string _cacheFilePath;

    // Internal class for caching
    private class GameDataCache
    {
        public DateTime AssemblyLastWriteTime { get; set; }
        public List<string> ValidEntityTypes { get; set; } = [];
        public Dictionary<string, string> EntityTypeToFullTypeName { get; set; } = [];
        public List<string> ValidMapIds { get; set; } = [];
        public List<string> ValidDirectionTypes { get; set; } = [];
        public List<string> ValidGameFlags { get; set; } = [];
    }

    /// <summary>
    /// Gets a list of valid entity type simple names (e.g., "Octorok", "Door").
    /// </summary>
    public virtual List<string> ValidEntityTypes { get; private set; } = [];

    /// <summary>
    /// Gets a dictionary mapping entity type simple names to their full namespace-qualified names.
    /// </summary>
    public virtual Dictionary<string, string> EntityTypeToFullTypeName { get; set; } = [];

    /// <summary>
    /// Gets a list of valid map IDs (e.g., "MainMap0", "Castle1").
    /// </summary>
    public virtual List<string> ValidMapIds { get; private set; } = [];

    /// <summary>
    /// Gets a list of valid direction types (e.g., "Up", "Down").
    /// </summary>
    public virtual List<string> ValidDirectionTypes { get; private set; } = [];

    /// <summary>
    /// Gets a list of valid game flags (e.g., "HasSword", "Door1").
    /// </summary>
    public virtual List<string> ValidGameFlags { get; private set; } = [];

    /// <summary>
    /// Initializes a new instance of the <see cref="GameDataService"/> class, loading all game data.
    /// </summary>
    public GameDataService()
    {
        // Determine paths for the game assembly and cache file
        var currentAssemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        _gameAssemblyPath = Path.Combine(currentAssemblyDirectory ?? string.Empty, GameAssemblyFileName);

        // Ensure cache directory exists, creating a subdirectory in TempPath
        var cacheDirectory = Path.Combine(Path.GetTempPath(), "BitLegendMapEditor");
        Directory.CreateDirectory(cacheDirectory);
        _cacheFilePath = Path.Combine(cacheDirectory, CacheFileName);

        LoadGameData(); // New method to handle cached or reflected loading
    }

    private void LoadGameData()
    {
        GameDataCache? cache = null;
        var gameAssemblyLastWriteTime = File.Exists(_gameAssemblyPath)
            ? File.GetLastWriteTime(_gameAssemblyPath)
            : DateTime.MinValue;

        // Try to load from cache
        if (File.Exists(_cacheFilePath))
        {
            try
            {
                var json = File.ReadAllText(_cacheFilePath);
                cache = JsonSerializer.Deserialize<GameDataCache>(json);

                // Invalidate cache if assembly is newer
                if (cache?.AssemblyLastWriteTime < gameAssemblyLastWriteTime)
                {
                    cache = null; // Cache is stale
                }
            }
            catch (JsonException)
            {
                cache = null; // Cache file corrupted
            }
            catch (IOException)
            {
                cache = null; // Cache file inaccessible
            }
        }

        if (cache != null)
        {
            // Load from cache
            ValidEntityTypes = cache.ValidEntityTypes;
            EntityTypeToFullTypeName = cache.EntityTypeToFullTypeName;
            ValidMapIds = cache.ValidMapIds;
            ValidDirectionTypes = cache.ValidDirectionTypes;
            ValidGameFlags = cache.ValidGameFlags;
        }
        else
        {
            // Perform reflection and build cache
            PerformReflection();
            SaveCache(gameAssemblyLastWriteTime);
        }
    }

    private void PerformReflection()
    {
        ValidEntityTypes.Clear();
        EntityTypeToFullTypeName.Clear();
        ValidMapIds.Clear();
        ValidDirectionTypes.Clear();
        ValidGameFlags.Clear();

        // Load the 0-bit Legend assembly
        var gameAssembly = Assembly.Load("BitLegend");
        if (gameAssembly == null)
        {
            // In a real application, proper error logging or handling would be done here.
            // For now, we'll return silently if the assembly can't be loaded.
            return;
        }

        // Find all types that implement IEntity
        var iEntityType = typeof(IEntity);
        var entityTypes = gameAssembly.GetTypes()
            .Where(t => iEntityType.IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
            .ToList();

        foreach (var type in entityTypes)
        {
            ValidEntityTypes.Add(type.Name);
            if (type.FullName != null)
            {
                EntityTypeToFullTypeName[type.Name] = type.FullName;
            }
        }

        // Also add EnemyType enum names as entities if they don't implement IEntity directly
        foreach (var name in Enum.GetNames<EnemyType>())
        {
            if (name != "None" && !ValidEntityTypes.Contains(name))
            {
                ValidEntityTypes.Add(name);
                // This is a heuristic: assuming EnemyType enum names correspond to classes in Entities.Enemies namespace
                EntityTypeToFullTypeName[name] = $"{name}";
            }
        }

        ValidEntityTypes = [.. ValidEntityTypes.Order()];

        // Hardcoded map IDs (as before)
        ValidMapIds.AddRange(
        [
            "Castle0", "Castle1", "Castle2", "Castle3", "Castle4", "Castle5",
            "Cave0", "Cave1",
            "MainMap0", "MainMap1", "MainMap2", "MainMap3", "MainMap4", "MainMap5"
        ]);
        ValidMapIds = [.. ValidMapIds.Order()];

        // Load valid direction types from the DirectionType enumeration.
        ValidDirectionTypes.AddRange(Enum.GetNames<DirectionType>());
        ValidDirectionTypes = [.. ValidDirectionTypes.Order()];

        // Load valid game flags from the GameFlag enumeration, excluding the 'None' flag.
        foreach (var name in Enum.GetNames<GameFlag>())
        {
            if (name != "None")
            {
                ValidGameFlags.Add(name);
            }
        }
        ValidGameFlags = [.. ValidGameFlags.Order()];
    }

    private void SaveCache(DateTime assemblyLastWriteTime)
    {
        var cache = new GameDataCache
        {
            AssemblyLastWriteTime = assemblyLastWriteTime,
            ValidEntityTypes = ValidEntityTypes,
            EntityTypeToFullTypeName = EntityTypeToFullTypeName,
            ValidMapIds = ValidMapIds,
            ValidDirectionTypes = ValidDirectionTypes,
            ValidGameFlags = ValidGameFlags
        };
        var json = JsonSerializer.Serialize(cache, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_cacheFilePath, json);
    }
}
