using SFML.Graphics;
using SFML.System;

namespace Agar.io_SFML.GameSeaBattle.Actors;

public class Score : Text
{
    public void Initialize(Font font,uint characterSize, Color fillColor, Color outlineColor, uint outlineThickness, Vector2f offset, Player player)
    {
        base.Initialize(font, characterSize, fillColor, outlineColor, outlineThickness, offset);
        
        UpdateScore(player.OpponentShipsDestroyed);
        player.OnDestroyedShipsChanged += UpdateScore;
    }

    private void UpdateScore(uint newScore)
    {
        UpdateText("Opponent's ships destroyed : " + newScore);
    } 
}