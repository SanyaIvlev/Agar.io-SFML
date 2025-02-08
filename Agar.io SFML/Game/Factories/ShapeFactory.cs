using Agar.io_SFML.Configs;
using SFML.Graphics;
using SFML.System;

namespace Agar.io_SFML.Factory;

public class ShapeFactory : ActorFactory
{
    private RenderWindow _window;
    private float _windowWidth;
    private float _windowHeight;

    public ShapeFactory(RenderWindow window, GameLoop _gameLoop) : base(_gameLoop)
    {
        _window = window;

        _windowWidth = WindowConfig.WindowWidth;
        _windowHeight = WindowConfig.WindowHeight;
    }

    public ShapeActor CreateBackground()
    {
        ShapeActor background = CreateActor<ShapeActor>();
        
        background.Initialize(_window, new RectangleShape(new Vector2f(_windowWidth, _windowHeight)));
        background.shape.FillColor = new Color(122, 122, 122);
        
        return background;
    }
}