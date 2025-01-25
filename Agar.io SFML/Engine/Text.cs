using SFML.Graphics;
using SFML.System;

namespace Agar.io_SFML;

public class Text : Actor
{
    private SFML.Graphics.Text Message;

    public void Initialize(Font font, uint characterSize, Color fillColor, Color outlineColor, uint outlineThickness,
        Vector2f position)
    {
        base.Initialize(position);
        
        Message = new("", font)
        {
            CharacterSize = characterSize,
            FillColor = fillColor,
            OutlineColor = outlineColor,
            OutlineThickness = outlineThickness,
        };
        
        SetPosition(position);
    }

    public void SetPosition(Vector2f position)
    {
        var floatRect = Message.GetLocalBounds();
        Message.Origin = new Vector2f(floatRect.Left + floatRect.Width/ 2, floatRect.Top + floatRect.Height / 2);
        
        Message.Position = position;
    }

    public void UpdateText(string newText)
    {
        var currentPosition = Message.Position;
        
        Message.DisplayedString = newText;
        
        SetPosition(currentPosition);
    }
    
    public override void Draw(RenderWindow window)
    {
        window.Draw(Message);
    }
}