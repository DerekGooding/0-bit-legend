using _0_Bit_Legend.Model;
using System.Runtime.InteropServices;

namespace _0_Bit_Legend;

public static class MainProgram
{
    public static LinkMovement LinkMovement { get; } = new();
    public static EnemyMovement EnemyMovement { get; } = new();

    private static GameFlags _flags = GameFlags.None;

    public static bool HasFlag(GameFlags flag) => (_flags & flag) != 0;
    public static bool HasFlags(GameFlags[] flags) => flags.All(flag => (_flags & flag) != 0);
    public static void SetFlag(GameFlags flag, bool value)
    {
        if (value)
            _flags |= flag;
        else
            _flags &= ~flag;
    }

    public static char[,] Map { get; } = new char[102, 33];
    public static int CurrentMap { get; set; } = 0;

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
        LoadMap(0, 52, 18, 'w');

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
                if (Health > 0 && !HasFlag(GameFlags.GameOver))
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
            var lastGameLine = 4 + 33; // Game content goes from line 4 to line 36 (inclusive), so next line is 37
            for (var i = lastGameLine; i < Console.WindowHeight; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write(new string(' ', Console.WindowWidth));
            }

            Thread.Sleep(wait);
            wait = 0;

            if (Health > 0 && !HasFlag(GameFlags.GameOver))
            {
                if (!HasFlag(GameFlags.Hit))
                {
                    if (!_attacking)
                    {
                        if ((GetAsyncKeyState(VK_W) & 0x8000) != 0 || (GetAsyncKeyState(VK_UP) & 0x8000) != 0)
                        {
                            LinkMovement.MoveLink(LinkMovement.GetPosX(), LinkMovement.GetPosY() - 1, 'w', false);
                        }
                        else if ((GetAsyncKeyState(VK_A) & 0x8000) != 0 || (GetAsyncKeyState(VK_LEFT) & 0x8000) != 0)
                        {
                            LinkMovement.MoveLink(LinkMovement.GetPosX() - 2, LinkMovement.GetPosY(), 'a', false);
                        }
                        else if ((GetAsyncKeyState(VK_S) & 0x8000) != 0 || (GetAsyncKeyState(VK_DOWN) & 0x8000) != 0)
                        {
                            LinkMovement.MoveLink(LinkMovement.GetPosX(), LinkMovement.GetPosY() + 1, 's', false);
                        }
                        else if ((GetAsyncKeyState(VK_D) & 0x8000) != 0 || (GetAsyncKeyState(VK_RIGHT) & 0x8000) != 0)
                        {
                            LinkMovement.MoveLink(LinkMovement.GetPosX() + 2, LinkMovement.GetPosY(), 'd', false);
                        }
                        else if (((GetAsyncKeyState(VK_LSHIFT) & 0x8000) != 0 || (GetAsyncKeyState(VK_RSHIFT) & 0x8000) != 0) && HasFlag(GameFlags.HasSword))
                        {
                            LinkMovement.Attack(LinkMovement.GetPrev(), _attacking);
                            _attacking = true;
                        }

                        if (!HasFlag(GameFlags.Hit) && waitEnemies <= 0)
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
                                        if (EnemyMovement.GetPrev1(i) == 'w')
                                        {
                                            passed = !EnemyMovement.Move(i, EnemyMovement.GetEnemyType(i), EnemyMovement.GetPosX(i), EnemyMovement.GetPosY(i) - 1, 'w', -1, false);
                                        }
                                        else if (EnemyMovement.GetPrev1(i) == 'a')
                                        {
                                            passed = !EnemyMovement.Move(i, EnemyMovement.GetEnemyType(i), EnemyMovement.GetPosX(i) - 2, EnemyMovement.GetPosY(i), 'a', -1, false);
                                        }
                                        else if (EnemyMovement.GetPrev1(i) == 's')
                                        {
                                            passed = !EnemyMovement.Move(i, EnemyMovement.GetEnemyType(i), EnemyMovement.GetPosX(i), EnemyMovement.GetPosY(i) + 1, 's', -1, false);
                                        }
                                        else if (EnemyMovement.GetPrev1(i) == 'd')
                                        {
                                            passed = !EnemyMovement.Move(i, EnemyMovement.GetEnemyType(i), EnemyMovement.GetPosX(i) + 2, EnemyMovement.GetPosY(i), 'd', -1, false);
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
                                            EnemyMovement.Move(i, EnemyMovement.GetEnemyType(i), EnemyMovement.GetPosX(i), EnemyMovement.GetPosY(i) - 1, 'w', -1, false);
                                        }
                                        else if (rnd2 == 2)
                                        {
                                            EnemyMovement.Move(i, EnemyMovement.GetEnemyType(i), EnemyMovement.GetPosX(i) - 2, EnemyMovement.GetPosY(i), 'a', -1, false);
                                        }
                                        else if (rnd2 == 3)
                                        {
                                            EnemyMovement.Move(i, EnemyMovement.GetEnemyType(i), EnemyMovement.GetPosX(i), EnemyMovement.GetPosY(i) + 1, 's', -1, false);
                                        }
                                        else if (rnd2 == 4)
                                        {
                                            EnemyMovement.Move(i, EnemyMovement.GetEnemyType(i), EnemyMovement.GetPosX(i) + 2, EnemyMovement.GetPosY(i), 'd', -1, false);
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
                                            if (EnemyMovement.GetPrev1(i) == 'w')
                                            {
                                                passed = !EnemyMovement.Move(i, EnemyMovement.GetEnemyType(i), EnemyMovement.GetPosX(i) - 2, EnemyMovement.GetPosY(i) - 1, 'w', -1, false);
                                            }
                                            else if (EnemyMovement.GetPrev1(i) == 'a')
                                            {
                                                passed = !EnemyMovement.Move(i, EnemyMovement.GetEnemyType(i), EnemyMovement.GetPosX(i) + 2, EnemyMovement.GetPosY(i) - 1, 'a', -1, false);
                                            }
                                            else if (EnemyMovement.GetPrev1(i) == 's')
                                            {
                                                passed = !EnemyMovement.Move(i, EnemyMovement.GetEnemyType(i), EnemyMovement.GetPosX(i) - 2, EnemyMovement.GetPosY(i) + 1, 's', -1, false);
                                            }
                                            else if (EnemyMovement.GetPrev1(i) == 'd')
                                            {
                                                passed = !EnemyMovement.Move(i, EnemyMovement.GetEnemyType(i), EnemyMovement.GetPosX(i) + 2, EnemyMovement.GetPosY(i) + 1, 'd', -1, false);
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
                                                EnemyMovement.Move(i, EnemyMovement.GetEnemyType(i), EnemyMovement.GetPosX(i) - 2, EnemyMovement.GetPosY(i) - 1, 'w', -1, false);
                                            }
                                            else if (rnd2 == 2)
                                            {
                                                EnemyMovement.Move(i, EnemyMovement.GetEnemyType(i), EnemyMovement.GetPosX(i) + 2, EnemyMovement.GetPosY(i) - 1, 'a', -1, false);
                                            }
                                            else if (rnd2 == 3)
                                            {
                                                EnemyMovement.Move(i, EnemyMovement.GetEnemyType(i), EnemyMovement.GetPosX(i) - 2, EnemyMovement.GetPosY(i) + 1, 's', -1, false);
                                            }
                                            else if (rnd2 == 4)
                                            {
                                                EnemyMovement.Move(i, EnemyMovement.GetEnemyType(i), EnemyMovement.GetPosX(i) + 2, EnemyMovement.GetPosY(i) + 1, 'd', -1, false);
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
                                        if (EnemyMovement.GetPrev1(i) == 'w')
                                        {
                                            passed = !EnemyMovement.Move(i, EnemyMovement.GetEnemyType(i), EnemyMovement.GetPosX(i) - 2, EnemyMovement.GetPosY(i) - 1, 'w', -1, false);
                                        }
                                        else if (EnemyMovement.GetPrev1(i) == 'a')
                                        {
                                            passed = !EnemyMovement.Move(i, EnemyMovement.GetEnemyType(i), EnemyMovement.GetPosX(i) + 2, EnemyMovement.GetPosY(i) - 1, 'a', -1, false);
                                        }
                                        else if (EnemyMovement.GetPrev1(i) == 's')
                                        {
                                            passed = !EnemyMovement.Move(i, EnemyMovement.GetEnemyType(i), EnemyMovement.GetPosX(i) - 2, EnemyMovement.GetPosY(i) + 1, 's', -1, false);
                                        }
                                        else if (EnemyMovement.GetPrev1(i) == 'd')
                                        {
                                            passed = !EnemyMovement.Move(i, EnemyMovement.GetEnemyType(i), EnemyMovement.GetPosX(i) + 2, EnemyMovement.GetPosY(i) + 1, 'd', -1, false);
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
                                            EnemyMovement.Move(i, EnemyMovement.GetEnemyType(i), EnemyMovement.GetPosX(i) - 2, EnemyMovement.GetPosY(i) - 1, 'w', -1, false);
                                        }
                                        else if (rnd2 == 2)
                                        {
                                            EnemyMovement.Move(i, EnemyMovement.GetEnemyType(i), EnemyMovement.GetPosX(i) + 2, EnemyMovement.GetPosY(i) - 1, 'a', -1, false);
                                        }
                                        else if (rnd2 == 3)
                                        {
                                            EnemyMovement.Move(i, EnemyMovement.GetEnemyType(i), EnemyMovement.GetPosX(i) - 2, EnemyMovement.GetPosY(i) + 1, 's', -1, false);
                                        }
                                        else if (rnd2 == 4)
                                        {
                                            EnemyMovement.Move(i, EnemyMovement.GetEnemyType(i), EnemyMovement.GetPosX(i) + 2, EnemyMovement.GetPosY(i) + 1, 'd', -1, false);
                                        }
                                    }
                                }
                                else if (EnemyMovement.GetEnemyType(i) == EnemyType.Dragon && waitDragon <= 0)
                                {
                                    waitDragon = 4;
                                    EnemyMovement.SetMotion(i, EnemyMovement.GetMotion(i) - 1);

                                    var phase = 'a';
                                    var speed = 1;
                                    if (EnemyMovement.GetMotion(i) <= 1)
                                    {
                                        phase = 'd';
                                        speed = 0;
                                        if (EnemyMovement.GetMotion(i) <= 0)
                                        {
                                            EnemyMovement.Move(-1, EnemyType.Fireball, EnemyMovement.GetPosX(i) - 3, EnemyMovement.GetPosY(i) + 3, 'w', -1, true);
                                            EnemyMovement.Move(-1, EnemyType.Fireball, EnemyMovement.GetPosX(i) - 3, EnemyMovement.GetPosY(i) + 1, 'a', -1, true);
                                            EnemyMovement.Move(-1, EnemyType.Fireball, EnemyMovement.GetPosX(i) - 3, EnemyMovement.GetPosY(i) - 1, 's', -1, true);
                                            EnemyMovement.SetMotion(i, 12);
                                        }
                                    }

                                    if (EnemyMovement.GetPosY(i) <= 7)
                                    {
                                        EnemyMovement.Move(i, EnemyMovement.GetEnemyType(i), EnemyMovement.GetPosX(i), EnemyMovement.GetPosY(i) + speed, phase, -1, false);
                                    }
                                    else if (EnemyMovement.GetPosY(i) >= 19)
                                    {
                                        EnemyMovement.Move(i, EnemyMovement.GetEnemyType(i), EnemyMovement.GetPosX(i), EnemyMovement.GetPosY(i) - speed, phase, -1, false);
                                    }
                                    else
                                    {
                                        if (rnd1 <= 4)
                                        {
                                            EnemyMovement.Move(i, EnemyMovement.GetEnemyType(i), EnemyMovement.GetPosX(i), EnemyMovement.GetPosY(i) + speed, phase, -1, false);
                                        }
                                        else
                                        {
                                            EnemyMovement.Move(i, EnemyMovement.GetEnemyType(i), EnemyMovement.GetPosX(i), EnemyMovement.GetPosY(i) - speed, phase, -1, false);
                                        }
                                    }
                                }
                                else if (EnemyMovement.GetEnemyType(i) == EnemyType.Fireball)
                                {
                                    if (EnemyMovement.GetPrev1(i) == 'w')
                                    {
                                        EnemyMovement.Move(i, EnemyMovement.GetEnemyType(i), EnemyMovement.GetPosX(i) - 3, EnemyMovement.GetPosY(i) - 2, 'w', -1, false);
                                    }
                                    else if (EnemyMovement.GetPrev1(i) == 'a')
                                    {
                                        EnemyMovement.Move(i, EnemyMovement.GetEnemyType(i), EnemyMovement.GetPosX(i) - 3, EnemyMovement.GetPosY(i), 'a', -1, false);
                                    }
                                    else if (EnemyMovement.GetPrev1(i) == 's')
                                    {
                                        EnemyMovement.Move(i, EnemyMovement.GetEnemyType(i), EnemyMovement.GetPosX(i) - 3, EnemyMovement.GetPosY(i) + 2, 's', -1, false);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        LinkMovement.Attack(LinkMovement.GetPrev(), _attacking);
                        if (LinkMovement.GetPrev() != 'w' && LinkMovement.GetPrev() != 'a' && LinkMovement.GetPrev() != 's' && LinkMovement.GetPrev() != 'd')
                        {
                            if (int.Parse(LinkMovement.GetPrev().ToString()) <= 0)
                            {
                                if (CurrentMap == 0)
                                {
                                    LoadMap(6, 50, 29, 'w');
                                }
                                else if (CurrentMap == 4)
                                {
                                    LoadMap(7, 50, 30, 'w');
                                }
                                else if (CurrentMap == 8)
                                {
                                    LoadMap(9, 50, 30, 'w');
                                }
                                else if (CurrentMap == 6)
                                {
                                    LoadMap(0, 16, 9, 's');
                                }
                                else if (CurrentMap == 7)
                                {
                                    LoadMap(4, 86, 10, 's');
                                }
                                else if (CurrentMap == 9)
                                {
                                    LoadMap(8, 51, 20, 's');
                                }
                                _attacking = false;
                            }
                            else
                            {
                                if (CurrentMap is 0 or 4 or 8)
                                {
                                    LinkMovement.MoveLink(LinkMovement.GetPosX(), LinkMovement.GetPosY() - 1, 'w', false);
                                    Thread.Sleep(50);
                                }
                                else if (CurrentMap is 6 or 7 or 9)
                                {
                                    LinkMovement.MoveLink(LinkMovement.GetPosX(), LinkMovement.GetPosY() + 1, 's', false);
                                    Thread.Sleep(50);
                                }

                                if (LinkMovement.GetPrev() == 'w' || LinkMovement.GetPrev() == 's')
                                {
                                    LinkMovement.SetPrev('0');
                                }
                                else
                                {
                                    LinkMovement.SetPrev((int.Parse(LinkMovement.GetPrev().ToString()) - 1).ToString()[0]);
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
                    SetFlag(GameFlags.Hit, false);

                    var x = 0;
                    var y = 0;

                    if (LinkMovement.GetPrev() == 'w' && LinkMovement.GetPosY() < 27)
                    {
                        y = 3;
                    }
                    else if (LinkMovement.GetPrev() == 'a' && LinkMovement.GetPosX() < 94)
                    {
                        x = 6;
                    }
                    else if (LinkMovement.GetPrev() == 's' && LinkMovement.GetPosY() > 3)
                    {
                        y = -3;
                    }
                    else if (LinkMovement.GetPrev() == 'd' && LinkMovement.GetPosX() > 7)
                    {
                        x = -6;
                    }

                    LinkMovement.MoveLink(LinkMovement.GetPosX() + x, LinkMovement.GetPosY() + y, LinkMovement.GetPrev(), false);
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
                        LoadMap(0, 52, 15, 'w');
                    } else
                    {
                        LoadMap(9, 50, 25, 'w');
                    }
                }
                _frames++;
            }
            else if (HasFlag(GameFlags.GameOver))
            {
                if (HasFlag(GameFlags.HasArmor)) credits = "                                  THANKS   LINK,                                                      #                                  YOU'RE   THE   HERO   OF   HYRULE.                                  #                                                                                                      #                                            =<>=    /\\                                                #                                            s^^s   /  |                                               #                                           ss~~ss |^##|                                               #                                           ~~~~~~ |#=#|                                               #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                              Awake,  my  young  Hero,                                                #                              For  peace  waits  not  on  the  morrow.                                #                              Now  go;  take  this  into  the  unknown:                               #                              It's  dangerous  to  go  alone!                                         #                                                                                                      #                              The  moon  sets,  and  the  moon  rises;                                #                              Darkness  only  this  night  comprises.                                 #                              What's  to  hope  with  a  quest  so  foggy?                            #                              It's  a  secret  to  everybody!                                         #                                                                                                      #                              Finally,  peace  returns  to  Hyrule.                                   #                              And  when  calamity  fell  succesful,                                   #                              The  dream  of  a  legend  lifted  clear:                               #                              Another  quest  will  start  from  here!                                #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                           ==================== STAFF =====================                           #                           =                                              =                           #                           =                                              =                           #                           =      PRODUCER....     Jayden Newman          =                           #                           =                                              =                           #                           =                                              =                           #                           =      PROGRAMMER.....   Jayden Newman         =                           #                           =                                              =                           #                           =                                              =                           #                           =      DESIGNER....    Jayden Newman           =                           #                           =                                              =                           #                           =                                              =                           #                           =                 <***>                        =                           #                           =          FFF     S^SSS>                      =                           #                           =          FFF     *S  SS>                     =                           #                           =                     =S>                      =                           #                           =                    =*SSSS**>                 =                           #                           =                    =*SSSSS*                  =                           #                           =                    ===  ==                   =                           #                           =                                              =                           #                           =                                              =                           #                           =      INSPIRATION...   Nintendo's             =                           #                           =                       The Legend of Zelda    =                           #                           =                                              =                           #                           =     ttt                                      =                           #                           =     tt^t                                     =                           #                           =     tttt                                     =                           #                           =                                              =                           #                           ================================================                           #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                            0-Bit  Legend                                             #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                         =====================================================                        #                         =~~~~                ~~            ~~            ~~~=                        #                         =~    ~~~   ~~~~~~~~~MM~~~~~M~~~         ~~~~~~     =                        #                         =  ~~      ~~~~MM~~~MMMM~~~MMM~M~~~~~               =                        #                         =~~  ~~~~~~~~MMMMMMMMMMMM~MMMMMMM~~MM~~~~~     ~~MMM=                        #                         =MM~~~      MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM=                        #                         =MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM...  ...MMMM=                        #                         =...     .......                   .....   .........=                        #                         =()......     ... ()......  ..()..()..()()()   ()()(=                        #                         =()() ()()  ()()..()()..()()()()()  ()   ()()()    (=                        #                         =()      /\\ ()()()()         ()()()     ()()   ()()(=                        #                         =  ()   |  \\ - ()  ()()()()  ()  ()()    ()()()()   =                        #                         =   XXXX|##^|-SSS ()()   () ()()()   ()  () ()()()  =                        #                         = XXXXXX|#=#|-XXX      ()    ()()  ()() ()()()() () =                        #                         =XXXXXXXXXXXXXXXX ()  ()()()() ()()() ()()  ()()    =                        #                         =XXXXXXXXXXXXXXX  ()()()()()()()()()()()()()()()()()=                        #                         =====================================================                        #";

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
                    LinkMovement.MoveLink(LinkMovement.GetPosX(), LinkMovement.GetPosY(), 'a', false);
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
                    LinkMovement.MoveLink(LinkMovement.GetPosX(), LinkMovement.GetPosY(), 'a', false);
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

    public static void LoadMap(int mapNum, int posX, int posY, char direction)
    {
        // Main Maps
        const string map0 = "======================================================                    ============================#=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=                    =XXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=                    =XXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX====================                    =XXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXXXXX///////XXXXXXXXXXXXXX=                                       =========XXXXXXXXXXXXXXXXXX=#=XXXXXXXXXXXX///////XXXXXXXXX======                                               =XXXXXXXXXXXXXXXXXX=#=XXXXXXXXX===///////==========                                                    =XXXXXXXXXXXXXXXXXX=#=XXXXXXXXX=                                                                       =XXXXXXXXXXXXXXXXXX=#=XXXXXXXXX=                                                                       =XXXXXXXXXXXXXXXXXX=#===========                                                                       =XXXXXXXXXXXXXXXXXX=#                                                                                  =====XXXXXXXXXXXXXX=#                                                                                      ======XXXXXXXXX=#                                                                                           ===========#                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #====                                                                                                  #=XX=                                                                                                  #=XX=                                                                                                  #=XX=                                                                                                  #=XX=                                                                                                  #=XX=                                                                                                  #=XX=                                                                                            ======#=XX=======                                                                                      =XXXX=#=XXXXXXXX=                                                                                 ======XXXX=#=XXXXXXXX=                                      ============================================XXXXXXXXX=#=XXXXXXXX=========                              =XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXXXXXXXXX================================XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#======================================================================================================#";
        const string map1 = "======================================================================================================#=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXXXXXXXXXXXXXXXXXXXXX======================================================XXXXXXXXXXXXXXXXXX=#=XXXXXXXXX====================                                                    =XXXXXXXXXXXXXXXXXX=#=XXXXXXXXX=                                                                       =XXXXXXXXXXXXXXXXXX=#=XXXXXXXXX=                                                                       =XXXXXXXXXXXXXXXXXX=#===========                                                                       =XXXXXXXXXXXXXXXXXX=#                                                                                  =====XXXXXXXXXXXXXX=#                       XXXXX               XXXXX               XXXXX                  ======XXXXXXXXX=#                       =XXX=               =XXX=               =XXX=                       =XXXXXXXXX=#                       =====               =====               =====                       ===========#                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #====                                                                                                  #=XX=                   XXXXX               XXXXX               XXXXX                            ======#=XX=                   =XXX=               =XXX=               =XXX=                     ========XXXX=#=XX=                   =====               =====               =====                     =XXXXXXXXXXX=#=XX=                                                                                     =XXXXXXXXXXX=#=XX======                                                                                =XXXXXXXXXXX=#=XXXXXXX=                                                                                =XXXXXXXXXXX=#=XXXXXXX==                                                                         =======XXXXXXXXXXX=#=XXXXXXXX=                                                                      ====XXXXXXXXXXXXXXXXX=#=XXXXXXXX=                                                                      =XXXXXXXXXXXXXXXXXXXX=#=XXXXXXXX=========                                                              =XXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXXXXXXXXX=====================================                    =======XXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=                    =XXXXXXXXXXXXXXXXXXXXXXXXXX=#======================================================                    ============================#";
        const string map2 = "=====         ~~~~~~~~~~~~~~ =============                       =====================================#=XXX=         ~~~~~~~~~~~~~~ =XXXXXXXXXXX=                       =XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXX=         ~~~~~~~~~~~~~~ =============                       =======XXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXX=         ~~~~~~~~~~~~~~                                           =XXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXX=         ~~~~~~~~~~~~~~                                           ============XXXXXXXXXXXXXXXXXX=#=XXX=         ~~~~~~~~~~~~~~                                                      =XXXXXXXXXXXXXXXXXX=#=XXX=         ~~~~~~~~~~~~~~                                                      =XXXXXXXXXXXXXXXXXX=#=XXX=         ~~~~~~~~~~~~~~                                                      =XXXXXXXXXXXXXXXXXX=#=XXX=         ~~~~~~~~~~~~~~                                                      ========XXXXXXXXXXX=#=XXX=         ~~~~~~~~~~~~~~                                                             =============#=XXX=         ~~~~~~~~~~~~~~                                                                          #=XXX=         ~~~~~~~~~~~~~~           XXXXX         XXXXX         XXXXX                              #=XXX=         ~~~~~~~~~~~~~~           XX=XX         XX=XX         XX=XX                              #=XXX=         ~~~~~~~~~~~~~~             =             =             =                                #=XXX=         ~~~~~~~~~~~~~~                                                                          #=XXX=         ~~~~~~~~~~~~~~                                                                          #=XXX=         ~~~~~~~~~~~~~~                                                                          #=XXX=         ~~~~~~~~~~~~~~                                                                          #=XXX=         ~~~~~~~~~~~~~~                                                                          #=XXX=         ~~~~~~~~~~~~~~           XXXXX         XXXXX         XXXXX                       =======#=XXX=         ~~~~~~~~~~~~~~           XX=XX         XX=XX         XX=XX                       =XXXXX=#=XXX=         ~~~~~~~~~~~~~~             =             =             =                         =XXXXX=#=XXX=         ~~~~~~~~~~~~~~                                                                   =XXXXX=#=XXX=====     ~~~~~~~~~~~~~~                                                               =====XXXXX=#=XXXXXXX=     ~~~~~~~~~~~~~~                                                               =XXXXXXXXX=#=XXXXXXX=     ~~~~~~~~~~~~~~                                                               =XXXXXXXXX=#=XXXXXXX=     ~~~~~~~~~~~~~~                                                         =======XXXXXXXXX=#=XXXXXXX======~~~~~~~~~~~~~~=====                                                    =XXXXXXXXXXXXXXX=#=XXXXXXXXXXXX=~~~~~~~~~~~~~~=XXX=                                  ===================XXXXXXXXXXXXXXX=#=XXXXXXXXXXXX=~~~~~~~~~~~~~~=XXX==========                         =XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXXXXX================XXXXXXXXXXXX===========================XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#======================================================================================================#";
        const string map3 = "=============================                                                  =======================#=XXXXXXXXXXXXXXXXXXXXXXXXXXX=                                                  =XXXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXX===================                                                  ======XXXXXXXXXXXXXXXX=#=XXXXXXXXX= XXXXX                                                                   =XXXXXXXXXXXXXXXX=#=XXXXXXXXX= XX=XX                                                                   =XXXXXXXXXXXXXXXX=#=XXXXXXXXX=   =                                                                     ==========XXXXXXX=#=XXX=======                                                                            XXXXX =XXXXXXX=#=XXX=                                                                                  XX=XX =XXXXXXX=#=XXX=                                                                                    =   =XXXXXXX=#=XXX=                                                                                        =XXXXXXX=#=XXX=                  XXXXX       XXXXX       XXXXX       XXXXX       XXXXX                 =XXXXXXX=#=XXX=                  XX=XX       XX=XX       XX=XX       XX=XX       XX=XX                 ====XXXX=#=====                    =           =           =           =           =                      =XXXX=#                                                                                                =XXXX=#                                                                                                =XXXX=#                                                                                                =XXXX=#                                                                                                =XXXX=#                                                                                                =XXXX=#                                                                                                =XXXX=#                       XXXXX       XXXXX       XXXXX       XXXXX       XXXXX                    ==XXX=#                       XX=XX       XX=XX       XX=XX       XX=XX       XX=XX                     =XXX=#                         =           =           =           =           =                 XXXXX =XXX=#                                                                                           XX=XX =XXX=#                                                                                             =   =XXX=#      XXXXX                                                                          XXXXX XXXXX =XXX=#===== XX=XX                                                                          XX=XX XX=XX =XXX=#=XXX=   =                                                                              =     =   =XXX=#=XXX======= XXXXX XXXXX                                                        XXXXX XXXXX =======XXX=#=XXXXXXXXX= XX=XX XX=XX                                                        XX=XX XX=XX =XXXXXXXXX=#=XXXXXXXXX=   =     =                                                            =     =   =XXXXXXXXX=#=XXXXXXXXX==================================================================================XXXXXXXXX=#=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#======================================================================================================#";
        const string map4 = "======================================================================================================#=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXXXXXXXXXXXXXXXXXXXXX====================================================XX///////XXXXXXXXXXX=#=XXXXXXXXX====================                                                  =XX///////XXXXXXXXXXX=#=XXXXXXXXX=   ~~~~~~~~~~~~~~                                                    ===///////=XXXXXXXXXX=#=XXXXXXXXX=   ~~~~~~~~~~~~~~                                                              =XXXXXXXXXX=#===========   ~~~~~~~~~~~~~~                                                              ============#              ~~~~~~~~~~~~~~                                                                          #              ~~~~~~~~~~~~~~           XXXXX         XXXXX         XXXXX                              #              ~~~~~~~~~~~~~~           =XXX=         =XXX=         =XXX=                              #              ~~~~~~~~~~~~~~           =====         =====         =====                              #              ~~~~~~~~~~~~~~                                                                          #              ~~~~~~~~~~~~~~                                                                          #              ~~~~~~~~~~~~~~                                                                          #              ~~~~~~~~~~~~~~                                                                          #              ~~~~~~~~~~~~~~                                                                          #              ~~~~~~~~~~~~~~           XXXXX         XXXXX         XXXXX                       =======#              ~~~~~~~~~~~~~~           =XXX=         =XXX=         =XXX=                       =XXXXX=#              ~~~~~~~~~~~~~~           =====         =====         =====                       =XXXXX=#              ~~~~~~~~~~~~~~                                                                   =XXXXX=#              ~~~~~~~~~~~~~~                                                               =====XXXXX=#              ~~~~~~~~~~~~~~                                                               =XXXXXXXXX=#              ~~~~~~~~~~~~~~                                                               =XXXXXXXXX=#=====         ~~~~~~~~~~~~~~                                                         =======XXXXXXXXX=#=XXX=         ~~~~~~~~~~~~~~ =====                                                   =XXXXXXXXXXXXXXX=#=XXX=         ~~~~~~~~~~~~~~ =XXX=                                 ===================XXXXXXXXXXXXXXX=#=XXX=         ~~~~~~~~~~~~~~ =XXX==========                        =XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXX=         ~~~~~~~~~~~~~~ =XXXXXXXXXXXX=                      ===XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXX=         ~~~~~~~~~~~~~~ =XXXXXXXXXXXX=                      =XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=====         ~~~~~~~~~~~~~~ ==============                      =====================================#";
        const string map5 = "======================================================================================================#=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXX===========================================================================XXXXXXXXXXXXXXXX=#=XXXXXXXXX= XXXXX                                                                   =XXXXXXXXXXXXXXXX=#=XXXXXXXXX= XXXXX                                                                   =XXXXXXXXXXXXXXXX=#=XXXXXXXXX=   =                                                                     ==========XXXXXXX=#=XXXXXXXXX=                                                                            XXXXX =XXXXXXX=#=XXX=======                                                                            XXXXX =XXXXXXX=#=XXX= XXXXX                                                                              =   =XXXXXXX=#=XXX= XXXXX                                                                                  =XXXXXXX=#=XXX=   =              XXXXX       XXXXX       XXXXX       XXXXX       XXXXX                 =XXXXXXX=#=XXX=                  =XXX=       =XXX=       =XXX=       =XXX=       =XXX=                 ====XXXX=#=XXX=                  =====       =====       =====       =====       =====                    =XXXX=#=====                                                                                           =XXXX=#                                                                                                =XXXX=#                                                                                                =XXXX=#                                                                                                =XXXX=#                                                                                                =XXXX=#                                                                                                =XXXX=#                       XXXXX       XXXXX       XXXXX       XXXXX       XXXXX                    ==XXX=#=====                  =XXX=       =XXX=       =XXX=       =XXX=       =XXX=                     =XXX=#=XXX=                  =====       =====       =====       =====       =====               XXXXX =XXX=#=XXX====                                                                                   XXXXX =XXX=#=XXXXXX=                                                                                     =   =XXX=#=XXXXXX= XXXXX                                                                       XXXXX XXXXX =XXX=#=XXXXXX= XXXXX                                                                       XXXXX XXXXX =XXX=#=XXXXXX=   =                                                                           =     =   =XXX=#=XXXXXX========== XXXXX                                                        XXXXX XXXXX =======XXX=#=XXXXXXXXXXXXXXX= XXXXX                                                        XXXXX XXXXX =XXXXXXXXX=#=XXXXXXXXXXXXXXX=   =                                                            =     =   =XXXXXXXXX=#=XXXXXXXXXXXXXXX=============                                                  =============XXXXXXXXX=#=XXXXXXXXXXXXXXXXXXXXXXXXXXX=                                                  =XXXXXXXXXXXXXXXXXXXXX=#=============================                                                  =======================#";

        // Caves
        var map6 = "======================================================================================================#=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXXX================================================================================XXXXXXXXXX=#=XXXXXXXXXX=                                                                              =XXXXXXXXXX=#=XXXXXXXXXX=                                                                              =XXXXXXXXXX=#=XXXXXXXXXX=                     IT'S   DANGEROUS   TO   GO   ALONE!                      =XXXXXXXXXX=#=XXXXXXXXXX=                                TAKE   THIS.                                  =XXXXXXXXXX=#=XXXXXXXXXX=                                                                              =XXXXXXXXXX=#=XXXXXXXXXX=                                   xxxxxxx                                    =XXXXXXXXXX=#=XXXXXXXXXX=              //////               x^ x ^x                //////              =XXXXXXXXXX=#=XXXXXXXXXX=              //////               /**=**\\                //////              =XXXXXXXXXX=#=XXXXXXXXXX=              ======              |_|***|_|               ======              =XXXXXXXXXX=#=XXXXXXXXXX=                                   |__|__|                                    =XXXXXXXXXX=#=XXXXXXXXXx=                                                                              =XXXXXXXXXX=#=XXXXXXXXXX=                                                                              =XXXXXXXXXX=#=XXXXXXXXXX=                                      S                                       =XXXXXXXXXX=#=XXXXXXXXXX=                                      S                                       =XXXXXXXXXX=#=XXXXXXXXXX=                                     ---                                      =XXXXXXXXXX=#=XXXXXXXXXX=                                      -                                       =XXXXXXXXXX=#=XXXXXXXXXX=                                                                              =XXXXXXXXXX=#=XXXXXXXXXX=                                                                              =XXXXXXXXXX=#=XXXXXXXXXX=                                                                              =XXXXXXXXXX=#=XXXXXXXXXX=                                                                              =XXXXXXXXXX=#=XXXXXXXXXX=                                                                              =XXXXXXXXXX=#=XXXXXXXXXX========================                                ========================XXXXXXXXXX=#=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=                                =XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=                                =XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=                                =XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=                                =XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#===================================                                ===================================#";
        var map7 = "======================================================================================================#=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXXX================================================================================XXXXXXXXXX=#=XXXXXXXXXX=                                                                              =XXXXXXXXXX=#=XXXXXXXXXX=                                                                              =XXXXXXXXXX=#=XXXXXXXXXX=                         BUY   SOMETHIN'   WILL   YA!                         =XXXXXXXXXX=#=XXXXXXXXXX=                                                                              =XXXXXXXXXX=#=XXXXXXXXXX=                                                                              =XXXXXXXXXX=#=XXXXXXXXXX=                                   xxxxxxx                                    =XXXXXXXXXX=#=XXXXXXXXXX=                                   x^ x ^x                                    =XXXXXXXXXX=#=XXXXXXXXXX=                                   /**=**\\                                    =XXXXXXXXXX=#=XXXXXXXXXX=                                  |_|***|_|                                   =XXXXXXXXXX=#=XXXXXXXXXX=                                   |__|__|                                    =XXXXXXXXXX=#=XXXXXXXXXx=                                                                              =XXXXXXXXXX=#=XXXXXXXXXX=                 =====                                                        =XXXXXXXXXX=#=XXXXXXXXXX=                 *****            =======               ## ##                 =XXXXXXXXXX=#=XXXXXXXXXX=                 =====            ==  = =               #####                 =XXXXXXXXXX=#=XXXXXXXXXX=                 *****                                   ###                  =XXXXXXXXXX=#=XXXXXXXXXX=                 RAFT              KEY                  ARMOR                 =XXXXXXXXXX=#=XXXXXXXXXX=                 35                10                   25                    =XXXXXXXXXX=#=XXXXXXXXXX=     r                                                                        =XXXXXXXXXX=#=XXXXXXXXXX=    RRR                                                                       =XXXXXXXXXX=#=XXXXXXXXXX=     r (X)                                                                    =XXXXXXXXXX=#=XXXXXXXXXX=                                                                              =XXXXXXXXXX=#=XXXXXXXXXX========================                                ========================XXXXXXXXXX=#=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=                                =XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=                                =XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=                                =XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=                                =XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#===================================                                ===================================#";

        // Castle
        const string map8 = "======================================================================================================#=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXX==========================================XXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=                                        =XXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXXXXXXXXXXXXXXXXX=======                                        =============XXXXXXXXXXXXXXXX=#=XXXXXXXXXXXXXXXXXXXXXXXX=                             |***                         =XXXXXXXXXXXXXXXX=#=XXXXXXXXX================                             |******                      ==================#=XXXXXXXXX=                                            |  ****                                        #=XXXXXXXXX=                                            =                                              #=XXXXXXXXX=                             = == =         = = == =                                       #=XXXXXXXXX=                             ======         = ======                                       #=XXXXXXXXX=           XXXXX             ||||||===========||||||             XXXXX                     #=XXXXXXXXX=           =/X/=             =======/////////=======             =/X/=                     #=XXXXXXXXX=           =====             ||||||=/////////=||||||             =====                     #=XXXXXXXXX=                             =======/////////=======                                       #=XXXXXXXXX=                                                                                           #=XXXXXXXXX=                                                                                           #=XXXXXXXXX=                                                                                           #=XXXXXXXXX=                                                                                           #=XXXXXXXXX=====                                                                                       #=XXXXXXXXXXXXX=                                                                                       #=XXXXXXXXXXXXX=                                                                                       #=XXXXXXXXXXXXX=                              ==============                                           #=XXXXXXXXXXXXX================================XXXXXXXXXXXX=                        ===================#XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX==========================XXXXXXXXXXXXXXXXX=#XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#======================================================================================================#";
        var map9 = "======================================================================================================#=/////////////////////////////////////////////  =====  //////////////////////////////////////////////=#=///////////////////////////////////////////  =========  ////////////////////////////////////////////=#=///////////////////////////////////////////  === O ===  ////////////////////////////////////////////=#=///////////////////////////////////////////  === V ===  ////////////////////////////////////////////=#=//////////=================================  =========  ==================================//////////=#=//////////=                                                                              =//////////=#=//////////=                                                                              =//////////=#=//////////=                                                                              =//////////=#=//////////=                                                                              =//////////=#=//////////=                                                                              =//////////=#=//////////=             XXXXX   XXXXX                         XXXXX   XXXXX              =//////////=#=//////////=             =/X/=   =/X/=                         =/X/=   =/X/=              =//////////=#=/////                   =====   =====                         =====   =====                    /////=#=//  =======                                                                              =======  //=#=//=========                                                                              =========//=#=//== O>  ==                                                                              ==  <O ==//=#=//=========                                                                              =========//=#=//  =======                                                                              =======  //=#=/////                   XXXXX   XXXXX                         XXXXX   XXXXX                    /////=#=//////////=             =/X/=   =/X/=                         =/X/=   =/X/=              =//////////=#=//////////=             =====   =====                         =====   =====              =//////////=#=//////////=                                                                              =//////////=#=//////////=                                                                              =//////////=#=//////////=                                                                              =//////////=#=//////////=                                                                              =//////////=#=//////////=                                                                              =//////////=#=//////////========================                                ========================//////////=#=/////////////////////////////////=                                =/////////////////////////////////=#=/////////////////////////////////=                                =/////////////////////////////////=#=/////////////////////////////////=                                =/////////////////////////////////=#=/////////////////////////////////=                                =/////////////////////////////////=#===================================                                ===================================#";
        const string map10 = "======================================================================================================#=////////////////////////////////////////////////////////////////////////////////////////////////////=#=////////////////////////////////////////////////////////////////////////////////////////////////////=#=////////////////////////////////////////////////////////////////////////////////////////////////////=#=////////////////////////////////////////////////////////////////////////////////////////////////////=#=////////////////===================================================================/////////////////=#=////////////////=                                                                 =/////////////////=#=////////////////=                                                                 =/////////////////=#=////////////////=                                                                 =/////////////////=#=/////////========                                                                 ========//////////=#=/////////=                                                                               =//////////=#=/////////=                                                                               =//////////=#=/////////=                                                                               =//////////=#=/////////=                                                                                     /////=#=/////////=                                                                               XXXXXXX  //=#=/////////=                                                                               XXXXXXXXX//=#=/////////=                                                                               XXXXXXXXX//=#=/////////=                                                                               XXXXXXXXX//=#=/////////=                                                                               XXXXXXX  //=#=/////////=                                                                                     /////=#=/////////=                                                                               =//////////=#=/////////=                                                                               =//////////=#=/////////=                                                                               =//////////=#=/////////========                                                                 ========//////////=#=////////////////=                                                                 =/////////////////=#=////////////////=                                                                 =/////////////////=#=////////////////=                                                                 =/////////////////=#=////////////////===================================================================/////////////////=#=////////////////////////////////////////////////////////////////////////////////////////////////////=#=////////////////////////////////////////////////////////////////////////////////////////////////////=#=////////////////////////////////////////////////////////////////////////////////////////////////////=#=////////////////////////////////////////////////////////////////////////////////////////////////////=#======================================================================================================#";
        const string map11 = "======================================================================================================#=////////////////////////////////////////////////////////////////////////////////////////////////////=#=////////////////////////////////////////////////////////////////////////////////////////////////////=#=////////////////////////////////////////////////////////////////////////////////////////////////////=#=////////////////////////////////////////////////////////////////////////////////////////////////////=#=/////////////////===================================================================////////////////=#=/////////////////=                                                                 =////////////////=#=/////////////////=                                                                 =////////////////=#=/////////////////=                                                                 =////////////////=#=//////////========                                                                 ========/////////=#=//////////=                                                                               =/////////=#=//////////=                                                                               =/////////=#=//////////=                                                                               =/////////=#=/////                                                                                     =/////////=#=//  XXXXXXX                                                                               =/////////=#=//XXXXXXXXX                                                                               =/////////=#=//XXXXXXXXX                                                                               =/////////=#=//XXXXXXXXX                                                                               =/////////=#=//  XXXXXXX                                                                               =/////////=#=/////                                                                                     =/////////=#=//////////=                                                                               =/////////=#=//////////=                                                                               =/////////=#=//////////=                                                                               =/////////=#=//////////========                                                                 ========/////////=#=/////////////////=                                                                 =////////////////=#=/////////////////=                                                                 =////////////////=#=/////////////////=                                                                 =////////////////=#=/////////////////===================================================================////////////////=#=////////////////////////////////////////////////////////////////////////////////////////////////////=#=////////////////////////////////////////////////////////////////////////////////////////////////////=#=////////////////////////////////////////////////////////////////////////////////////////////////////=#=////////////////////////////////////////////////////////////////////////////////////////////////////=#======================================================================================================#";
        var map12 = "======================================================================================================#=////////////////////////////////////////////////////////////////////////////////////////////////////=#=////////////////////////////////////////////////////////////////////////////////////////////////////=#=////////////////////////////////////////////////////////////////////////////////////////////////////=#=////////////////////////////////////////////////////////////////////////////////////////////////////=#=//////////================================================================================//////////=#=//////////=                                                                              =//////////=#=//////////=                                                                              =//////////=#=//////////=                                                                              =//////////=#=//////////=                                                                              =//////////=#=//////////=                                                                              =//////////=#=//////////=                                                                              =//////////=#=//////////=                                                                              =//////////=#=//////////=                                                                                    /////=#=//////////=                                                                              =======  //=#=//////////=                                                                              =========//=#=//////////=                                                                              =========//=#=//////////=                                                                              =========//=#=//////////=                                                                              =======  //=#=//////////=                                                                                    /////=#=//////////=                                                                              =//////////=#=//////////=                                                                              =//////////=#=//////////=                                                                              =//////////=#=//////////=                                                                              =//////////=#=//////////=                                                                              =//////////=#=//////////=                                                                              =//////////=#=//////////=                                                                              =//////////=#=//////////=================================  XXXXXXXXX  ==================================//////////=#=///////////////////////////////////////////  XXXXXXXXX  ////////////////////////////////////////////=#=///////////////////////////////////////////  XXXXXXXXX  ////////////////////////////////////////////=#=///////////////////////////////////////////  XXXXXXXXX  ////////////////////////////////////////////=#=///////////////////////////////////////////    XXXXX    ////////////////////////////////////////////=#======================================================================================================#";
        const string map13 = "======================================================================================================#=////////////////////////////////////////////////////////////////////////////////////////////////////=#=////////////////////////////////////////////////////////////////////////////////////////////////////=#=////////////////////////////////////////////////////////////////////////////////////////////////////=#=////////////////////////////////////////////////////////////////////////////////////////////////////=#=/////////////////===================================================================////////////////=#=/////////////////=                                                                 =////////////////=#=/////////////////=        ==================================================       =////////////////=#=/////////////////=        ==================================================       =////////////////=#=//////////========        ==================================================       ========/////////=#=//////////=               =====          /////          /////          =====              =/////////=#=//////////=               =====          =///=          =///=          =====              =/////////=#=//////////=               =====          =====          =====          =====              =/////////=#=/////                     =====//////////                    //////////=====              =/////////=#=//  XXXXXXX               ======///==///=        =<>=        =///==///======              =/////////=#=//XXXXXXXXX               ===============        s^^s        ===============              =/////////=#=//XXXXXXXXX               =====//////////       ss~~ss       //////////=====              =/////////=#=//XXXXXXXXX               ======///==///=       ~~~~~~       =///==///======              =/////////=#=//  XXXXXXX               ===============                    ===============              =/////////=#=/////                                    XXXXX          XXXXX                             =/////////=#=//////////=                              =XXX=          =XXX=                             =/////////=#=//////////=                              =====          =====                             =/////////=#=//////////=                                                                               =/////////=#=//////////========                                                                 ========/////////=#=/////////////////=          XXXXX                                    XXXXX         =////////////////=#=/////////////////=          =XXX=                                    =XXX=         =////////////////=#=/////////////////=          =====                                    =====         =////////////////=#=/////////////////===================================================================////////////////=#=////////////////////////////////////////////////////////////////////////////////////////////////////=#=////////////////////////////////////////////////////////////////////////////////////////////////////=#=////////////////////////////////////////////////////////////////////////////////////////////////////=#=////////////////////////////////////////////////////////////////////////////////////////////////////=#======================================================================================================#";

        var lCText = false;

        if (mapNum == 6 && HasFlag(GameFlags.HasSword))
        {
            map6 = $"{map6.AsSpan(0, 1854)}=XXXXXXXXXX=                                                                              =XXXXXXXXXX=#=XXXXXXXXXX=                                                                              =XXXXXXXXXX=#=XXXXXXXXXX=                                                                              =XXXXXXXXXX=#=XXXXXXXXXX=                                                                              =XXXXXXXXXX=#{map6.AsSpan(2266)}";
        }
        else if (mapNum == 7 && HasFlag(GameFlags.HasRaft) && !HasFlag(GameFlags.HasArmor))
        {
            map7 = $"{map7.AsSpan(0, 1751)}=XXXXXXXXXX=                                                                              =XXXXXXXXXX=#=XXXXXXXXXX=                                  =======               ## ##                 =XXXXXXXXXX=#=XXXXXXXXXX=                                  ==  = =               #####                 =XXXXXXXXXX=#=XXXXXXXXXX=                                                         ###                  =XXXXXXXXXX=#{map7.AsSpan(2163)}";
        }
        else if (mapNum == 7 && !HasFlag(GameFlags.HasRaft) && HasFlag(GameFlags.HasArmor))
        {
            map7 = $"{map7.AsSpan(0, 1751)}=XXXXXXXXXX=                 =====                                                        =XXXXXXXXXX=#=XXXXXXXXXX=                 *****            =======                                     =XXXXXXXXXX=#=XXXXXXXXXX=                 =====            ==  = =                                     =XXXXXXXXXX=#=XXXXXXXXXX=                 *****                                                        =XXXXXXXXXX=#{map7.AsSpan(2163)}";
        }
        else if (mapNum == 7 && HasFlag(GameFlags.HasRaft) && HasFlag(GameFlags.HasArmor))
        {
            map7 = $"{map7.AsSpan(0, 1751)}=XXXXXXXXXX=                                                                              =XXXXXXXXXX=#=XXXXXXXXXX=                                  =======                                     =XXXXXXXXXX=#=XXXXXXXXXX=                                  ==  = =                                     =XXXXXXXXXX=#=XXXXXXXXXX=                                                                              =XXXXXXXXXX=#{map7.AsSpan(2163)}";
        }
        else if (mapNum == 9 && CurrentMap == 8)
        {
            lCText = true;
            map9 = $"{map9.AsSpan(0, 1133)}=//////////=                                                                              =//////////=#=//////////=                                                                              =//////////=#=/////                       ||  TILL'  YOUR  FOES  ARE  BUT  HITHER,  ||                       /////=#=//  =======                 ||        SEALED  THE  PORTAL  IS.        ||                 =======  //=#=//=========                                                                              =========//=#=//== O>  ==                                                                              ==  <O ==//=#=//=========                                                                              =========//=#=//  =======                                                                              =======  //=#=/////                                                                                           ////=#=//////////=                                                                              =//////////=#=//////////=                                                                              =//////////=#{map9.AsSpan(2266)}";
        }
        else if (mapNum == 9 && HasFlag(GameFlags.Door1) && !HasFlag(GameFlags.Door2))
        {
            map9 = $"{map9.AsSpan(0, 1339)}=/////                   =====   =====                         =====   =====                    /////=#=//  XXXXXXX                                                                              =======  //=#=//XXXXXXXXX                                                                              =========//=#=//XXXXXXXXX                                                                              ==  <O ==//=#=//XXXXXXXXX                                                                              =========//=#=//  XXXXXXX                                                                              =======  //=#=/////                   XXXXX   XXXXX                         XXXXX   XXXXX                    /////=#{map9.AsSpan(2060)}";
            if (HasFlag(GameFlags.Door3))
            {
                map9 = $"{map9.AsSpan(0, 103)}=/////////////////////////////////////////////  =====  //////////////////////////////////////////////=#=///////////////////////////////////////////  =========  ////////////////////////////////////////////=#=///////////////////////////////////////////  =========  ////////////////////////////////////////////=#=///////////////////////////////////////////  =========  ////////////////////////////////////////////=#=//////////=================================  =========  ==================================//////////=#{map9.AsSpan(618)}";
            }
        }
        else if (mapNum == 9 && !HasFlag(GameFlags.Door1) && HasFlag(GameFlags.Door2))
        {
            map9 = $"{map9.AsSpan(0, 1339)}=/////                   =====   =====                         =====   =====                    /////=#=//  =======                                                                              XXXXXXX  //=#=//=========                                                                              XXXXXXXXX//=#=//== O>  ==                                                                              XXXXXXXXX//=#=//=========                                                                              XXXXXXXXX//=#=//  =======                                                                              XXXXXXX  //=#=/////                   XXXXX   XXXXX                         XXXXX   XXXXX                    /////=#{map9.AsSpan(2060)}";
            if (HasFlag(GameFlags.Door3))
            {
                map9 = $"{map9.AsSpan(0, 103)}=/////////////////////////////////////////////  =====  //////////////////////////////////////////////=#=///////////////////////////////////////////  =========  ////////////////////////////////////////////=#=///////////////////////////////////////////  =========  ////////////////////////////////////////////=#=///////////////////////////////////////////  =========  ////////////////////////////////////////////=#=//////////=================================  =========  ==================================//////////=#{map9.AsSpan(618)}";
            }
        }
        else if (mapNum == 9 && HasFlag(GameFlags.Door1) && HasFlag(GameFlags.Door2))
        {
            map9 = $"{map9.AsSpan(0, 1339)}=/////                   =====   =====                         =====   =====                    /////=#=//  XXXXXXX                                                                              XXXXXXX  //=#=//XXXXXXXXX                                                                              XXXXXXXXX//=#=//XXXXXXXXX                                                                              XXXXXXXXX//=#=//XXXXXXXXX                                                                              XXXXXXXXX//=#=//  XXXXXXX                                                                              XXXXXXX  //=#=/////                   XXXXX   XXXXX                         XXXXX   XXXXX                    /////=#{map9.AsSpan(2060)}";
            if (HasFlag(GameFlags.Door3) && (cEnemies1 > 0 || cEnemies2 > 0))
            {
                map9 = $"{map9.AsSpan(0, 103)}=/////////////////////////////////////////////  =====  //////////////////////////////////////////////=#=///////////////////////////////////////////  =========  ////////////////////////////////////////////=#=///////////////////////////////////////////  =========  ////////////////////////////////////////////=#=///////////////////////////////////////////  =========  ////////////////////////////////////////////=#=//////////=================================  =========  ==================================//////////=#{map9.AsSpan(618)}";
            }
            else if (HasFlag(GameFlags.Door3))
            {
                map9 = $"{map9.AsSpan(0, 103)}=/////////////////////////////////////////////  XXXXX  //////////////////////////////////////////////=#=///////////////////////////////////////////  XXXXXXXXX  ////////////////////////////////////////////=#=///////////////////////////////////////////  XXXXXXXXX  ////////////////////////////////////////////=#=///////////////////////////////////////////  XXXXXXXXX  ////////////////////////////////////////////=#=//////////=================================  XXXXXXXXX  ==================================//////////=#{map9.AsSpan(618)}";
            }
        }
        else if (mapNum == 9 && HasFlag(GameFlags.Door3))
        {
            map9 = $"{map9.AsSpan(0, 103)}=/////////////////////////////////////////////  =====  //////////////////////////////////////////////=#=///////////////////////////////////////////  =========  ////////////////////////////////////////////=#=///////////////////////////////////////////  =========  ////////////////////////////////////////////=#=///////////////////////////////////////////  =========  ////////////////////////////////////////////=#=//////////=================================  =========  ==================================//////////=#{map9.AsSpan(618)}";
        }
        else if (mapNum == 12 && HasFlag(GameFlags.Dragon))
        {
            map12 = $"{map12.AsSpan(0, 1339)}=//////////=                                                                                    /////=#=//////////=                                                                              XXXXXXX  //=#=//////////=                                                                              XXXXXXXXX//=#=//////////=                                                                              XXXXXXXXX//=#=//////////=                                                                              XXXXXXXXX//=#=//////////=                                                                              XXXXXXX  //=#=//////////=                                                                                    /////=#{map12.AsSpan(2060)}";
        }

        var val = 0;

        if (!(posX == 16 && posY == 6) && !(posX == 86 && posY == 7) && !(posX == 51 && posY == 17))
        {
            CurrentMap = mapNum;
        }

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
                if (map0[val] != '#')
                {
                    if (mapNum == 0)
                    {
                        Map[j, i] = map0[val];
                    }
                    else if (mapNum == 1)
                    {
                        Map[j, i] = map1[val];
                    }
                    else if (mapNum == 2)
                    {
                        Map[j, i] = map2[val];
                    }
                    else if (mapNum == 3)
                    {
                        Map[j, i] = map3[val];
                    }
                    else if (mapNum == 4)
                    {
                        Map[j, i] = map4[val];
                    }
                    else if (mapNum == 5)
                    {
                        Map[j, i] = map5[val];
                    }
                    else if (mapNum == 6)
                    {
                        Map[j, i] = map6[val];
                    }
                    else if (mapNum == 7)
                    {
                        Map[j, i] = map7[val];
                    }
                    else if (mapNum == 8)
                    {
                        Map[j, i] = map8[val];
                    }
                    else if (mapNum == 9)
                    {
                        Map[j, i] = map9[val];
                    }
                    else if (mapNum == 10)
                    {
                        Map[j, i] = map10[val];
                    }
                    else if (mapNum == 11)
                    {
                        Map[j, i] = map11[val];
                    }
                    else if (mapNum == 12)
                    {
                        Map[j, i] = map12[val];
                    }
                    else if (mapNum == 13)
                    {
                        Map[j, i] = map13[val];
                    }

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
                    Console.WriteLine("                                     " + map0.Split("#")[j]);
                }
                Thread.Sleep(120);
            }
        }

        if (mapNum == 1)
        {
            EnemyMovement.Move(-1, EnemyType.Octorok, 75, 13, 'a', -1, true);
            EnemyMovement.Move(-1, EnemyType.Octorok, 9, 12, 'd', -1, true);
            EnemyMovement.Move(-1, EnemyType.Octorok, 23, 26, 'a', -1, true);
        }
        else if (mapNum == 2)
        {
            EnemyMovement.Move(-1, EnemyType.Octorok, 59, 23, 'd', -1, true);
            EnemyMovement.Move(-1, EnemyType.Spider, 76, 6, 'a', 5, true);
        }
        else if (mapNum == 3)
        {
            EnemyMovement.Move(-1, EnemyType.Spider, 44, 25, 'a', 6, true);
            EnemyMovement.Move(-1, EnemyType.Octorok, 38, 14, 'd', -1, true);
            EnemyMovement.Move(-1, EnemyType.Octorok, 83, 9, 'a', -1, true);
        }
        else if (mapNum == 4)
        {
            EnemyMovement.Move(-1, EnemyType.Octorok, 35, 23, 'a', -1, true);
            EnemyMovement.Move(-1, EnemyType.Octorok, 69, 6, 'a', -1, true);
        }
        else if (mapNum == 5)
        {
            EnemyMovement.Move(-1, EnemyType.Spider, 81, 9, 'a', 4, true);
            EnemyMovement.Move(-1, EnemyType.Spider, 32, 5, 'd', 6, true);
        }
        else if (mapNum == 8)
        {
            //enemyMovement.Move(-1, EnemyType.Spider, 26, 20, 'd', 4, true);
            cEnemies1 = 4;
            cEnemies2 = 4;
        }
        else if (mapNum == 10)
        {
            if (cEnemies1 >= 1)
            {
                EnemyMovement.Move(-1, EnemyType.Bat, 70, 11, 'a', -1, true);
            }
            if (cEnemies1 >= 2)
            {
                EnemyMovement.Move(-1, EnemyType.Bat, 32, 9, 'd', -1, true);
            }
            if (cEnemies1 >= 3)
            {
                EnemyMovement.Move(-1, EnemyType.Bat, 53, 15, 'a', -1, true);
            }
            if (cEnemies1 >= 4)
            {
                EnemyMovement.Move(-1, EnemyType.Bat, 20, 20, 'd', -1, true);
            }
        }
        else if (mapNum == 11)
        {
            if (cEnemies2 >= 1)
            {
                EnemyMovement.Move(-1, EnemyType.Bat, 27, 9, 'a', -1, true);
            }
            if (cEnemies2 >= 2)
            {
                EnemyMovement.Move(-1, EnemyType.Bat, 56, 20, 'd', -1, true);
            }
            if (cEnemies2 >= 3)
            {
                EnemyMovement.Move(-1, EnemyType.Bat, 73, 15, 'a', -1, true);
            }
            if (cEnemies2 >= 4)
            {
                EnemyMovement.Move(-1, EnemyType.Bat, 18, 11, 'd', -1, true);
            }
        }
        else if (mapNum == 12)
        {
            if (!HasFlag(GameFlags.Dragon)) EnemyMovement.Move(-1, EnemyType.Dragon, 71, 13, 'a', 12, true);
        }

        if ((CurrentMap == 2 || CurrentMap == 4) && posX == 21)
        {
            LinkMovement.SetPosX(posX);
            LinkMovement.SetPosY(posY);
            LinkMovement.DeployRaft(LinkMovement.GetPrev2());
        }
        else
        {
            LinkMovement.MoveLink(posX, posY, direction, true);
        }

        if (lCText)
        {
            lCText = false;
            SetFlag(GameFlags.Text, true);
            wait = 750;
        }

        _start = true;
    }

    public static void Wait(int time)
    {
        _attacking = true;
        LinkMovement.SetPrev(time.ToString()[0]);
    }

    public static void UpdateRow(int row)
    {
        var line = "";
        for (var x = 0; x < 102; x++)
        {
            line += Map[x, row];
        }
        _strs[row] = line;
        //Console.SetCursorPosition(37, row);
        //Console.Write(line);
        //Console.SetCursorPosition(0, 0);
    }

    public static void UpdateHud()
    {
        //oldHud = $"~~~~~~~~~~~~~~~~~~~~~~~~~~~#XXXXXXXXXXXXXXXXXXXXXXXXXXX#X                         X#X                         X#X                         X#X         HEALTH:         X#X                         X#X       <3  <3  <3        X#X                         X#X                         X#X  ---------------------  X#X       MAIN ITEM:        X#X                         X#X           S             X#X           S             X#X         - - -           X#X           -             X#X  ---------------------  X#X                         X#X      RUPEES:   {rupees.ToString().PadRight(4)}     X#X                         X#X      KEYS:     {keys.ToString().PadRight(4)}     X#X                         X#X                         X#X                         X#XXXXXXXXXXXXXXXXXXXXXXXXXXX#~~~~~~~~~~~~~~~~~~~~~~~~~~~#";
        _hud = $"~~~~~~~~~~~~~~~~~~~~~~~~~~~#XXXXXXXXXXXXXXXXXXXXXXXXXXX#X                         X#X                         X#X                         X#X         HEALTH:         X#X                         X#X       <3  <3  <3        X#X                         X#X                         X#X  ---------------------  X#X                         X#X    r                    X#X   RRR          {Rupees,-4}     X#X    r                    X#X                         X#X  =======       {Keys,-4}     X#X  ==  = =                X#X                         X#X                         X#XXXXXXXXXXXXXXXXXXXXXXXXXXX#~~~~~~~~~~~~~~~~~~~~~~~~~~~#";
        _hud = Health > 2.5
            ? $"{_hud.AsSpan(0, 196)}X       <3  <3  <3        X#{_hud.AsSpan(224)}" : Health > 2
            ? $"{_hud.AsSpan(0, 196)}X       <3  <3  =         X#{_hud.AsSpan(224)}" : Health > 1.5
            ? $"{_hud.AsSpan(0, 196)}X       <3  <3            X#{_hud.AsSpan(224)}" : Health > 1
            ? $"{_hud.AsSpan(0, 196)}X       <3  =             X#{_hud.AsSpan(224)}" : Health > 0.5
            ? $"{_hud.AsSpan(0, 196)}X       <3                X#{_hud.AsSpan(224)}" : Health > 0
            ? $"{_hud.AsSpan(0, 196)}X       =                 X#{_hud.AsSpan(224)}" : $"{_hud.AsSpan(0, 196)}X                         X#{_hud.AsSpan(224)}";
    }

    public static void Tabs(int tabs)
    {
        for (var x = 0; x < tabs; x++)
        {
            Console.Write("  ");
        }
    }
}
