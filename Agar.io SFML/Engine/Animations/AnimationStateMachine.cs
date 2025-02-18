using SFML.Graphics;

namespace Agar.io_SFML.Animations;

public class AnimationStateMachine
{
    public Texture CurrentFrame => _currentState.CurrentFrame;
    
    private Dictionary<State, List<Transition>> _transitionMap;

    private State _currentState;

    public AnimationStateMachine(State initialState)
    {
        _currentState = initialState;
        
        _transitionMap = new Dictionary<State, List<Transition>>();
        
        _transitionMap.Add(_currentState, new List<Transition>());
    }

    public void Update()
    {
        List<Transition> currentTransitions = _transitionMap[_currentState];

        foreach (var transition in currentTransitions)
        {
            if (transition.IsConditionComplied())
            {
                _currentState = transition.ChangeState();
            }
        }

        _currentState.Update();
    }

    public void AddTransition(State from, State to, Func<bool> condition)
    {
        Transition newTransition = new(to, condition);
        
        if (_transitionMap.ContainsKey(from))
        {
            _transitionMap[from].Add(newTransition);
        }
        else
        {
            _transitionMap.Add(from, new List<Transition> { newTransition } );
        }
        
    }
}