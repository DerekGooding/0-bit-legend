using _0_bit_legend.MapEditor.Models;
using System.IO;
using System.Text;
using System.Collections.Generic; // Added for List<string>
using System; // Added for Environment.NewLine

namespace _0_bit_legend.MapEditor.Services
{
    public class MapFileSaverService
    {
        private const string GameMapsPath = @"0-Bit Legend\Maps"; // Relative path to game maps

        public void SaveMap(MapData mapData)
        {
            // Construct the file path for the map
            string fileName = $"{mapData.Name}.cs"; // Assuming the file name matches the map name
            string filePath = Path.Combine(GameMapsPath, fileName);

            // Generate the C# file content
            string fileContent = GenerateMapFileContent(mapData);

            // Write the content back to the file
            File.WriteAllText(filePath, fileContent);
        }

        private string GenerateMapFileContent(MapData mapData)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("using _0_Bit_Legend.Content;");
            sb.AppendLine("using _0_Bit_Legend.Entities;");
            sb.AppendLine("using _0_Bit_Legend.Model;");
            sb.AppendLine("using _0_Bit_Legend.Model.Enums;");
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("");
            sb.AppendLine("namespace _0_Bit_Legend.Maps");
            sb.AppendLine("{");
            sb.AppendLine($"    public class {mapData.Name} : BaseMap");
            sb.AppendLine("    {");
            sb.AppendLine($"        public override string Name => \"{mapData.Name}\";");
            sb.AppendLine("");

            // Raw map data
            sb.AppendLine("        public override string[] Raw => new string[]");
            sb.AppendLine("        {");
            foreach (string line in mapData.Raw)
            {
                sb.AppendLine($"            \"{line}\",");
            }
            sb.AppendLine("        }");
            sb.AppendLine("");

            // Entity Locations
            sb.AppendLine("        public override List<EntityLocation> EntityLocations { get; } = new List<EntityLocation>()");
            sb.AppendLine("        {");
            foreach (EntityData entity in mapData.EntityLocations)
            {
                // Ensure EntityType is correctly referenced (e.g., typeof(Octorok))
                // and Condition is a valid C# boolean expression or "true"/"false"
                sb.AppendLine($"            new(typeof(_0_Bit_Legend.Entities.{entity.EntityType}), new({entity.X}, {entity.Y}), () => {entity.Condition}),");
            }
            sb.AppendLine("        }");
            sb.AppendLine("");

            // Area Transitions
            sb.AppendLine("        public override List<NewAreaInfo> AreaTransitions { get; } = new List<NewAreaInfo>()");
            sb.AppendLine("        {");
            foreach (TransitionData transition in mapData.AreaTransitions)
            {
                // Ensure MapId and DirectionType are correctly referenced as enums
                sb.AppendLine($"            new(MapId: WorldMap.MapName.{transition.MapId}, StartPosition: new({transition.StartPositionX}, {transition.StartPositionY}), DirectionType.{transition.DirectionType}, Size: new({transition.SizeX}, {transition.SizeY}), Position: new({transition.PositionX}, {transition.PositionY})),");
            }
            sb.AppendLine("        }");
            sb.AppendLine("    }");
            sb.AppendLine("}");

            return sb.ToString();
        }
    }
}