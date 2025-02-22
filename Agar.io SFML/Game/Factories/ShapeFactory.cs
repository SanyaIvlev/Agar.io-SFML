using Agar.io_SFML.Animations;
using Agar.io_SFML.Configs;
using SFML.Graphics;
using SFML.System;

namespace Agar.io_SFML.Factory;

public class ShapeFactory : ActorFactory
{
    private RenderWindow _window;
    private float _windowWidth;
    private float _windowHeight;

    public ShapeFactory(RenderWindow window)
    {
        _window = window;

        _windowWidth = WindowConfig.WindowWidth;
        _windowHeight = WindowConfig.WindowHeight;
    }

    public ShapeActor CreateShape(Texture texture)
    {
        ShapeActor shapeActor = CreateActor<ShapeActor>();
        
        shapeActor.Initialize(_window, new RectangleShape((Vector2f)texture.Size));
        
        shapeActor.shape.Texture = texture;
        
        return shapeActor;
    }
    
    public Button CreateButton(string textureName)
    {
        Button button = CreateActor<Button>();

        var texture = TextureLoader.Instance.FindTextureByName(textureName);
        
        button.Initialize(_window, new RectangleShape((Vector2f)texture.Size));
        
        var shape = button.shape;
        shape.Texture = texture;

        shape.Origin = new Vector2f(shape.TextureRect.Width / 2f, shape.TextureRect.Height / 2f);
        
        
        return button;
    }

    public void CreateBackground()
    {
        ShapeActor background = CreateActor<ShapeActor>();
        
        Texture backgroundTexture = TextureLoader.Instance.FindTextureByName("background");
        
        background.Initialize(_window, new RectangleShape(new Vector2f(_windowWidth, _windowHeight)), backgroundTexture);
        background.shape.FillColor = new Color(180, 180, 180);
    }

    public void DestroyAll(Actor[] actors)
    {
        foreach (var actor in actors)
        {
            Destroy(actor);
        }
    }
}