using SFML.Graphics;
using SFML.System;

namespace Agar.io_SFML;

public class Text : Actor, IDrawable, IUpdatable
{
    private SFML.Graphics.Text _message;
    private Camera _camera;
    private Vector2f _offset;

    public void Initialize(Font font, uint characterSize, Color fillColor, Color outlineColor, uint outlineThickness,
        Vector2f offset, Camera camera)
    {
        _message = new("", font)
        {
            CharacterSize = characterSize,
            FillColor = fillColor,
            OutlineColor = outlineColor,
            OutlineThickness = outlineThickness,
        };
        
        _camera = camera;
        
        _offset = offset;
        
        
        base.Initialize(offset + _camera.GetGlobalViewPosition());
        
        SetPosition(_offset);
    }

    public void SetPosition(Vector2f position)
    {
        var floatRect = _message.GetLocalBounds();
        _message.Origin = new Vector2f(floatRect.Left + floatRect.Width / 2, floatRect.Top + floatRect.Height / 2);


        var globalViewPosition = _camera.GetGlobalViewPosition();
        
        _message.Position = globalViewPosition + position;
    }

    public void UpdateText(string newText)
    {
        var currentPosition = _offset;
        
        _message.DisplayedString = newText;
        
        SetPosition(currentPosition);
    }

    public void Update()
    {
        Vector2f globalViewPosition = _camera.GetGlobalViewPosition();

        _message.Position = globalViewPosition + _offset;
    }
    
    public void Draw(RenderWindow window)
    {
        window.Draw(_message);
    }
}