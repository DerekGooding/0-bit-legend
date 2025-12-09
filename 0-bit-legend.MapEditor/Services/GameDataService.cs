using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using _0_Bit_Legend.Model.Enums; // Access enums from the game project
using _0_Bit_Legend.Entities; // Access IEntity

namespace _0_bit_legend.MapEditor.Services
{
    /// <summary>
    /// Provides game-specific data for the map editor, loaded via reflection from the game's assembly.
    /// </summary>
    public class GameDataService
    {
        /// <summary>
        /// Gets a list of valid entity type simple names (e.g., "Octorok", "Door").
        /// </summary>
        public List<string> ValidEntityTypes { get; private set; } = new();

        /// <summary>
        /// Gets a dictionary mapping entity type simple names to their full namespace-qualified names.
        /// </summary>
        public Dictionary<string, string> EntityTypeToFullTypeName { get; private set; } = new();

        /// <summary>
        /// Gets a list of valid map IDs (e.g., "MainMap0", "Castle1").
        /// </summary>
        public List<string> ValidMapIds { get; private set; } = new();

        /// <summary>
        /// Gets a list of valid direction types (e.g., "Up", "Down").
        /// </summary>
        public List<string> ValidDirectionTypes { get; private set; } = new();

        /// <summary>
        /// Gets a list of valid game flags (e.g., "HasSword", "Door1").
        /// </summary>
        public List<string> ValidGameFlags { get; private set; } = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="GameDataService"/> class, loading all game data.
        /// </summary>
        public GameDataService()
        {
            LoadValidEntityTypes();
            LoadValidMapIds();
            LoadValidDirectionTypes();
            LoadValidGameFlags();
        }

        /// <summary>
        /// Loads valid entity types by reflecting on the game's assembly for types implementing IEntity
        /// and supplementing with EnemyType enum values.
        /// </summary>
        private void LoadValidEntityTypes()
        {
            ValidEntityTypes.Clear();
            EntityTypeToFullTypeName.Clear();

            // Load the 0-Bit Legend assembly
            Assembly? gameAssembly = Assembly.Load("0-Bit Legend"); // Adjust assembly name if different
            if (gameAssembly == null)
            {
                // In a real application, proper error logging or handling would be done here.
                // For now, we'll return silently if the assembly can't be loaded.
                return;
            }

            // Find all types that implement IEntity
            Type iEntityType = typeof(IEntity);
            var entityTypes = gameAssembly.GetTypes()
                .Where(t => iEntityType.IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
                .ToList();

            foreach (Type type in entityTypes)
            {
                ValidEntityTypes.Add(type.Name);
                if (type.FullName != null)
                {
                    EntityTypeToFullTypeName[type.Name] = type.FullName;
                }
            }

            // Also add EnemyType enum names as entities if they don't implement IEntity directly
            foreach (var name in Enum.GetNames(typeof(EnemyType)))
            {
                if (name != "None" && !ValidEntityTypes.Contains(name))
                {
                    ValidEntityTypes.Add(name);
                    // This is a heuristic: assuming EnemyType enum names correspond to classes in Entities.Enemies namespace
                    EntityTypeToFullTypeName[name] = $"_0_Bit_Legend.Entities.Enemies.{name}";
                }
            }

            ValidEntityTypes = ValidEntityTypes.OrderBy(s => s).ToList();
        }

        /// <summary>
        /// Loads valid map IDs based on hardcoded values. In a production environment,
        /// these would typically be discovered dynamically from map files or configuration.
        /// </summary>
        private void LoadValidMapIds()
        {
            ValidMapIds.Clear();
            ValidMapIds.AddRange(new string[]
            {
                "Castle0", "Castle1", "Castle2", "Castle3", "Castle4", "Castle5",
                "Cave0", "Cave1",
                "MainMap0", "MainMap1", "MainMap2", "MainMap3", "MainMap4", "MainMap5"
            });
            ValidMapIds = ValidMapIds.OrderBy(s => s).ToList();
        }

        /// <summary>
        /// Loads valid direction types from the <see cref="DirectionType"/> enumeration.
        /// </summary>
        private void LoadValidDirectionTypes()
        {
            ValidDirectionTypes.Clear();
            ValidDirectionTypes.AddRange(Enum.GetNames(typeof(DirectionType)));
            ValidDirectionTypes = ValidDirectionTypes.OrderBy(s => s).ToList();
        }

        /// <summary>
        /// Loads valid game flags from the <see cref="GameFlag"/> enumeration, excluding the 'None' flag.
        /// </summary>
        private void LoadValidGameFlags()
        {
            ValidGameFlags.Clear();
            foreach (var name in Enum.GetNames(typeof(GameFlag)))
            {
                if (name != "None")
                {
                    ValidGameFlags.Add(name);
                }
            }
            ValidGameFlags = ValidGameFlags.OrderBy(s => s).ToList();
        }
    }
}
