using SFML.Graphics;

namespace Agar.io_SFML.Animations;

public class AnimationStateMachine
{
    public Texture CurrentFrame => _currentState.CurrentFrame;
    
    private Dictionary<State, List<Transition>> _stateVariants;

    private State _currentState;

    public AnimationStateMachine(State initialState)
    {
        _currentState = initialState;
    }

    public void Update()
    {
        List<Transition> currentTransitions = _stateVariants[_currentState];

        foreach (var transition in currentTransitions)
        {
            if (transition.IsConditionComplied())
            {
                _currentState = transition.ChangeState();
            }
        }

        _currentState.Update();
    }

    public void AddTransition(State from, State to)
    {
        Transition newTransition = new(to);
        _stateVariants[from].Add(newTransition);
    }
}