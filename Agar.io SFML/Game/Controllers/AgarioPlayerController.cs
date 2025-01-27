using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Agar.io_SFML;

public class AgarioPlayerController : Controller
{
    public override void Initialize(Player controlledPlayer, RenderWindow window)
    {
        base.Initialize(controlledPlayer, window);
    }
    
    public override void Update()
    {
        if (PositionWithinBorders())
        {
            ProcessMousePosition();
            base.Update();
        }
    }

    private void ProcessMousePosition()
    {
        NewPosition = (Vector2f)Mouse.GetPosition(_window);
    }

    private bool PositionWithinBorders()
        => NewPosition.X <= _window.Size.X || NewPosition.X >= 0 ||
           NewPosition.Y <= _window.Size.Y || NewPosition.Y >= 0;
}