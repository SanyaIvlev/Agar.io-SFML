using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Agar.io_SFML;

public class AgarioPlayerController : Controller
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