using SFML.Graphics;

namespace Agar.io_SFML.Animations;

public class ShapeAnimator : Actor, IUpdatable
{
    private Shape _shape;
    private AnimationStateMachine _animationStateMachine;

    public ShapeAnimator(Shape shape, State state)
    {
        _shape = shape;
        _animationStateMachine = new (state);
    }

    public void Update()
    {
        _animationStateMachine.Update();
        
        _shape.Texture = _animationStateMachine.CurrentFrame;
    }

    public void AddTransition(State from, State to)
    {
        _animationStateMachine.AddTransition(from, to);
    }
}