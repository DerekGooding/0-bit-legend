using System.Collections.Generic;
using System.Linq;

namespace _0_bit_legend.MapEditor.Models
{
    public class MapData
    {
        public string Name { get; set; }
        public List<string> Raw { get; set; } = new List<string>();
        public List<EntityData> EntityLocations { get; set; } = new List<EntityData>();
        public List<TransitionData> AreaTransitions { get; set; } = new List<TransitionData>();

        // Constructor for easy initialization
        public MapData(string name, string[] raw)
        {
            Name = name;
            Raw = raw.ToList();
        }

        public MapData() { } // Parameterless constructor for serialization
    }
}
