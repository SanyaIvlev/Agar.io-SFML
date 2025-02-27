using Agar.io_SFML.Animations;
using Agar.io_SFML.Configs;
using Agar.io_SFML.Engine;

namespace Agar.io_SFML;

public class Boot
{
    public static Boot Instance;
    
    private GameMode _gameMode;
    private ButtonBindsSet _buttonBindsSet;
    
    private Lobby _lobby;

    private uint _windowWidth;
    private uint _windowHeight;
    private string _windowName;

    public Boot()
    {
        Instance = this;
    }

    public void StartLobby()
    {
        new ConfigProcesser().ReadWholeConfig(); 
        
        new TextureLoader(); 
        
        _windowWidth = (uint)WindowConfig.WindowWidth;
        _windowHeight = (uint)WindowConfig.WindowHeight;
        _windowName = WindowConfig.WindowName;
        
        GameLoop gameLoop = new((_windowWidth, _windowHeight), _windowName);
        
        _lobby = new();
        
        gameLoop.AddOnGameUpdateCallback(_lobby.Update);

        _lobby.Start();
        
        gameLoop.Start();
    }

    public void StartGameLoop()
    {
        GameLoop gameLoop = Dependency.Get<GameLoop>() ?? new GameLoop((_windowWidth, _windowHeight), _windowName);
        gameLoop.RemoveOnGameUpdateCallback(_lobby.Update);
        
        Game game = new();
        
        gameLoop.AddOnGameUpdateCallback(game.Update);
        
        game.Start();
    }
}