using SFML.Graphics;
using SFML.Window;

namespace Agar.io_SFML;

public class GameLoop
{
    public const int TARGET_FPS = 60;
    public const int SECOND_TO_MICROSECONDS = 1000000;
    public const long TIME_BEFORE_NEXT_FRAME = SECOND_TO_MICROSECONDS / TARGET_FPS;
    
    public const uint WINDOW_WIDTH = 1600;
    public const uint WINDOW_HEIGHT = 800;
    
    private string WINDOW_NAME = "Agar.io";
    private RenderWindow _window;
    
    private InputHandler _inputHandler;
    
    private List<IDrawable> _drawables;
    private List<IUpdatable> _updatables;
    
    private Game _game;

    public void AddDrawable(IDrawable drawable)
    {
        if(!_drawables.Contains(drawable))
            _drawables.Add(drawable);
    }

    public void AddUpdatable(IUpdatable updatable)
    {
        if(!_updatables.Contains(updatable))
            _updatables.Add(updatable);
    }
    
    public void RemoveUpdatable(IUpdatable actor)
    {
        if (_updatables.Contains(actor))
            _updatables.Remove(actor);
    }

    public void RemoveDrawable(IDrawable drawable)
    {
        if (_drawables.Contains(drawable))
            _drawables.Remove(drawable);
    }
    
    
    public void Start()
    {
        Time.Start();

        _inputHandler = new();
        
        _updatables = new();
        _drawables = new();
        
        _game = new(_inputHandler);

        _game.OnUpdatableSpawned += AddUpdatable;
        _game.OnUpdatableDestroyed += RemoveUpdatable;
        
        _game.OnDrawableSpawned += AddDrawable;
        _game.OnDrawableDestroyed += RemoveDrawable;
        
        _game.Start();


        
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

        WaitBeforeEnd();
    }

    private bool IsGameLoopEnded()
        => _game.IsGameEnded || !_window.IsOpen;
    
    
    private void ProcessInput()
    {
        _inputHandler.ProcessInput(_window);
    }

    private void Update()
    {
        _window.DispatchEvents();
         
        foreach (var updatable in _updatables)
        {
            updatable.Update();
        }
        
        _game.Update();
        
        Time.Update();
        
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
    
    
    private void WaitBeforeEnd()
    {
        Thread.Sleep(3000);
    }

    
    private void WindowClosed(object? sender, EventArgs e)
    {
        RenderWindow window = (RenderWindow)sender;
        window.Close();
    }
}