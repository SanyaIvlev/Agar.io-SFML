using Agar.io_SFML.Engine;
using SFML.Graphics;

namespace Agar.io_SFML;

public class ShapeActor : Actor, IDrawable
{
    public Shape shape;
    public bool IsActive = true;
    
    protected RenderWindow Window;

    public void Initialize(Shape shape)
    {
        Window = Dependency.Get<RenderWindow>();
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