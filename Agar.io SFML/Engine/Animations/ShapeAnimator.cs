using SFML.Graphics;

namespace Agar.io_SFML.Animations;

public class ShapeAnimator : Actor, IUpdatable
{
    private Shape _shape;
    private AnimationStateMachine _animationStateMachine;

    public ShapeAnimator Initialize(Shape shape)
    {
        _shape = shape;
        return this;
    }
    
    public ShapeAnimator AddInitialState(State state)
    {
        _animationStateMachine = new (state);
        return this;
    }

    public void Update()
    {
        _animationStateMachine.Update();
        
        _shape.Texture = _animationStateMachine.CurrentFrame;
    }

    public ShapeAnimator AddTransition(State from, State to, Func<bool> condition)
    {
        Transition newTransition = new(to, condition);
        
        _animationStateMachine.AddTransition(from, newTransition);
        return this;
    }
}