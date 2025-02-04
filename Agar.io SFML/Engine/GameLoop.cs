using Agar.io_SFML.Configs;
using Agar.io_SFML.Extensions;
using SFML.Graphics;
using SFML.System;

namespace Agar.io_SFML;

public class GameLoop
{
    private const int TargetFps = 1200;
    private const int SecondToMicroseconds = 1000000;
    private const float TimeBeforeNextFrame =  SecondToMicroseconds * 1f / TargetFps;
    
    public Action OnGameUpdateNeeded;
    
    private List<IDrawable> _drawables;
    private List<IUpdatable> _updatables;
    
    private List<Controller> _controllers;
    
    private KeyInputSet _keyInputs;
    
    private readonly RenderWindow _window;
    
    private readonly GameMode _gameMode;

    private Camera _camera;

    public GameLoop(RenderWindow window, GameMode gameMode, KeyInputSet keyInputs, Camera camera)
    {
        _window = window;
        
        _gameMode = gameMode;
        
        _updatables = [];
        _drawables = [];
        _controllers = [];

        _keyInputs = keyInputs;
        
        _camera = camera;
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

        WaitBeforeEnd();
    }

    private bool IsGameLoopEnded()
        => _gameMode.IsGameEnded || !_window.IsOpen;
    
    
    private void ProcessInput()
    {
        _window.DispatchEvents();

        foreach (Controller controller in _controllers)
        {
            controller.Update();
        }
    }

    private void Update()
    {
        _keyInputs.Update();
        
        foreach (var updatable in _updatables)
        {
            updatable.Update();
        }
        
        OnGameUpdateNeeded?.Invoke();

        Time.UpdateTimer();
    }

    private void Render()
    {
        _window.Clear(new Color(122,122,122));
        
        _camera.Update();

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