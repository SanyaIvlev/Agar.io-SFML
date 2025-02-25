using Agar.io_SFML.Audio;
using Agar.io_SFML.Configs;
using Agar.io_SFML.Engine;
using Agar.io_SFML.Extensions;
using SFML.Graphics;
using SFML.System;

namespace Agar.io_SFML.Factory;

public class EatableActorFactory : ActorFactory
{
    private Vector2f _startPosition;
    private AgarioController _controller;
    private Color _color;
    
    private readonly RenderWindow _window;

    private Action<EatableActor> _onPlayerDeath;
    
    private readonly int _windowWidth;
    private readonly int _windowHeight;
    private readonly AgarioAudioSystem _audioSystem;
    
    private AnimatorFactory _animatorFactory;

    public EatableActorFactory()
    {
        _window = Dependency.Get<RenderWindow>();
        _audioSystem = Dependency.Get<AgarioAudioSystem>();
        _animatorFactory = Dependency.Get<AnimatorFactory>();
        
        _windowWidth = WindowConfig.WindowWidth;
        _windowHeight = WindowConfig.WindowHeight;
    }

    public void SetPlayerDeathResponse(Action<EatableActor> onPlayerDeath)
    {
        _onPlayerDeath += onPlayerDeath;
    }

    public AgarioController CreateController(bool isHuman)
    {
        Player controlledPlayer = CreatePlayer(isHuman);

        if (isHuman)
        {
            _controller = CreateActor<AgarioPlayerController>();
            _controller.Initialize(controlledPlayer, _window, _audioSystem);
        }
        else
        {
            _controller = CreateActor<AgarioAIController>();
            _controller.Initialize(controlledPlayer, _window);
        }
        
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
        
        newPlayer.Initalize(_startPosition, _color);

        newPlayer.OnDestroyed += _onPlayerDeath;
        
       _animatorFactory.CreatePlayerAnimator(newPlayer, isHuman); 
        
        return newPlayer;
    }

    public Food CreateFood()
    {
        Food newFood = CreateActor<Food>();
        
        _startPosition = MathExtensions.GetRandomPosition(_windowWidth, _windowHeight);
        
        _color = _color.GetRandomColor();
        
        newFood.Initialize(_startPosition, _color);
        
        _animatorFactory.CreateFoodAnimator(newFood.shape);
        
        return newFood;
    }
}