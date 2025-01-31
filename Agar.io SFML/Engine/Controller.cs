using Agar.io_SFML.Extensions;
using SFML.Graphics;
using SFML.System;

namespace Agar.io_SFML;

public class Controller : Actor
{
    public Actor ControlledActor { get; protected set; }

    protected RenderWindow _window;
    
    protected Vector2f NewPosition;

    private Vector2f _direction;
    
    public virtual void Initialize(Actor controlledPlayer, RenderWindow window)
    {
        ControlledActor = controlledPlayer;
        
        _window = window;
    }

    public override void Update()
    {
        MakeDirection();
        
        ControlledActor.Direction = _direction;
    }

    public void SwapWith(Controller anotherController)
    {
        (ControlledActor, anotherController.ControlledActor) = (anotherController.ControlledActor, ControlledActor);
    }
    
    private void MakeDirection()
    {
        Vector2f nonNormalizedDirection = new Vector2f(NewPosition.X - ControlledActor.Position.X, NewPosition.Y - ControlledActor.Position.Y);
        
        _direction = nonNormalizedDirection.Normalize();
    }
}