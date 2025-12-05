namespace _0_Bit_Legend.Entities.Enemies;

public abstract class BaseEnemy : IEnemy
{
    public abstract EnemyType Type { get; }

    public abstract  Vector2 Size { get; }

    public int Hp { get; set; } = 1;
    public int Motion { get; set; }
    public DirectionType Prev1 { get; set; }
    public DirectionType Prev2 { get; set; }
    public Vector2 Position { get; set; } = Vector2.Zero;
    public DirectionType Direction { get; set; }

    public abstract void Draw();
    public abstract bool InBounds(Vector2 position);
    public abstract void Move();
    public virtual void TakeDamage()
    {
        Hp--;
        if (Hp <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        EnemyManager.Remove(this);
        SpawnRupee(Position);
    }

    public virtual void SpawnRupee(Vector2 position) { }

    public bool TryMove(Vector2 position, DirectionType direction, int motion)
    {
        Motion = motion;

        var posX = position.X;
        var posY = position.Y;

        if (InBounds(position))
        {
            //TODO => Wall detection

            var blocking = new char[] { '=', 'X', 't', 'n', 'B', '{', '}', '|', '/', '\\', '_', '~' };
            var damageHit = new char[] { '|', '_', '\\' };
            if (Type == EnemyType.Dragon || Type == EnemyType.Spider || Type == EnemyType.Bat) // || (!IsTouching(blocking)))
            {
                Prev1 = direction;

                if (Type == EnemyType.Octorok)
                {
                    if (direction is DirectionType.Left or DirectionType.Right)
                    {
                        Prev2 = direction;
                    }
                }
                else if (Type == EnemyType.Spider)
                {
                    if (direction is DirectionType.Up or DirectionType.Down)
                    {
                        Prev2 = DirectionType.Left;
                    }
                    else if (direction is DirectionType.Left or DirectionType.Right)
                    {
                        Prev2 = DirectionType.Right;
                    }
                }
                else if (Type == EnemyType.Bat)
                {
                    if (Prev2 == DirectionType.Right)
                    {
                        Prev2 = DirectionType.Left;
                    }
                    else if (Prev2 == DirectionType.Left)
                    {
                        Prev2 = DirectionType.Right;
                    }
                }

                Draw();

                Position = new(posX, posY);

                return true;
            }
            //TODO => Detect taking damage in the collision system
            //else if (IsTouching(damageHit))
            //{
            //    PlayerController.Hit();
            //    if (Type == EnemyType.Fireball)
            //    {
            //        EnemyManager.Remove(this);
            //    }
            //    else
            //    {
            //        Draw();
            //    }
            //}
            else
            {
                if (Type == EnemyType.Fireball)
                {
                    EnemyManager.Remove(this);
                }
                else
                {
                    Draw();
                }
            }
        }
        return false;
    }
}
