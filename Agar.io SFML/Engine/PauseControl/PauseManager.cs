using Agar.io_SFML.Extensions;

namespace Agar.io_SFML.PauseControl;

public class PauseManager
{
    private readonly List<IPauseHandler> _pauseHandlers;
    private bool _isPaused = false;

    public PauseManager()
    {
        _pauseHandlers = new ();
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
        _isPaused = !_isPaused;
        foreach (var handler in _pauseHandlers)
        {
            handler.SetPaused(_isPaused);
        }
    }
}