using SFML.Graphics;
using SFML.System;

namespace Agar.io_SFML;

public class BotActionHandler : IActionHandler
{
    private Vector2f _newPosition;
    
    private readonly Random _random;
    
    private readonly RenderWindow _window;

    public BotActionHandler(RenderWindow window)
    {
        _window = window;
        
        _random = new();
    }

    public Vector2f GetPosition()
        => _newPosition;
    
    public void ProcessAction()
    {
        _newPosition = GetRandomPosition();
    }

    private Vector2f GetRandomPosition()
    {
        int x = _random.Next(0, (int)_window.Size.X);
        int y = _random.Next(0, (int)_window.Size.Y);

        return new(x, y);
    }
}