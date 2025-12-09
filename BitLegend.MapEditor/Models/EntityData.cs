namespace BitLegend.MapEditor.Models;

public class EntityData
{
    public string EntityType { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public string Condition { get; set; }

    public EntityData(string entityType, int x, int y, string condition = "")
    {
        EntityType = entityType;
        X = x;
        Y = y;
        Condition = condition;
    }

    public EntityData()
    {
        EntityType = string.Empty;
        Condition = string.Empty;
    }
}
