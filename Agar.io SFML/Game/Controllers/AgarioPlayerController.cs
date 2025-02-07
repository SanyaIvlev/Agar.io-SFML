using SFML.Window;

namespace Agar.io_SFML;

public class AgarioPlayerController : AgarioController
{
    public override void Update()
    {
        ProcessMousePosition();
        base.Update();
    }

    private void ProcessMousePosition()
    {
        NewPosition = _window.MapPixelToCoords(Mouse.GetPosition(_window));
    }
}