using Agar.io_SFML.Animations;
using Agar.io_SFML.Configs;
using Agar.io_SFML.Engine;
using SFML.Graphics;
using SFML.System;

namespace Agar.io_SFML.Factory;

public class ShapeFactory : ActorFactory
{
    private RenderWindow _window;
    
    private float _windowWidth;
    private float _windowHeight;
    
    private Vector2f _windowCenter;

    public ShapeFactory()
    {
        Dependency.Register(this);
        
        _window = Dependency.Get<RenderWindow>();

        _windowWidth = WindowConfig.WindowWidth;
        _windowHeight = WindowConfig.WindowHeight;

        _windowCenter = new(_window.Size.X / 2f, _window.Size.Y / 2f);
    }

    public ShapeActor CreateSkinShape(Texture texture)
    {
        ShapeActor shapeActor = CreateActor<ShapeActor>();
        
        shapeActor.Initialize(new RectangleShape((Vector2f)texture.Size));
        
        shapeActor.shape.Texture = texture;
            
        shapeActor.shape.Origin = new(shapeActor.shape.TextureRect.Width / 2f, shapeActor.shape.TextureRect.Height / 2f);
        shapeActor.IsActive = false;
        shapeActor.shape.Position = _windowCenter;

        shapeActor.shape.Scale *= 5;
        
        return shapeActor;
    }
    
    public Button CreateButton(string textureName)
    {
        Button button = CreateActor<Button>();

        var texture = TextureLoader.Instance.FindTextureByName(textureName);
        
        button.Initialize(new RectangleShape((Vector2f)texture.Size));
        
        var shape = button.shape;
        shape.Texture = texture;

        shape.Origin = new Vector2f(shape.TextureRect.Width / 2f, shape.TextureRect.Height / 2f);
        
        
        return button;
    }

    public void CreateBackground()
    {
        ShapeActor background = CreateActor<ShapeActor>();
        
        Texture backgroundTexture = TextureLoader.Instance.FindTextureByName("Background");
        
        background.Initialize(new RectangleShape(new Vector2f(_windowWidth, _windowHeight)), backgroundTexture);
        background.shape.FillColor = new Color(180, 180, 180);
    }
}