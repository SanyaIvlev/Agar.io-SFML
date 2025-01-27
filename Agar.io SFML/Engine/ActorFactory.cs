using SFML.Graphics;

namespace Agar.io_SFML;

public class ActorFactory
{
    private GameLoop _gameLoop;
    
    
    protected ActorFactory(GameLoop gameLoop)
    {
        _gameLoop = gameLoop;
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
    }
    
    protected void Destroy(Actor actor)
    {
        if (actor is IUpdatable updatable)
            _gameLoop.RemoveUpdatable(updatable);
        
        if(actor is IDrawable drawable)
            _gameLoop.RemoveDrawable(drawable);

        if (actor is Controller controller)
            _gameLoop.RemoveController(controller);
    }
}