namespace _0_Bit_Legend.Animations;

public class CaveTransition : IAnimation
{
    public void Call()
    {
        (var position, var _) = PlayerController.GetPlayerInfo();

        for(var y = 1; y <= 3; y++)
            StepHeroUp(position, y);
    }

    private static void StepHeroUp(Vector2 position, int y)
    {
        PlayerController.SetPosition(position.Offset(0, -y));
        ForceRedraw();
        Thread.Sleep(500);
    }
}
