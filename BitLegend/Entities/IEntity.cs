using BitLegend.Model.Enums;

namespace BitLegend.Entities;

public interface IEntity : IWorldSpace
{
    public DirectionType Direction { get; set; }

    public void Draw();
}
