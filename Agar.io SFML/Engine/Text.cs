using SFML.Graphics;
using SFML.System;

namespace Agar.io_SFML;

public class Text : Actor
{
    protected SFML.Graphics.Text Message;
    
    public Text(Font font, uint characterSize, Color fillColor, Color outlineColor, uint outlineThickness, Vector2f position) : base(position)
    {
        Message = new("", font)
        {
            CharacterSize = characterSize,
            FillColor = fillColor,
            OutlineColor = outlineColor,
            OutlineThickness = outlineThickness,
            Position = position,
        };
    }

    public void SetText(string newText)
    {
        Message.DisplayedString = newText; 
    }
    
    public override void Draw(RenderWindow window)
    {
        window.Draw(Message);
    }
}