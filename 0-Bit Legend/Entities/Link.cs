namespace _0_Bit_Legend.Entities;

public class Link : IEntity
{
    public Vector2 Position { get; set; } = Vector2.Zero;
    public DirectionType Direction { get; set; }

    public void Draw(int posX, int posY, DirectionType direction)
    {
        var spaceslot = ' ';
        var underslot = '_';
        if (HasFlag(GameFlag.HasArmor))
        {
            spaceslot = '#';
            underslot = '#';
        }

        if (direction == DirectionType.Up)
        {
            Map[posX - 2, posY - 1] = ' ';
            Map[posX - 1, posY - 1] = '_';
            Map[posX, posY - 1] = '_';
            Map[posX + 1, posY - 1] = '_';
            Map[posX + 2, posY - 1] = ' ';

            Map[posX - 2, posY] = '|';
            Map[posX - 1, posY] = spaceslot;
            Map[posX, posY] = '=';
            Map[posX + 1, posY] = spaceslot;
            Map[posX + 2, posY] = '|';

            Map[posX - 2, posY + 1] = '|';
            Map[posX - 1, posY + 1] = '^';
            Map[posX, posY + 1] = spaceslot;
            Map[posX + 1, posY + 1] = '^';
            Map[posX + 2, posY + 1] = '|';

            Map[posX - 2, posY + 2] = ' ';
            Map[posX - 1, posY + 2] = '\\';
            Map[posX, posY + 2] = '_';
            Map[posX + 1, posY + 2] = '/';
            Map[posX + 2, posY + 2] = ' ';
        }
        else if (direction == DirectionType.Left)
        {
            Map[posX - 2, posY - 1] = ' ';
            Map[posX - 1, posY - 1] = ' ';
            Map[posX, posY - 1] = '/';
            Map[posX + 1, posY - 1] = '\\';
            Map[posX + 2, posY - 1] = ' ';

            Map[posX - 2, posY] = ' ';
            Map[posX - 1, posY] = '/';
            Map[posX, posY] = ' ';
            Map[posX + 1, posY] = ' ';
            Map[posX + 2, posY] = '|';

            Map[posX - 2, posY + 1] = '|';
            Map[posX - 1, posY + 1] = '^';
            Map[posX, posY + 1] = spaceslot;
            Map[posX + 1, posY + 1] = spaceslot;
            Map[posX + 2, posY + 1] = '|';

            Map[posX - 2, posY + 2] = '|';
            Map[posX - 1, posY + 2] = underslot;
            Map[posX, posY + 2] = '=';
            Map[posX + 1, posY + 2] = underslot;
            Map[posX + 2, posY + 2] = '|';
        }
        else if (direction == DirectionType.Down)
        {
            Map[posX - 2, posY - 1] = ' ';
            Map[posX - 1, posY - 1] = ' ';
            Map[posX, posY - 1] = '_';
            Map[posX + 1, posY - 1] = ' ';
            Map[posX + 2, posY - 1] = ' ';

            Map[posX - 2, posY] = ' ';
            Map[posX - 1, posY] = '/';
            Map[posX, posY] = ' ';
            Map[posX + 1, posY] = '\\';
            Map[posX + 2, posY] = ' ';

            Map[posX - 2, posY + 1] = '|';
            Map[posX - 1, posY + 1] = '^';
            Map[posX, posY + 1] = spaceslot;
            Map[posX + 1, posY + 1] = '^';
            Map[posX + 2, posY + 1] = '|';

            Map[posX - 2, posY + 2] = '|';
            Map[posX - 1, posY + 2] = underslot;
            Map[posX, posY + 2] = '=';
            Map[posX + 1, posY + 2] = underslot;
            Map[posX + 2, posY + 2] = '|';
        }
        else if (direction == DirectionType.Right)
        {
            Map[posX - 2, posY - 1] = ' ';
            Map[posX - 1, posY - 1] = '/';
            Map[posX, posY - 1] = '\\';
            Map[posX + 1, posY - 1] = ' ';
            Map[posX + 2, posY - 1] = ' ';

            Map[posX - 2, posY] = '|';
            Map[posX - 1, posY] = ' ';
            Map[posX, posY] = ' ';
            Map[posX + 1, posY] = '\\';
            Map[posX + 2, posY] = ' ';

            Map[posX - 2, posY + 1] = '|';
            Map[posX - 1, posY + 1] = spaceslot;
            Map[posX, posY + 1] = spaceslot;
            Map[posX + 1, posY + 1] = '^';
            Map[posX + 2, posY + 1] = '|';

            Map[posX - 2, posY + 2] = '|';
            Map[posX - 1, posY + 2] = underslot;
            Map[posX, posY + 2] = '=';
            Map[posX + 1, posY + 2] = underslot;
            Map[posX + 2, posY + 2] = '|';
        }
    }

    public bool IsTouching(int posX, int posY, char symbol)
    {
        if (symbol == '/')
        {
            return Map[posX, posY - 1] == '/';
        }

        if (Map[posX - 2, posY - 1] == symbol
            || Map[posX - 1, posY - 1] == symbol
            || Map[posX, posY - 1] == symbol
            || Map[posX + 1, posY - 1] == symbol
            || Map[posX + 2, posY - 1] == symbol
            || Map[posX - 2, posY] == symbol
            || Map[posX - 1, posY] == symbol
            || Map[posX, posY] == symbol
            || Map[posX + 1, posY] == symbol
            || Map[posX + 2, posY] == symbol
            || Map[posX - 2, posY + 1] == symbol
            || Map[posX - 1, posY + 1] == symbol
            || Map[posX, posY + 1] == symbol
            || Map[posX + 1, posY + 1] == symbol
            || Map[posX + 2, posY + 1] == symbol
            || Map[posX - 2, posY + 2] == symbol
            || Map[posX - 1, posY + 2] == symbol
            || Map[posX, posY + 2] == symbol
            || Map[posX + 1, posY + 2] == symbol
            || Map[posX + 2, posY + 2] == symbol)
        {
            if (symbol is 'R' or 'r')
            {
                for (var i = 0; i < 4; i++)
                {
                    for (var j = 0; j < 5; j++)
                    {
                        if (Map[posX - 2 + j, posY - 1 + i] is 'R' or 'r')
                        {
                            MainProgram.EnemyManager.RemoveRupee(posX - 2 + j, posY - 1 + i);
                        }
                    }
                }
            }
            return true;
        }
        return false;
    }

    public bool IsTouching(int posX, int posY, char[] symbols) => throw new NotImplementedException();
}
