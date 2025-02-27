using Agar.io_SFML.Engine;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Agar.io_SFML;

public class Button : ShapeActor
{
    public bool CanClick = true;
    
    private Action _onClick;

    public void Initialize(Shape buttonShape)
    {
        base.Initialize(buttonShape);
        
        ButtonBindsSet bindsSet = Dependency.Get<ButtonBindsSet>();
        
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
        if (!CanClick)
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

        var buttonLeft = buttonShape.Position.X - shape.TextureRect.Size.X / 2f;
        var buttonTop = buttonShape.Position.Y - shape.TextureRect.Size.Y / 2f;
        var buttonRight = buttonLeft + buttonShape.TextureRect.Size.X;
        var buttonBottom = buttonTop + buttonShape.TextureRect.Size.Y;

        return mousePosition.X > buttonLeft && mousePosition.X < buttonRight && mousePosition.Y > buttonTop &&
               mousePosition.Y < buttonBottom;
    }
}