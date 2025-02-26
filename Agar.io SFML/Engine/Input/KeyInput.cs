using SFML.Window;

namespace Agar.io_SFML;

public class KeyInput
{
    public Action OnPressed;
    public Action OnHeldDown; // for future

    private bool _wasDown;
    private bool _isDown;

    private Keyboard.Key _key;

    public KeyInput(Keyboard.Key key)
    {
        _key = key;
    }

    public void AddCallBackOnPressed(Action action)
        => OnPressed += action;
    
    public void AddCallBackOnHeldDown(Action action)
        => OnHeldDown += action;

    public void ReadInput()
    {
        _wasDown = _isDown;
        _isDown = Keyboard.IsKeyPressed(_key);
    }
    
    public void UpdateCallbacks()
    {
        if (_isDown && !_wasDown)
        { 
            OnPressed?.Invoke();
        }
        else if (_isDown && _wasDown)
        {
            OnHeldDown?.Invoke();
        }
    }
}