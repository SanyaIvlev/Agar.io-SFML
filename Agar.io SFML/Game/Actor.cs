using SFML.Graphics;
using SFML.System;

namespace Agar.io_SFML;

public class Actor: IUpdatable, IDrawable
{
    protected (uint width, uint height) _fieldSize;
    
    protected Vector2f _position;
    protected Vector2f _velocity;
    
    protected Random _random;
    
    protected Shape _shape;
    

    public Actor((uint width, uint height) fieldSize, Vector2f startPosition)
    {
        _position = startPosition;
        
        _fieldSize = fieldSize;
        
        _random = new ();
    }
    
    public void Update()
    {
        _shape.Position = _position;
    }
    
    public void Draw(RenderWindow window)
    {
        window.Draw(_shape);
    }
    
}