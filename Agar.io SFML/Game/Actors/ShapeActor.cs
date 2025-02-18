using SFML.Graphics;

namespace Agar.io_SFML;

//не дуже подобається iдея робити окремий клас який спадкується вiд актору для фiгур, напевно пiзнiше по iншому зроблю
public class ShapeActor : Actor, IDrawable
{
    public Shape shape;
    private RenderWindow _window;

    public void Initialize(RenderWindow window, Shape shape)
    {
        _window = window;
        this.shape = shape;
    }

    public void Initialize(RenderWindow window, Shape shape, Texture texture)
    {
        Initialize(window, shape);
        shape.Texture = texture;
    }
    
    public void Draw(RenderWindow window)
    {
        _window.Draw(shape);
    }
}