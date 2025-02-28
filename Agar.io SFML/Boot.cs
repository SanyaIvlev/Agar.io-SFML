using Agar.io_SFML.Configs;
using Agar.io_SFML.Engine.Scene;
using Agar.io_SFML.GameSeaBattle;

namespace Agar.io_SFML;

public class Boot
{
    private uint _windowWidth;
    private uint _windowHeight;
    private string _windowName;

    public void Start()
    {
        // _windowWidth = (uint)WindowConfig.WindowWidth;
        // _windowHeight = (uint)WindowConfig.WindowHeight;
        // _windowName = WindowConfig.WindowName;
        //
        // SceneHandler sceneHandler = new((_windowWidth, _windowHeight), _windowName);
        // sceneHandler.InitializeScene<Lobby>();
        new Field().Generate();
    }
}