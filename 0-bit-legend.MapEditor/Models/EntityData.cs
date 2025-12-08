namespace _0_bit_legend.MapEditor.Models
{
    public class EntityData
    {
        public string EntityType { get; set; } // Store as string for simplicity in editor
        public int X { get; set; }
        public int Y { get; set; }
        public string Condition { get; set; } // Store condition as string/expression if needed, or simplify

        public EntityData(string entityType, int x, int y, string condition = "")
        {
            EntityType = entityType;
            X = x;
            Y = y;
            Condition = condition;
        }

        public EntityData() { }
    }
}
