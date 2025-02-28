using Agar.io_SFML.Engine;
using Agar.io_SFML.Engine;
using SFML.Graphics;
using SFML.System;

namespace Agar.io_SFML;

public class Camera
{
    public View view { get; private set; }

    private Actor _focusedActor;
    private RenderTarget _target;

    public Camera(RenderTarget renderTarget)
    {
        Dependency.Register(this);
        
        _target = renderTarget;

        view = new(new FloatRect(0,0,_target.Size.X, _target.Size.Y));
    }

    public void ZoomViewport(float zoom)
    {
        var sizeY = _target.Size.Y;
        var sizeX = _target.Size.X;
        
        view = new(new FloatRect(sizeX / zoom, sizeY / zoom, sizeX - sizeX / 4f,
            sizeY - sizeY / 4f));
    }

    public void FocusOn(Actor actor)
    {
        _focusedActor = actor;
    }

    public void Update()
    {
        if (_focusedActor != null)
            view.Center = _focusedActor.Position;
        
        _target.SetView(view);
    }

    public Vector2f GetGlobalViewPosition()
    {
        var position = new Vector2f(_focusedActor.Position.X - view.Size.X / 2f, _focusedActor.Position.Y - view.Size.Y / 2f);
        return position;
    }

    public void Destroy()
    {
        Dependency.Unregister(this);
    }
}