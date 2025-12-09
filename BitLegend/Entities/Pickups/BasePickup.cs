namespace BitLegend.Entities.Pickups;

public abstract class BasePickup : IPickup
{
    public Vector2 Position { get; set; } = Vector2.Zero;
    public DirectionType Direction { get; set; }

    public abstract string[] Image { get; }
    public abstract Action OnPickup { get; }

    public abstract Vector2 Size { get; }

    public void Draw() => DrawToScreen(Image, Position);

    public void HandleCollision() => OnPickup.Invoke();
}