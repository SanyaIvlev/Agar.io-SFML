using SFML.System;

namespace Agar.io_SFML;

public interface IController
{
    Vector2f GetPosition();
    
    void ProcessAction();
}