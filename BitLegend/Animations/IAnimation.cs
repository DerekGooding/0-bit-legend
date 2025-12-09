namespace BitLegend.Animations;

//Animations take input control away from the player momentarily like when displaying credits or game over
public interface IAnimation
{
    public void Call();
}
