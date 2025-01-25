using SFML.Graphics;
using SFML.System;

namespace Agar.io_SFML;

public class Actor: IDrawable, IUpdatable
{
    public Vector2f Position { get; protected set; }
    
    public Actor()
    {
        
    }

    protected void Initialize(Vector2f initialPosition)
    {
        Position = initialPosition;
    }
    
    public virtual void Update()
    {
        
    }
    
    public virtual void Draw(RenderWindow window)
    {
        
    }
    
}