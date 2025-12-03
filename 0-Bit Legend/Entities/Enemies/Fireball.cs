namespace _0_Bit_Legend.Entities.Enemies;

public class Fireball : BaseEnemy
{
    public override EnemyType Type => EnemyType.Fireball;
    public override char[] MapStorage { get; } = new string(' ', 6).ToCharArray();

    public override void Clear()
    {
        Map[Position.X + 0, Position.Y] = MapStorage[0];
        Map[Position.X + 1, Position.Y] = MapStorage[1];
        Map[Position.X + 2, Position.Y] = MapStorage[2];

        Map[Position.X + 0, Position.Y + 1] = MapStorage[3];
        Map[Position.X + 1, Position.Y + 1] = MapStorage[4];
        Map[Position.X + 2, Position.Y + 1] = MapStorage[5];
    }

    public override void Draw()
    {
        var posX = Position.X;
        var posY = Position.Y;

        Map[posX + 0, posY] = 'F';
        Map[posX + 1, posY] = 'F';
        Map[posX + 2, posY] = 'F';

        Map[posX + 0, posY + 1] = 'F';
        Map[posX + 1, posY + 1] = 'F';
        Map[posX + 2, posY + 1] = 'F';
    }

    public override bool InBounds(Vector2 position)
    {
        var inPosX = position.X + 3;
        var inPosY = position.Y + 1;

        return position.X > 0 && inPosX < 102 && position.Y > 0 && inPosY < 33;
    }

    public override bool IsTouching(char symbol)
    {
        var posX = Position.X;
        var posY = Position.Y;

        //(Map[posX, posY] == symbol || Map[posX + 1, posY] == symbol || Map[posX + 2, posY] == symbol || Map[posX, posY + 1] == symbol || Map[posX + 1, posY + 1] == symbol || Map[posX + 2, posY + 1] == symbol))))
        for (var i = 0; i < 3; i++)
        {
            for (var j = 0; j < 2; j++)
            {
                if (Map[posX + i, posY + j] == symbol)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public override bool IsTouching(char[] symbols)
    {
        var posX = Position.X;
        var posY = Position.Y;

        for (var i = 0; i < 3; i++)
        {
            for (var j = 0; j < 2; j++)
            {
                if (symbols.Any(x => x ==Map[posX + i, posY + j]))
                {
                    return true;
                }
            }
        }
        return false;
    }

    public override void Move()
    {
        var newPosition = Prev1 switch
        {
            DirectionType.Up => Position.Offset(-3, -2),
            DirectionType.Left => Position.Offset(x: -3),
            DirectionType.Down => Position.Offset(-3, + 2),
            DirectionType.Right => Position.Offset(x: 3),
            _ => throw new NotSupportedException()
        };

        EnemyManager.Move(this, newPosition, Prev1, -1);
    }
    public override void TakeDamage()
    {
        //nothing
    }
}