using Agar.io_SFML.Animations;
using SFML.Graphics;
using SFML.Window;

namespace Agar.io_SFML.Engine.Scene;

public class SceneHandler
{
    private Scene _currentScene;
    
    private GameLoop _gameLoop;
    private RenderWindow _renderWindow;

    public SceneHandler((uint x, uint y) windowScale, string windowName)
    {
        _renderWindow = new RenderWindow(new VideoMode(windowScale.x, windowScale.y), windowName);
        
        Service<SceneHandler>.Set(this);
        Service<RenderWindow>.Set(_renderWindow);
        
        new ConfigProcesser().ReadWholeConfig(); 
        
        new TextureLoader(); 
    }

    public void InitializeScene<T>() where T : Scene, new()
    {
        _gameLoop = new GameLoop(_renderWindow);
        
        EventBus<OnGameLoopReset>.Raise(new()); 
        
        _currentScene = new T();
        _currentScene.Start();

        _gameLoop.AddOnGameUpdateCallback(_currentScene.Update);
        
        _gameLoop.Start();
    }

    public void SelectScene<T>() where T : Scene, new()
    {
        _gameLoop.StopGameLoop();
        _gameLoop = null;

        InitializeScene<T>();
    }
}