using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Agar.io_SFML;

public class Button : ShapeActor, IUpdatable
{
    public Action OnClick;

    private bool _isClicked;
    private bool _wasClicked;

    public void AddCallback(Action action)
    {
        OnClick += action;
    }

    public void RemoveCallback(Action action)
    {
        OnClick -= action;
    }

    public void Update()
    {
        _wasClicked = _isClicked;
        _isClicked = Mouse.IsButtonPressed(Mouse.Button.Left);
        
        if (_isClicked && !_wasClicked && MouseIsInsideShape())
        {
            OnClick?.Invoke();
        }
    }

    private bool MouseIsInsideShape()
    {
        Vector2i mousePosition = Mouse.GetPosition(Window);

        var buttonShape = shape;

        var buttonLeft = buttonShape.Position.X - shape.TextureRect.Size.X / 2f;
        var buttonTop = buttonShape.Position.Y - shape.TextureRect.Size.Y / 2f;
        var buttonRight = buttonLeft + buttonShape.TextureRect.Size.X;
        var buttonBottom = buttonTop + buttonShape.TextureRect.Size.Y;

        return mousePosition.X > buttonLeft && mousePosition.X < buttonRight && mousePosition.Y > buttonTop &&
               mousePosition.Y < buttonBottom;
    }
}