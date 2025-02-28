using Agar.io_SFML.Engine;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Agar.io_SFML;

public class Button : ShapeActor
{
    public bool IsClickable = true;
    
    private Action _onClick;

    public void Initialize(Shape buttonShape)
    {
        base.Initialize(buttonShape);

        ButtonBindsSet bindsSet = Service<ButtonBindsSet>.Get;
        
        MouseInput input = bindsSet.AddMouseBind(Mouse.Button.Left);
        input.AddCallBackOnPressed(OnMouseClick);
    }

    public void AddCallback(Action action)
    {
        _onClick += action;
    }

    public void RemoveCallback(Action action)
    {
        _onClick -= action;
    }

    public void SetPosition(Vector2f position)
    {
        shape.Position = position;
    }

    public Vector2f GetPosition()
        => shape.Position;

    public float GetWidth()
        => shape.TextureRect.Width;
    
    public float GetHeight() 
        => shape.TextureRect.Height;

    private void OnMouseClick()
    {
        if (!IsClickable)
            return;
        
        if (IsHovered())
        {
            _onClick?.Invoke();
        }
    }

    public bool IsHovered()
    {
        Vector2i mousePosition = Mouse.GetPosition(Window);

        var buttonShape = shape;

        var buttonLeft = buttonShape.Position.X;
        var buttonTop = buttonShape.Position.Y;
        var buttonRight = buttonLeft + buttonShape.TextureRect.Size.X;
        var buttonBottom = buttonTop + buttonShape.TextureRect.Size.Y;

        return mousePosition.X > buttonLeft && mousePosition.X < buttonRight && mousePosition.Y > buttonTop &&
               mousePosition.Y < buttonBottom;
    }
}