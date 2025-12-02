using _0_Bit_Legend.Maps;
using _0_Bit_Legend.Model.Enums;

namespace _0_Bit_Legend;

public static class MainProgram
{
    public static PlayerController PlayerController { get; } = new();
    public static EnemyManager EnemyManager { get; } = new();
    private static readonly InputController _inputController = new();

    private static GameFlag _flags = GameFlag.None;
    public static GameState State { get; private set; } = GameState.Idle;

    private static readonly List<IMap> _maps = [];

    public static char[,] Map { get; } = new char[102, 33];
    public static int CurrentMap { get; private set; }

    private static readonly string[] _strs = new string[33];

    public static double Health { get; set; } = 3;
    public static int Rupees { get; set; }
    public static int Keys { get; set; }

    public static int cEnemies1 = 4;
    public static int cEnemies2 = 4;
    public static int waitEnemies = 1;
    public static int waitDragon = 1;
    public static int wait;
    public static int iFrames;

    private static int _frames;
    private static string _hud = "";
    private static bool _start;

    private static string _credits = string.Empty;

    public static void Main()
    {
        InitializeMaps();

        Console.CursorVisible = false;
        LoadMap(0, 52, 18, Direction.Up);

        _credits = "                                  THANKS   LINK,                                                      #                                  YOU'RE   THE   HERO   OF   HYRULE.                                  #                                                                                                      #                                            =<>=    /\\                                                #                                            s^^s   /  |                                               #                                           ss~~ss |^  |                                               #                                           ~~~~~~ |_=_|                                               #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                              Awake,  my  young  Hero,                                                #                              For  peace  waits  not  on  the  morrow.                                #                              Now  go;  take  this  into  the  unknown:                               #                              It's  dangerous  to  go  alone!                                         #                                                                                                      #                              The  moon  sets,  and  the  moon  rises;                                #                              Darkness  only  this  night  comprises.                                 #                              What's  to  hope  with  a  quest  so  foggy?                            #                              It's  a  secret  to  everybody!                                         #                                                                                                      #                              Finally,  peace  returns  to  Hyrule.                                   #                              And  when  calamity  fell  succesful,                                   #                              The  dream  of  a  legend  lifted  clear:                               #                              Another  quest  will  start  from  here!                                #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                           ==================== STAFF =====================                           #                           =                                              =                           #                           =                                              =                           #                           =      PRODUCER....     Jayden Newman          =                           #                           =                                              =                           #                           =                                              =                           #                           =      PROGRAMMER.....   Jayden Newman         =                           #                           =                                              =                           #                           =                                              =                           #                           =      DESIGNER....    Jayden Newman           =                           #                           =                                              =                           #                           =                                              =                           #                           =                 <***>                        =                           #                           =          FFF     S^SSS>                      =                           #                           =          FFF     *S  SS>                     =                           #                           =                     =S>                      =                           #                           =                    =*SSSS**>                 =                           #                           =                    =*SSSSS*                  =                           #                           =                    ===  ==                   =                           #                           =                                              =                           #                           =                                              =                           #                           =      INSPIRATION...   Nintendo's             =                           #                           =                       The Legend of Zelda    =                           #                           =                                              =                           #                           =     ttt                                      =                           #                           =     tt^t                                     =                           #                           =     tttt                                     =                           #                           =                                              =                           #                           ================================================                           #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                            0-Bit  Legend                                             #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                         =====================================================                        #                         =~~~~                ~~            ~~            ~~~=                        #                         =~    ~~~   ~~~~~~~~~MM~~~~~M~~~         ~~~~~~     =                        #                         =  ~~      ~~~~MM~~~MMMM~~~MMM~M~~~~~               =                        #                         =~~  ~~~~~~~~MMMMMMMMMMMM~MMMMMMM~~MM~~~~~     ~~MMM=                        #                         =MM~~~      MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM=                        #                         =MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM...  ...MMMM=                        #                         =...     .......                   .....   .........=                        #                         =()......     ... ()......  ..()..()..()()()   ()()(=                        #                         =()() ()()  ()()..()()..()()()()()  ()   ()()()    (=                        #                         =()      /\\ ()()()()         ()()()     ()()   ()()(=                        #                         =  ()   |  \\ - ()  ()()()()  ()  ()()    ()()()()   =                        #                         =   XXXX|  ^|-SSS ()()   () ()()()   ()  () ()()()  =                        #                         = XXXXXX|_=_|-XXX      ()    ()()  ()() ()()()() () =                        #                         =XXXXXXXXXXXXXXXX ()  ()()()() ()()() ()()  ()()    =                        #                         =XXXXXXXXXXXXXXX  ()()()()()()()()()()()()()()()()()=                        #                         =====================================================                        #";
        while (_frames < 118)
        {
            waitEnemies--;
            waitDragon--;

            if (iFrames > 0)
            {
                iFrames--;
            } else
            {
                iFrames = 0;
            }

            Console.SetCursorPosition(0, 4);
            DrawHud();
            DrawGame();

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

            // Frame Rate: ~ 12 FPS
            Thread.Sleep(83);
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
            for (var i = 0; i < EnemyManager.GetTotal(); i++)
            {
                var passed = false;
                var rnd1 = Random.Shared.Next(10);

                if (EnemyManager.GetEnemyType(i) == EnemyType.Octorok)
                {
                    if (rnd1 > 2)
                    {
                        if (EnemyManager.GetPrev1(i) == Direction.Up)
                        {
                            passed = !EnemyManager.Move(i,
                                                            EnemyManager.GetEnemyType(i),
                                                            EnemyManager.GetPosX(i),
                                                            EnemyManager.GetPosY(i) - 1,
                                                            Direction.Up,
                                                            -1,
                                                            false);
                        }
                        else if (EnemyManager.GetPrev1(i) == Direction.Left)
                        {
                            passed = !EnemyManager.Move(i,
                                                            EnemyManager.GetEnemyType(i),
                                                            EnemyManager.GetPosX(i) - 2,
                                                            EnemyManager.GetPosY(i),
                                                            Direction.Left,
                                                            -1,
                                                            false);
                        }
                        else if (EnemyManager.GetPrev1(i) == Direction.Down)
                        {
                            passed = !EnemyManager.Move(i,
                                                            EnemyManager.GetEnemyType(i),
                                                            EnemyManager.GetPosX(i),
                                                            EnemyManager.GetPosY(i) + 1,
                                                            Direction.Down,
                                                            -1,
                                                            false);
                        }
                        else if (EnemyManager.GetPrev1(i) == Direction.Right)
                        {
                            passed = !EnemyManager.Move(i,
                                                            EnemyManager.GetEnemyType(i),
                                                            EnemyManager.GetPosX(i) + 2,
                                                            EnemyManager.GetPosY(i),
                                                            Direction.Right,
                                                            -1,
                                                            false);
                        }
                    }
                    else
                    {
                        passed = true;
                    }

                    if (passed)
                    {
                        var rnd2 = Random.Shared.Next(4) + 1;
                        if (rnd2 == 1)
                        {
                            EnemyManager.Move(i,
                                                EnemyManager.GetEnemyType(i),
                                                EnemyManager.GetPosX(i),
                                                EnemyManager.GetPosY(i) - 1,
                                                Direction.Up,
                                                -1,
                                                false);
                        }
                        else if (rnd2 == 2)
                        {
                            EnemyManager.Move(i,
                                                EnemyManager.GetEnemyType(i),
                                                EnemyManager.GetPosX(i) - 2,
                                                EnemyManager.GetPosY(i),
                                                Direction.Left,
                                                -1,
                                                false);
                        }
                        else if (rnd2 == 3)
                        {
                            EnemyManager.Move(i,
                                                EnemyManager.GetEnemyType(i),
                                                EnemyManager.GetPosX(i),
                                                EnemyManager.GetPosY(i) + 1,
                                                Direction.Down,
                                                -1,
                                                false);
                        }
                        else if (rnd2 == 4)
                        {
                            EnemyManager.Move(i,
                                                EnemyManager.GetEnemyType(i),
                                                EnemyManager.GetPosX(i) + 2,
                                                EnemyManager.GetPosY(i),
                                                Direction.Right,
                                                -1,
                                                false);
                        }
                    }
                }
                else if (EnemyManager.GetEnemyType(i) == EnemyType.Spider)
                {
                    EnemyManager.SetMotion(i, EnemyManager.GetMotion(i) - 1);
                    if (EnemyManager.GetMotion(i) > 0)
                    {
                        if (rnd1 > 2)
                        {
                            if (EnemyManager.GetPrev1(i) == Direction.Up)
                            {
                                passed = !EnemyManager.Move(i,
                                                                EnemyManager.GetEnemyType(i),
                                                                EnemyManager.GetPosX(i) - 2,
                                                                EnemyManager.GetPosY(i) - 1,
                                                                Direction.Up,
                                                                -1,
                                                                false);
                            }
                            else if (EnemyManager.GetPrev1(i) == Direction.Left)
                            {
                                passed = !EnemyManager.Move(i,
                                                                EnemyManager.GetEnemyType(i),
                                                                EnemyManager.GetPosX(i) + 2,
                                                                EnemyManager.GetPosY(i) - 1,
                                                                Direction.Left,
                                                                -1,
                                                                false);
                            }
                            else if (EnemyManager.GetPrev1(i) == Direction.Down)
                            {
                                passed = !EnemyManager.Move(i,
                                                                EnemyManager.GetEnemyType(i),
                                                                EnemyManager.GetPosX(i) - 2,
                                                                EnemyManager.GetPosY(i) + 1,
                                                                Direction.Down,
                                                                -1,
                                                                false);
                            }
                            else if (EnemyManager.GetPrev1(i) == Direction.Right)
                            {
                                passed = !EnemyManager.Move(i,
                                                                EnemyManager.GetEnemyType(i),
                                                                EnemyManager.GetPosX(i) + 2,
                                                                EnemyManager.GetPosY(i) + 1,
                                                                Direction.Right,
                                                                -1,
                                                                false);
                            }
                        }
                        else
                        {
                            passed = true;
                        }

                        if (passed)
                        {
                            var rnd2 = Random.Shared.Next(4) + 1;
                            if (rnd2 == 1)
                            {
                                EnemyManager.Move(i,
                                                    EnemyManager.GetEnemyType(i),
                                                    EnemyManager.GetPosX(i) - 2,
                                                    EnemyManager.GetPosY(i) - 1,
                                                    Direction.Up,
                                                    -1,
                                                    false);
                            }
                            else if (rnd2 == 2)
                            {
                                EnemyManager.Move(i,
                                                    EnemyManager.GetEnemyType(i),
                                                    EnemyManager.GetPosX(i) + 2,
                                                    EnemyManager.GetPosY(i) - 1,
                                                    Direction.Left,
                                                    -1,
                                                    false);
                            }
                            else if (rnd2 == 3)
                            {
                                EnemyManager.Move(i,
                                                    EnemyManager.GetEnemyType(i),
                                                    EnemyManager.GetPosX(i) - 2,
                                                    EnemyManager.GetPosY(i) + 1,
                                                    Direction.Down,
                                                    -1,
                                                    false);
                            }
                            else if (rnd2 == 4)
                            {
                                EnemyManager.Move(i,
                                                    EnemyManager.GetEnemyType(i),
                                                    EnemyManager.GetPosX(i) + 2,
                                                    EnemyManager.GetPosY(i) + 1,
                                                    Direction.Right,
                                                    -1,
                                                    false);
                            }
                        }
                    }
                    else if (EnemyManager.GetMotion(i) <= -5)
                    {
                        EnemyManager.SetMotion(i, 10);
                    }
                }
                else if (EnemyManager.GetEnemyType(i) == EnemyType.Bat)
                {
                    if (rnd1 > 4)
                    {
                        if (EnemyManager.GetPrev1(i) == Direction.Up)
                        {
                            passed = !EnemyManager.Move(i,
                                                            EnemyManager.GetEnemyType(i),
                                                            EnemyManager.GetPosX(i) - 2,
                                                            EnemyManager.GetPosY(i) - 1,
                                                            Direction.Up,
                                                            -1,
                                                            false);
                        }
                        else if (EnemyManager.GetPrev1(i) == Direction.Left)
                        {
                            passed = !EnemyManager.Move(i,
                                                            EnemyManager.GetEnemyType(i),
                                                            EnemyManager.GetPosX(i) + 2,
                                                            EnemyManager.GetPosY(i) - 1,
                                                            Direction.Left,
                                                            -1,
                                                            false);
                        }
                        else if (EnemyManager.GetPrev1(i) == Direction.Down)
                        {
                            passed = !EnemyManager.Move(i,
                                                            EnemyManager.GetEnemyType(i),
                                                            EnemyManager.GetPosX(i) - 2,
                                                            EnemyManager.GetPosY(i) + 1,
                                                            Direction.Down,
                                                            -1,
                                                            false);
                        }
                        else if (EnemyManager.GetPrev1(i) == Direction.Right)
                        {
                            passed = !EnemyManager.Move(i,
                                                            EnemyManager.GetEnemyType(i),
                                                            EnemyManager.GetPosX(i) + 2,
                                                            EnemyManager.GetPosY(i) + 1,
                                                            Direction.Right,
                                                            -1,
                                                            false);
                        }
                    }
                    else
                    {
                        passed = true;
                    }

                    if (passed)
                    {
                        var rnd2 = Random.Shared.Next(4) + 1;
                        if (rnd2 == 1)
                        {
                            EnemyManager.Move(i,
                                                EnemyManager.GetEnemyType(i),
                                                EnemyManager.GetPosX(i) - 2,
                                                EnemyManager.GetPosY(i) - 1,
                                                Direction.Up,
                                                -1,
                                                false);
                        }
                        else if (rnd2 == 2)
                        {
                            EnemyManager.Move(i,
                                                EnemyManager.GetEnemyType(i),
                                                EnemyManager.GetPosX(i) + 2,
                                                EnemyManager.GetPosY(i) - 1,
                                                Direction.Left,
                                                -1,
                                                false);
                        }
                        else if (rnd2 == 3)
                        {
                            EnemyManager.Move(i,
                                                EnemyManager.GetEnemyType(i),
                                                EnemyManager.GetPosX(i) - 2,
                                                EnemyManager.GetPosY(i) + 1,
                                                Direction.Down,
                                                -1,
                                                false);
                        }
                        else if (rnd2 == 4)
                        {
                            EnemyManager.Move(i,
                                                EnemyManager.GetEnemyType(i),
                                                EnemyManager.GetPosX(i) + 2,
                                                EnemyManager.GetPosY(i) + 1,
                                                Direction.Right,
                                                -1,
                                                false);
                        }
                    }
                }
                else if (EnemyManager.GetEnemyType(i) == EnemyType.Dragon && waitDragon <= 0)
                {
                    waitDragon = 4;
                    EnemyManager.SetMotion(i, EnemyManager.GetMotion(i) - 1);

                    var phase = Direction.Left;
                    var speed = 1;
                    if (EnemyManager.GetMotion(i) <= 1)
                    {
                        phase = Direction.Right;
                        speed = 0;
                        if (EnemyManager.GetMotion(i) <= 0)
                        {
                            EnemyManager.Move(-1,
                                                EnemyType.Fireball,
                                                EnemyManager.GetPosX(i) - 3,
                                                EnemyManager.GetPosY(i) + 3,
                                                Direction.Up,
                                                -1,
                                                true);
                            EnemyManager.Move(-1,
                                                EnemyType.Fireball,
                                                EnemyManager.GetPosX(i) - 3,
                                                EnemyManager.GetPosY(i) + 1,
                                                Direction.Left,
                                                -1,
                                                true);
                            EnemyManager.Move(-1,
                                                EnemyType.Fireball,
                                                EnemyManager.GetPosX(i) - 3,
                                                EnemyManager.GetPosY(i) - 1,
                                                Direction.Down,
                                                -1,
                                                true);
                            EnemyManager.SetMotion(i, 12);
                        }
                    }

                    if (EnemyManager.GetPosY(i) <= 7)
                    {
                        EnemyManager.Move(i,
                                            EnemyManager.GetEnemyType(i),
                                            EnemyManager.GetPosX(i),
                                            EnemyManager.GetPosY(i) + speed,
                                            phase,
                                            -1,
                                            false);
                    }
                    else if (EnemyManager.GetPosY(i) >= 19)
                    {
                        EnemyManager.Move(
                            i,
                            EnemyManager.GetEnemyType(i),
                            EnemyManager.GetPosX(i),
                            EnemyManager.GetPosY(i) - speed,
                            phase,
                            -1,
                            false);
                    }
                    else
                    {
                        if (rnd1 <= 4)
                        {
                            EnemyManager.Move(i,
                                                EnemyManager.GetEnemyType(i),
                                                EnemyManager.GetPosX(i),
                                                EnemyManager.GetPosY(i) + speed,
                                                phase,
                                                -1,
                                                false);
                        }
                        else
                        {
                            EnemyManager.Move(i,
                                                EnemyManager.GetEnemyType(i),
                                                EnemyManager.GetPosX(i),
                                                EnemyManager.GetPosY(i) - speed,
                                                phase,
                                                -1,
                                                false);
                        }
                    }
                }
                else if (EnemyManager.GetEnemyType(i) == EnemyType.Fireball)
                {
                    if (EnemyManager.GetPrev1(i) == Direction.Up)
                    {
                        EnemyManager.Move(i,
                                            EnemyManager.GetEnemyType(i),
                                            EnemyManager.GetPosX(i) - 3,
                                            EnemyManager.GetPosY(i) - 2,
                                            Direction.Up,
                                            -1,
                                            false);
                    }
                    else if (EnemyManager.GetPrev1(i) == Direction.Left)
                    {
                        EnemyManager.Move(i,
                                            EnemyManager.GetEnemyType(i),
                                            EnemyManager.GetPosX(i) - 3,
                                            EnemyManager.GetPosY(i),
                                            Direction.Left,
                                            -1,
                                            false);
                    }
                    else if (EnemyManager.GetPrev1(i) == Direction.Down)
                    {
                        EnemyManager.Move(i,
                                            EnemyManager.GetEnemyType(i),
                                            EnemyManager.GetPosX(i) - 3,
                                            EnemyManager.GetPosY(i) + 2,
                                            Direction.Down,
                                            -1,
                                            false);
                    }
                }
            }
        }
    }
    private static void HandleWaiting()
    {
        Thread.Sleep(100);

        if (CurrentMap is 0 or 4 or 8)
        {
            PlayerController.MoveUp();
            Thread.Sleep(50);
        }
        else if (CurrentMap is 6 or 7 or 9)
        {
            PlayerController.MoveDown();
            Thread.Sleep(50);
        }

        PlayerController.MovementWait--;
        if (PlayerController.MovementWait <= 0)
        {
            if (CurrentMap == 0)
            {
                LoadMap(6, 50, 29, Direction.Up);
            }
            else if (CurrentMap == 4)
            {
                LoadMap(7, 50, 30, Direction.Up);
            }
            else if (CurrentMap == 8)
            {
                LoadMap(9, 50, 30, Direction.Up);
            }
            else if (CurrentMap == 6)
            {
                LoadMap(0, 16, 9, Direction.Down);
            }
            else if (CurrentMap == 7)
            {
                LoadMap(4, 86, 10, Direction.Down);
            }
            else if (CurrentMap == 9)
            {
                LoadMap(8, 51, 20, Direction.Down);
            }
            State = GameState.Idle;
        }
    }
    private static void HandleAttacking() => PlayerController.Attack();
    private static void HandleHit()
    {
        Thread.Sleep(100);
        State = GameState.Idle;

        if (PlayerController.GetPrev() == Direction.Up && PlayerController.Position.Y < 27)
        {
            PlayerController.MoveDown(3);
        }
        else if (PlayerController.GetPrev() == Direction.Left && PlayerController.Position.X < 94)
        {
            PlayerController.MoveRight(3);
        }
        else if (PlayerController.GetPrev() == Direction.Down && PlayerController.Position.Y > 3)
        {
            PlayerController.MoveUp(3);
        }
        else if (PlayerController.GetPrev() == Direction.Right && PlayerController.Position.X > 7)
        {
            PlayerController.MoveLeft(3);
        }
    }

    private static void HandleDeath()
    {
        if (_frames <= 16)
        {
            Thread.Sleep(50);
            for (var i = 0; i < 102; i++)
            {
                Map[i, _frames] = ' ';
                Map[i, 32 - _frames] = ' ';
            }
            UpdateRow(_frames);
            UpdateRow(32 - _frames);

            if (_frames % 2 == 0)
            {
                PlayerController.PlayEffect('*');
            }
            else
            {
                PlayerController.PlayEffect('+');
            }
            UpdateRow(PlayerController.Position.Y - 1);
            UpdateRow(PlayerController.Position.Y);
            UpdateRow(PlayerController.Position.Y + 1);
            UpdateRow(PlayerController.Position.Y + 2);
        }
        else if (_frames == 25)
        {
            for (var i = 0; i < 33; i++)
            {
                _strs[i] = "                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                         XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX                          #                         X                                                 X                          #                         X                                                 X                          #                         X                                                 X                          #                         X     Your hero fell.                             X                          #                         X                                                 X                          #                         X     Press any button to CONTINUE                X                          #                         X                                                 X                          #                         X                                                 X                          #                         X                                                 X                          #                         XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX                          #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #".Split("#")[i];
            }
        }
        else if (_frames == 26)
        {
            while (Console.KeyAvailable)
            {
                Console.ReadKey(true);
            }
            Console.ReadKey(true);

            Health = 3;
            _frames = -1;
            _start = false;

            cEnemies1 = 4;
            cEnemies2 = 4;

            if (CurrentMap <= 8)
            {
                LoadMap(0, 52, 15, Direction.Up);
            }
            else
            {
                LoadMap(9, 50, 25, Direction.Up);
            }
        }
        _frames++;
    }

    private static void HandleGameOver()
    {
        if (HasFlag(GameFlag.HasArmor)) _credits = "                                  THANKS   LINK,                                                      #                                  YOU'RE   THE   HERO   OF   HYRULE.                                  #                                                                                                      #                                            =<>=    /\\                                                #                                            s^^s   /  |                                               #                                           ss~~ss |^##|                                               #                                           ~~~~~~ |#=#|                                               #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                              Awake,  my  young  Hero,                                                #                              For  peace  waits  not  on  the  morrow.                                #                              Now  go;  take  this  into  the  unknown:                               #                              It's  dangerous  to  go  alone!                                         #                                                                                                      #                              The  moon  sets,  and  the  moon  rises;                                #                              Darkness  only  this  night  comprises.                                 #                              What's  to  hope  with  a  quest  so  foggy?                            #                              It's  a  secret  to  everybody!                                         #                                                                                                      #                              Finally,  peace  returns  to  Hyrule.                                   #                              And  when  calamity  fell  succesful,                                   #                              The  dream  of  a  legend  lifted  clear:                               #                              Another  quest  will  start  from  here!                                #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                           ==================== STAFF =====================                           #                           =                                              =                           #                           =                                              =                           #                           =      PRODUCER....     Jayden Newman          =                           #                           =                                              =                           #                           =                                              =                           #                           =      PROGRAMMER.....   Jayden Newman         =                           #                           =                                              =                           #                           =                                              =                           #                           =      DESIGNER....    Jayden Newman           =                           #                           =                                              =                           #                           =                                              =                           #                           =                 <***>                        =                           #                           =          FFF     S^SSS>                      =                           #                           =          FFF     *S  SS>                     =                           #                           =                     =S>                      =                           #                           =                    =*SSSS**>                 =                           #                           =                    =*SSSSS*                  =                           #                           =                    ===  ==                   =                           #                           =                                              =                           #                           =                                              =                           #                           =      INSPIRATION...   Nintendo's             =                           #                           =                       The Legend of Zelda    =                           #                           =                                              =                           #                           =     ttt                                      =                           #                           =     tt^t                                     =                           #                           =     tttt                                     =                           #                           =                                              =                           #                           ================================================                           #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                            0-Bit  Legend                                             #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                         =====================================================                        #                         =~~~~                ~~            ~~            ~~~=                        #                         =~    ~~~   ~~~~~~~~~MM~~~~~M~~~         ~~~~~~     =                        #                         =  ~~      ~~~~MM~~~MMMM~~~MMM~M~~~~~               =                        #                         =~~  ~~~~~~~~MMMMMMMMMMMM~MMMMMMM~~MM~~~~~     ~~MMM=                        #                         =MM~~~      MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM=                        #                         =MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM...  ...MMMM=                        #                         =...     .......                   .....   .........=                        #                         =()......     ... ()......  ..()..()..()()()   ()()(=                        #                         =()() ()()  ()()..()()..()()()()()  ()   ()()()    (=                        #                         =()      /\\ ()()()()         ()()()     ()()   ()()(=                        #                         =  ()   |  \\ - ()  ()()()()  ()  ()()    ()()()()   =                        #                         =   XXXX|##^|-SSS ()()   () ()()()   ()  () ()()()  =                        #                         = XXXXXX|#=#|-XXX      ()    ()()  ()() ()()()() () =                        #                         =XXXXXXXXXXXXXXXX ()  ()()()() ()()() ()()  ()()    =                        #                         =XXXXXXXXXXXXXXX  ()()()()()()()()()()()()()()()()()=                        #                         =====================================================                        #";

        if (_frames < 13)
        {
            for (var i = 0; i < 32; i++)
            {
                Map[_frames, i] = ' ';
                Map[101 - _frames, i] = ' ';
            }
            UpdateRow(_frames);
            UpdateRow(32 - _frames);

            PlayerController.PlaceZelda();
            PlayerController.MoveLeft(0);
        }
        else if (_frames < 30)
        {
            for (var i = 0; i < 102; i++)
            {
                Map[i, _frames - 13] = ' ';
                Map[i, 45 - _frames] = ' ';
            }
            UpdateRow(_frames - 13);
            UpdateRow(45 - _frames);

            PlayerController.PlaceZelda();
            PlayerController.MoveLeft(0);
        }
        else if (_frames == 30)
        {
            var count = 0;
            for (var i = 0; i < 7; i++)
            {
                var row = "";
                for (var j = 0; j < 102; j++)
                {
                    row += _credits[count];
                    count++;
                }
                _strs[i + 11] = row;

                count++;
            }
        }
        else if (_frames is > 30 and < 111)
        {
            if (_frames == 31) Thread.Sleep(3500);
            for (var i = 0; i < 31; i++)
            {
                _strs[i] = _strs[i + 1];
            }

            var row = "";
            for (var i = 0; i < 102; i++)
            {
                row += _credits[(103 * (_frames - 18)) + i];
            }
            _strs[31] = row;
            Thread.Sleep(600);
        }
        else if (_frames is >= 111 and < 117)
        {
            for (var i = 0; i < 31; i++)
            {
                _strs[i] = _strs[i + 1];
            }
            _strs[31] = "                                                                                                     ";
            Thread.Sleep(600);
        }
        _frames++;
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

    public static void LoadMap(int mapNum, int posX, int posY, Direction direction)
    {
        var map = string.Concat(_maps[mapNum].Raw);

        var lCText = false;

        if (mapNum == 6 && HasFlag(GameFlag.HasSword))
        {
            map = $"{map.AsSpan(0, 1854)}=XXXXXXXXXX=                                                                              =XXXXXXXXXX=#=XXXXXXXXXX=                                                                              =XXXXXXXXXX=#=XXXXXXXXXX=                                                                              =XXXXXXXXXX=#=XXXXXXXXXX=                                                                              =XXXXXXXXXX=#{map.AsSpan(2266)}";
        }
        else if (mapNum == 7 && HasFlag(GameFlag.HasRaft) && !HasFlag(GameFlag.HasArmor))
        {
            map = $"{map.AsSpan(0, 1751)}=XXXXXXXXXX=                                                                              =XXXXXXXXXX=#=XXXXXXXXXX=                                  =======               ## ##                 =XXXXXXXXXX=#=XXXXXXXXXX=                                  ==  = =               #####                 =XXXXXXXXXX=#=XXXXXXXXXX=                                                         ###                  =XXXXXXXXXX=#{map.AsSpan(2163)}";
        }
        else if (mapNum == 7 && !HasFlag(GameFlag.HasRaft) && HasFlag(GameFlag.HasArmor))
        {
            map = $"{map.AsSpan(0, 1751)}=XXXXXXXXXX=                 =====                                                        =XXXXXXXXXX=#=XXXXXXXXXX=                 *****            =======                                     =XXXXXXXXXX=#=XXXXXXXXXX=                 =====            ==  = =                                     =XXXXXXXXXX=#=XXXXXXXXXX=                 *****                                                        =XXXXXXXXXX=#{map.AsSpan(2163)}";
        }
        else if (mapNum == 7 && HasFlag(GameFlag.HasRaft) && HasFlag(GameFlag.HasArmor))
        {
            map = $"{map.AsSpan(0, 1751)}=XXXXXXXXXX=                                                                              =XXXXXXXXXX=#=XXXXXXXXXX=                                  =======                                     =XXXXXXXXXX=#=XXXXXXXXXX=                                  ==  = =                                     =XXXXXXXXXX=#=XXXXXXXXXX=                                                                              =XXXXXXXXXX=#{map.AsSpan(2163)}";
        }
        else if (mapNum == 9 && CurrentMap == 8)
        {
            lCText = true;
            map = $"{map.AsSpan(0, 1133)}=//////////=                                                                              =//////////=#=//////////=                                                                              =//////////=#=/////                       ||  TILL'  YOUR  FOES  ARE  BUT  HITHER,  ||                       /////=#=//  =======                 ||        SEALED  THE  PORTAL  IS.        ||                 =======  //=#=//=========                                                                              =========//=#=//== O>  ==                                                                              ==  <O ==//=#=//=========                                                                              =========//=#=//  =======                                                                              =======  //=#=/////                                                                                           ////=#=//////////=                                                                              =//////////=#=//////////=                                                                              =//////////=#{map.AsSpan(2266)}";
        }
        else if (mapNum == 9 && HasFlag(GameFlag.Door1) && !HasFlag(GameFlag.Door2))
        {
            map = $"{map.AsSpan(0, 1339)}=/////                   =====   =====                         =====   =====                    /////=#=//  XXXXXXX                                                                              =======  //=#=//XXXXXXXXX                                                                              =========//=#=//XXXXXXXXX                                                                              ==  <O ==//=#=//XXXXXXXXX                                                                              =========//=#=//  XXXXXXX                                                                              =======  //=#=/////                   XXXXX   XXXXX                         XXXXX   XXXXX                    /////=#{map.AsSpan(2060)}";
            if (HasFlag(GameFlag.Door3))
            {
                map = $"{map.AsSpan(0, 103)}=/////////////////////////////////////////////  =====  //////////////////////////////////////////////=#=///////////////////////////////////////////  =========  ////////////////////////////////////////////=#=///////////////////////////////////////////  =========  ////////////////////////////////////////////=#=///////////////////////////////////////////  =========  ////////////////////////////////////////////=#=//////////=================================  =========  ==================================//////////=#{map.AsSpan(618)}";
            }
        }
        else if (mapNum == 9 && !HasFlag(GameFlag.Door1) && HasFlag(GameFlag.Door2))
        {
            map = $"{map.AsSpan(0, 1339)}=/////                   =====   =====                         =====   =====                    /////=#=//  =======                                                                              XXXXXXX  //=#=//=========                                                                              XXXXXXXXX//=#=//== O>  ==                                                                              XXXXXXXXX//=#=//=========                                                                              XXXXXXXXX//=#=//  =======                                                                              XXXXXXX  //=#=/////                   XXXXX   XXXXX                         XXXXX   XXXXX                    /////=#{map.AsSpan(2060)}";
            if (HasFlag(GameFlag.Door3))
            {
                map = $"{map.AsSpan(0, 103)}=/////////////////////////////////////////////  =====  //////////////////////////////////////////////=#=///////////////////////////////////////////  =========  ////////////////////////////////////////////=#=///////////////////////////////////////////  =========  ////////////////////////////////////////////=#=///////////////////////////////////////////  =========  ////////////////////////////////////////////=#=//////////=================================  =========  ==================================//////////=#{map.AsSpan(618)}";
            }
        }
        else if (mapNum == 9 && HasFlag(GameFlag.Door1) && HasFlag(GameFlag.Door2))
        {
            map = $"{map.AsSpan(0, 1339)}=/////                   =====   =====                         =====   =====                    /////=#=//  XXXXXXX                                                                              XXXXXXX  //=#=//XXXXXXXXX                                                                              XXXXXXXXX//=#=//XXXXXXXXX                                                                              XXXXXXXXX//=#=//XXXXXXXXX                                                                              XXXXXXXXX//=#=//  XXXXXXX                                                                              XXXXXXX  //=#=/////                   XXXXX   XXXXX                         XXXXX   XXXXX                    /////=#{map.AsSpan(2060)}";
            if (HasFlag(GameFlag.Door3) && (cEnemies1 > 0 || cEnemies2 > 0))
            {
                map = $"{map.AsSpan(0, 103)}=/////////////////////////////////////////////  =====  //////////////////////////////////////////////=#=///////////////////////////////////////////  =========  ////////////////////////////////////////////=#=///////////////////////////////////////////  =========  ////////////////////////////////////////////=#=///////////////////////////////////////////  =========  ////////////////////////////////////////////=#=//////////=================================  =========  ==================================//////////=#{map.AsSpan(618)}";
            }
            else if (HasFlag(GameFlag.Door3))
            {
                map = $"{map.AsSpan(0, 103)}=/////////////////////////////////////////////  XXXXX  //////////////////////////////////////////////=#=///////////////////////////////////////////  XXXXXXXXX  ////////////////////////////////////////////=#=///////////////////////////////////////////  XXXXXXXXX  ////////////////////////////////////////////=#=///////////////////////////////////////////  XXXXXXXXX  ////////////////////////////////////////////=#=//////////=================================  XXXXXXXXX  ==================================//////////=#{map.AsSpan(618)}";
            }
        }
        else if (mapNum == 9 && HasFlag(GameFlag.Door3))
        {
            map = $"{map.AsSpan(0, 103)}=/////////////////////////////////////////////  =====  //////////////////////////////////////////////=#=///////////////////////////////////////////  =========  ////////////////////////////////////////////=#=///////////////////////////////////////////  =========  ////////////////////////////////////////////=#=///////////////////////////////////////////  =========  ////////////////////////////////////////////=#=//////////=================================  =========  ==================================//////////=#{map.AsSpan(618)}";
        }
        else if (mapNum == 12 && HasFlag(GameFlag.Dragon))
        {
            map = $"{map.AsSpan(0, 1339)}=//////////=                                                                                    /////=#=//////////=                                                                              XXXXXXX  //=#=//////////=                                                                              XXXXXXXXX//=#=//////////=                                                                              XXXXXXXXX//=#=//////////=                                                                              XXXXXXXXX//=#=//////////=                                                                              XXXXXXX  //=#=//////////=                                                                                    /////=#{map.AsSpan(2060)}";
        }

        var val = 0;

        //if (!(posX == 16 && posY == 6) && !(posX == 86 && posY == 7) && !(posX == 51 && posY == 17))
        //{
        //    CurrentMap = mapNum;
        //}
        CurrentMap = mapNum;

        while (EnemyManager.GetTotal() != 0)
        {
            EnemyManager.Remove(0, EnemyManager.GetEnemyType(0));
        }

        // Load the map
        for (var i = 0; i < 33; i++)
        {
            _strs[i] = "";
            for (var j = 0; j < 102; j++)
            {
                if (map[val] != '#')
                {
                    Map[j, i] = map[val];

                    _strs[i] += Map[j, i];
                }
                else
                {
                    j--;
                }
                val++;
            }
        }

        if (mapNum == 0 && !_start)
        {
            var skippedLines = "\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n";

            for (var i = 0; i < 14; i++)
            {
                skippedLines = skippedLines[2..];
                Console.Write(skippedLines);

                for (var j = 0; j < (i * 2) + 5; j++)
                {
                    Console.WriteLine("                                     " + map.Split("#")[j]);
                }
                Thread.Sleep(120);
            }
        }

        if (mapNum == 1)
        {
            EnemyManager.Move(-1, EnemyType.Octorok, 75, 13, Direction.Left, -1, true);
            EnemyManager.Move(-1, EnemyType.Octorok, 9, 12, Direction.Right, -1, true);
            EnemyManager.Move(-1, EnemyType.Octorok, 23, 26, Direction.Left, -1, true);
        }
        else if (mapNum == 2)
        {
            EnemyManager.Move(-1, EnemyType.Octorok, 59, 23, Direction.Right, -1, true);
            EnemyManager.Move(-1, EnemyType.Spider, 76, 6, Direction.Left, 5, true);
        }
        else if (mapNum == 3)
        {
            EnemyManager.Move(-1, EnemyType.Spider, 44, 25, Direction.Left, 6, true);
            EnemyManager.Move(-1, EnemyType.Octorok, 38, 14, Direction.Right, -1, true);
            EnemyManager.Move(-1, EnemyType.Octorok, 83, 9, Direction.Left, -1, true);
        }
        else if (mapNum == 4)
        {
            EnemyManager.Move(-1, EnemyType.Octorok, 35, 23, Direction.Left, -1, true);
            EnemyManager.Move(-1, EnemyType.Octorok, 69, 6, Direction.Left, -1, true);
        }
        else if (mapNum == 5)
        {
            EnemyManager.Move(-1, EnemyType.Spider, 81, 9, Direction.Left, 4, true);
            EnemyManager.Move(-1, EnemyType.Spider, 32, 5, Direction.Right, 6, true);
        }
        else if (mapNum == 8)
        {
            //enemyMovement.Move(-1, EnemyType.Spider, 26, 20, Direction.Right, 4, true);
            cEnemies1 = 4;
            cEnemies2 = 4;
        }
        else if (mapNum == 10)
        {
            if (cEnemies1 >= 1)
            {
                EnemyManager.Move(-1, EnemyType.Bat, 70, 11, Direction.Left, -1, true);
            }
            if (cEnemies1 >= 2)
            {
                EnemyManager.Move(-1, EnemyType.Bat, 32, 9, Direction.Right, -1, true);
            }
            if (cEnemies1 >= 3)
            {
                EnemyManager.Move(-1, EnemyType.Bat, 53, 15, Direction.Left, -1, true);
            }
            if (cEnemies1 >= 4)
            {
                EnemyManager.Move(-1, EnemyType.Bat, 20, 20, Direction.Right, -1, true);
            }
        }
        else if (mapNum == 11)
        {
            if (cEnemies2 >= 1)
            {
                EnemyManager.Move(-1, EnemyType.Bat, 27, 9, Direction.Left, -1, true);
            }
            if (cEnemies2 >= 2)
            {
                EnemyManager.Move(-1, EnemyType.Bat, 56, 20, Direction.Right, -1, true);
            }
            if (cEnemies2 >= 3)
            {
                EnemyManager.Move(-1, EnemyType.Bat, 73, 15, Direction.Left, -1, true);
            }
            if (cEnemies2 >= 4)
            {
                EnemyManager.Move(-1, EnemyType.Bat, 18, 11, Direction.Right, -1, true);
            }
        }
        else if (mapNum == 12)
        {
            if (!HasFlag(GameFlag.Dragon)) EnemyManager.Move(-1, EnemyType.Dragon, 71, 13, Direction.Left, 12, true);
        }

        if ((CurrentMap == 2 || CurrentMap == 4) && posX == 21)
        {
            PlayerController.SetPosition(new(posX, posY));
            PlayerController.DeployRaft(PlayerController.GetPrev2());
        }
        else
        {
            PlayerController.SpawnLink(posX, posY, direction);
        }

        if (lCText)
        {
            lCText = false;
            SetFlag(GameFlag.Text, true);
            wait = 750;
        }

        _start = true;
    }

    public static void WaitForTransition(int time = 2)
    {
        State = GameState.Waiting;
        PlayerController.MovementWait = time;
    }

    public static void UpdateRow(int row)
    {
        var line = "";
        for (var x = 0; x < 102; x++)
        {
            line += Map[x, row];
        }
        _strs[row] = line;
    }

    private static void DrawHud()
    {
        _hud = $"~~~~~~~~~~~~~~~~~~~~~~~~~~~#XXXXXXXXXXXXXXXXXXXXXXXXXXX#X                         X#X                         X#X                         X#X         HEALTH:         X#X                         X#X       <3  <3  <3        X#X                         X#X                         X#X  ---------------------  X#X                         X#X    r                    X#X   RRR          {Rupees,-4}     X#X    r                    X#X                         X#X  =======       {Keys,-4}     X#X  ==  = =                X#X                         X#X                         X#XXXXXXXXXXXXXXXXXXXXXXXXXXX#~~~~~~~~~~~~~~~~~~~~~~~~~~~#";
        _hud = Health > 2.5
            ? $"{_hud.AsSpan(0, 196)}X       <3  <3  <3        X#{_hud.AsSpan(224)}" : Health > 2
            ? $"{_hud.AsSpan(0, 196)}X       <3  <3  =         X#{_hud.AsSpan(224)}" : Health > 1.5
            ? $"{_hud.AsSpan(0, 196)}X       <3  <3            X#{_hud.AsSpan(224)}" : Health > 1
            ? $"{_hud.AsSpan(0, 196)}X       <3  =             X#{_hud.AsSpan(224)}" : Health > 0.5
            ? $"{_hud.AsSpan(0, 196)}X       <3                X#{_hud.AsSpan(224)}" : Health > 0
            ? $"{_hud.AsSpan(0, 196)}X       =                 X#{_hud.AsSpan(224)}" : $"{_hud.AsSpan(0, 196)}X                         X#{_hud.AsSpan(224)}";
    }

    private static void DrawGame()
    {
        for (var i = 0; i < 33; i++)
        {
            if (State is not GameState.Dead and not GameState.GameOver)
            {
                if (i is > 5 and < 28)
                {
                    Console.Write("            " + _hud.Split("#")[i - 6] + "            ");
                }
                else
                {
                    Console.Write("                                                   ");
                }
            }
            else
            {
                Console.Write("                                     ");
            }

            Console.WriteLine(_strs[i]);
        }

        // Clear remaining lines at the bottom of the console
        const int lastGameLine = 4 + 33; // Game content goes from line 4 to line 36 (inclusive), so next line is 37
        for (var i = lastGameLine; i < Console.WindowHeight; i++)
        {
            Console.SetCursorPosition(0, i);
            Console.Write(new string(' ', Console.WindowWidth));
        }

        Thread.Sleep(wait);
        wait = 0;
    }

    public static bool HasFlag(GameFlag flag) => (_flags & flag) != 0;
    public static bool HasFlags(GameFlag[] flags) => flags.All(flag => (_flags & flag) != 0);
    public static void SetFlag(GameFlag flag, bool value)
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
}
