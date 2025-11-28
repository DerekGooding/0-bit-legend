using System.Runtime.InteropServices;

namespace _0_Bit_Legend;

public class MainProgram
{
    protected static LinkMovement linkMovement = new();
    protected static EnemyMovement enemyMovement = new();

    protected static string[,] map = new string[102, 33];
    protected static int currentMap = 0;

    private static readonly string[] _strs = new string[33];

    public static double health = 3;
    public static int rupees = 0;
    public static int keys = 0;
    private static int _frames = 0;

    public static bool hasSword;
    public static bool hasArmor;
    public static bool hasRaft;
    public static bool gameOver;
    private static string _hud = "";

    public static bool cDoor1;
    public static bool cDoor2;
    public static bool cDoor3;
    public static bool cText;
    public static int cEnemies1 = 4;
    public static int cEnemies2 = 4;
    public static bool cDragon;

    public static bool hit;
    private static bool _attacking;
    private static bool _start;

    public static int waitEnemies = 1;
    public static int waitDragon = 1;
    public static int wait;
    public static int iFrames = 0;

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

        LoadMap(0, 52, 18, "w");
        Console.Clear();

        string credits = "                                  THANKS   LINK,                                                      #                                  YOU'RE   THE   HERO   OF   HYRULE.                                  #                                                                                                      #                                            =<>=    /\\                                                #                                            s^^s   /  |                                               #                                           ss~~ss |^  |                                               #                                           ~~~~~~ |_=_|                                               #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                              Awake,  my  young  Hero,                                                #                              For  peace  waits  not  on  the  morrow.                                #                              Now  go;  take  this  into  the  unknown:                               #                              It's  dangerous  to  go  alone!                                         #                                                                                                      #                              The  moon  sets,  and  the  moon  rises;                                #                              Darkness  only  this  night  comprises.                                 #                              What's  to  hope  with  a  quest  so  foggy?                            #                              It's  a  secret  to  everybody!                                         #                                                                                                      #                              Finally,  peace  returns  to  Hyrule.                                   #                              And  when  calamity  fell  succesful,                                   #                              The  dream  of  a  legend  lifted  clear:                               #                              Another  quest  will  start  from  here!                                #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                           ==================== STAFF =====================                           #                           =                                              =                           #                           =                                              =                           #                           =      PRODUCER....     Jayden Newman          =                           #                           =                                              =                           #                           =                                              =                           #                           =      PROGRAMMER.....   Jayden Newman         =                           #                           =                                              =                           #                           =                                              =                           #                           =      DESIGNER....    Jayden Newman           =                           #                           =                                              =                           #                           =                                              =                           #                           =                 <***>                        =                           #                           =          FFF     S^SSS>                      =                           #                           =          FFF     *S  SS>                     =                           #                           =                     =S>                      =                           #                           =                    =*SSSS**>                 =                           #                           =                    =*SSSSS*                  =                           #                           =                    ===  ==                   =                           #                           =                                              =                           #                           =                                              =                           #                           =      INSPIRATION...   Nintendo's             =                           #                           =                       The Legend of Zelda    =                           #                           =                                              =                           #                           =     ttt                                      =                           #                           =     tt^t                                     =                           #                           =     tttt                                     =                           #                           =                                              =                           #                           ================================================                           #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                            0-Bit  Legend                                             #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                         =====================================================                        #                         =~~~~                ~~            ~~            ~~~=                        #                         =~    ~~~   ~~~~~~~~~MM~~~~~M~~~         ~~~~~~     =                        #                         =  ~~      ~~~~MM~~~MMMM~~~MMM~M~~~~~               =                        #                         =~~  ~~~~~~~~MMMMMMMMMMMM~MMMMMMM~~MM~~~~~     ~~MMM=                        #                         =MM~~~      MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM=                        #                         =MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM...  ...MMMM=                        #                         =...     .......                   .....   .........=                        #                         =()......     ... ()......  ..()..()..()()()   ()()(=                        #                         =()() ()()  ()()..()()..()()()()()  ()   ()()()    (=                        #                         =()      /\\ ()()()()         ()()()     ()()   ()()(=                        #                         =  ()   |  \\ - ()  ()()()()  ()  ()()    ()()()()   =                        #                         =   XXXX|  ^|-SSS ()()   () ()()()   ()  () ()()()  =                        #                         = XXXXXX|_=_|-XXX      ()    ()()  ()() ()()()() () =                        #                         =XXXXXXXXXXXXXXXX ()  ()()()() ()()() ()()  ()()    =                        #                         =XXXXXXXXXXXXXXX  ()()()()()()()()()()()()()()()()()=                        #                         =====================================================                        #";
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

                Console.Clear();
            //Console.SetCursorPosition(0, 0);
            Console.WriteLine(" \n \n \n ");
            UpdateHud();

            for (int i = 0; i < 33; i++)
            {
                if (health > 0 && !gameOver)
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

            Thread.Sleep(wait);
            wait = 0;

            if (health > 0 && !gameOver)
            {
                if (!hit)
                {
                    if (!_attacking)
                    {
                        if ((GetAsyncKeyState(VK_W) & 0x8000) != 0 || (GetAsyncKeyState(VK_UP) & 0x8000) != 0)
                        {
                            linkMovement.MoveLink(linkMovement.GetPosX(), linkMovement.GetPosY() - 1, "w", false);
                        }
                        else if ((GetAsyncKeyState(VK_A) & 0x8000) != 0 || (GetAsyncKeyState(VK_LEFT) & 0x8000) != 0)
                        {
                            linkMovement.MoveLink(linkMovement.GetPosX() - 2, linkMovement.GetPosY(), "a", false);
                        }
                        else if ((GetAsyncKeyState(VK_S) & 0x8000) != 0 || (GetAsyncKeyState(VK_DOWN) & 0x8000) != 0)
                        {
                            linkMovement.MoveLink(linkMovement.GetPosX(), linkMovement.GetPosY() + 1, "s", false);
                        }
                        else if ((GetAsyncKeyState(VK_D) & 0x8000) != 0 || (GetAsyncKeyState(VK_RIGHT) & 0x8000) != 0)
                        {
                            linkMovement.MoveLink(linkMovement.GetPosX() + 2, linkMovement.GetPosY(), "d", false);
                        }
                        else if (((GetAsyncKeyState(VK_LSHIFT) & 0x8000) != 0 || (GetAsyncKeyState(VK_RSHIFT) & 0x8000) != 0) && hasSword)
                        {
                            linkMovement.Attack(linkMovement.GetPrev(), _attacking);
                            _attacking = true;
                        }

                        if (!hit && waitEnemies <= 0)
                        {
                            waitEnemies = 2;
                            for (int i = 0; i < enemyMovement.GetTotal(); i++)
                            {
                                bool passed = false;
                                int rnd1 = Random.Shared.Next(10);

                                if (enemyMovement.GetEnemyType(i) == "octorok")
                                {
                                    if (rnd1 > 2)
                                    {
                                        if (enemyMovement.GetPrev1(i) == "w")
                                        {
                                            passed = !enemyMovement.Move(i, enemyMovement.GetEnemyType(i), enemyMovement.GetPosX(i), enemyMovement.GetPosY(i) - 1, "w", -1, false);
                                        }
                                        else if (enemyMovement.GetPrev1(i) == "a")
                                        {
                                            passed = !enemyMovement.Move(i, enemyMovement.GetEnemyType(i), enemyMovement.GetPosX(i) - 2, enemyMovement.GetPosY(i), "a", -1, false);
                                        }
                                        else if (enemyMovement.GetPrev1(i) == "s")
                                        {
                                            passed = !enemyMovement.Move(i, enemyMovement.GetEnemyType(i), enemyMovement.GetPosX(i), enemyMovement.GetPosY(i) + 1, "s", -1, false);
                                        }
                                        else if (enemyMovement.GetPrev1(i) == "d")
                                        {
                                            passed = !enemyMovement.Move(i, enemyMovement.GetEnemyType(i), enemyMovement.GetPosX(i) + 2, enemyMovement.GetPosY(i), "d", -1, false);
                                        }
                                    }
                                    else
                                    {
                                        passed = true;
                                    }

                                    if (passed)
                                    {
                                        int rnd2 = Random.Shared.Next(4) + 1;
                                        if (rnd2 == 1)
                                        {
                                            enemyMovement.Move(i, enemyMovement.GetEnemyType(i), enemyMovement.GetPosX(i), enemyMovement.GetPosY(i) - 1, "w", -1, false);
                                        }
                                        else if (rnd2 == 2)
                                        {
                                            enemyMovement.Move(i, enemyMovement.GetEnemyType(i), enemyMovement.GetPosX(i) - 2, enemyMovement.GetPosY(i), "a", -1, false);
                                        }
                                        else if (rnd2 == 3)
                                        {
                                            enemyMovement.Move(i, enemyMovement.GetEnemyType(i), enemyMovement.GetPosX(i), enemyMovement.GetPosY(i) + 1, "s", -1, false);
                                        }
                                        else if (rnd2 == 4)
                                        {
                                            enemyMovement.Move(i, enemyMovement.GetEnemyType(i), enemyMovement.GetPosX(i) + 2, enemyMovement.GetPosY(i), "d", -1, false);
                                        }
                                    }
                                }
                                else if (enemyMovement.GetEnemyType(i) == "spider")
                                {
                                    enemyMovement.SetMotion(i, enemyMovement.GetMotion(i) - 1);
                                    if (enemyMovement.GetMotion(i) > 0)
                                    {
                                        if (rnd1 > 2)
                                        {
                                            if (enemyMovement.GetPrev1(i) == "w")
                                            {
                                                passed = !enemyMovement.Move(i, enemyMovement.GetEnemyType(i), enemyMovement.GetPosX(i) - 2, enemyMovement.GetPosY(i) - 1, "w", -1, false);
                                            }
                                            else if (enemyMovement.GetPrev1(i) == "a")
                                            {
                                                passed = !enemyMovement.Move(i, enemyMovement.GetEnemyType(i), enemyMovement.GetPosX(i) + 2, enemyMovement.GetPosY(i) - 1, "a", -1, false);
                                            }
                                            else if (enemyMovement.GetPrev1(i) == "s")
                                            {
                                                passed = !enemyMovement.Move(i, enemyMovement.GetEnemyType(i), enemyMovement.GetPosX(i) - 2, enemyMovement.GetPosY(i) + 1, "s", -1, false);
                                            }
                                            else if (enemyMovement.GetPrev1(i) == "d")
                                            {
                                                passed = !enemyMovement.Move(i, enemyMovement.GetEnemyType(i), enemyMovement.GetPosX(i) + 2, enemyMovement.GetPosY(i) + 1, "d", -1, false);
                                            }
                                        }
                                        else
                                        {
                                            passed = true;
                                        }

                                        if (passed)
                                        {
                                            int rnd2 = Random.Shared.Next(4) + 1;
                                            if (rnd2 == 1)
                                            {
                                                enemyMovement.Move(i, enemyMovement.GetEnemyType(i), enemyMovement.GetPosX(i) - 2, enemyMovement.GetPosY(i) - 1, "w", -1, false);
                                            }
                                            else if (rnd2 == 2)
                                            {
                                                enemyMovement.Move(i, enemyMovement.GetEnemyType(i), enemyMovement.GetPosX(i) + 2, enemyMovement.GetPosY(i) - 1, "a", -1, false);
                                            }
                                            else if (rnd2 == 3)
                                            {
                                                enemyMovement.Move(i, enemyMovement.GetEnemyType(i), enemyMovement.GetPosX(i) - 2, enemyMovement.GetPosY(i) + 1, "s", -1, false);
                                            }
                                            else if (rnd2 == 4)
                                            {
                                                enemyMovement.Move(i, enemyMovement.GetEnemyType(i), enemyMovement.GetPosX(i) + 2, enemyMovement.GetPosY(i) + 1, "d", -1, false);
                                            }
                                        }
                                    }
                                    else if (enemyMovement.GetMotion(i) <= -5)
                                    {
                                        enemyMovement.SetMotion(i, 10);
                                    }
                                }
                                else if (enemyMovement.GetEnemyType(i) == "bat")
                                {
                                    if (rnd1 > 4)
                                    {
                                        if (enemyMovement.GetPrev1(i) == "w")
                                        {
                                            passed = !enemyMovement.Move(i, enemyMovement.GetEnemyType(i), enemyMovement.GetPosX(i) - 2, enemyMovement.GetPosY(i) - 1, "w", -1, false);
                                        }
                                        else if (enemyMovement.GetPrev1(i) == "a")
                                        {
                                            passed = !enemyMovement.Move(i, enemyMovement.GetEnemyType(i), enemyMovement.GetPosX(i) + 2, enemyMovement.GetPosY(i) - 1, "a", -1, false);
                                        }
                                        else if (enemyMovement.GetPrev1(i) == "s")
                                        {
                                            passed = !enemyMovement.Move(i, enemyMovement.GetEnemyType(i), enemyMovement.GetPosX(i) - 2, enemyMovement.GetPosY(i) + 1, "s", -1, false);
                                        }
                                        else if (enemyMovement.GetPrev1(i) == "d")
                                        {
                                            passed = !enemyMovement.Move(i, enemyMovement.GetEnemyType(i), enemyMovement.GetPosX(i) + 2, enemyMovement.GetPosY(i) + 1, "d", -1, false);
                                        }
                                    }
                                    else
                                    {
                                        passed = true;
                                    }

                                    if (passed)
                                    {
                                        int rnd2 = Random.Shared.Next(4) + 1;
                                        if (rnd2 == 1)
                                        {
                                            enemyMovement.Move(i, enemyMovement.GetEnemyType(i), enemyMovement.GetPosX(i) - 2, enemyMovement.GetPosY(i) - 1, "w", -1, false);
                                        }
                                        else if (rnd2 == 2)
                                        {
                                            enemyMovement.Move(i, enemyMovement.GetEnemyType(i), enemyMovement.GetPosX(i) + 2, enemyMovement.GetPosY(i) - 1, "a", -1, false);
                                        }
                                        else if (rnd2 == 3)
                                        {
                                            enemyMovement.Move(i, enemyMovement.GetEnemyType(i), enemyMovement.GetPosX(i) - 2, enemyMovement.GetPosY(i) + 1, "s", -1, false);
                                        }
                                        else if (rnd2 == 4)
                                        {
                                            enemyMovement.Move(i, enemyMovement.GetEnemyType(i), enemyMovement.GetPosX(i) + 2, enemyMovement.GetPosY(i) + 1, "d", -1, false);
                                        }
                                    }
                                }
                                else if (enemyMovement.GetEnemyType(i) == "dragon" && waitDragon <= 0)
                                {
                                    waitDragon = 4;
                                    enemyMovement.SetMotion(i, enemyMovement.GetMotion(i) - 1);

                                    string phase = "a";
                                    int speed = 1;
                                    if (enemyMovement.GetMotion(i) <= 1)
                                    {
                                        phase = "d";
                                        speed = 0;
                                        if (enemyMovement.GetMotion(i) <= 0)
                                        {
                                            enemyMovement.Move(-1, "fireball", enemyMovement.GetPosX(i) - 3, enemyMovement.GetPosY(i) + 3, "w", -1, true);
                                            enemyMovement.Move(-1, "fireball", enemyMovement.GetPosX(i) - 3, enemyMovement.GetPosY(i) + 1, "a", -1, true);
                                            enemyMovement.Move(-1, "fireball", enemyMovement.GetPosX(i) - 3, enemyMovement.GetPosY(i) - 1, "s", -1, true);
                                            enemyMovement.SetMotion(i, 12);
                                        }
                                    }

                                    if (enemyMovement.GetPosY(i) <= 7)
                                    {
                                        enemyMovement.Move(i, enemyMovement.GetEnemyType(i), enemyMovement.GetPosX(i), enemyMovement.GetPosY(i) + speed, phase, -1, false);
                                    }
                                    else if (enemyMovement.GetPosY(i) >= 19)
                                    {
                                        enemyMovement.Move(i, enemyMovement.GetEnemyType(i), enemyMovement.GetPosX(i), enemyMovement.GetPosY(i) - speed, phase, -1, false);
                                    }
                                    else
                                    {
                                        if (rnd1 <= 4)
                                        {
                                            enemyMovement.Move(i, enemyMovement.GetEnemyType(i), enemyMovement.GetPosX(i), enemyMovement.GetPosY(i) + speed, phase, -1, false);
                                        }
                                        else
                                        {
                                            enemyMovement.Move(i, enemyMovement.GetEnemyType(i), enemyMovement.GetPosX(i), enemyMovement.GetPosY(i) - speed, phase, -1, false);
                                        }
                                    }
                                }
                                else if (enemyMovement.GetEnemyType(i) == "fireball")
                                {
                                    if (enemyMovement.GetPrev1(i) == "w")
                                    {
                                        enemyMovement.Move(i, enemyMovement.GetEnemyType(i), enemyMovement.GetPosX(i) - 3, enemyMovement.GetPosY(i) - 2, "w", -1, false);
                                    }
                                    else if (enemyMovement.GetPrev1(i) == "a")
                                    {
                                        enemyMovement.Move(i, enemyMovement.GetEnemyType(i), enemyMovement.GetPosX(i) - 3, enemyMovement.GetPosY(i), "a", -1, false);
                                    }
                                    else if (enemyMovement.GetPrev1(i) == "s")
                                    {
                                        enemyMovement.Move(i, enemyMovement.GetEnemyType(i), enemyMovement.GetPosX(i) - 3, enemyMovement.GetPosY(i) + 2, "s", -1, false);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        linkMovement.Attack(linkMovement.GetPrev(), _attacking);
                        if (linkMovement.GetPrev() != "w" && linkMovement.GetPrev() != "a" && linkMovement.GetPrev() != "s" && linkMovement.GetPrev() != "d")
                        {
                            if (int.Parse(linkMovement.GetPrev()) <= 0)
                            {
                                if (currentMap == 0)
                                {
                                    LoadMap(6, 50, 29, "w");
                                }
                                else if (currentMap == 4)
                                {
                                    LoadMap(7, 50, 30, "w");
                                }
                                else if (currentMap == 8)
                                {
                                    LoadMap(9, 50, 30, "w");
                                }
                                else if (currentMap == 6)
                                {
                                    LoadMap(0, 16, 9, "s");
                                }
                                else if (currentMap == 7)
                                {
                                    LoadMap(4, 86, 10, "s");
                                }
                                else if (currentMap == 9)
                                {
                                    LoadMap(8, 51, 20, "s");
                                }
                                _attacking = false;
                            }
                            else
                            {
                                if (currentMap is 0 or 4 or 8)
                                {
                                    linkMovement.MoveLink(linkMovement.GetPosX(), linkMovement.GetPosY() - 1, "w", false);
                                    Thread.Sleep(50);
                                }
                                else if (currentMap is 6 or 7 or 9)
                                {
                                    linkMovement.MoveLink(linkMovement.GetPosX(), linkMovement.GetPosY() + 1, "s", false);
                                    Thread.Sleep(50);
                                }

                                if (linkMovement.GetPrev() == "w" || linkMovement.GetPrev() == "s")
                                {
                                    linkMovement.SetPrev("0");
                                }
                                else
                                {
                                    linkMovement.SetPrev((Int32.Parse(linkMovement.GetPrev()) - 1).ToString());
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
                    hit = false;

                    int x = 0;
                    int y = 0;

                    if (linkMovement.GetPrev() == "w" && linkMovement.GetPosY() < 27)
                    {
                        y = 3;
                    }
                    else if (linkMovement.GetPrev() == "a" && linkMovement.GetPosX() < 94)
                    {
                        x = 6;
                    }
                    else if (linkMovement.GetPrev() == "s" && linkMovement.GetPosY() > 3)
                    {
                        y = -3;
                    }
                    else if (linkMovement.GetPrev() == "d" && linkMovement.GetPosX() > 7)
                    {
                        x = -6;
                    }

                    linkMovement.MoveLink(linkMovement.GetPosX() + x, linkMovement.GetPosY() + y, linkMovement.GetPrev(), false);
                }
            }
            else if (health <= 0)
            {
                if (_frames <= 16)
                {
                    Thread.Sleep(50);
                    for (int i = 0; i < 102; i++)
                    {
                        map[i, _frames] = " ";
                        map[i, 32 - _frames] = " ";
                    }
                    UpdateRow(_frames);
                    UpdateRow(32 - _frames);

                    if (_frames % 2 == 0)
                    {
                        linkMovement.PlayEffect("*");
                    }
                    else
                    {
                        linkMovement.PlayEffect("+");
                    }
                    UpdateRow(linkMovement.GetPosY() - 1);
                    UpdateRow(linkMovement.GetPosY());
                    UpdateRow(linkMovement.GetPosY() + 1);
                    UpdateRow(linkMovement.GetPosY() + 2);
                }
                else if (_frames == 25)
                {
                    for (int i = 0; i < 33; i++)
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

                    health = 3;
                    _frames = -1;
                    _start = false;

                    cEnemies1 = 4;
                    cEnemies2 = 4;

                    if (currentMap <= 8)
                    {
                        LoadMap(0, 52, 15, "w");
                    } else
                    {
                        LoadMap(9, 50, 25, "w");
                    }
                }
                _frames++;
            }
            else if (gameOver)
            {
                if (hasArmor) credits = "                                  THANKS   LINK,                                                      #                                  YOU'RE   THE   HERO   OF   HYRULE.                                  #                                                                                                      #                                            =<>=    /\\                                                #                                            s^^s   /  |                                               #                                           ss~~ss |^##|                                               #                                           ~~~~~~ |#=#|                                               #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                              Awake,  my  young  Hero,                                                #                              For  peace  waits  not  on  the  morrow.                                #                              Now  go;  take  this  into  the  unknown:                               #                              It's  dangerous  to  go  alone!                                         #                                                                                                      #                              The  moon  sets,  and  the  moon  rises;                                #                              Darkness  only  this  night  comprises.                                 #                              What's  to  hope  with  a  quest  so  foggy?                            #                              It's  a  secret  to  everybody!                                         #                                                                                                      #                              Finally,  peace  returns  to  Hyrule.                                   #                              And  when  calamity  fell  succesful,                                   #                              The  dream  of  a  legend  lifted  clear:                               #                              Another  quest  will  start  from  here!                                #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                           ==================== STAFF =====================                           #                           =                                              =                           #                           =                                              =                           #                           =      PRODUCER....     Jayden Newman          =                           #                           =                                              =                           #                           =                                              =                           #                           =      PROGRAMMER.....   Jayden Newman         =                           #                           =                                              =                           #                           =                                              =                           #                           =      DESIGNER....    Jayden Newman           =                           #                           =                                              =                           #                           =                                              =                           #                           =                 <***>                        =                           #                           =          FFF     S^SSS>                      =                           #                           =          FFF     *S  SS>                     =                           #                           =                     =S>                      =                           #                           =                    =*SSSS**>                 =                           #                           =                    =*SSSSS*                  =                           #                           =                    ===  ==                   =                           #                           =                                              =                           #                           =                                              =                           #                           =      INSPIRATION...   Nintendo's             =                           #                           =                       The Legend of Zelda    =                           #                           =                                              =                           #                           =     ttt                                      =                           #                           =     tt^t                                     =                           #                           =     tttt                                     =                           #                           =                                              =                           #                           ================================================                           #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                            0-Bit  Legend                                             #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                         =====================================================                        #                         =~~~~                ~~            ~~            ~~~=                        #                         =~    ~~~   ~~~~~~~~~MM~~~~~M~~~         ~~~~~~     =                        #                         =  ~~      ~~~~MM~~~MMMM~~~MMM~M~~~~~               =                        #                         =~~  ~~~~~~~~MMMMMMMMMMMM~MMMMMMM~~MM~~~~~     ~~MMM=                        #                         =MM~~~      MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM=                        #                         =MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM...  ...MMMM=                        #                         =...     .......                   .....   .........=                        #                         =()......     ... ()......  ..()..()..()()()   ()()(=                        #                         =()() ()()  ()()..()()..()()()()()  ()   ()()()    (=                        #                         =()      /\\ ()()()()         ()()()     ()()   ()()(=                        #                         =  ()   |  \\ - ()  ()()()()  ()  ()()    ()()()()   =                        #                         =   XXXX|##^|-SSS ()()   () ()()()   ()  () ()()()  =                        #                         = XXXXXX|#=#|-XXX      ()    ()()  ()() ()()()() () =                        #                         =XXXXXXXXXXXXXXXX ()  ()()()() ()()() ()()  ()()    =                        #                         =XXXXXXXXXXXXXXX  ()()()()()()()()()()()()()()()()()=                        #                         =====================================================                        #";

                if (_frames < 13)
                {
                    for (int i = 0; i < 32; i++)
                    {
                        map[_frames, i] = " ";
                        map[101 - _frames, i] = " ";
                    }
                    UpdateRow(_frames);
                    UpdateRow(32 - _frames);

                    linkMovement.PlaceZelda();
                    linkMovement.MoveLink(linkMovement.GetPosX(), linkMovement.GetPosY(), "a", false);
                }
                else if (_frames < 30)
                {
                    for (int i = 0; i < 102; i++)
                    {
                        map[i, _frames - 13] = " ";
                        map[i, 45 - _frames] = " ";
                    }
                    UpdateRow(_frames - 13);
                    UpdateRow(45 - _frames);

                    linkMovement.PlaceZelda();
                    linkMovement.MoveLink(linkMovement.GetPosX(), linkMovement.GetPosY(), "a", false);
                }
                else if (_frames == 30)
                {
                    int count = 0;
                    for (int i = 0; i < 7; i++)
                    {
                        string row = "";
                        for (int j = 0; j < 102; j++)
                        {
                            row += credits[count];
                            count++;
                        }
                        _strs[i + 11] = row.ToString();

                        count++;
                    }
                }
                else if (_frames is > 30 and < 111)
                {
                    if (_frames == 31) Thread.Sleep(3500);
                    for (int i = 0; i < 31; i++)
                    {
                        _strs[i] = _strs[i + 1];
                    }

                    string row = "";
                    for (int i = 0; i < 102; i++)
                    {
                        row += credits[103 * (_frames - 18) + i];
                    }
                    _strs[31] = row;
                    Thread.Sleep(600);
                }
                else if (_frames is >= 111 and < 117)
                {
                    for (int i = 0; i < 31; i++)
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

    public static void LoadMap(int mapNum, int posX, int posY, string direction)
    {
        // Main Maps
        string map0 = "======================================================                    ============================#=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=                    =XXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=                    =XXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX====================                    =XXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXXXXX///////XXXXXXXXXXXXXX=                                       =========XXXXXXXXXXXXXXXXXX=#=XXXXXXXXXXXX///////XXXXXXXXX======                                               =XXXXXXXXXXXXXXXXXX=#=XXXXXXXXX===///////==========                                                    =XXXXXXXXXXXXXXXXXX=#=XXXXXXXXX=                                                                       =XXXXXXXXXXXXXXXXXX=#=XXXXXXXXX=                                                                       =XXXXXXXXXXXXXXXXXX=#===========                                                                       =XXXXXXXXXXXXXXXXXX=#                                                                                  =====XXXXXXXXXXXXXX=#                                                                                      ======XXXXXXXXX=#                                                                                           ===========#                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #====                                                                                                  #=XX=                                                                                                  #=XX=                                                                                                  #=XX=                                                                                                  #=XX=                                                                                                  #=XX=                                                                                                  #=XX=                                                                                            ======#=XX=======                                                                                      =XXXX=#=XXXXXXXX=                                                                                 ======XXXX=#=XXXXXXXX=                                      ============================================XXXXXXXXX=#=XXXXXXXX=========                              =XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXXXXXXXXX================================XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#======================================================================================================#";
        string map1 = "======================================================================================================#=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXXXXXXXXXXXXXXXXXXXXX======================================================XXXXXXXXXXXXXXXXXX=#=XXXXXXXXX====================                                                    =XXXXXXXXXXXXXXXXXX=#=XXXXXXXXX=                                                                       =XXXXXXXXXXXXXXXXXX=#=XXXXXXXXX=                                                                       =XXXXXXXXXXXXXXXXXX=#===========                                                                       =XXXXXXXXXXXXXXXXXX=#                                                                                  =====XXXXXXXXXXXXXX=#                       XXXXX               XXXXX               XXXXX                  ======XXXXXXXXX=#                       =XXX=               =XXX=               =XXX=                       =XXXXXXXXX=#                       =====               =====               =====                       ===========#                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #                                                                                                      #====                                                                                                  #=XX=                   XXXXX               XXXXX               XXXXX                            ======#=XX=                   =XXX=               =XXX=               =XXX=                     ========XXXX=#=XX=                   =====               =====               =====                     =XXXXXXXXXXX=#=XX=                                                                                     =XXXXXXXXXXX=#=XX======                                                                                =XXXXXXXXXXX=#=XXXXXXX=                                                                                =XXXXXXXXXXX=#=XXXXXXX==                                                                         =======XXXXXXXXXXX=#=XXXXXXXX=                                                                      ====XXXXXXXXXXXXXXXXX=#=XXXXXXXX=                                                                      =XXXXXXXXXXXXXXXXXXXX=#=XXXXXXXX=========                                                              =XXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXXXXXXXXX=====================================                    =======XXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=                    =XXXXXXXXXXXXXXXXXXXXXXXXXX=#======================================================                    ============================#";
        string map2 = "=====         ~~~~~~~~~~~~~~ =============                       =====================================#=XXX=         ~~~~~~~~~~~~~~ =XXXXXXXXXXX=                       =XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXX=         ~~~~~~~~~~~~~~ =============                       =======XXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXX=         ~~~~~~~~~~~~~~                                           =XXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXX=         ~~~~~~~~~~~~~~                                           ============XXXXXXXXXXXXXXXXXX=#=XXX=         ~~~~~~~~~~~~~~                                                      =XXXXXXXXXXXXXXXXXX=#=XXX=         ~~~~~~~~~~~~~~                                                      =XXXXXXXXXXXXXXXXXX=#=XXX=         ~~~~~~~~~~~~~~                                                      =XXXXXXXXXXXXXXXXXX=#=XXX=         ~~~~~~~~~~~~~~                                                      ========XXXXXXXXXXX=#=XXX=         ~~~~~~~~~~~~~~                                                             =============#=XXX=         ~~~~~~~~~~~~~~                                                                          #=XXX=         ~~~~~~~~~~~~~~           XXXXX         XXXXX         XXXXX                              #=XXX=         ~~~~~~~~~~~~~~           XX=XX         XX=XX         XX=XX                              #=XXX=         ~~~~~~~~~~~~~~             =             =             =                                #=XXX=         ~~~~~~~~~~~~~~                                                                          #=XXX=         ~~~~~~~~~~~~~~                                                                          #=XXX=         ~~~~~~~~~~~~~~                                                                          #=XXX=         ~~~~~~~~~~~~~~                                                                          #=XXX=         ~~~~~~~~~~~~~~                                                                          #=XXX=         ~~~~~~~~~~~~~~           XXXXX         XXXXX         XXXXX                       =======#=XXX=         ~~~~~~~~~~~~~~           XX=XX         XX=XX         XX=XX                       =XXXXX=#=XXX=         ~~~~~~~~~~~~~~             =             =             =                         =XXXXX=#=XXX=         ~~~~~~~~~~~~~~                                                                   =XXXXX=#=XXX=====     ~~~~~~~~~~~~~~                                                               =====XXXXX=#=XXXXXXX=     ~~~~~~~~~~~~~~                                                               =XXXXXXXXX=#=XXXXXXX=     ~~~~~~~~~~~~~~                                                               =XXXXXXXXX=#=XXXXXXX=     ~~~~~~~~~~~~~~                                                         =======XXXXXXXXX=#=XXXXXXX======~~~~~~~~~~~~~~=====                                                    =XXXXXXXXXXXXXXX=#=XXXXXXXXXXXX=~~~~~~~~~~~~~~=XXX=                                  ===================XXXXXXXXXXXXXXX=#=XXXXXXXXXXXX=~~~~~~~~~~~~~~=XXX==========                         =XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXXXXX================XXXXXXXXXXXX===========================XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#======================================================================================================#";
        string map3 = "=============================                                                  =======================#=XXXXXXXXXXXXXXXXXXXXXXXXXXX=                                                  =XXXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXX===================                                                  ======XXXXXXXXXXXXXXXX=#=XXXXXXXXX= XXXXX                                                                   =XXXXXXXXXXXXXXXX=#=XXXXXXXXX= XX=XX                                                                   =XXXXXXXXXXXXXXXX=#=XXXXXXXXX=   =                                                                     ==========XXXXXXX=#=XXX=======                                                                            XXXXX =XXXXXXX=#=XXX=                                                                                  XX=XX =XXXXXXX=#=XXX=                                                                                    =   =XXXXXXX=#=XXX=                                                                                        =XXXXXXX=#=XXX=                  XXXXX       XXXXX       XXXXX       XXXXX       XXXXX                 =XXXXXXX=#=XXX=                  XX=XX       XX=XX       XX=XX       XX=XX       XX=XX                 ====XXXX=#=====                    =           =           =           =           =                      =XXXX=#                                                                                                =XXXX=#                                                                                                =XXXX=#                                                                                                =XXXX=#                                                                                                =XXXX=#                                                                                                =XXXX=#                                                                                                =XXXX=#                       XXXXX       XXXXX       XXXXX       XXXXX       XXXXX                    ==XXX=#                       XX=XX       XX=XX       XX=XX       XX=XX       XX=XX                     =XXX=#                         =           =           =           =           =                 XXXXX =XXX=#                                                                                           XX=XX =XXX=#                                                                                             =   =XXX=#      XXXXX                                                                          XXXXX XXXXX =XXX=#===== XX=XX                                                                          XX=XX XX=XX =XXX=#=XXX=   =                                                                              =     =   =XXX=#=XXX======= XXXXX XXXXX                                                        XXXXX XXXXX =======XXX=#=XXXXXXXXX= XX=XX XX=XX                                                        XX=XX XX=XX =XXXXXXXXX=#=XXXXXXXXX=   =     =                                                            =     =   =XXXXXXXXX=#=XXXXXXXXX==================================================================================XXXXXXXXX=#=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#======================================================================================================#";
        string map4 = "======================================================================================================#=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXXXXXXXXXXXXXXXXXXXXX====================================================XX///////XXXXXXXXXXX=#=XXXXXXXXX====================                                                  =XX///////XXXXXXXXXXX=#=XXXXXXXXX=   ~~~~~~~~~~~~~~                                                    ===///////=XXXXXXXXXX=#=XXXXXXXXX=   ~~~~~~~~~~~~~~                                                              =XXXXXXXXXX=#===========   ~~~~~~~~~~~~~~                                                              ============#              ~~~~~~~~~~~~~~                                                                          #              ~~~~~~~~~~~~~~           XXXXX         XXXXX         XXXXX                              #              ~~~~~~~~~~~~~~           =XXX=         =XXX=         =XXX=                              #              ~~~~~~~~~~~~~~           =====         =====         =====                              #              ~~~~~~~~~~~~~~                                                                          #              ~~~~~~~~~~~~~~                                                                          #              ~~~~~~~~~~~~~~                                                                          #              ~~~~~~~~~~~~~~                                                                          #              ~~~~~~~~~~~~~~                                                                          #              ~~~~~~~~~~~~~~           XXXXX         XXXXX         XXXXX                       =======#              ~~~~~~~~~~~~~~           =XXX=         =XXX=         =XXX=                       =XXXXX=#              ~~~~~~~~~~~~~~           =====         =====         =====                       =XXXXX=#              ~~~~~~~~~~~~~~                                                                   =XXXXX=#              ~~~~~~~~~~~~~~                                                               =====XXXXX=#              ~~~~~~~~~~~~~~                                                               =XXXXXXXXX=#              ~~~~~~~~~~~~~~                                                               =XXXXXXXXX=#=====         ~~~~~~~~~~~~~~                                                         =======XXXXXXXXX=#=XXX=         ~~~~~~~~~~~~~~ =====                                                   =XXXXXXXXXXXXXXX=#=XXX=         ~~~~~~~~~~~~~~ =XXX=                                 ===================XXXXXXXXXXXXXXX=#=XXX=         ~~~~~~~~~~~~~~ =XXX==========                        =XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXX=         ~~~~~~~~~~~~~~ =XXXXXXXXXXXX=                      ===XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXX=         ~~~~~~~~~~~~~~ =XXXXXXXXXXXX=                      =XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=====         ~~~~~~~~~~~~~~ ==============                      =====================================#";
        string map5 = "======================================================================================================#=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXX===========================================================================XXXXXXXXXXXXXXXX=#=XXXXXXXXX= XXXXX                                                                   =XXXXXXXXXXXXXXXX=#=XXXXXXXXX= XXXXX                                                                   =XXXXXXXXXXXXXXXX=#=XXXXXXXXX=   =                                                                     ==========XXXXXXX=#=XXXXXXXXX=                                                                            XXXXX =XXXXXXX=#=XXX=======                                                                            XXXXX =XXXXXXX=#=XXX= XXXXX                                                                              =   =XXXXXXX=#=XXX= XXXXX                                                                                  =XXXXXXX=#=XXX=   =              XXXXX       XXXXX       XXXXX       XXXXX       XXXXX                 =XXXXXXX=#=XXX=                  =XXX=       =XXX=       =XXX=       =XXX=       =XXX=                 ====XXXX=#=XXX=                  =====       =====       =====       =====       =====                    =XXXX=#=====                                                                                           =XXXX=#                                                                                                =XXXX=#                                                                                                =XXXX=#                                                                                                =XXXX=#                                                                                                =XXXX=#                                                                                                =XXXX=#                       XXXXX       XXXXX       XXXXX       XXXXX       XXXXX                    ==XXX=#=====                  =XXX=       =XXX=       =XXX=       =XXX=       =XXX=                     =XXX=#=XXX=                  =====       =====       =====       =====       =====               XXXXX =XXX=#=XXX====                                                                                   XXXXX =XXX=#=XXXXXX=                                                                                     =   =XXX=#=XXXXXX= XXXXX                                                                       XXXXX XXXXX =XXX=#=XXXXXX= XXXXX                                                                       XXXXX XXXXX =XXX=#=XXXXXX=   =                                                                           =     =   =XXX=#=XXXXXX========== XXXXX                                                        XXXXX XXXXX =======XXX=#=XXXXXXXXXXXXXXX= XXXXX                                                        XXXXX XXXXX =XXXXXXXXX=#=XXXXXXXXXXXXXXX=   =                                                            =     =   =XXXXXXXXX=#=XXXXXXXXXXXXXXX=============                                                  =============XXXXXXXXX=#=XXXXXXXXXXXXXXXXXXXXXXXXXXX=                                                  =XXXXXXXXXXXXXXXXXXXXX=#=============================                                                  =======================#";

        // Caves
        string map6 = "======================================================================================================#=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXXX================================================================================XXXXXXXXXX=#=XXXXXXXXXX=                                                                              =XXXXXXXXXX=#=XXXXXXXXXX=                                                                              =XXXXXXXXXX=#=XXXXXXXXXX=                     IT'S   DANGEROUS   TO   GO   ALONE!                      =XXXXXXXXXX=#=XXXXXXXXXX=                                TAKE   THIS.                                  =XXXXXXXXXX=#=XXXXXXXXXX=                                                                              =XXXXXXXXXX=#=XXXXXXXXXX=                                   xxxxxxx                                    =XXXXXXXXXX=#=XXXXXXXXXX=              //////               x^ x ^x                //////              =XXXXXXXXXX=#=XXXXXXXXXX=              //////               /**=**\\                //////              =XXXXXXXXXX=#=XXXXXXXXXX=              ======              |_|***|_|               ======              =XXXXXXXXXX=#=XXXXXXXXXX=                                   |__|__|                                    =XXXXXXXXXX=#=XXXXXXXXXx=                                                                              =XXXXXXXXXX=#=XXXXXXXXXX=                                                                              =XXXXXXXXXX=#=XXXXXXXXXX=                                      S                                       =XXXXXXXXXX=#=XXXXXXXXXX=                                      S                                       =XXXXXXXXXX=#=XXXXXXXXXX=                                     ---                                      =XXXXXXXXXX=#=XXXXXXXXXX=                                      -                                       =XXXXXXXXXX=#=XXXXXXXXXX=                                                                              =XXXXXXXXXX=#=XXXXXXXXXX=                                                                              =XXXXXXXXXX=#=XXXXXXXXXX=                                                                              =XXXXXXXXXX=#=XXXXXXXXXX=                                                                              =XXXXXXXXXX=#=XXXXXXXXXX=                                                                              =XXXXXXXXXX=#=XXXXXXXXXX========================                                ========================XXXXXXXXXX=#=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=                                =XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=                                =XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=                                =XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=                                =XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#===================================                                ===================================#";
        string map7 = "======================================================================================================#=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXXX================================================================================XXXXXXXXXX=#=XXXXXXXXXX=                                                                              =XXXXXXXXXX=#=XXXXXXXXXX=                                                                              =XXXXXXXXXX=#=XXXXXXXXXX=                         BUY   SOMETHIN'   WILL   YA!                         =XXXXXXXXXX=#=XXXXXXXXXX=                                                                              =XXXXXXXXXX=#=XXXXXXXXXX=                                                                              =XXXXXXXXXX=#=XXXXXXXXXX=                                   xxxxxxx                                    =XXXXXXXXXX=#=XXXXXXXXXX=                                   x^ x ^x                                    =XXXXXXXXXX=#=XXXXXXXXXX=                                   /**=**\\                                    =XXXXXXXXXX=#=XXXXXXXXXX=                                  |_|***|_|                                   =XXXXXXXXXX=#=XXXXXXXXXX=                                   |__|__|                                    =XXXXXXXXXX=#=XXXXXXXXXx=                                                                              =XXXXXXXXXX=#=XXXXXXXXXX=                 =====                                                        =XXXXXXXXXX=#=XXXXXXXXXX=                 *****            =======               ## ##                 =XXXXXXXXXX=#=XXXXXXXXXX=                 =====            ==  = =               #####                 =XXXXXXXXXX=#=XXXXXXXXXX=                 *****                                   ###                  =XXXXXXXXXX=#=XXXXXXXXXX=                 RAFT              KEY                  ARMOR                 =XXXXXXXXXX=#=XXXXXXXXXX=                 35                10                   25                    =XXXXXXXXXX=#=XXXXXXXXXX=     r                                                                        =XXXXXXXXXX=#=XXXXXXXXXX=    RRR                                                                       =XXXXXXXXXX=#=XXXXXXXXXX=     r (X)                                                                    =XXXXXXXXXX=#=XXXXXXXXXX=                                                                              =XXXXXXXXXX=#=XXXXXXXXXX========================                                ========================XXXXXXXXXX=#=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=                                =XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=                                =XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=                                =XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=                                =XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#===================================                                ===================================#";

        // Castle
        string map8 = "======================================================================================================#=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXX==========================================XXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=                                        =XXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXXXXXXXXXXXXXXXXX=======                                        =============XXXXXXXXXXXXXXXX=#=XXXXXXXXXXXXXXXXXXXXXXXX=                             |***                         =XXXXXXXXXXXXXXXX=#=XXXXXXXXX================                             |******                      ==================#=XXXXXXXXX=                                            |  ****                                        #=XXXXXXXXX=                                            =                                              #=XXXXXXXXX=                             = == =         = = == =                                       #=XXXXXXXXX=                             ======         = ======                                       #=XXXXXXXXX=           XXXXX             ||||||===========||||||             XXXXX                     #=XXXXXXXXX=           =/X/=             =======/////////=======             =/X/=                     #=XXXXXXXXX=           =====             ||||||=/////////=||||||             =====                     #=XXXXXXXXX=                             =======/////////=======                                       #=XXXXXXXXX=                                                                                           #=XXXXXXXXX=                                                                                           #=XXXXXXXXX=                                                                                           #=XXXXXXXXX=                                                                                           #=XXXXXXXXX=====                                                                                       #=XXXXXXXXXXXXX=                                                                                       #=XXXXXXXXXXXXX=                                                                                       #=XXXXXXXXXXXXX=                              ==============                                           #=XXXXXXXXXXXXX================================XXXXXXXXXXXX=                        ===================#XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX==========================XXXXXXXXXXXXXXXXX=#XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX=#======================================================================================================#";
        string map9 = "======================================================================================================#=/////////////////////////////////////////////  =====  //////////////////////////////////////////////=#=///////////////////////////////////////////  =========  ////////////////////////////////////////////=#=///////////////////////////////////////////  === O ===  ////////////////////////////////////////////=#=///////////////////////////////////////////  === V ===  ////////////////////////////////////////////=#=//////////=================================  =========  ==================================//////////=#=//////////=                                                                              =//////////=#=//////////=                                                                              =//////////=#=//////////=                                                                              =//////////=#=//////////=                                                                              =//////////=#=//////////=                                                                              =//////////=#=//////////=             XXXXX   XXXXX                         XXXXX   XXXXX              =//////////=#=//////////=             =/X/=   =/X/=                         =/X/=   =/X/=              =//////////=#=/////                   =====   =====                         =====   =====                    /////=#=//  =======                                                                              =======  //=#=//=========                                                                              =========//=#=//== O>  ==                                                                              ==  <O ==//=#=//=========                                                                              =========//=#=//  =======                                                                              =======  //=#=/////                   XXXXX   XXXXX                         XXXXX   XXXXX                    /////=#=//////////=             =/X/=   =/X/=                         =/X/=   =/X/=              =//////////=#=//////////=             =====   =====                         =====   =====              =//////////=#=//////////=                                                                              =//////////=#=//////////=                                                                              =//////////=#=//////////=                                                                              =//////////=#=//////////=                                                                              =//////////=#=//////////=                                                                              =//////////=#=//////////========================                                ========================//////////=#=/////////////////////////////////=                                =/////////////////////////////////=#=/////////////////////////////////=                                =/////////////////////////////////=#=/////////////////////////////////=                                =/////////////////////////////////=#=/////////////////////////////////=                                =/////////////////////////////////=#===================================                                ===================================#";
        string map10 = "======================================================================================================#=////////////////////////////////////////////////////////////////////////////////////////////////////=#=////////////////////////////////////////////////////////////////////////////////////////////////////=#=////////////////////////////////////////////////////////////////////////////////////////////////////=#=////////////////////////////////////////////////////////////////////////////////////////////////////=#=////////////////===================================================================/////////////////=#=////////////////=                                                                 =/////////////////=#=////////////////=                                                                 =/////////////////=#=////////////////=                                                                 =/////////////////=#=/////////========                                                                 ========//////////=#=/////////=                                                                               =//////////=#=/////////=                                                                               =//////////=#=/////////=                                                                               =//////////=#=/////////=                                                                                     /////=#=/////////=                                                                               XXXXXXX  //=#=/////////=                                                                               XXXXXXXXX//=#=/////////=                                                                               XXXXXXXXX//=#=/////////=                                                                               XXXXXXXXX//=#=/////////=                                                                               XXXXXXX  //=#=/////////=                                                                                     /////=#=/////////=                                                                               =//////////=#=/////////=                                                                               =//////////=#=/////////=                                                                               =//////////=#=/////////========                                                                 ========//////////=#=////////////////=                                                                 =/////////////////=#=////////////////=                                                                 =/////////////////=#=////////////////=                                                                 =/////////////////=#=////////////////===================================================================/////////////////=#=////////////////////////////////////////////////////////////////////////////////////////////////////=#=////////////////////////////////////////////////////////////////////////////////////////////////////=#=////////////////////////////////////////////////////////////////////////////////////////////////////=#=////////////////////////////////////////////////////////////////////////////////////////////////////=#======================================================================================================#";
        string map11 = "======================================================================================================#=////////////////////////////////////////////////////////////////////////////////////////////////////=#=////////////////////////////////////////////////////////////////////////////////////////////////////=#=////////////////////////////////////////////////////////////////////////////////////////////////////=#=////////////////////////////////////////////////////////////////////////////////////////////////////=#=/////////////////===================================================================////////////////=#=/////////////////=                                                                 =////////////////=#=/////////////////=                                                                 =////////////////=#=/////////////////=                                                                 =////////////////=#=//////////========                                                                 ========/////////=#=//////////=                                                                               =/////////=#=//////////=                                                                               =/////////=#=//////////=                                                                               =/////////=#=/////                                                                                     =/////////=#=//  XXXXXXX                                                                               =/////////=#=//XXXXXXXXX                                                                               =/////////=#=//XXXXXXXXX                                                                               =/////////=#=//XXXXXXXXX                                                                               =/////////=#=//  XXXXXXX                                                                               =/////////=#=/////                                                                                     =/////////=#=//////////=                                                                               =/////////=#=//////////=                                                                               =/////////=#=//////////=                                                                               =/////////=#=//////////========                                                                 ========/////////=#=/////////////////=                                                                 =////////////////=#=/////////////////=                                                                 =////////////////=#=/////////////////=                                                                 =////////////////=#=/////////////////===================================================================////////////////=#=////////////////////////////////////////////////////////////////////////////////////////////////////=#=////////////////////////////////////////////////////////////////////////////////////////////////////=#=////////////////////////////////////////////////////////////////////////////////////////////////////=#=////////////////////////////////////////////////////////////////////////////////////////////////////=#======================================================================================================#";
        string map12 = "======================================================================================================#=////////////////////////////////////////////////////////////////////////////////////////////////////=#=////////////////////////////////////////////////////////////////////////////////////////////////////=#=////////////////////////////////////////////////////////////////////////////////////////////////////=#=////////////////////////////////////////////////////////////////////////////////////////////////////=#=//////////================================================================================//////////=#=//////////=                                                                              =//////////=#=//////////=                                                                              =//////////=#=//////////=                                                                              =//////////=#=//////////=                                                                              =//////////=#=//////////=                                                                              =//////////=#=//////////=                                                                              =//////////=#=//////////=                                                                              =//////////=#=//////////=                                                                                    /////=#=//////////=                                                                              =======  //=#=//////////=                                                                              =========//=#=//////////=                                                                              =========//=#=//////////=                                                                              =========//=#=//////////=                                                                              =======  //=#=//////////=                                                                                    /////=#=//////////=                                                                              =//////////=#=//////////=                                                                              =//////////=#=//////////=                                                                              =//////////=#=//////////=                                                                              =//////////=#=//////////=                                                                              =//////////=#=//////////=                                                                              =//////////=#=//////////=                                                                              =//////////=#=//////////=================================  XXXXXXXXX  ==================================//////////=#=///////////////////////////////////////////  XXXXXXXXX  ////////////////////////////////////////////=#=///////////////////////////////////////////  XXXXXXXXX  ////////////////////////////////////////////=#=///////////////////////////////////////////  XXXXXXXXX  ////////////////////////////////////////////=#=///////////////////////////////////////////    XXXXX    ////////////////////////////////////////////=#======================================================================================================#";
        string map13 = "======================================================================================================#=////////////////////////////////////////////////////////////////////////////////////////////////////=#=////////////////////////////////////////////////////////////////////////////////////////////////////=#=////////////////////////////////////////////////////////////////////////////////////////////////////=#=////////////////////////////////////////////////////////////////////////////////////////////////////=#=/////////////////===================================================================////////////////=#=/////////////////=                                                                 =////////////////=#=/////////////////=        ==================================================       =////////////////=#=/////////////////=        ==================================================       =////////////////=#=//////////========        ==================================================       ========/////////=#=//////////=               =====          /////          /////          =====              =/////////=#=//////////=               =====          =///=          =///=          =====              =/////////=#=//////////=               =====          =====          =====          =====              =/////////=#=/////                     =====//////////                    //////////=====              =/////////=#=//  XXXXXXX               ======///==///=        =<>=        =///==///======              =/////////=#=//XXXXXXXXX               ===============        s^^s        ===============              =/////////=#=//XXXXXXXXX               =====//////////       ss~~ss       //////////=====              =/////////=#=//XXXXXXXXX               ======///==///=       ~~~~~~       =///==///======              =/////////=#=//  XXXXXXX               ===============                    ===============              =/////////=#=/////                                    XXXXX          XXXXX                             =/////////=#=//////////=                              =XXX=          =XXX=                             =/////////=#=//////////=                              =====          =====                             =/////////=#=//////////=                                                                               =/////////=#=//////////========                                                                 ========/////////=#=/////////////////=          XXXXX                                    XXXXX         =////////////////=#=/////////////////=          =XXX=                                    =XXX=         =////////////////=#=/////////////////=          =====                                    =====         =////////////////=#=/////////////////===================================================================////////////////=#=////////////////////////////////////////////////////////////////////////////////////////////////////=#=////////////////////////////////////////////////////////////////////////////////////////////////////=#=////////////////////////////////////////////////////////////////////////////////////////////////////=#=////////////////////////////////////////////////////////////////////////////////////////////////////=#======================================================================================================#";

        bool lCText = false;

        if (mapNum == 6 && hasSword)
        {
            map6 = map6.Substring(0, 1854) + "=XXXXXXXXXX=                                                                              =XXXXXXXXXX=#=XXXXXXXXXX=                                                                              =XXXXXXXXXX=#=XXXXXXXXXX=                                                                              =XXXXXXXXXX=#=XXXXXXXXXX=                                                                              =XXXXXXXXXX=#" + map6.Substring(2266);
        }
        else if (mapNum == 7 && hasRaft && !hasArmor)
        {
            map7 = map7.Substring(0, 1751) + "=XXXXXXXXXX=                                                                              =XXXXXXXXXX=#=XXXXXXXXXX=                                  =======               ## ##                 =XXXXXXXXXX=#=XXXXXXXXXX=                                  ==  = =               #####                 =XXXXXXXXXX=#=XXXXXXXXXX=                                                         ###                  =XXXXXXXXXX=#" + map7.Substring(2163);
        }
        else if (mapNum == 7 && !hasRaft && hasArmor)
        {
            map7 = map7.Substring(0, 1751) + "=XXXXXXXXXX=                 =====                                                        =XXXXXXXXXX=#=XXXXXXXXXX=                 *****            =======                                     =XXXXXXXXXX=#=XXXXXXXXXX=                 =====            ==  = =                                     =XXXXXXXXXX=#=XXXXXXXXXX=                 *****                                                        =XXXXXXXXXX=#" + map7.Substring(2163);
        }
        else if (mapNum == 7 && hasRaft && hasArmor)
        {
            map7 = map7.Substring(0, 1751) + "=XXXXXXXXXX=                                                                              =XXXXXXXXXX=#=XXXXXXXXXX=                                  =======                                     =XXXXXXXXXX=#=XXXXXXXXXX=                                  ==  = =                                     =XXXXXXXXXX=#=XXXXXXXXXX=                                                                              =XXXXXXXXXX=#" + map7.Substring(2163);
        }
        else if (mapNum == 9 && currentMap == 8)
        {
            lCText = true;
            map9 = map9.Substring(0, 1133) + "=//////////=                                                                              =//////////=#=//////////=                                                                              =//////////=#=/////                       ||  TILL'  YOUR  FOES  ARE  BUT  HITHER,  ||                       /////=#=//  =======                 ||        SEALED  THE  PORTAL  IS.        ||                 =======  //=#=//=========                                                                              =========//=#=//== O>  ==                                                                              ==  <O ==//=#=//=========                                                                              =========//=#=//  =======                                                                              =======  //=#=/////                                                                                           ////=#=//////////=                                                                              =//////////=#=//////////=                                                                              =//////////=#" + map9.Substring(2266);
        }
        else if (mapNum == 9 && cDoor1 && !cDoor2)
        {
            map9 = map9.Substring(0, 1339) + "=/////                   =====   =====                         =====   =====                    /////=#=//  XXXXXXX                                                                              =======  //=#=//XXXXXXXXX                                                                              =========//=#=//XXXXXXXXX                                                                              ==  <O ==//=#=//XXXXXXXXX                                                                              =========//=#=//  XXXXXXX                                                                              =======  //=#=/////                   XXXXX   XXXXX                         XXXXX   XXXXX                    /////=#" + map9.Substring(2060);
            if (cDoor3)
            {
                map9 = map9.Substring(0, 103) + "=/////////////////////////////////////////////  =====  //////////////////////////////////////////////=#=///////////////////////////////////////////  =========  ////////////////////////////////////////////=#=///////////////////////////////////////////  =========  ////////////////////////////////////////////=#=///////////////////////////////////////////  =========  ////////////////////////////////////////////=#=//////////=================================  =========  ==================================//////////=#" + map9.Substring(618);
            }
        }
        else if (mapNum == 9 && !cDoor1 && cDoor2)
        {
            map9 = map9.Substring(0, 1339) + "=/////                   =====   =====                         =====   =====                    /////=#=//  =======                                                                              XXXXXXX  //=#=//=========                                                                              XXXXXXXXX//=#=//== O>  ==                                                                              XXXXXXXXX//=#=//=========                                                                              XXXXXXXXX//=#=//  =======                                                                              XXXXXXX  //=#=/////                   XXXXX   XXXXX                         XXXXX   XXXXX                    /////=#" + map9.Substring(2060);
            if (cDoor3)
            {
                map9 = map9.Substring(0, 103) + "=/////////////////////////////////////////////  =====  //////////////////////////////////////////////=#=///////////////////////////////////////////  =========  ////////////////////////////////////////////=#=///////////////////////////////////////////  =========  ////////////////////////////////////////////=#=///////////////////////////////////////////  =========  ////////////////////////////////////////////=#=//////////=================================  =========  ==================================//////////=#" + map9.Substring(618);
            }
        }
        else if (mapNum == 9 && cDoor1 && cDoor2)
        {
            map9 = map9.Substring(0, 1339) + "=/////                   =====   =====                         =====   =====                    /////=#=//  XXXXXXX                                                                              XXXXXXX  //=#=//XXXXXXXXX                                                                              XXXXXXXXX//=#=//XXXXXXXXX                                                                              XXXXXXXXX//=#=//XXXXXXXXX                                                                              XXXXXXXXX//=#=//  XXXXXXX                                                                              XXXXXXX  //=#=/////                   XXXXX   XXXXX                         XXXXX   XXXXX                    /////=#" + map9.Substring(2060);
            if (cDoor3 && (cEnemies1 > 0 || cEnemies2 > 0))
            {
                map9 = map9.Substring(0, 103) + "=/////////////////////////////////////////////  =====  //////////////////////////////////////////////=#=///////////////////////////////////////////  =========  ////////////////////////////////////////////=#=///////////////////////////////////////////  =========  ////////////////////////////////////////////=#=///////////////////////////////////////////  =========  ////////////////////////////////////////////=#=//////////=================================  =========  ==================================//////////=#" + map9.Substring(618);
            }
            else if (cDoor3)
            {
                map9 = map9.Substring(0, 103) + "=/////////////////////////////////////////////  XXXXX  //////////////////////////////////////////////=#=///////////////////////////////////////////  XXXXXXXXX  ////////////////////////////////////////////=#=///////////////////////////////////////////  XXXXXXXXX  ////////////////////////////////////////////=#=///////////////////////////////////////////  XXXXXXXXX  ////////////////////////////////////////////=#=//////////=================================  XXXXXXXXX  ==================================//////////=#" + map9.Substring(618);
            }
        }
        else if (mapNum == 9 && cDoor3)
        {
            map9 = map9.Substring(0, 103) + "=/////////////////////////////////////////////  =====  //////////////////////////////////////////////=#=///////////////////////////////////////////  =========  ////////////////////////////////////////////=#=///////////////////////////////////////////  =========  ////////////////////////////////////////////=#=///////////////////////////////////////////  =========  ////////////////////////////////////////////=#=//////////=================================  =========  ==================================//////////=#" + map9.Substring(618);
        }
        else if (mapNum == 12 && cDragon)
        {
            map12 = map12.Substring(0, 1339) + "=//////////=                                                                                    /////=#=//////////=                                                                              XXXXXXX  //=#=//////////=                                                                              XXXXXXXXX//=#=//////////=                                                                              XXXXXXXXX//=#=//////////=                                                                              XXXXXXXXX//=#=//////////=                                                                              XXXXXXX  //=#=//////////=                                                                                    /////=#" + map12.Substring(2060);
        }

        int val = 0;

        if (!(posX == 16 && posY == 6) && !(posX == 86 && posY == 7) && !(posX == 51 && posY == 17))
        {
            currentMap = mapNum;
        }

        while (enemyMovement.GetTotal() != 0)
        {
            enemyMovement.Remove(0, enemyMovement.GetEnemyType(0));
        }

        // Load the map
        for (int i = 0; i < 33; i++)
        {
            _strs[i] = "";
            for (int j = 0; j < 102; j++)
            {
                if (map0[val].ToString() != "#")
                {
                    if (mapNum == 0)
                    {
                        map[j, i] = map0[val].ToString();
                    }
                    else if (mapNum == 1)
                    {
                        map[j, i] = map1[val].ToString();
                    }
                    else if (mapNum == 2)
                    {
                        map[j, i] = map2[val].ToString();
                    }
                    else if (mapNum == 3)
                    {
                        map[j, i] = map3[val].ToString();
                    }
                    else if (mapNum == 4)
                    {
                        map[j, i] = map4[val].ToString();
                    }
                    else if (mapNum == 5)
                    {
                        map[j, i] = map5[val].ToString();
                    }
                    else if (mapNum == 6)
                    {
                        map[j, i] = map6[val].ToString();
                    }
                    else if (mapNum == 7)
                    {
                        map[j, i] = map7[val].ToString();
                    }
                    else if (mapNum == 8)
                    {
                        map[j, i] = map8[val].ToString();
                    }
                    else if (mapNum == 9)
                    {
                        map[j, i] = map9[val].ToString();
                    }
                    else if (mapNum == 10)
                    {
                        map[j, i] = map10[val].ToString();
                    }
                    else if (mapNum == 11)
                    {
                        map[j, i] = map11[val].ToString();
                    }
                    else if (mapNum == 12)
                    {
                        map[j, i] = map12[val].ToString();
                    }
                    else if (mapNum == 13)
                    {
                        map[j, i] = map13[val].ToString();
                    }

                    _strs[i] += map[j, i];
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
            string skippedLines = "\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n";

            for (int i = 0; i < 14; i++)
            {
                Console.Clear();

                skippedLines = skippedLines.Substring(2);
                Console.Write(skippedLines);

                for (int j = 0; j < (i * 2) + 5; j++)
                {
                    Console.WriteLine("                                     " + map0.Split("#")[j]);
                }
                Thread.Sleep(120);
            }
        }

        if (mapNum == 1)
        {
            enemyMovement.Move(-1, "octorok", 75, 13, "a", -1, true);
            enemyMovement.Move(-1, "octorok", 9, 12, "d", -1, true);
            enemyMovement.Move(-1, "octorok", 23, 26, "a", -1, true);
        }
        else if (mapNum == 2)
        {
            enemyMovement.Move(-1, "octorok", 59, 23, "d", -1, true);
            enemyMovement.Move(-1, "spider", 76, 6, "a", 5, true);
        }
        else if (mapNum == 3)
        {
            enemyMovement.Move(-1, "spider", 44, 25, "a", 6, true);
            enemyMovement.Move(-1, "octorok", 38, 14, "d", -1, true);
            enemyMovement.Move(-1, "octorok", 83, 9, "a", -1, true);
        }
        else if (mapNum == 4)
        {
            enemyMovement.Move(-1, "octorok", 35, 23, "a", -1, true);
            enemyMovement.Move(-1, "octorok", 69, 6, "a", -1, true);
        }
        else if (mapNum == 5)
        {
            enemyMovement.Move(-1, "spider", 81, 9, "a", 4, true);
            enemyMovement.Move(-1, "spider", 32, 5, "d", 6, true);
        }
        else if (mapNum == 8)
        {
            //enemyMovement.Move(-1, "spider", 26, 20, "d", 4, true);
            cEnemies1 = 4;
            cEnemies2 = 4;
        }
        else if (mapNum == 10)
        {
            if (cEnemies1 >= 1)
            {
                enemyMovement.Move(-1, "bat", 70, 11, "a", -1, true);
            }
            if (cEnemies1 >= 2)
            {
                enemyMovement.Move(-1, "bat", 32, 9, "d", -1, true);
            }
            if (cEnemies1 >= 3)
            {
                enemyMovement.Move(-1, "bat", 53, 15, "a", -1, true);
            }
            if (cEnemies1 >= 4)
            {
                enemyMovement.Move(-1, "bat", 20, 20, "d", -1, true);
            }
        }
        else if (mapNum == 11)
        {
            if (cEnemies2 >= 1)
            {
                enemyMovement.Move(-1, "bat", 27, 9, "a", -1, true);
            }
            if (cEnemies2 >= 2)
            {
                enemyMovement.Move(-1, "bat", 56, 20, "d", -1, true);
            }
            if (cEnemies2 >= 3)
            {
                enemyMovement.Move(-1, "bat", 73, 15, "a", -1, true);
            }
            if (cEnemies2 >= 4)
            {
                enemyMovement.Move(-1, "bat", 18, 11, "d", -1, true);
            }
        }
        else if (mapNum == 12)
        {
            if (!cDragon) enemyMovement.Move(-1, "dragon", 71, 13, "a", 12, true);
        }

        if ((currentMap == 2 || currentMap == 4) && posX == 21)
        {
            linkMovement.SetPosX(posX);
            linkMovement.SetPosY(posY);
            linkMovement.DeployRaft(linkMovement.GetPrev2());
        }
        else
        {
            linkMovement.MoveLink(posX, posY, direction, true);
        }

        if (lCText)
        {
            lCText = false;
            cText = true;
            wait = 750;
        }

        _start = true;
    }

    public static void Wait(int time)
    {
        _attacking = true;
        linkMovement.SetPrev(time.ToString());
    }

    public static void UpdateRow(int row)
    {
        string line = "";
        for (int x = 0; x < 102; x++)
        {
            line += map[x, row];
        }
        _strs[row] = line;
        //Console.SetCursorPosition(37, row);
        //Console.Write(line);
        //Console.SetCursorPosition(0, 0);
    }

    public static void UpdateHud()
    {
        //oldHud = $"~~~~~~~~~~~~~~~~~~~~~~~~~~~#XXXXXXXXXXXXXXXXXXXXXXXXXXX#X                         X#X                         X#X                         X#X         HEALTH:         X#X                         X#X       <3  <3  <3        X#X                         X#X                         X#X  ---------------------  X#X       MAIN ITEM:        X#X                         X#X           S             X#X           S             X#X         - - -           X#X           -             X#X  ---------------------  X#X                         X#X      RUPEES:   {rupees.ToString().PadRight(4)}     X#X                         X#X      KEYS:     {keys.ToString().PadRight(4)}     X#X                         X#X                         X#X                         X#XXXXXXXXXXXXXXXXXXXXXXXXXXX#~~~~~~~~~~~~~~~~~~~~~~~~~~~#";
        _hud = $"~~~~~~~~~~~~~~~~~~~~~~~~~~~#XXXXXXXXXXXXXXXXXXXXXXXXXXX#X                         X#X                         X#X                         X#X         HEALTH:         X#X                         X#X       <3  <3  <3        X#X                         X#X                         X#X  ---------------------  X#X                         X#X    r                    X#X   RRR          {rupees.ToString().PadRight(4)}     X#X    r                    X#X                         X#X  =======       {keys.ToString().PadRight(4)}     X#X  ==  = =                X#X                         X#X                         X#XXXXXXXXXXXXXXXXXXXXXXXXXXX#~~~~~~~~~~~~~~~~~~~~~~~~~~~#";
        if (health > 2.5)
        {
            _hud = _hud.Substring(0, 196) + "X       <3  <3  <3        X#" + _hud.Substring(224);
        }
        else if (health > 2)
        {
            _hud = _hud.Substring(0, 196) + "X       <3  <3  =         X#" + _hud.Substring(224);
        }
        else if (health > 1.5)
        {
            _hud = _hud.Substring(0, 196) + "X       <3  <3            X#" + _hud.Substring(224);
        }
        else if (health > 1)
        {
            _hud = _hud.Substring(0, 196) + "X       <3  =             X#" + _hud.Substring(224);
        }
        else if (health > 0.5)
        {
            _hud = _hud.Substring(0, 196) + "X       <3                X#" + _hud.Substring(224);
        }
        else if (health > 0)
        {
            _hud = _hud.Substring(0, 196) + "X       =                 X#" + _hud.Substring(224);
        }
        else
        {
            _hud = _hud.Substring(0, 196) + "X                         X#" + _hud.Substring(224);
        }
    }

    public static void Tabs(int tabs)
    {
        for (int x = 0; x < tabs; x++)
        {
            Console.Write("  ");
        }
    }
}
