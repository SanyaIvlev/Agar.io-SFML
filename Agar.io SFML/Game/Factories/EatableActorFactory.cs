using Agar.io_SFML.Configs;
using Agar.io_SFML.Extensions;
using SFML.Graphics;
using SFML.System;

namespace Agar.io_SFML.Factory;

public class EatableActorFactory : ActorFactory
{
    private Vector2f _startPosition;
    private Controller _controller;
    private Color _color;
    private Color _outline;
    
    private readonly RenderWindow _window;

    private Action<EatableActor> _onPlayerDeath;
    
    private readonly int _windowWidth;
    private readonly int _windowHeight;
    
    public EatableActorFactory(RenderWindow window, GameLoop gameLoop) : base(gameLoop)
    {
        _window = window;
        
        _windowWidth = WindowConfig.WindowHeight;
        _windowHeight = WindowConfig.WindowWidth;
    }

    public void SetPlayerDeathResponse(Action<EatableActor> onPlayerDeath)
    {
        _onPlayerDeath += onPlayerDeath;
    }

    public Controller CreateController(bool isHuman)
    {
        Player controlledPlayer = CreatePlayer(isHuman);
        
        if(isHuman)
            _controller = CreateActor<AgarioPlayerController>();
        else
            _controller = CreateActor<AgarioAIController>();
        
        _controller.Initialize(controlledPlayer, _window);
        
        return _controller;
    }
    
    private Player CreatePlayer(bool isHuman)
    {
        Player newPlayer = CreateActor<Player>();
        
        if (isHuman)
        {
            _startPosition = new (_windowWidth / 2f, _windowHeight / 2f);
        }
        else
        {
            _startPosition = MathExtensions.GetRandomPosition(_windowWidth, _windowHeight);
        }
        
        _color = _color.GetRandomColor();
        _outline = _color.GetDarkerShade();
        
        newPlayer.Initalize(_startPosition, _color, _outline);

        newPlayer.OnDestroyed += _onPlayerDeath;
        
        return newPlayer;
    }

    public Food CreateFood()
    {
        Food newFood = CreateActor<Food>();
        
        _startPosition = MathExtensions.GetRandomPosition(_windowWidth, _windowHeight);
        
        _color = _color.GetRandomColor();
        
        newFood.Initialize(_startPosition, _color);
        
        return newFood;
    }

    public void Destroy(Actor actor)
    {
        base.Destroy(actor);
    }
}