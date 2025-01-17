using SFML.Graphics;
using SFML.System;

namespace Agar.io_SFML;

public class EatableActor : Actor, IUpdatable
{
    public CircleShape shape { get; protected init; }
    
    public Action<EatableActor> OnDestroyed;
    
    public uint Bounty { get; protected set; } 

    public EatableActor(Vector2f initialPosition) : base(initialPosition)
    {
        
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