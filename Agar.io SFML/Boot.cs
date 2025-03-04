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
        new ConfigProcesser().ReadWholeConfig("SeaBattleConfig.ini"); 
        
        _windowWidth = (uint)SeaBattleWindowConfig.WindowWidth;
        _windowHeight = (uint)SeaBattleWindowConfig.WindowHeight;
        _windowName = SeaBattleWindowConfig.WindowName;
        
        SceneHandler sceneHandler = new((_windowWidth, _windowHeight), _windowName);
        sceneHandler.StartWithScene<SeaBattleGame>();
    }
}