using SFML.Window;

namespace Agar.io_SFML;

public class KeyInput : BaseInput
{
    private Keyboard.Key _key;

    public KeyInput(Keyboard.Key key)
    {
        _key = key;
    }

    protected override bool IsButtonPressed()
        => Keyboard.IsKeyPressed(_key);
}