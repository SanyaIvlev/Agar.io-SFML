using SFML.Graphics;
using SFML.System;

namespace Agar.io_SFML;

public class Score : Text
{
    public Score(Font font,uint characterSize, Color fillColor, Color outlineColor, uint outlineThickness, Vector2f position, Player mainPlayer)
        : base(font, characterSize, fillColor, outlineColor, outlineThickness, position)
    {
        UpdateScore(mainPlayer.Bounty);
        mainPlayer.OnBountyChanged += UpdateScore;
    }

    private void UpdateScore(uint newScore)
    {
        UpdateText("Score : " + newScore);
    } 
}