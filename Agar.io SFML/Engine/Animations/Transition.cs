namespace Agar.io_SFML.Animations;

public class Transition
{
    private State _to;

    public Transition(State to)
    {
        _to = to;
    }

    public State ChangeState()
    {
        _to.StartAnimation();

        return _to;
    }
    
    public virtual bool IsConditionComplied()
    {
        return false;
    }
}