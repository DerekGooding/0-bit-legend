using _0_Bit_Legend.Maps;
using _0_Bit_Legend.Model;
using System.Runtime.InteropServices;

namespace _0_Bit_Legend;

public static class MainProgram
{
    public static LinkMovement LinkMovement { get; } = new();
    public static EnemyMovement EnemyMovement { get; } = new();

    private static GameFlag _flags = GameFlag.None;

    public static bool HasFlag(GameFlag flag) => (_flags & flag) != 0;
    public static bool HasFlags(GameFlag[] flags) => flags.All(flag => (_flags & flag) != 0);
    public static void SetFlag(GameFlag flag, bool value)
    {
        if (value)
            _flags |= flag;
        else
            _flags &= ~flag;
    }

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
    public static int iFrames = 0;

    private static int _frames;
    private static string _hud = "";
    private static bool _attacking;
    private static bool _start;


    public static void Main()
    {
        InitializeMaps();
        // Win32 API to check key state
        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(int vKey);

        // WASD keys
        const int VK_W = 0x57;
        const int VK_A = 0x41;
        const int VK_S = 0x53;
        const int VK_D = 0x44;

        // Arrow keys
        const int VK_UP = 0x26;
        const int VK_LEFT = 0x25;
        const int VK_DOWN = 0x28;
        const int VK_RIGHT = 0x27;

        // Attack keys
        const int VK_LSHIFT = 0xA0;
        const int VK_RSHIFT = 0xA1;

        Console.CursorVisible = false;
        LoadMap(0, 52, 18, Direction.Up);

        var credits = "                                  THANKS   LINK,                                                      #                                  YOU'RE   THE   HERO   OF   HYRULE.                                  #                                                                                                      #                                            =<>=    /\\                                                #                                            s^^s   /  |                                               #                                           ss~~ss |^  |                                               #                                           ~~~~~~ |_=_|                                               #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                              Awake,  my  young  Hero,                                                #                              For  peace  waits  not  on  the  morrow.                                #                              Now  go;  take  this  into  the  unknown:                               #                              It's  dangerous  to  go  alone!                                         #                                                                                                      #                              The  moon  sets,  and  the  moon  rises;                                #                              Darkness  only  this  night  comprises.                                 #                              What's  to  hope  with  a  quest  so  foggy?                            #                              It's  a  secret  to  everybody!                                         #                                                                                                      #                              Finally,  peace  returns  to  Hyrule.                                   #                              And  when  calamity  fell  succesful,                                   #                              The  dream  of  a  legend  lifted  clear:                               #                              Another  quest  will  start  from  here!                                #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                           ==================== STAFF =====================                           #                           =                                              =                           #                           =                                              =                           #                           =      PRODUCER....     Jayden Newman          =                           #                           =                                              =                           #                           =                                              =                           #                           =      PROGRAMMER.....   Jayden Newman         =                           #                           =                                              =                           #                           =                                              =                           #                           =      DESIGNER....    Jayden Newman           =                           #                           =                                              =                           #                           =                                              =                           #                           =                 <***>                        =                           #                           =          FFF     S^SSS>                      =                           #                           =          FFF     *S  SS>                     =                           #                           =                     =S>                      =                           #                           =                    =*SSSS**>                 =                           #                           =                    =*SSSSS*                  =                           #                           =                    ===  ==                   =                           #                           =                                              =                           #                           =                                              =                           #                           =      INSPIRATION...   Nintendo's             =                           #                           =                       The Legend of Zelda    =                           #                           =                                              =                           #                           =     ttt                                      =                           #                           =     tt^t                                     =                           #                           =     tttt                                     =                           #                           =                                              =                           #                           ================================================                           #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                            0-Bit  Legend                                             #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                         =====================================================                        #                         =~~~~                ~~            ~~            ~~~=                        #                         =~    ~~~   ~~~~~~~~~MM~~~~~M~~~         ~~~~~~     =                        #                         =  ~~      ~~~~MM~~~MMMM~~~MMM~M~~~~~               =                        #                         =~~  ~~~~~~~~MMMMMMMMMMMM~MMMMMMM~~MM~~~~~     ~~MMM=                        #                         =MM~~~      MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM=                        #                         =MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM...  ...MMMM=                        #                         =...     .......                   .....   .........=                        #                         =()......     ... ()......  ..()..()..()()()   ()()(=                        #                         =()() ()()  ()()..()()..()()()()()  ()   ()()()    (=                        #                         =()      /\\ ()()()()         ()()()     ()()   ()()(=                        #                         =  ()   |  \\ - ()  ()()()()  ()  ()()    ()()()()   =                        #                         =   XXXX|  ^|-SSS ()()   () ()()()   ()  () ()()()  =                        #                         = XXXXXX|_=_|-XXX      ()    ()()  ()() ()()()() () =                        #                         =XXXXXXXXXXXXXXXX ()  ()()()() ()()() ()()  ()()    =                        #                         =XXXXXXXXXXXXXXX  ()()()()()()()()()()()()()()()()()=                        #                         =====================================================                        #";
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
            UpdateHud();

            for (var i = 0; i < 33; i++)
            {
                if (Health > 0 && !HasFlag(GameFlag.GameOver))
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

            if (Health > 0 && !HasFlag(GameFlag.GameOver))
            {
                if (!HasFlag(GameFlag.Hit))
                {
                    if (!_attacking)
                    {
                        if ((GetAsyncKeyState(VK_W) & 0x8000) != 0 || (GetAsyncKeyState(VK_UP) & 0x8000) != 0)
                        {
                            LinkMovement.MoveLink(LinkMovement.GetPosX(), LinkMovement.GetPosY() - 1, Direction.Up);
                        }
                        else if ((GetAsyncKeyState(VK_A) & 0x8000) != 0 || (GetAsyncKeyState(VK_LEFT) & 0x8000) != 0)
                        {
                            LinkMovement.MoveLink(LinkMovement.GetPosX() - 2, LinkMovement.GetPosY(), Direction.Left);
                        }
                        else if ((GetAsyncKeyState(VK_S) & 0x8000) != 0 || (GetAsyncKeyState(VK_DOWN) & 0x8000) != 0)
                        {
                            LinkMovement.MoveLink(LinkMovement.GetPosX(), LinkMovement.GetPosY() + 1, Direction.Down);
                        }
                        else if ((GetAsyncKeyState(VK_D) & 0x8000) != 0 || (GetAsyncKeyState(VK_RIGHT) & 0x8000) != 0)
                        {
                            LinkMovement.MoveLink(LinkMovement.GetPosX() + 2, LinkMovement.GetPosY(), Direction.Right);
                        }
                        else if (((GetAsyncKeyState(VK_LSHIFT) & 0x8000) != 0 || (GetAsyncKeyState(VK_RSHIFT) & 0x8000) != 0) && HasFlag(GameFlag.HasSword))
                        {
                            LinkMovement.Attack(LinkMovement.GetPrev(), _attacking);
                            _attacking = true;
                        }

                        if (!HasFlag(GameFlag.Hit) && waitEnemies <= 0)
                        {
                            waitEnemies = 2;
                            for (var i = 0; i < EnemyMovement.GetTotal(); i++)
                            {
                                var passed = false;
                                var rnd1 = Random.Shared.Next(10);

                                if (EnemyMovement.GetEnemyType(i) == EnemyType.Octorok)
                                {
                                    if (rnd1 > 2)
                                    {
                                        if (EnemyMovement.GetPrev1(i) == Direction.Up)
                                        {
                                            passed = !EnemyMovement.Move(i,
                                                                         EnemyMovement.GetEnemyType(i),
                                                                         EnemyMovement.GetPosX(i),
                                                                         EnemyMovement.GetPosY(i) - 1,
                                                                         Direction.Up,
                                                                         -1,
                                                                         false);
                                        }
                                        else if (EnemyMovement.GetPrev1(i) == Direction.Left)
                                        {
                                            passed = !EnemyMovement.Move(i,
                                                                         EnemyMovement.GetEnemyType(i),
                                                                         EnemyMovement.GetPosX(i) - 2,
                                                                         EnemyMovement.GetPosY(i),
                                                                         Direction.Left,
                                                                         -1,
                                                                         false);
                                        }
                                        else if (EnemyMovement.GetPrev1(i) == Direction.Down)
                                        {
                                            passed = !EnemyMovement.Move(i,
                                                                         EnemyMovement.GetEnemyType(i),
                                                                         EnemyMovement.GetPosX(i),
                                                                         EnemyMovement.GetPosY(i) + 1,
                                                                         Direction.Down,
                                                                         -1,
                                                                         false);
                                        }
                                        else if (EnemyMovement.GetPrev1(i) == Direction.Right)
                                        {
                                            passed = !EnemyMovement.Move(i,
                                                                         EnemyMovement.GetEnemyType(i),
                                                                         EnemyMovement.GetPosX(i) + 2,
                                                                         EnemyMovement.GetPosY(i),
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
                                            EnemyMovement.Move(i,
                                                               EnemyMovement.GetEnemyType(i),
                                                               EnemyMovement.GetPosX(i),
                                                               EnemyMovement.GetPosY(i) - 1,
                                                               Direction.Up,
                                                               -1,
                                                               false);
                                        }
                                        else if (rnd2 == 2)
                                        {
                                            EnemyMovement.Move(i,
                                                               EnemyMovement.GetEnemyType(i),
                                                               EnemyMovement.GetPosX(i) - 2,
                                                               EnemyMovement.GetPosY(i),
                                                               Direction.Left,
                                                               -1,
                                                               false);
                                        }
                                        else if (rnd2 == 3)
                                        {
                                            EnemyMovement.Move(i,
                                                               EnemyMovement.GetEnemyType(i),
                                                               EnemyMovement.GetPosX(i),
                                                               EnemyMovement.GetPosY(i) + 1,
                                                               Direction.Down,
                                                               -1,
                                                               false);
                                        }
                                        else if (rnd2 == 4)
                                        {
                                            EnemyMovement.Move(i,
                                                               EnemyMovement.GetEnemyType(i),
                                                               EnemyMovement.GetPosX(i) + 2,
                                                               EnemyMovement.GetPosY(i),
                                                               Direction.Right,
                                                               -1,
                                                               false);
                                        }
                                    }
                                }
                                else if (EnemyMovement.GetEnemyType(i) == EnemyType.Spider)
                                {
                                    EnemyMovement.SetMotion(i, EnemyMovement.GetMotion(i) - 1);
                                    if (EnemyMovement.GetMotion(i) > 0)
                                    {
                                        if (rnd1 > 2)
                                        {
                                            if (EnemyMovement.GetPrev1(i) == Direction.Up)
                                            {
                                                passed = !EnemyMovement.Move(i,
                                                                             EnemyMovement.GetEnemyType(i),
                                                                             EnemyMovement.GetPosX(i) - 2,
                                                                             EnemyMovement.GetPosY(i) - 1,
                                                                             Direction.Up,
                                                                             -1,
                                                                             false);
                                            }
                                            else if (EnemyMovement.GetPrev1(i) == Direction.Left)
                                            {
                                                passed = !EnemyMovement.Move(i,
                                                                             EnemyMovement.GetEnemyType(i),
                                                                             EnemyMovement.GetPosX(i) + 2,
                                                                             EnemyMovement.GetPosY(i) - 1,
                                                                             Direction.Left,
                                                                             -1,
                                                                             false);
                                            }
                                            else if (EnemyMovement.GetPrev1(i) == Direction.Down)
                                            {
                                                passed = !EnemyMovement.Move(i,
                                                                             EnemyMovement.GetEnemyType(i),
                                                                             EnemyMovement.GetPosX(i) - 2,
                                                                             EnemyMovement.GetPosY(i) + 1,
                                                                             Direction.Down,
                                                                             -1,
                                                                             false);
                                            }
                                            else if (EnemyMovement.GetPrev1(i) == Direction.Right)
                                            {
                                                passed = !EnemyMovement.Move(i,
                                                                             EnemyMovement.GetEnemyType(i),
                                                                             EnemyMovement.GetPosX(i) + 2,
                                                                             EnemyMovement.GetPosY(i) + 1,
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
                                                EnemyMovement.Move(i,
                                                                   EnemyMovement.GetEnemyType(i),
                                                                   EnemyMovement.GetPosX(i) - 2,
                                                                   EnemyMovement.GetPosY(i) - 1,
                                                                   Direction.Up,
                                                                   -1,
                                                                   false);
                                            }
                                            else if (rnd2 == 2)
                                            {
                                                EnemyMovement.Move(i,
                                                                   EnemyMovement.GetEnemyType(i),
                                                                   EnemyMovement.GetPosX(i) + 2,
                                                                   EnemyMovement.GetPosY(i) - 1,
                                                                   Direction.Left,
                                                                   -1,
                                                                   false);
                                            }
                                            else if (rnd2 == 3)
                                            {
                                                EnemyMovement.Move(i,
                                                                   EnemyMovement.GetEnemyType(i),
                                                                   EnemyMovement.GetPosX(i) - 2,
                                                                   EnemyMovement.GetPosY(i) + 1,
                                                                   Direction.Down,
                                                                   -1,
                                                                   false);
                                            }
                                            else if (rnd2 == 4)
                                            {
                                                EnemyMovement.Move(i,
                                                                   EnemyMovement.GetEnemyType(i),
                                                                   EnemyMovement.GetPosX(i) + 2,
                                                                   EnemyMovement.GetPosY(i) + 1,
                                                                   Direction.Right,
                                                                   -1,
                                                                   false);
                                            }
                                        }
                                    }
                                    else if (EnemyMovement.GetMotion(i) <= -5)
                                    {
                                        EnemyMovement.SetMotion(i, 10);
                                    }
                                }
                                else if (EnemyMovement.GetEnemyType(i) == EnemyType.Bat)
                                {
                                    if (rnd1 > 4)
                                    {
                                        if (EnemyMovement.GetPrev1(i) == Direction.Up)
                                        {
                                            passed = !EnemyMovement.Move(i,
                                                                         EnemyMovement.GetEnemyType(i),
                                                                         EnemyMovement.GetPosX(i) - 2,
                                                                         EnemyMovement.GetPosY(i) - 1,
                                                                         Direction.Up,
                                                                         -1,
                                                                         false);
                                        }
                                        else if (EnemyMovement.GetPrev1(i) == Direction.Left)
                                        {
                                            passed = !EnemyMovement.Move(i,
                                                                         EnemyMovement.GetEnemyType(i),
                                                                         EnemyMovement.GetPosX(i) + 2,
                                                                         EnemyMovement.GetPosY(i) - 1,
                                                                         Direction.Left,
                                                                         -1,
                                                                         false);
                                        }
                                        else if (EnemyMovement.GetPrev1(i) == Direction.Down)
                                        {
                                            passed = !EnemyMovement.Move(i,
                                                                         EnemyMovement.GetEnemyType(i),
                                                                         EnemyMovement.GetPosX(i) - 2,
                                                                         EnemyMovement.GetPosY(i) + 1,
                                                                         Direction.Down,
                                                                         -1,
                                                                         false);
                                        }
                                        else if (EnemyMovement.GetPrev1(i) == Direction.Right)
                                        {
                                            passed = !EnemyMovement.Move(i,
                                                                         EnemyMovement.GetEnemyType(i),
                                                                         EnemyMovement.GetPosX(i) + 2,
                                                                         EnemyMovement.GetPosY(i) + 1,
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
                                            EnemyMovement.Move(i,
                                                               EnemyMovement.GetEnemyType(i),
                                                               EnemyMovement.GetPosX(i) - 2,
                                                               EnemyMovement.GetPosY(i) - 1,
                                                               Direction.Up,
                                                               -1,
                                                               false);
                                        }
                                        else if (rnd2 == 2)
                                        {
                                            EnemyMovement.Move(i,
                                                               EnemyMovement.GetEnemyType(i),
                                                               EnemyMovement.GetPosX(i) + 2,
                                                               EnemyMovement.GetPosY(i) - 1,
                                                               Direction.Left,
                                                               -1,
                                                               false);
                                        }
                                        else if (rnd2 == 3)
                                        {
                                            EnemyMovement.Move(i,
                                                               EnemyMovement.GetEnemyType(i),
                                                               EnemyMovement.GetPosX(i) - 2,
                                                               EnemyMovement.GetPosY(i) + 1,
                                                               Direction.Down,
                                                               -1,
                                                               false);
                                        }
                                        else if (rnd2 == 4)
                                        {
                                            EnemyMovement.Move(i,
                                                               EnemyMovement.GetEnemyType(i),
                                                               EnemyMovement.GetPosX(i) + 2,
                                                               EnemyMovement.GetPosY(i) + 1,
                                                               Direction.Right,
                                                               -1,
                                                               false);
                                        }
                                    }
                                }
                                else if (EnemyMovement.GetEnemyType(i) == EnemyType.Dragon && waitDragon <= 0)
                                {
                                    waitDragon = 4;
                                    EnemyMovement.SetMotion(i, EnemyMovement.GetMotion(i) - 1);

                                    var phase = Direction.Left;
                                    var speed = 1;
                                    if (EnemyMovement.GetMotion(i) <= 1)
                                    {
                                        phase = Direction.Right;
                                        speed = 0;
                                        if (EnemyMovement.GetMotion(i) <= 0)
                                        {
                                            EnemyMovement.Move(-1,
                                                               EnemyType.Fireball,
                                                               EnemyMovement.GetPosX(i) - 3,
                                                               EnemyMovement.GetPosY(i) + 3,
                                                               Direction.Up,
                                                               -1,
                                                               true);
                                            EnemyMovement.Move(-1,
                                                               EnemyType.Fireball,
                                                               EnemyMovement.GetPosX(i) - 3,
                                                               EnemyMovement.GetPosY(i) + 1,
                                                               Direction.Left,
                                                               -1,
                                                               true);
                                            EnemyMovement.Move(-1,
                                                               EnemyType.Fireball,
                                                               EnemyMovement.GetPosX(i) - 3,
                                                               EnemyMovement.GetPosY(i) - 1,
                                                               Direction.Down,
                                                               -1,
                                                               true);
                                            EnemyMovement.SetMotion(i, 12);
                                        }
                                    }

                                    if (EnemyMovement.GetPosY(i) <= 7)
                                    {
                                        EnemyMovement.Move(i,
                                                           EnemyMovement.GetEnemyType(i),
                                                           EnemyMovement.GetPosX(i),
                                                           EnemyMovement.GetPosY(i) + speed,
                                                           phase,
                                                           -1,
                                                           false);
                                    }
                                    else if (EnemyMovement.GetPosY(i) >= 19)
                                    {
                                        EnemyMovement.Move(
                                            i,
                                            EnemyMovement.GetEnemyType(i),
                                            EnemyMovement.GetPosX(i),
                                            EnemyMovement.GetPosY(i) - speed,
                                            phase,
                                            -1,
                                            false);
                                    }
                                    else
                                    {
                                        if (rnd1 <= 4)
                                        {
                                            EnemyMovement.Move(i,
                                                               EnemyMovement.GetEnemyType(i),
                                                               EnemyMovement.GetPosX(i),
                                                               EnemyMovement.GetPosY(i) + speed,
                                                               phase,
                                                               -1,
                                                               false);
                                        }
                                        else
                                        {
                                            EnemyMovement.Move(i,
                                                               EnemyMovement.GetEnemyType(i),
                                                               EnemyMovement.GetPosX(i),
                                                               EnemyMovement.GetPosY(i) - speed,
                                                               phase,
                                                               -1,
                                                               false);
                                        }
                                    }
                                }
                                else if (EnemyMovement.GetEnemyType(i) == EnemyType.Fireball)
                                {
                                    if (EnemyMovement.GetPrev1(i) == Direction.Up)
                                    {
                                        EnemyMovement.Move(i,
                                                           EnemyMovement.GetEnemyType(i),
                                                           EnemyMovement.GetPosX(i) - 3,
                                                           EnemyMovement.GetPosY(i) - 2,
                                                           Direction.Up,
                                                           -1,
                                                           false);
                                    }
                                    else if (EnemyMovement.GetPrev1(i) == Direction.Left)
                                    {
                                        EnemyMovement.Move(i,
                                                           EnemyMovement.GetEnemyType(i),
                                                           EnemyMovement.GetPosX(i) - 3,
                                                           EnemyMovement.GetPosY(i),
                                                           Direction.Left,
                                                           -1,
                                                           false);
                                    }
                                    else if (EnemyMovement.GetPrev1(i) == Direction.Down)
                                    {
                                        EnemyMovement.Move(i,
                                                           EnemyMovement.GetEnemyType(i),
                                                           EnemyMovement.GetPosX(i) - 3,
                                                           EnemyMovement.GetPosY(i) + 2,
                                                           Direction.Down,
                                                           -1,
                                                           false);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        LinkMovement.Attack(LinkMovement.GetPrev(), _attacking);
                        if (LinkMovement.GetPrev() == Direction.None)
                        {
                            if (LinkMovement.MovementWait <= 0)
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
                                _attacking = false;
                            }
                            else
                            {
                                if (CurrentMap is 0 or 4 or 8)
                                {
                                    LinkMovement.MoveLink(LinkMovement.GetPosX(), LinkMovement.GetPosY() - 1, Direction.Up);
                                    Thread.Sleep(50);
                                }
                                else if (CurrentMap is 6 or 7 or 9)
                                {
                                    LinkMovement.MoveLink(LinkMovement.GetPosX(), LinkMovement.GetPosY() + 1, Direction.Down);
                                    Thread.Sleep(50);
                                }

                                if (LinkMovement.GetPrev() is Direction.Up or Direction.Down)
                                {
                                    LinkMovement.SetPrev(Direction.None);
                                }
                                else
                                {
                                    LinkMovement.MovementWait--;
                                }
                            }
                        }
                        else
                        {
                            _attacking = false;
                        }
                        Thread.Sleep(100);
                    }
                }
                else
                {
                    Thread.Sleep(100);
                    SetFlag(GameFlag.Hit, false);

                    var x = 0;
                    var y = 0;

                    if (LinkMovement.GetPrev() == Direction.Up && LinkMovement.GetPosY() < 27)
                    {
                        y = 3;
                    }
                    else if (LinkMovement.GetPrev() == Direction.Left && LinkMovement.GetPosX() < 94)
                    {
                        x = 6;
                    }
                    else if (LinkMovement.GetPrev() == Direction.Down && LinkMovement.GetPosY() > 3)
                    {
                        y = -3;
                    }
                    else if (LinkMovement.GetPrev() == Direction.Right && LinkMovement.GetPosX() > 7)
                    {
                        x = -6;
                    }

                    LinkMovement.MoveLink(LinkMovement.GetPosX() + x, LinkMovement.GetPosY() + y, LinkMovement.GetPrev());
                }
            }
            else if (Health <= 0)
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
                        LinkMovement.PlayEffect('*');
                    }
                    else
                    {
                        LinkMovement.PlayEffect('+');
                    }
                    UpdateRow(LinkMovement.GetPosY() - 1);
                    UpdateRow(LinkMovement.GetPosY());
                    UpdateRow(LinkMovement.GetPosY() + 1);
                    UpdateRow(LinkMovement.GetPosY() + 2);
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
                    } else
                    {
                        LoadMap(9, 50, 25, Direction.Up);
                    }
                }
                _frames++;
            }
            else if (HasFlag(GameFlag.GameOver))
            {
                if (HasFlag(GameFlag.HasArmor)) credits = "                                  THANKS   LINK,                                                      #                                  YOU'RE   THE   HERO   OF   HYRULE.                                  #                                                                                                      #                                            =<>=    /\\                                                #                                            s^^s   /  |                                               #                                           ss~~ss |^##|                                               #                                           ~~~~~~ |#=#|                                               #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                              Awake,  my  young  Hero,                                                #                              For  peace  waits  not  on  the  morrow.                                #                              Now  go;  take  this  into  the  unknown:                               #                              It's  dangerous  to  go  alone!                                         #                                                                                                      #                              The  moon  sets,  and  the  moon  rises;                                #                              Darkness  only  this  night  comprises.                                 #                              What's  to  hope  with  a  quest  so  foggy?                            #                              It's  a  secret  to  everybody!                                         #                                                                                                      #                              Finally,  peace  returns  to  Hyrule.                                   #                              And  when  calamity  fell  succesful,                                   #                              The  dream  of  a  legend  lifted  clear:                               #                              Another  quest  will  start  from  here!                                #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                           ==================== STAFF =====================                           #                           =                                              =                           #                           =                                              =                           #                           =      PRODUCER....     Jayden Newman          =                           #                           =                                              =                           #                           =                                              =                           #                           =      PROGRAMMER.....   Jayden Newman         =                           #                           =                                              =                           #                           =                                              =                           #                           =      DESIGNER....    Jayden Newman           =                           #                           =                                              =                           #                           =                                              =                           #                           =                 <***>                        =                           #                           =          FFF     S^SSS>                      =                           #                           =          FFF     *S  SS>                     =                           #                           =                     =S>                      =                           #                           =                    =*SSSS**>                 =                           #                           =                    =*SSSSS*                  =                           #                           =                    ===  ==                   =                           #                           =                                              =                           #                           =                                              =                           #                           =      INSPIRATION...   Nintendo's             =                           #                           =                       The Legend of Zelda    =                           #                           =                                              =                           #                           =     ttt                                      =                           #                           =     tt^t                                     =                           #                           =     tttt                                     =                           #                           =                                              =                           #                           ================================================                           #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                            0-Bit  Legend                                             #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                         =====================================================                        #                         =~~~~                ~~            ~~            ~~~=                        #                         =~    ~~~   ~~~~~~~~~MM~~~~~M~~~         ~~~~~~     =                        #                         =  ~~      ~~~~MM~~~MMMM~~~MMM~M~~~~~               =                        #                         =~~  ~~~~~~~~MMMMMMMMMMMM~MMMMMMM~~MM~~~~~     ~~MMM=                        #                         =MM~~~      MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM=                        #                         =MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM...  ...MMMM=                        #                         =...     .......                   .....   .........=                        #                         =()......     ... ()......  ..()..()..()()()   ()()(=                        #                         =()() ()()  ()()..()()..()()()()()  ()   ()()()    (=                        #                         =()      /\\ ()()()()         ()()()     ()()   ()()(=                        #                         =  ()   |  \\ - ()  ()()()()  ()  ()()    ()()()()   =                        #                         =   XXXX|##^|-SSS ()()   () ()()()   ()  () ()()()  =                        #                         = XXXXXX|#=#|-XXX      ()    ()()  ()() ()()()() () =                        #                         =XXXXXXXXXXXXXXXX ()  ()()()() ()()() ()()  ()()    =                        #                         =XXXXXXXXXXXXXXX  ()()()()()()()()()()()()()()()()()=                        #                         =====================================================                        #";

                if (_frames < 13)
                {
                    for (var i = 0; i < 32; i++)
                    {
                        Map[_frames, i] = ' ';
                        Map[101 - _frames, i] = ' ';
                    }
                    UpdateRow(_frames);
                    UpdateRow(32 - _frames);

                    LinkMovement.PlaceZelda();
                    LinkMovement.MoveLink(LinkMovement.GetPosX(), LinkMovement.GetPosY(), Direction.Left);
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

                    LinkMovement.PlaceZelda();
                    LinkMovement.MoveLink(LinkMovement.GetPosX(), LinkMovement.GetPosY(), Direction.Left);
                }
                else if (_frames == 30)
                {
                    var count = 0;
                    for (var i = 0; i < 7; i++)
                    {
                        var row = "";
                        for (var j = 0; j < 102; j++)
                        {
                            row += credits[count];
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
                        row += credits[(103 * (_frames - 18)) + i];
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

            // Frame Rate: ~ 12 FPS
            Thread.Sleep(83);
        }
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

        while (EnemyMovement.GetTotal() != 0)
        {
            EnemyMovement.Remove(0, EnemyMovement.GetEnemyType(0));
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
            EnemyMovement.Move(-1, EnemyType.Octorok, 75, 13, Direction.Left, -1, true);
            EnemyMovement.Move(-1, EnemyType.Octorok, 9, 12, Direction.Right, -1, true);
            EnemyMovement.Move(-1, EnemyType.Octorok, 23, 26, Direction.Left, -1, true);
        }
        else if (mapNum == 2)
        {
            EnemyMovement.Move(-1, EnemyType.Octorok, 59, 23, Direction.Right, -1, true);
            EnemyMovement.Move(-1, EnemyType.Spider, 76, 6, Direction.Left, 5, true);
        }
        else if (mapNum == 3)
        {
            EnemyMovement.Move(-1, EnemyType.Spider, 44, 25, Direction.Left, 6, true);
            EnemyMovement.Move(-1, EnemyType.Octorok, 38, 14, Direction.Right, -1, true);
            EnemyMovement.Move(-1, EnemyType.Octorok, 83, 9, Direction.Left, -1, true);
        }
        else if (mapNum == 4)
        {
            EnemyMovement.Move(-1, EnemyType.Octorok, 35, 23, Direction.Left, -1, true);
            EnemyMovement.Move(-1, EnemyType.Octorok, 69, 6, Direction.Left, -1, true);
        }
        else if (mapNum == 5)
        {
            EnemyMovement.Move(-1, EnemyType.Spider, 81, 9, Direction.Left, 4, true);
            EnemyMovement.Move(-1, EnemyType.Spider, 32, 5, Direction.Right, 6, true);
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
                EnemyMovement.Move(-1, EnemyType.Bat, 70, 11, Direction.Left, -1, true);
            }
            if (cEnemies1 >= 2)
            {
                EnemyMovement.Move(-1, EnemyType.Bat, 32, 9, Direction.Right, -1, true);
            }
            if (cEnemies1 >= 3)
            {
                EnemyMovement.Move(-1, EnemyType.Bat, 53, 15, Direction.Left, -1, true);
            }
            if (cEnemies1 >= 4)
            {
                EnemyMovement.Move(-1, EnemyType.Bat, 20, 20, Direction.Right, -1, true);
            }
        }
        else if (mapNum == 11)
        {
            if (cEnemies2 >= 1)
            {
                EnemyMovement.Move(-1, EnemyType.Bat, 27, 9, Direction.Left, -1, true);
            }
            if (cEnemies2 >= 2)
            {
                EnemyMovement.Move(-1, EnemyType.Bat, 56, 20, Direction.Right, -1, true);
            }
            if (cEnemies2 >= 3)
            {
                EnemyMovement.Move(-1, EnemyType.Bat, 73, 15, Direction.Left, -1, true);
            }
            if (cEnemies2 >= 4)
            {
                EnemyMovement.Move(-1, EnemyType.Bat, 18, 11, Direction.Right, -1, true);
            }
        }
        else if (mapNum == 12)
        {
            if (!HasFlag(GameFlag.Dragon)) EnemyMovement.Move(-1, EnemyType.Dragon, 71, 13, Direction.Left, 12, true);
        }

        if ((CurrentMap == 2 || CurrentMap == 4) && posX == 21)
        {
            LinkMovement.SetPosX(posX);
            LinkMovement.SetPosY(posY);
            LinkMovement.DeployRaft(LinkMovement.GetPrev2());
        }
        else
        {
            LinkMovement.SpawnLink(posX, posY);
        }

        if (lCText)
        {
            lCText = false;
            SetFlag(GameFlag.Text, true);
            wait = 750;
        }

        _start = true;
    }

    public static void Wait(int time)
    {
        _attacking = true;
        if(LinkMovement.MovementWait == 0)
            LinkMovement.MovementWait = time;
        LinkMovement.SetPrev(Direction.None);
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

    public static void UpdateHud()
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

    //public static void Tabs(int tabs)
    //{
    //    for (var x = 0; x < tabs; x++)
    //    {
    //        Console.Write("  ");
    //    }
    //}
}
