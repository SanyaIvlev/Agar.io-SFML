using SFML.Graphics;
using SFML.Window;

namespace Agar.io_SFML;

public class GameLoop
{
    private const int TARGET_FPS = 120;
    private const int SECOND_TO_MICROSECONDS = 1000000;
    private const long TIME_BEFORE_NEXT_FRAME = SECOND_TO_MICROSECONDS / TARGET_FPS;
    
    private const uint WINDOW_WIDTH = 800;
    private const uint WINDOW_HEIGHT = 600;
    
    private string WINDOW_NAME = "Agar.io";
    private RenderWindow _window;
    
    private InputHandler _inputHandler;
    
    private List<IDrawable> _drawables;
    private List<IUpdatable> _updatables;
    
    private Game _game;
    
    public void Start()
    {
        Time.Start();
        
        CreateWindow();

        Run();
    }

    private void CreateWindow()
    {
        _window = new(new VideoMode(WINDOW_WIDTH, WINDOW_HEIGHT), WINDOW_NAME);
        _window.Closed += WindowClosed;
    }
    
    private void Run()
    {
        long totalTimeBeforeUpdate = 0;
        long previousTimeElapsed = 0;
        long deltaTime;
        
        while (!IsGameLoopEnded())
        {
            ProcessInput();

            long elapsedTime = Time.GetElapsedTimeAsMicroseconds();
            
            deltaTime = elapsedTime - previousTimeElapsed;
            previousTimeElapsed = elapsedTime;
            
            totalTimeBeforeUpdate += deltaTime;

            if (totalTimeBeforeUpdate >= TIME_BEFORE_NEXT_FRAME)
            {
                Update();
                Render();
            }
        }
    }

    private bool IsGameLoopEnded()
        => false;
    
    
    private void ProcessInput()
    {
        _inputHandler.ProcessInput(_window);
    }

    private void Update()
    {
        _window.DispatchEvents();
        
        Time.Update();

        foreach (var updatable in _updatables)
        {
            updatable.Update();
        }
        
        _game.Update();
        
    }

    private void Render()
    {
        _window.Clear();

        foreach (var drawable in _drawables)
        {
            drawable.Draw(_window);
        }
        
        _window.Display();
    }
    
    private void WindowClosed(object? sender, EventArgs e)
    {
        RenderWindow window = (RenderWindow)sender;
        window.Close();
    }
}