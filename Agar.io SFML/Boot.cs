using Agar.io_SFML.Factory;
using SFML.Graphics;
using SFML.Window;
using Agar.io_SFML.Configs;

namespace Agar.io_SFML;

public class Boot
{
    private RenderWindow _window;

    private uint _windowWidth;
    private uint _windowHeight;
    private string _windowName;

    public void Start()
    {
        ConfigProcesser.ReadWholeConfig();

        _windowWidth = (uint)WindowConfig.WindowWidth;
        _windowHeight = (uint)WindowConfig.WindowHeight;
        _windowName = WindowConfig.WindowName;
        
        CreateWindow();

        Camera camera = new(_window, new(_windowWidth / 4f, _windowHeight / 4f, _windowWidth * 3/4f, _windowHeight * 3/4f));
        
        KeyInputSet keyInputSet = new KeyInputSet();
        
        GameMode gameMode = new();
        
        GameLoop gameLoop = new(_window, gameMode, keyInputSet, camera);
        
        EatableActorFactory eatableActorFactory = new(_window, gameLoop);
        TextFactory textFactory = new(gameLoop, camera);
        
        Game game = new(gameMode, keyInputSet, eatableActorFactory, textFactory, camera);
        
        game.Start(gameLoop);
        gameLoop.Start(); 
    }
    
    private void CreateWindow()
    {
        _window = new(new VideoMode(_windowWidth, _windowHeight), _windowName);
        _window.Closed += WindowClosed;
    }
    
    private void WindowClosed(object? sender, EventArgs e)
    {
        RenderWindow window = (RenderWindow)sender!;
        window.Close();
    }
}