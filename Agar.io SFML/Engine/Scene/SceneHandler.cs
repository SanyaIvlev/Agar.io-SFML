using Agar.io_SFML.Animations;
using SFML.Graphics;
using SFML.Window;

namespace Agar.io_SFML.Engine.Scene;

public class SceneHandler
{
    private Scene _currentScene;
    
    private Queue<Scene> _sceneQueue;
    private RenderWindow _renderWindow;

    public SceneHandler((uint x, uint y) windowScale, string windowName)
    {
        _sceneQueue = new Queue<Scene>();
        
        _renderWindow = new RenderWindow(new VideoMode(windowScale.x, windowScale.y), windowName);
        
        Service<SceneHandler>.Set(this);
        Service<RenderWindow>.Set(_renderWindow);
        
        new TextureLoader(); 
    }

    public void StartWithScene<T>() where T : Scene, new()
    {
        _sceneQueue.Enqueue(new T());
        
        while (_renderWindow.IsOpen && _sceneQueue.Count > 0)
        {
            _currentScene = _sceneQueue.Dequeue();
            
            _currentScene.InitializeAndStart();
        }

        _renderWindow.Close();
    }

    public void SelectScene<T>() where T : Scene, new()
    {
        _sceneQueue.Enqueue(new T());
        _currentScene.Stop();
    }
}