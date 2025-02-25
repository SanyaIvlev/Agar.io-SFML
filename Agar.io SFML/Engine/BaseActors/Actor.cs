using SFML.Graphics;
using SFML.System;

namespace Agar.io_SFML;

public class Actor
{
    public Vector2f Position { get; protected set; }

    public Vector2f Direction;
    
    public Shape gbf;

    protected void Initialize(Vector2f initialPosition)
    {
        Position = initialPosition;
    }
    
}