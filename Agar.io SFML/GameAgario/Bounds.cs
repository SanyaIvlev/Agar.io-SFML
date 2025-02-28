using Agar.io_SFML.Configs;
using SFML.System;

namespace Agar.io_SFML;

public class Bounds
{
    private int _fieldWidth;
    private int _fieldHeight;
    
    public Bounds()
    {
        _fieldWidth = WindowConfig.WindowWidth;
        _fieldHeight = WindowConfig.WindowHeight;
    }

    public bool IsInside(Vector2f position)
        => position.X < _fieldWidth && position.X > 0 &&
           position.Y < _fieldHeight && position.Y > 0;
}