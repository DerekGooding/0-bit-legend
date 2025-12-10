namespace BitLegend.MapEditor.Model;

public class EntityData : IResizableAndMovable
{
    public string EntityType { get; set; }
    public double X { get; set; }
    public double Y { get; set; }
    public double Width { get; set; }
    public double Height { get; set; }
    public string Condition { get; set; }

    public EntityData(string entityType, double x, double y, double width, double height, string condition = "")
    {
        EntityType = entityType;
        X = x;
        Y = y;
        Width = width;
        Height = height;
        Condition = condition;
    }

    public EntityData()
    {
        EntityType = string.Empty;
        Condition = string.Empty;
        X = 0;
        Y = 0;
        Width = 1; // Default to 1 cell width
        Height = 1; // Default to 1 cell height
    }
}
