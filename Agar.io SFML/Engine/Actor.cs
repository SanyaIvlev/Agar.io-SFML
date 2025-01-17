using SFML.Graphics;
using SFML.System;

namespace Agar.io_SFML;

public class Actor: IUpdatable, IDrawable
{
    public CircleShape shape { get; protected init; }

    public Action<Actor> OnDestroyed;

    public Vector2f Position { get; protected set; }
    
    public uint Bounty { get; protected set; }

    protected Vector2f Direction;
    
    public Actor(Vector2f startPosition)
    {
        Position = startPosition;
    }
    
    public virtual void Update()
    {
        shape.Position = Position;
    }
    
    public void Draw(RenderWindow window)
    {
        window.Draw(shape);
    }
    
}