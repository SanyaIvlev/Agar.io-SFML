using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Agar.io_SFML;

public class InputHandler
{
    public Action<Vector2f> newPosition;
    
    public void ProcessInput(RenderWindow window)
    {
        Vector2f mousePosition = (Vector2f)Mouse.GetPosition(window);
        
        newPosition?.Invoke(mousePosition);
    }
}