using SFML.System;

namespace Agar.io_SFML;

public interface IPositionHandler
{
    Vector2f GetPosition();
    
    void ProcessAction();
}