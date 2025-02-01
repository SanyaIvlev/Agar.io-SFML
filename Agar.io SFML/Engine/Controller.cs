using Agar.io_SFML.Extensions;
using SFML.Graphics;
using SFML.System;

namespace Agar.io_SFML;

public class Controller : Actor
{
    public Actor Pawn { get; protected set; }

    protected RenderWindow _window;
    
    protected Vector2f NewPosition;

    private Vector2f _direction;
    
    public virtual void Initialize(Actor controlledPlayer, RenderWindow window)
    {
        Pawn = controlledPlayer;
        
        _window = window;
    }

    public override void Update()
    {
        MakeDirection();
        
        Pawn.Direction = _direction;
    }

    public void SwapWith(Controller anotherController)
    {
        (Pawn, anotherController.Pawn) = (anotherController.Pawn, Pawn);
    }
    
    private void MakeDirection()
    {
        Vector2f nonNormalizedDirection = new Vector2f(NewPosition.X - Pawn.Position.X, NewPosition.Y - Pawn.Position.Y);
        
        _direction = nonNormalizedDirection.Normalize();
    }
}