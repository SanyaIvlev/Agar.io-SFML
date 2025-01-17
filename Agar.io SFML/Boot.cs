using SFML.Graphics;
using SFML.Window;

namespace Agar.io_SFML;

public class Boot
{
    public const uint WindowWidth = 1600;
    public const uint WindowHeight = 800;

    private const string WindowName = "Agar.io";

    private RenderWindow _window;
    
    public void Start()
    {
        CreateWindow();
        
        GameMode gameMode = new();
        
        Game game = new(_window, gameMode);
        GameLoop gameLoop = new(_window, gameMode);
        
        gameLoop.Initialize(game);
        game.Start(gameLoop);
        gameLoop.Start();
    }
    
    private void CreateWindow()
    {
        _window = new(new VideoMode(WindowWidth, WindowHeight), WindowName);
        _window.Closed += WindowClosed;
    }
    
    private void WindowClosed(object? sender, EventArgs e)
    {
        RenderWindow window = (RenderWindow)sender!;
        window.Close();
    }
}