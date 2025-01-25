using SFML.Graphics;
using SFML.System;

namespace Agar.io_SFML;

public class Food : EatableActor
{
    public void Initialize(Vector2f initialPosition, Color color)
    {
        base.Initialize(initialPosition);
        
        shape = new()
        {
            Position = initialPosition,
            Radius = 5,
            FillColor = color
        };

        Bounty = 1;
    }
}