using Agar.io_SFML.Configs;
using Agar.io_SFML.Engine;
using Agar.io_SFML.Extensions;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Agar.io_SFML;

public class GameLoop
{
    private const int TargetFps = 1200;
    private const int SecondToMicroseconds = 1000000;
    private const float TimeBeforeNextFrame =  SecondToMicroseconds * 1f / TargetFps;
    
    private Action _onGameUpdateNeeded;
    
    private List<IDrawable> _drawables;
    private List<IUpdatable> _updatables;
    
    private List<Controller> _controllers;
    
    private ButtonBindsSet _buttonBindses;
    
    private readonly RenderWindow _window;
    
    private readonly GameMode _gameMode;

    private Camera _camera;

    public GameLoop((uint x, uint y) windowScale, string windowName)
    {
        Dependency.Register(this);
        
        _window = new RenderWindow(new VideoMode(windowScale.x, windowScale.y), windowName);
        _window.Closed += (sender, args) => CloseApplicationImmediately();
        
        Dependency.Register(_window);
        
        _camera = new Camera(_window);
        
        _gameMode = new();
        
        _updatables = [];
        _drawables = [];
        _controllers = [];

        _buttonBindses = new();
    }

    public void AddOnGameUpdateCallback(Action callback)
        => _onGameUpdateNeeded += callback;
    
    public void RemoveOnGameUpdateCallback(Action callback)
        => _onGameUpdateNeeded -= callback;
    
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
        _updatables.SwapRemove(actor);
    }

    public void RemoveDrawable(IDrawable drawable)
    {
        _drawables.SwapRemove(drawable);
    }

    public void AddController(Controller controller)
    {
        if(!_controllers.Contains(controller))
            _controllers.Add(controller);
    }

    public void RemoveController(Controller controller)
    {
        _controllers.SwapRemove(controller);
    }
    
    public void Start()
    {
        Time.Start();
        
        Run();
    }
    
    private void Run()
    {
        while (!IsGameLoopEnded())
        {
            ProcessInput();

            if (Time.UpdateTimeBeforeUpdate() < TimeBeforeNextFrame)
                continue;
            
            Update();
            Render();
        }
        
        End();
    }

    private bool IsGameLoopEnded()
        => _gameMode.IsGameEnded || !_window.IsOpen;
    
    
    private void ProcessInput()
    {
        _window.DispatchEvents();
        
        _buttonBindses.ReadInputs();

        foreach (Controller controller in _controllers)
        {
            controller.Update();
        }
    }

    private void Update()
    {
        _buttonBindses.UpdateCallbacks();
        
        foreach (var updatable in _updatables)
        {
            updatable.OnMouseClick();
        }
        
        _onGameUpdateNeeded?.Invoke();

        Time.UpdateTimer();
    }

    private void Render()
    {
        _window.Clear(new Color(100, 100,100));
        
        _camera?.Update();

        foreach (var drawable in _drawables)
        {
            drawable.Draw(_window);
        }
        
        _window.Display();
    }
    
    private void End()
    {
        Thread.Sleep(1500);

        CloseApplicationImmediately();
    }
    
    private void CloseApplicationImmediately()
    {
        EventBus<GameOverEvent>.Raise(new());
        _window.Close();
    }

}