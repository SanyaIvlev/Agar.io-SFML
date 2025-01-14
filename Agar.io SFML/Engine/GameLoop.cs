using SFML.Graphics;
using SFML.Window;

namespace Agar.io_SFML;

public class GameLoop
{
    public const int TARGET_FPS = 60;
    public const int SECOND_TO_MICROSECONDS = 1000000;
    public const long TIME_BEFORE_NEXT_FRAME = SECOND_TO_MICROSECONDS / TARGET_FPS;
    
    public Action OnInputProcessed;
    public Action OnGameUpdateNeeded;
    
    private List<IDrawable> _drawables;
    private List<IUpdatable> _updatables;
    
    private RenderWindow _window;
    
    private GameMode _gameMode;

    public GameLoop(RenderWindow window, GameMode gameMode)
    {
        _window = window;
        
        _gameMode = gameMode;
    }
    
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

    public void Initialize(Game game)
    {
        Time.Start();
        
        _updatables = new();
        _drawables = new();

        game.OnUpdatableSpawned += AddUpdatable;
        game.OnUpdatableDestroyed += RemoveUpdatable;
        
        game.OnDrawableSpawned += AddDrawable;
        game.OnDrawableDestroyed += RemoveDrawable;
    }
    
    public void Start()
    {
        Run();
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
        => _gameMode.IsGameEnded || !_window.IsOpen;
    
    
    private void ProcessInput()
    {
        _window.DispatchEvents();
        OnInputProcessed?.Invoke();
    }

    private void Update()
    {
        foreach (var updatable in _updatables)
        {
            updatable.Update();
        }
        
        OnGameUpdateNeeded?.Invoke();

        Time.Update();
    }

    private void Render()
    {
        _window.Clear(new Color(122,122,122));

        foreach (var drawable in _drawables)
        {
            drawable.Draw(_window);
        }
        
        _window.Display();
    }
    
    
    private void WaitBeforeEnd()
    {
        Thread.Sleep(1000);
    }
}