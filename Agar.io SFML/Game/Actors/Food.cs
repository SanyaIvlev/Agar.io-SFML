using SFML.Graphics;
using SFML.System;

namespace Agar.io_SFML;

public class Food : Actor
{
    public Food(Vector2f initialPosition, Color color) : base(initialPosition)
    {
        shape = new CircleShape()
        {
            Position = initialPosition,
            Radius = 5,
            FillColor = color
        };

        Bounty = 1;
    }
}