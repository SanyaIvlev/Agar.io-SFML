using SFML.Graphics;
using SFML.System;

namespace Agar.io_SFML;

public class Controller : Actor
{
    public Player ControlledPlayer { get; protected set; }

    protected RenderWindow _window;
    
    protected Vector2f NewPosition;
    
    public virtual void Initialize(Player controlledPlayer, RenderWindow window)
    {
        ControlledPlayer = controlledPlayer;
        
        _window = window;
    }

    public override void Update()
    {
        ControlledPlayer.TargetPosition = NewPosition;
    }

    public void SwapWith(Controller anotherController)
    {
        (ControlledPlayer, anotherController.ControlledPlayer) = (anotherController.ControlledPlayer, ControlledPlayer);
    }
}