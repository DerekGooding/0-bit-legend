using _0_Bit_Legend.Content;
using _0_Bit_Legend.Entities;
using _0_Bit_Legend.Managers;
using _0_Bit_Legend.Maps;

namespace _0_Bit_Legend;

public static class MainProgram
{
    public readonly static Vector2 GlobalMapOffset = new(50, 4);
    public readonly static Vector2 GlobalMapRequirement = new(50 + 103, 33 + 4);
    private static int _lastW = Console.WindowWidth;
    private static int _lastH = Console.WindowHeight;
    public static PlayerController PlayerController { get; } = new();
    public static EntityManager EntityManager { get; } = new();
    private static readonly InputController _inputController = new();

    private static GameFlag _flags = GameFlag.None;
    public static GameState State { get; private set; } = GameState.Idle;

    private static readonly List<IMap> _maps = [];

    //Testing purposes until loading maps is properly abstracted/DI
    public static void SetMapZero(IMap map)
    {
        _maps.Clear();
        _maps.Add(map);
    }
    public static int CurrentMap { get; private set; }

    public static int Rupees { get; set; } = 100;
    public static int Keys { get; set; }

    public static int cEnemies1 = 4;
    public static int cEnemies2 = 4;
    public static int waitEnemies = 1;
    public static int waitDragon = 1;
    public static int wait;

    private static string _credits = string.Empty;

    public static bool RequiresRedraw = true;

    public static void Main()
    {
        InitializeMaps();

        Console.CursorVisible = false;
        LoadMap(0, new(52, 18), DirectionType.Up);

        _credits = string.Concat(Credits.Lose);

        while (true)
        {
            Update();
            if(RequiresRedraw)
            {
                Draw();
                RequiresRedraw = false;
            }

            // Frame Rate: ~ 12 FPS
            Thread.Sleep(83);
        }
    }

    private static void Update()
    {

        var currentW = Console.WindowWidth;
        var currentH = Console.WindowHeight;
        if (_lastW != currentW || _lastH != currentH)
        {
            Console.Clear();
            _lastW = currentW;
            _lastH = currentH;
            RequiresRedraw = true;
        }


        waitEnemies--;
        waitDragon--;

        switch (State)
        {
            case GameState.Idle:
                HandleMovement();
                break;
            case GameState.Waiting:
                HandleWaiting();
                break;
            case GameState.Attacking:
                HandleAttacking();
                break;
            case GameState.Hit:
                HandleHit();
                break;
            case GameState.Dead:
                HandleDeath();
                break;
            case GameState.GameOver:
                HandleGameOver();
                break;
        }
    }

    private static void HandleMovement()
    {
        var rawInput = _inputController.GetInputState();
        switch (ResolveCardinalDirection(rawInput))
        {
            case InputType.Up:
                PlayerController.MoveUp();
                break;

            case InputType.Down:
                PlayerController.MoveDown();
                break;

            case InputType.Left:
                PlayerController.MoveLeft();
                break;

            case InputType.Right:
                PlayerController.MoveRight();
                break;
        }

        if (HasFlag(GameFlag.HasSword) && (rawInput & InputType.Attack) != 0)
        {
            PlayerController.Attack();
            State = GameState.Attacking;
        }

        if (waitEnemies <= 0)
        {
            waitEnemies = 2;
            EntityManager.MoveAll();
        }
    }
    private static void HandleWaiting()
    {
        Thread.Sleep(100);

        if (CurrentMap is 0 or 4 or 6)
        {
            PlayerController.MoveUp();
            Thread.Sleep(50);
        }
        else if (CurrentMap is 7 or 8 or 9)
        {
            PlayerController.MoveDown();
            Thread.Sleep(50);
        }

        PlayerController.MovementWait--;
        if (PlayerController.MovementWait <= 0)
        {
            if (CurrentMap == 0)
            {
                LoadMap(6, new(50, 29), DirectionType.Up);
            }
            else if (CurrentMap == 4)
            {
                LoadMap(7, new(50, 29), DirectionType.Up);
            }
            else if (CurrentMap == 8)
            {
                LoadMap(9, new(50, 30), DirectionType.Up);
            }
            else if (CurrentMap == 6)
            {
                LoadMap(0, new(16, 9), DirectionType.Down);
            }
            else if (CurrentMap == 7)
            {
                LoadMap(4, new(86, 10), DirectionType.Down);
            }
            else if (CurrentMap == 9)
            {
                LoadMap(8, new(51, 20), DirectionType.Down);
            }
            State = GameState.Idle;
        }
    }
    private static void HandleAttacking() => PlayerController.Attack();
    private static void HandleHit()
    {
        Thread.Sleep(100);
        State = GameState.Idle;
        var (Position, _) = PlayerController.GetPlayerInfo();

        if (PlayerController.GetPrev() == DirectionType.Up && Position.Y < 27)
        {
            PlayerController.MoveDown(3);
        }
        else if (PlayerController.GetPrev() == DirectionType.Left && Position.X < 94)
        {
            PlayerController.MoveRight(3);
        }
        else if (PlayerController.GetPrev() == DirectionType.Down && Position.Y > 3)
        {
            PlayerController.MoveUp(3);
        }
        else if (PlayerController.GetPrev() == DirectionType.Right && Position.X > 7)
        {
            PlayerController.MoveLeft(3);
        }
        PlayerController.Hit();
    }
    private static void HandleDeath()
    {
        Thread.Sleep(50);
        PlayerController.PlayEffect('*');
        PlayerController.PlayEffect('+');

        while (Console.KeyAvailable)
        {
            Console.ReadKey(true);
        }
        Console.ReadKey(true);

        PlayerController.Health = 3;

        cEnemies1 = 4;
        cEnemies2 = 4;

        if (CurrentMap <= 8)
        {
            LoadMap(0, new(52, 15), DirectionType.Up);
        }
        else
        {
            LoadMap(9, new(50, 25), DirectionType.Up);
        }
        State = GameState.Idle;

    }
    private static void HandleGameOver()
    {
        if (HasFlag(GameFlag.HasArmor)) _credits = string.Concat(Credits.WinArmor);

        PlayerController.PlacePrincess();
        PlayerController.MoveLeft(0);

        PlayerController.PlacePrincess();
        PlayerController.MoveLeft(0);

        Thread.Sleep(3500);
        Thread.Sleep(600);
        Thread.Sleep(600);

    }

    private static void InitializeMaps()
    {
        _maps.Add(new MainMap0());
        _maps.Add(new MainMap1());
        _maps.Add(new MainMap2());
        _maps.Add(new MainMap3());
        _maps.Add(new MainMap4());
        _maps.Add(new MainMap5());

        _maps.Add(new Cave0());
        _maps.Add(new Cave1());

        _maps.Add(new Castle0());
        _maps.Add(new Castle1());
        _maps.Add(new Castle2());
        _maps.Add(new Castle3());
        _maps.Add(new Castle4());
        _maps.Add(new Castle5());
    }

    public static void LoadMap(int mapNum, Vector2 position, DirectionType direction)
    {
        CurrentMap = mapNum;
        EntityManager.RemoveAll();

        var map = _maps[mapNum];
        foreach(var item in map.EntityLocations)
        {
            if (!item.IsActive.Invoke()) continue;
            var obj = Activator.CreateInstance(item.EntityType);
            if (obj is not IEntity entity)
                throw new Exception("Error in map data. Non-entity in EntityLocation list");
            entity.Position = item.Position;
            EntityManager.Add(entity);
        }

        if (mapNum == 1)
        {
            EntityManager.SpawnEnemy(EnemyType.Octorok, new(75, 13), DirectionType.Left, -1);
            EntityManager.SpawnEnemy(EnemyType.Octorok, new(9, 12), DirectionType.Right, -1);
            EntityManager.SpawnEnemy(EnemyType.Octorok, new(23, 26), DirectionType.Left, -1);
        }
        else if (mapNum == 2)
        {
            EntityManager.SpawnEnemy(EnemyType.Octorok, new(59, 23), DirectionType.Right, -1);
            EntityManager.SpawnEnemy(EnemyType.Spider, new(76, 6), DirectionType.Left, 5);
        }
        else if (mapNum == 3)
        {
            EntityManager.SpawnEnemy(EnemyType.Spider, new(44, 25), DirectionType.Left, 6);
            EntityManager.SpawnEnemy(EnemyType.Octorok, new(38, 14), DirectionType.Right, -1);
            EntityManager.SpawnEnemy(EnemyType.Octorok, new(83, 9), DirectionType.Left, -1);
        }
        else if (mapNum == 4)
        {
            EntityManager.SpawnEnemy(EnemyType.Octorok, new(35, 23), DirectionType.Left, -1);
            EntityManager.SpawnEnemy(EnemyType.Octorok, new(69, 6), DirectionType.Left, -1);
        }
        else if (mapNum == 5)
        {
            EntityManager.SpawnEnemy(EnemyType.Spider, new(81, 9), DirectionType.Left, 4);
            EntityManager.SpawnEnemy(EnemyType.Spider, new(32, 5), DirectionType.Right, 6);
        }
        else if (mapNum == 8)
        {
            //enemyMovement.Move(EnemyType.Spider, new(26, 20), Direction.Right, 4);
            cEnemies1 = 4;
            cEnemies2 = 4;
        }
        else if (mapNum == 10)
        {
            if (cEnemies1 >= 1)
            {
                EntityManager.SpawnEnemy(EnemyType.Bat, new(70, 11), DirectionType.Left, -1);
            }
            if (cEnemies1 >= 2)
            {
                EntityManager.SpawnEnemy(EnemyType.Bat, new(32, 9), DirectionType.Right, -1);
            }
            if (cEnemies1 >= 3)
            {
                EntityManager.SpawnEnemy(EnemyType.Bat, new(53, 15), DirectionType.Left, -1);
            }
            if (cEnemies1 >= 4)
            {
                EntityManager.SpawnEnemy(EnemyType.Bat, new(20, 20), DirectionType.Right, -1);
            }
        }
        else if (mapNum == 11)
        {
            if (cEnemies2 >= 1)
            {
                EntityManager.SpawnEnemy(EnemyType.Bat, new(27, 9), DirectionType.Left, -1);
            }
            if (cEnemies2 >= 2)
            {
                EntityManager.SpawnEnemy(EnemyType.Bat, new(56, 20), DirectionType.Right, -1);
            }
            if (cEnemies2 >= 3)
            {
                EntityManager.SpawnEnemy(EnemyType.Bat, new(73, 15), DirectionType.Left, -1);
            }
            if (cEnemies2 >= 4)
            {
                EntityManager.SpawnEnemy(EnemyType.Bat, new(18, 11), DirectionType.Right, -1);
            }
        }
        else if (mapNum == 12)
        {
            if (!HasFlag(GameFlag.Dragon)) EntityManager.SpawnEnemy(EnemyType.Dragon, new(71, 13), DirectionType.Left, 12);
        }

        if ((CurrentMap == 2 || CurrentMap == 4) && position.X == 21)
        {
            PlayerController.SetPosition(position);
            PlayerController.DeployRaft(PlayerController.GetPrev2());
        }
        else
        {
            PlayerController.SpawnLink(position, direction);
        }

        //if (lCText)
        //{
        //    lCText = false;
        //    SetFlag(GameFlag.Text);
        //    wait = 750;
        //}

        //_start = true;
    }

    public static void WaitForTransition(int time = 2)
    {
        State = GameState.Waiting;
        PlayerController.MovementWait = time;
    }

    private static void DrawHud() => DrawToScreen(Hud.GetImage(), Hud.Position);

    private static void Draw()
    {
        if(_lastW < GlobalMapRequirement.X || _lastH < GlobalMapRequirement.Y)
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Fullscreen window to see game");
            return;
        }

        //DrawHud();
        DrawToScreen(_maps[CurrentMap].Raw, Vector2.Zero);

        PlayerController.Draw();
        EntityManager.Draw();
    }

    public static bool HasFlag(GameFlag flag) => (_flags & flag) != 0;
    public static bool HasFlags(GameFlag[] flags) => flags.All(flag => (_flags & flag) != 0);
    public static void SetFlag(GameFlag flag, bool value = true)
    {
        if (value)
            _flags |= flag;
        else
            _flags &= ~flag;
    }

    public static void SetGameState(GameState state) => State = state;

    private static InputType ResolveCardinalDirection(InputType input)
    {
        var vertical = input & (InputType.Up | InputType.Down);
        var horizontal = input & (InputType.Left | InputType.Right);

        if (vertical != InputType.None)
        {
            return (vertical & InputType.Up) != 0 ? InputType.Up : InputType.Down;
        }

        if (horizontal != InputType.None)
        {
            return (horizontal & InputType.Left) != 0 ? InputType.Left : InputType.Right;
        }

        return InputType.None;
    }

    public static void DrawToScreen(string[] image, Vector2 position)
    {
        var xOffset = position.X + GlobalMapOffset.X;
        var yOffset = position.Y + GlobalMapOffset.Y;

        for( var i = 0; i < image.Length; i++ )
        {
            Console.SetCursorPosition(xOffset, yOffset + i);
            Console.Write(image[i]);
        }
    }

    private static Vector2 _heroSize = new(4, 3);
    public static List<ICollider> GetCollisions()
    {
        (var Position, var _) = PlayerController.GetPlayerInfo();
        var result = new List<ICollider>();

        result.AddRange(EntityManager.GetCollisions(Position, _heroSize));

        return result;
    }

    public static bool Overlaps(int ax, int ay, int aw, int ah, int bx, int by, int bw, int bh)
        => ax < bx + bw &&
               bx < ax + aw &&
               ay < by + bh &&
               by < ay + ah;
}
