using SFML.Window;

namespace Agar.io_SFML;

public class KeyBind
{
    public Action OnPressed;

    public string Name { get; private set; }
    private Keyboard.Key _key;

    public KeyBind(string name, Keyboard.Key key)
    {
        Name = name;
        _key = key;
    }

    public void Update()
    {
        bool isPressed = Keyboard.IsKeyPressed(_key);

        if (isPressed)
        {
            OnPressed?.Invoke();
        }
    }
}