using _0_Bit_Legend.Content;
using _0_Bit_Legend.Entities;
using _0_Bit_Legend.Entities.Triggers;
using _0_Bit_Legend.Managers;
using _0_Bit_Legend.Maps;

namespace _0_Bit_Legend;

public static class MainProgram
{
    public readonly static Vector2 GlobalMapOffset = new(50, 4);
    public readonly static Vector2 GlobalSize = new(102, 33);
    public readonly static Vector2 GlobalMapRequirement =
        new(GlobalMapOffset.X + GlobalSize.X, GlobalMapOffset.Y + GlobalSize.Y);

    private static int _lastW = Console.WindowWidth;
    private static int _lastH = Console.WindowHeight;
    public static PlayerController PlayerController { get; } = new();
    public static EntityManager EntityManager { get; } = new();
    private static readonly InputController _inputController = new();

    private static GameFlag _flags = GameFlag.None;
    public static GameState State { get; private set; } = GameState.Idle;

    public static int CurrentMap { get; private set; }

    public static bool[,] WallMap { get; } = new bool[GlobalSize.X, GlobalSize.Y];

    private static char[][] _screen = [];
    private static Vector2 _heroSize = new(4, 3);
    private static int rupees = 100;
    private static int keys;

    public static int Rupees
    {
        get => rupees;
        set
        {
            if (rupees == value) return;
            rupees = value;
            RequireHudDraw = true;
        }
    }
    public static int Keys
    {
        get => keys;
        set
        {
            if (keys == value) return;
            keys = value;
            RequireHudDraw = true;
        }
    }

    public static int cEnemies1 = 4;
    public static int cEnemies2 = 4;
    private static int waitEnemies;

    private static bool _debugWall;

    public static bool RequiresRedraw { get; set; } = true;
    public static bool RequireHudDraw { get; set; } = true;

    public static void Main()
    {
        Console.CursorVisible = false;
        LoadMap(0, new(52, 18), DirectionType.Up);

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
            RequireHudDraw = true;
        }

        waitEnemies--;

        switch (State)
        {
            case GameState.Idle:
                HandleMovement();
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

        if ((rawInput & InputType.DebugWall) != 0)
        {
            _debugWall = !_debugWall;
            RequiresRedraw = true;
        }

        if (waitEnemies <= 0)
        {
            waitEnemies = 2;
            EntityManager.MoveAll();
        }
    }
    private static void HandleAttacking() => PlayerController.Attack();
    private static void HandleHit()
    {
        Thread.Sleep(100);
        State = GameState.Idle;
        var (_, Direction) = PlayerController.GetPlayerInfo();

        if (Direction == DirectionType.Up)
        {
            PlayerController.MoveDown(3);
        }
        else if (Direction == DirectionType.Left)
        {
            PlayerController.MoveRight(3);
        }
        else if (Direction == DirectionType.Down)
        {
            PlayerController.MoveUp(3);
        }
        else if (Direction == DirectionType.Right)
        {
            PlayerController.MoveLeft(3);
        }

        PlayerController.Hit();
    }
    private static void HandleDeath()
    {
        Thread.Sleep(50);
        PlayerController.TakeDamageEffect();

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
        PlayerController.PlacePrincess();
        PlayerController.MoveLeft(0);

        PlayerController.PlacePrincess();
        PlayerController.MoveLeft(0);

        Thread.Sleep(3500);
        Thread.Sleep(600);
        Thread.Sleep(600);

    }

    public static void LoadMap(int mapNum, Vector2 position, DirectionType direction)
    {
        CurrentMap = mapNum;
        UpdateWallMap();
        EntityManager.RemoveAll();

        var map = WorldMap.Maps[mapNum];
        foreach(var item in map.EntityLocations)
        {
            if (!item.IsActive.Invoke()) continue;
            var obj = Activator.CreateInstance(item.EntityType);
            if (obj is not IEntity entity)
                throw new Exception("Error in map data. Non-entity in EntityLocation list");
            entity.Position = item.Position;
            EntityManager.Add(entity);
        }

        foreach(var item in map.AreaTransitions)
        {
            EntityManager.Add(NewArea.Initialize(item));
        }

        if (mapNum == 8)
        {
            cEnemies1 = 4;
            cEnemies2 = 4;
        }

        if ((CurrentMap == 2 || CurrentMap == 4) && position.X == 21)
        {
            PlayerController.SetPosition(position);
            //TODO => Raft system
            //PlayerController.DeployRaft(PlayerController.GetPrev2());
        }
        else
        {
            PlayerController.SpawnLink(position, direction);
        }
    }

    private static void UpdateWallMap()
    {
        var map = WorldMap.Maps[CurrentMap].Raw;
        for(var y = 0; y < map.Length; y++)
        {
            var line = map[y];
            for(var x = 0; x < line.Length; x++)
                WallMap[x,y] = Environments.Walls.Any(symbol => symbol == line[x]);
        }
    }
    public static void ForceRedraw() => Draw();
    private static void Draw()
    {
        if(_lastW <= GlobalMapRequirement.X || _lastH <= GlobalMapRequirement.Y)
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Fullscreen window to see game");
            return;
        }

        _screen = [.. WorldMap.Maps[CurrentMap].RawChars.Select(row => (char[])row.Clone())];
        EntityManager.Draw();
        PlayerController.Draw();

        PrintScreen();
        if (RequireHudDraw)
        {
            PrintHud();
            RequireHudDraw = false;
        }


        if (_debugWall) DrawWallsDebug();
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
        for( var y = 0; y < image.Length; y++ )
        {
            for (var x = 0; x < image[y].Length; x++)
            {
                _screen[position.Y + y][position.X + x] = image[y][x];
            }
        }
    }

    public static void PrintHud()
    {
        var image = Hud.GetImage(PlayerController.Health);
        for (var y = 0; y < image.Length; y++)
        {
            Console.SetCursorPosition(Hud.AbsolutePosition.X, Hud.AbsolutePosition.Y + y);
            Console.Write(image[y]);
        }

    }

    private static void PrintScreen()
    {
        var xOffset = GlobalMapOffset.X;
        var yOffset = GlobalMapOffset.Y;

        for (var i = 0; i < _screen.Length; i++)
        {
            Console.SetCursorPosition(xOffset, yOffset + i);
            Console.Write(_screen[i]);
        }
    }

    private static void DrawWallsDebug()
    {
        Console.SetCursorPosition(0, 0);
        Console.WriteLine("Debug Mode Enabled");

        List<Vector2> walls = [];
        for( var x = 0; x < GlobalSize.X; x++ )
        {
            for( var y = 0; y < GlobalSize.Y; y++ )
            {
                if (WallMap[x,y])
                    walls.Add(new Vector2(x,y));
            }
        }
        List<Vector2> entities = [];
        foreach(var (Position, Size) in EntityManager.GetPositionalData())
        {
            for(var x = 0; x <= Size.X; x++ )
            {
                for(var y = 0; y <= Size.Y;y++ )
                {
                    entities.Add(new Vector2(Position.X + x, Position.Y + y));
                }
            }
        }


        HandleDebugDraw(walls);
        HandleDebugDraw(entities, ConsoleColor.Yellow);
    }



    public static List<ICollider> GetCollisions()
    {
        (var Position, var _) = PlayerController.GetPlayerInfo();
        var result = new List<ICollider>();

        result.AddRange(EntityManager.GetCollisions(new(Position, _heroSize)));

        return result;
    }

    public static bool Overlaps(int ax, int ay, int aw, int ah, int bx, int by, int bw, int bh)
        => ax < bx + bw &&
               bx < ax + aw &&
               ay < by + bh &&
               by < ay + ah;

    public static void HandleDebugDraw(List<Vector2> points, ConsoleColor color = ConsoleColor.Red)
    {
        Console.BackgroundColor = color;
        foreach (var point in points)
        {
            Console.SetCursorPosition(point.X + GlobalMapOffset.X, point.Y + GlobalMapOffset.Y);

            Console.Write(' ');
        }
        Console.BackgroundColor = ConsoleColor.Black;
    }
}
