using SFML.Graphics;

namespace Agar.io_SFML.Engine.Scene;

public class Scene
{
    private GameLoop _gameLoop;
    
    public Scene()
    {
        Service<Scene>.Set(this);
        
        _gameLoop = new GameLoop();
        
        EventBus<OnGameLoopReset>.Raise(new()); 
    }
    
    public void InitializeAndStart()
    {
        _gameLoop.AddOnGameUpdateCallback(Update);

        Start();
        
        _gameLoop.Start();
    }
    
    public void Stop()
        => _gameLoop.StopGameLoop();

    public virtual void Start() { }

    public virtual void Update() { }
}