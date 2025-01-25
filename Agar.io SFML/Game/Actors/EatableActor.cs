using SFML.Graphics;
using SFML.System;

namespace Agar.io_SFML;

public class EatableActor : Actor
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

    public override void Update()
    {
        shape.Position = Position;
    }

    public override void Draw(RenderWindow window)
    {
        window.Draw(shape);
    }
}