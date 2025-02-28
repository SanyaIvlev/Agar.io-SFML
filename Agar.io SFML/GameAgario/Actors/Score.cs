using SFML.Graphics;
using SFML.System;

namespace Agar.io_SFML;

public class Score : Text
{
    public void Initialize(Font font,uint characterSize, Color fillColor, Color outlineColor, uint outlineThickness, Vector2f offset, Player mainPlayer, Camera camera)
    {
        base.Initialize(font, characterSize, fillColor, outlineColor, outlineThickness, offset, camera);
        
        UpdateScore(mainPlayer.Bounty);
        mainPlayer.OnBountyChanged += UpdateScore;
    }

    private void UpdateScore(uint newScore)
    {
        UpdateText("Score : " + newScore);
    } 
}