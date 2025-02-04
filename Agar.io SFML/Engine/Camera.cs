using SFML.Graphics;
using SFML.System;

namespace Agar.io_SFML;

public class Camera
{
    public View view { get; private set; }

    private Actor _focusedActor;
    private RenderTarget _target;

    public Camera(RenderTarget renderTarget, FloatRect floatRect)
    {
        _target = renderTarget;

        view = new(floatRect);
    }

    public void FocusOn(Actor actor)
    {
        _focusedActor = actor;
    }

    public void Update()
    {
        view.Center = _focusedActor.Position;
        
        _target.SetView(view);
    }

    public Vector2f GetGlobalViewPosition()
    {
        var position = new Vector2f(_focusedActor.Position.X - view.Size.X / 2f, _focusedActor.Position.Y - view.Size.Y / 2f);
        return position;
    }
}