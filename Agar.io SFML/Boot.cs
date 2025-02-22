using Agar.io_SFML.Factory;
using SFML.Graphics;
using SFML.Window;
using Agar.io_SFML.Configs;
using Agar.io_SFML.PauseControl;
using SFML.System;

namespace Agar.io_SFML;

public class Boot
{
    public static Boot Instance;

    public GameLoop GameLoopInstance => Instance._gameLoop;
    public PauseManager pauseManager;
    
    private RenderWindow _window;
    private GameLoop _gameLoop;

    private uint _windowWidth;
    private uint _windowHeight;
    private string _windowName;

    public Boot()
    {
        Instance = this;
    }

    public void Start()
    {
        ConfigProcesser.ReadWholeConfig();
         
        pauseManager = new ();

        _windowWidth = (uint)WindowConfig.WindowWidth;
        _windowHeight = (uint)WindowConfig.WindowHeight;
        _windowName = WindowConfig.WindowName;
            
        CreateWindow();

        Camera camera = new(_window, new(_windowWidth / 4f, _windowHeight / 4f, _windowWidth * 3/4f, _windowHeight * 3/4f));
        
        KeyInputSet keyInputSet = new KeyInputSet();
        
        GameMode gameMode = new();
        
        GameLoop gameLoop = new(_window, gameMode, keyInputSet, camera);
        
        Game game = new(gameMode, keyInputSet, camera);
        
        game.Start(gameLoop, _window);
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