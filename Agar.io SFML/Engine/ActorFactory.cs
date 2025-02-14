﻿using Agar.io_SFML.PauseControl;
using SFML.Graphics;

namespace Agar.io_SFML;

public class ActorFactory
{
    private GameLoop _gameLoop;
    
    private PauseManager _pauseManager;
    
    
    protected ActorFactory(GameLoop gameLoop)
    {
        _gameLoop = gameLoop;
        _pauseManager = Boot.Instance.pauseManager;
    }

    protected TActor CreateActor<TActor>() where TActor : Actor, new()
    {
        var actorInstance = new TActor();
        Register(actorInstance);
        
        return actorInstance;
    } 
    
    private void Register(Actor actor)
    {
        if (actor is IUpdatable updatable)
            _gameLoop.AddUpdatable(updatable);
        
        if(actor is IDrawable drawable)
            _gameLoop.AddDrawable(drawable);
        
        if(actor is Controller controller)
            _gameLoop.AddController(controller);
        
        if(actor is IPauseHandler pauseHandler)
            _pauseManager.Register(pauseHandler);
    }
    
    protected void Destroy(Actor actor)
    {
        if (actor is IUpdatable updatable)
            _gameLoop.RemoveUpdatable(updatable);
        
        if(actor is IDrawable drawable)
            _gameLoop.RemoveDrawable(drawable);

        if (actor is Controller controller)
            _gameLoop.RemoveController(controller);
        
        if(actor is IPauseHandler pauseHandler)
            _pauseManager.Unregister(pauseHandler);
    }
}