﻿using Agar.io_SFML.Engine;
using SFML.Graphics;

namespace Agar.io_SFML;

public class ShapeActor : Actor, IDrawable
{
    public Shape shape;
    
    protected RenderWindow Window;

    public void Initialize(Shape shape)
    {
        Window = Service<RenderWindow>.Get;
        this.shape = shape;
    }

    public void Initialize(Shape shape, Texture texture)
    {
        Initialize(shape);
        shape.Texture = texture;
    }
    
    public void Draw(RenderWindow window)
    {
        if (!IsActive)
            return;
        
        Window.Draw(shape);
    }
}