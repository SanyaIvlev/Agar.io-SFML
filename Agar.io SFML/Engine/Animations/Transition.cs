namespace Agar.io_SFML.Animations;

public class Transition
{
    private State _to;
    private Func<bool> _condition;

    public Transition(State to, Func<bool> condition)
    {
        _to = to;
        _condition = condition;
    }

    public State ChangeState()
    {
        _to.StartAnimation();

        return _to;
    }
    
    public virtual bool IsConditionComplied()
    {
        return _condition.Invoke();
    }
}