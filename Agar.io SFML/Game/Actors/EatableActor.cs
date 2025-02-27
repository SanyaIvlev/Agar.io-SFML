using SFML.Graphics;
using SFML.System;

namespace Agar.io_SFML;

public class EatableActor : Actor, IDrawable, IUpdatable
{
    public Vector2f Center
    {
        get
        {
            var halfOfRadius = shape.Radius / 2f;
            return new(shape.Position.X + halfOfRadius, shape.Position.Y + halfOfRadius);
        }
    }
    
    public CircleShape shape { get; protected set; }
    
    public Action<EatableActor> OnDestroyed;
    
    public uint Bounty { get; protected set; } 

    protected void Initialize(Vector2f initialPosition)
    {
        base.Initialize(initialPosition);
    }

    public virtual void OnMouseClick()
    {
        shape.Position = Position;
    }

    public void Draw(RenderWindow window)
    {
        window.Draw(shape);
    }
}