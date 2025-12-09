namespace BitLegend.Content;

public static class Hud
{
    public static string[] GetImage(double health) =>
    [
        "~~~~~~~~~~~~~~~~~~~~~~~~~~~",
        "XXXXXXXXXXXXXXXXXXXXXXXXXXX",
        "X                         X",
        "X                         X",
        "X                         X",
        "X         HEALTH:         X",
        "X                         X",
        HealthBar(health),
        "X                         X",
        "X                         X",
        "X  ---------------------  X",
        "X                         X",
        "X    r                    X",
       $"X   RRR          {Rupees,-4}     X",
        "X    r                    X",
        "X                         X",
        "X                         X",
       $"X  =======       {Keys,-4}     X",
        "X  ==  = =                X",
        "X                         X",
        "XXXXXXXXXXXXXXXXXXXXXXXXXXX",
        "~~~~~~~~~~~~~~~~~~~~~~~~~~~",
    ];
    public static Vector2 AbsolutePosition = new(5,8);

    private static string HealthBar(double health) => health switch
    {
        3.0 => "X       <3  <3  <3        X",
        2.5 => "X       <3  <3  =         X",
        2.0 => "X       <3  <3            X",
        1.5 => "X       <3      =         X",
        1.0 => "X       <3                X",
        0.5 => "X       =                 X",
        _   => "X       =                 X",
    };

//    var health = PlayerController.Health;
//    var _hud = $"~~~~~~~~~~~~~~~~~~~~~~~~~~~#XXXXXXXXXXXXXXXXXXXXXXXXXXX#X                         X#X                         X#X                         X#X         HEALTH:         X#X                         X#X       <3  <3  <3        X#X                         X#X                         X#X  ---------------------  X#X                         X#X    r                    X#X   RRR          {Rupees,-4}     X#X    r                    X#X                         X#X  =======       {Keys,-4}     X#X  ==  = =                X#X                         X#X                         X#XXXXXXXXXXXXXXXXXXXXXXXXXXX#~~~~~~~~~~~~~~~~~~~~~~~~~~~#";
//    _hud = health > 2.5
//            ? $"{_hud.AsSpan(0, 196)}X       <3  <3  <3        X#{_hud.AsSpan(224)}" : health > 2
//            ? $"{_hud.AsSpan(0, 196)}X       <3  <3  =         X#{_hud.AsSpan(224)}" : health > 1.5
//            ? $"{_hud.AsSpan(0, 196)}X       <3  <3            X#{_hud.AsSpan(224)}" : health > 1
//            ? $"{_hud.AsSpan(0, 196)}X       <3  =             X#{_hud.AsSpan(224)}" : health > 0.5
//            ? $"{_hud.AsSpan(0, 196)}X       <3                X#{_hud.AsSpan(224)}" : health > 0
//            ? $"{_hud.AsSpan(0, 196)}X       =                 X#{_hud.AsSpan(224)}" : $"{_hud.AsSpan(0, 196)}X                         X#{_hud.AsSpan(224)}";
}
