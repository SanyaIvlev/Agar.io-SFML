using SFML.Graphics;
using SFML.System;

namespace Agar.io_SFML;

public class Actor: IUpdatable, IDrawable
{
    protected (uint width, uint height) _fieldSize;
    
    protected Vector2f _position;
    protected Vector2f _velocity;
    
    protected Random _random;
    
    private Shape _shape;
    

    public Actor()
    {
        _random = new ();
    }
    
    public void Update()
    {
        
    }
    
    public void Draw(RenderWindow window)
    {
        window.Draw(_shape);
    }

    
    
}