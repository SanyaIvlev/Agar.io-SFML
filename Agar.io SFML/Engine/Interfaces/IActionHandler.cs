using SFML.System;

namespace Agar.io_SFML;

public interface IActionHandler
{
    Vector2f GetPosition();
    
    void ProcessAction();
}