using SFML.Graphics;
using SFML.System;

namespace Agar.io_SFML.GameSeaBattle;

public class TextFactory : ActorFactory
{
    private Font _font;

    public TextFactory()
    {
        string fontPath = PathUtils.FontsDirectory + "Obelix Pro.ttf";
        _font = new Font(fontPath);
    }
    
    public void CreateScore(Actors.Player player)
    {
        Actors.Score score = CreateActor<Actors.Score>();
        
        var playerField = player.field;

        var leftTopCell = playerField.GetCell(5,0);

        var vector2F = leftTopCell.GetPosition();
        Vector2f position = vector2F - new Vector2f(0, 50);
        score.Initialize(_font, 25, Color.White, Color.Black, 3, position, player);
    }
}