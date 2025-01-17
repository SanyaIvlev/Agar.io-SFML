using SFML.Graphics;
using SFML.System;

namespace Agar.io_SFML;

public class Actor: IDrawable
{
    public Vector2f Position { get; protected set; }
    
    public Actor(Vector2f startPosition)
    {
        Position = startPosition;
    }
    
    public virtual void Update()
    {
        
    }
    
    public virtual void Draw(RenderWindow window)
    {
        
    }
    
}