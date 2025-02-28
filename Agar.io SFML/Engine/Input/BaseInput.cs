namespace Agar.io_SFML;

public class BaseInput
{
    private Action _onPressed;
    private Action _onHeldDown;

    private bool _wasDown;
    private bool _isDown;

    public void AddCallBackOnPressed(Action action)
        => _onPressed += action;
    
    public void AddCallBackOnHeldDown(Action action)
        => _onHeldDown += action;

    protected virtual bool IsButtonPressed()
    {
        return false;
    }

    public void UpdateCallbacks()
    {
        _wasDown = _isDown;
        _isDown = IsButtonPressed();
        
        if (_isDown && !_wasDown)
        { 
            _onPressed?.Invoke();
        }
        else if (_isDown && _wasDown)
        {
            _onHeldDown?.Invoke();
        }
    }

    public void RemoveCallBacks()
    {
        _onPressed = null;
        _onHeldDown = null;
    }
}