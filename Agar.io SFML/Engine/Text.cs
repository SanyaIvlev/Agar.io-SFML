using SFML.Graphics;
using SFML.System;

namespace Agar.io_SFML;

public class Text : IDrawable
{
    protected SFML.Graphics.Text _text;
    
    public Text(Font font, uint characterSize, Color fillColor, Color outlineColor, uint outlineThickness, Vector2f position)
    {
        _text = new("", font)
        {
            CharacterSize = characterSize,
            FillColor = fillColor,
            OutlineColor = outlineColor,
            OutlineThickness = outlineThickness,
            Position = position,
        };
    }

    public void Update(string newText)
    {
        _text.DisplayedString = newText;
    }
    
    public void Draw(RenderWindow window)
    {
        window.Draw(_text);
    }
}