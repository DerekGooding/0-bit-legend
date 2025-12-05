namespace _0_Bit_Legend.Animations;

public class CaveTransition : IAnimation
{
    public void Call()
    {
        PlayerController.MoveUp();
        //player draw
        Thread.Sleep(100);
        PlayerController.MoveUp();
        //player draw
        Thread.Sleep(100);
        PlayerController.MoveUp();
        //player draw
        Thread.Sleep(100);
    }
}