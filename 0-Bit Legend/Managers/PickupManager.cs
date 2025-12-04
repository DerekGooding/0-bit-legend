using _0_Bit_Legend.Entities.Pickups;

namespace _0_Bit_Legend.Managers;

public class PickupManager
{
    private readonly List<IPickup> _pickups = [];

    public void RemoveAll() => _pickups.Clear();
    public void Remove(IPickup pickup)
    {
        //pickup.Clear();
        _pickups.Remove(pickup);
        //var type = enemy.Type;

        //UpdateRow(enemy.Position.Y);
        //UpdateRow(enemy.Position.Y + 1);
        //UpdateRow(enemy.Position.Y + 2);

        //if (type == EnemyType.Dragon)
        //{
        //    UpdateRow(enemy.Position.Y + 3);
        //    UpdateRow(enemy.Position.Y + 4);
        //    UpdateRow(enemy.Position.Y + 5);
        //    UpdateRow(enemy.Position.Y + 6);
        //}


        //if (type == EnemyType.Bat)
        //{
        //    if (CurrentMap == 10)
        //    {
        //        cEnemies1--;
        //    }
        //    else if (CurrentMap == 11)
        //    {
        //        cEnemies2--;
        //    }
        //}
    }

    public IPickup GetPickupAt(Vector2 target)
    {
        var posX = target.X;
        var posY = target.Y;

        for (var i = 0; i < _pickups.Count; i++)
        {
            var inPosX = 0;
            var inPosY = 0;

            var position = _pickups[i].Position;

            if (posX >= position.X && posX <= position.X + inPosX && posY >= position.Y && posY <= position.Y + inPosY)
            {
                return _pickups[i];
            }
        }
        return _pickups[0];
    }
}