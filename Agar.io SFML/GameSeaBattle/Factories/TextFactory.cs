using Agar.io_SFML.Configs;
using Agar.io_SFML.Engine;
using Agar.io_SFML.GameSeaBattle.Actors;
using SFML.Graphics;
using SFML.System;

namespace Agar.io_SFML.GameSeaBattle;

public class TextFactory : ActorFactory
{
    private Font _font;

    private int _windowWidth;
    private int _windowHeight;

    private int _fieldWidth;

    public TextFactory()
    {
        Service<TextFactory>.Set(this);
        
        string fontPath = PathUtils.FontsDirectory + "Obelix Pro.ttf";
        _font = new Font(fontPath);

        _windowWidth = SeaBattleWindowConfig.WindowWidth;
        _windowHeight = SeaBattleWindowConfig.WindowHeight;

        _fieldWidth = SeaBattleFieldConfig.Width;
    }

    public Text CreateText()
    {
        Text text = CreateActor<Text>();
        
        text.Initialize(_font, 25, Color.White, Color.Black, 3, new Vector2f(_windowWidth / 2f, _windowHeight / 2f));
        
        return text;
    }
    
    public void CreateScore(Actors.Player player)
    {
        Actors.Score score = CreateActor<Actors.Score>();
        
        var playerField = player.field;

        var leftTopCell = playerField.GetCell(_fieldWidth / 2,0);

        var vector2F = leftTopCell.GetPosition();
        Vector2f position = vector2F - new Vector2f(0, 30);
        score.Initialize(_font, 25, Color.White, Color.Black, 3, position, player);
    }

    public void CreateFieldName(Actors.Player player)
    {
        Text text = CreateActor<Text>();
        
        var playerField = player.field;

        var leftTopCell = playerField.GetCell(_fieldWidth / 2,0);

        var vector2F = leftTopCell.GetPosition();
        Vector2f position = vector2F - new Vector2f(0, 60);
        text.Initialize(_font, 25, Color.White, Color.Black, 3, position);
        
        text.UpdateText(player.Name);
    }
}