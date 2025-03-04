using SFML.Graphics;

namespace Agar.io_SFML.Engine.Scene;

public class Scene
{
    private GameLoop _gameLoop;
    
    public Scene()
    {
        Service<Scene>.Set(this);
        
        
    }
    
    public void InitializeAndStart()
    {
        _gameLoop = new GameLoop();
        
        EventBus<OnGameLoopReset>.Raise(new()); 
        
        _gameLoop.AddOnGameUpdateCallback(Update);

        Start();
        
        _gameLoop.Start();
        
        _gameLoop.StopGameLoop();
    }

    public virtual void Start() { }

    public virtual void Update() { }
}