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
        _gameLoop.AddUpdatable(actor);
        _gameLoop.AddDrawable(actor);
    }
    
    protected void Destroy(Actor actor)
    {
        _gameLoop.RemoveUpdatable(actor);
        _gameLoop.RemoveDrawable(actor);
    }
}