using SFML.Graphics;
using SFML.System;

namespace Agar.io_SFML;

public class Score : IDrawable
{
    private Text _text;

    public Score(Font font, Player mainPlayer)
    {
        _text = new("Score : " + mainPlayer.Bounty, font)
        {
            CharacterSize = 25,
            FillColor = Color.Black,
            OutlineColor = Color.White,
            OutlineThickness = 2,
            Position = new Vector2f(3, 3),
        };
        
        mainPlayer.OnBountyChanged += UpdateScore;
    }

    private void UpdateScore(uint newScore)
    {
        _text.DisplayedString = "Score : " + newScore;
    } 
    
    public void Draw(RenderWindow window)
    {
        window.Draw(_text);
    }
}