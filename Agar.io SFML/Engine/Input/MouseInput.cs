using SFML.Window;

namespace Agar.io_SFML;

public class MouseInput : BaseInput
{
    private Mouse.Button _button;

    public MouseInput(Mouse.Button button)
    {
        _button = button;
    }

    protected override bool IsButtonPressed()
        => Mouse.IsButtonPressed(_button);
}