using Agar.io_SFML.Extensions;
using SFML.Graphics;
using SFML.System;

namespace Agar.io_SFML.Factory;

public class EatableActorFactory
{
    private GameLoop _gameLoop;
    private RenderWindow _renderWindow;
    
    private Vector2f _startPosition;
    private IController _controller;
    private Color _color;

    private int _maxWidth => (int)Boot.WindowWidth;
    private int _maxHeight => (int)Boot.WindowHeight;
    
    
    public EatableActorFactory(RenderWindow window, GameLoop gameLoop)
    {
        _renderWindow = window;
        _gameLoop = gameLoop;
    }
    
    public Player CreatePlayer(bool isHuman)
    {
        if (isHuman)
        {
            _controller = new HumanController(_renderWindow);
            _startPosition = new (_maxWidth / 2f, _maxHeight / 2f);
        }
        else
        {
            _controller = new BotController(_renderWindow);
            _startPosition = MathExtensions.GetRandomPosition(_maxWidth, _maxHeight);
        }
        _color = _color.GetRandomColor();

        Player newPlayer = new(_controller, isHuman, _startPosition, _color, _renderWindow);

        Register(newPlayer);
        
        return newPlayer;
    }

    public void Register(EatableActor actor)
    {
        _gameLoop.AddUpdatable(actor);
        _gameLoop.AddDrawable(actor);
    }

    public Food CreateFood()
    {
        _startPosition = MathExtensions.GetRandomPosition(_maxWidth, _maxHeight);
        
        _color = _color.GetRandomColor();
        
        var newFood = new Food(_startPosition, _color);
        
        Register(newFood);
        
        return newFood;
    }
    
    public void Unregister(EatableActor actor)
    {
        _gameLoop.RemoveUpdatable(actor);
        _gameLoop.RemoveDrawable(actor);
    }
}