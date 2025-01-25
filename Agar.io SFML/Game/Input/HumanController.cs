using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Agar.io_SFML;

public class HumanController : IController
{
    private Vector2f _mousePosition;
    private readonly RenderWindow _window;

    public HumanController(RenderWindow window)
    {
        _window = window;
    }

    public Vector2f GetPosition()
        => _mousePosition;
    
    public void ProcessAction()
    {
        _mousePosition = (Vector2f)Mouse.GetPosition(_window);
    }
}