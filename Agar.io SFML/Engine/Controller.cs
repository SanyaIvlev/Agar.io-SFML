using Agar.io_SFML.Engine;
using Agar.io_SFML.Extensions;
using SFML.Graphics;
using SFML.System;

namespace Agar.io_SFML;

public class Controller : Actor
{
    public Actor Pawn { get; private set; }
    
    protected Action<Actor> OnPawnUpdated;

    protected RenderWindow _window;
    
    protected Vector2f NewPosition;

    private Vector2f _direction;
    
    public virtual void Initialize(Actor controlledPlayer)
    {
        SetPawn(controlledPlayer);
        _window = Dependency.Get<RenderWindow>();
    }

    public virtual void Update()
    {
        MakeDirection();
        
        Pawn.Direction = _direction;
    }

    public void SetPawn(Actor pawn)
    {
        Pawn = pawn;
        OnPawnUpdated?.Invoke(pawn);
    }
    
    private void MakeDirection()
    {
        Vector2f nonNormalizedDirection = new Vector2f(NewPosition.X - Pawn.Position.X, NewPosition.Y - Pawn.Position.Y);
        
        _direction = nonNormalizedDirection.Normalize();
    }
}