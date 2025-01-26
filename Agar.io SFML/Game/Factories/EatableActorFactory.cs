using Agar.io_SFML.Extensions;
using SFML.Graphics;
using SFML.System;

namespace Agar.io_SFML.Factory;

public class EatableActorFactory : ActorFactory
{
    private Vector2f _startPosition;
    private IController _controller;
    private Color _color;

    private int _maxWidth => (int)Boot.WindowWidth;
    private int _maxHeight => (int)Boot.WindowHeight;
    
    private readonly RenderWindow _window;
    
    public EatableActorFactory(RenderWindow window, GameLoop gameLoop) : base(gameLoop)
    {
        _window = window;
    }
    
    public Player CreatePlayer(bool isHuman)
    {
        Player newPlayer = CreateActor<Player>();
        
        if (isHuman)
        {
            _controller = new HumanController(_window);
            _startPosition = new (_maxWidth / 2f, _maxHeight / 2f);
        }
        else
        {
            _controller = new BotController(_window);
            _startPosition = MathExtensions.GetRandomPosition(_maxWidth, _maxHeight);
        }
        _color = _color.GetRandomColor();

        newPlayer.Initalize(_controller, isHuman, _startPosition, _color, _window);
        
        return newPlayer;
    }

    public Food CreateFood()
    {
        Food newFood = CreateActor<Food>();
        
        _startPosition = MathExtensions.GetRandomPosition(_maxWidth, _maxHeight);
        
        _color = _color.GetRandomColor();
        
        newFood.Initialize(_startPosition, _color);
        
        return newFood;
    }

    public void Destroy(Actor actor)
    {
        base.Destroy(actor);
    }
}