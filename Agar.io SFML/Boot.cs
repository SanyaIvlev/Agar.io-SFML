using Agar.io_SFML.Animations;
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
    private GameMode _gameMode;
    private KeyInputSet _keyInputSet;
    private Camera _camera;
    
    private AnimatorFactory _animatorFactory;

    private uint _windowWidth;
    private uint _windowHeight;
    private string _windowName;

    public Boot()
    {
        Instance = this;
    }

    public void StartLobby()
    {
        ConfigProcesser.ReadWholeConfig();

        new TextureLoader(); 
        
        _windowWidth = (uint)WindowConfig.WindowWidth;
        _windowHeight = (uint)WindowConfig.WindowHeight;
        _windowName = WindowConfig.WindowName;
            
        CreateWindow();
        
        _keyInputSet = new KeyInputSet();
        
        _gameMode = new();
        
        _gameLoop = new(_window, _gameMode, _keyInputSet);
        
        Lobby lobby = new(_window);

        lobby.Start(_animatorFactory);
        
        _gameLoop.Start();
    }

    public void StartGameLoop()
    {
        pauseManager = new ();
        
        Game game = new(_gameMode, _keyInputSet, _camera);
        
        _camera = new(_window, new(_windowWidth / 4f, _windowHeight / 4f, _windowWidth * 3/4f, _windowHeight * 3/4f));
        _gameLoop.AddCamera(_camera);
        
        game.Start(_gameLoop, _window, _animatorFactory);
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