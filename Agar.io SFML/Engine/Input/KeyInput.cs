using SFML.Window;

namespace Agar.io_SFML;

public class KeyInput
{
    public Action OnPressed;
    public Action OnHoldDown; // for future

    private bool _wasDown;
    private bool _isDown;

    private Keyboard.Key _key;

    public KeyInput(Keyboard.Key key)
    {
        _key = key;
    }
    
    public void Update()
    {
        _wasDown = _isDown;
        _isDown = Keyboard.IsKeyPressed(_key);

        if (_isDown && !_wasDown)
        {
            OnPressed?.Invoke();
        }
        else if (_isDown && _wasDown)
        {
            OnHoldDown?.Invoke();
        }
    }
}