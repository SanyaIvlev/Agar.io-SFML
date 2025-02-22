using SFML.Graphics;

namespace Agar.io_SFML;

public class ShapeActor : Actor, IDrawable
{
    public Shape shape;
    public bool IsVisible = true;
    
    protected RenderWindow Window;

    public void Initialize(RenderWindow window, Shape shape)
    {
        Window = window;
        this.shape = shape;
    }

    public void Initialize(RenderWindow window, Shape shape, Texture texture)
    {
        Initialize(window, shape);
        shape.Texture = texture;
    }
    
    public void Draw(RenderWindow window)
    {
        if (!IsVisible)
            return;
        
        Window.Draw(shape);
    }
}