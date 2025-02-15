using Agar.io_SFML.Extensions;

namespace Agar.io_SFML.PauseControl;

public class PauseManager
{
    private readonly List<IPauseHandler> _pauseHandlers;
    public bool IsPaused { get; private set; }

    public PauseManager()
    {
        _pauseHandlers = new ();
        IsPaused = false;
    }

    public void Register(IPauseHandler pauseHandler)
    {
        _pauseHandlers.Add(pauseHandler);
    }

    public void Unregister(IPauseHandler pauseHandler)
    {
        _pauseHandlers.SwapRemove(pauseHandler);
    }

    public void SwitchPauseState()
    {
        IsPaused = !IsPaused;
        foreach (var handler in _pauseHandlers)
        {
            handler.SetPaused(IsPaused);
        }
    }
}