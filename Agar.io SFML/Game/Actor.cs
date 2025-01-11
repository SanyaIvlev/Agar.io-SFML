using SFML.Graphics;
using SFML.System;

namespace Agar.io_SFML;

public class Actor: IUpdatable, IDrawable
{
    public CircleShape shape { get; protected set; }

    public Action<Actor> OnDestroy;

    public Vector2f Position { get; protected set; }
    
    public uint Bounty { get; protected set; }

    protected Vector2f _velocity;
    
    protected (uint width, uint height) _fieldSize;
    
    protected Random _random;
    
    public Actor(Vector2f startPosition)
    {
        Position = startPosition;
        
        _fieldSize = (GameLoop.WINDOW_WIDTH, GameLoop.WINDOW_HEIGHT);
        
        _random = new ();
    }
    
    public void Update()
    {
        shape.Position = Position;
    }
    
    public void Draw(RenderWindow window)
    {
        window.Draw(shape);
    }
    
}