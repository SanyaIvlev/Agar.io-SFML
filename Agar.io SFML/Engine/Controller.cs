using Agar.io_SFML.Extensions;
using SFML.Graphics;
using SFML.System;

namespace Agar.io_SFML;

public class Controller : Actor
{
    public Player ControlledPlayer { get; protected set; }

    protected RenderWindow _window;
    
    protected Vector2f NewPosition;

    private Vector2f _direction;
    
    public virtual void Initialize(Player controlledPlayer, RenderWindow window)
    {
        ControlledPlayer = controlledPlayer;
        
        _window = window;
    }

    public override void Update()
    {
        MakeDirection();
        
        ControlledPlayer.Direction = _direction;
    }

    public void SwapWith(Controller anotherController)
    {
        (ControlledPlayer, anotherController.ControlledPlayer) = (anotherController.ControlledPlayer, ControlledPlayer);
    }
    
    private void MakeDirection()
    {
        Vector2f nonNormalizedDirection = new Vector2f(NewPosition.X - ControlledPlayer.Position.X, NewPosition.Y - ControlledPlayer.Position.Y);
        
        _direction = nonNormalizedDirection.Normalize();
    }
}