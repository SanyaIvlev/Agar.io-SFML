using Agar.io_SFML.Configs;
using SFML.Graphics;
using SFML.System;

namespace Agar.io_SFML.Factory;

public class TextFactory : ActorFactory
{
    private const string FontName = "Obelix Pro.ttf";
    
    private Font _font;
    
    private readonly int _windowWidth;
    private readonly int _windowHeight;

    public TextFactory(GameLoop gameLoop) : base (gameLoop)
    {
        _font = new (GetFontLocation(FontName));
        
        _windowWidth = WindowConfig.WindowWidth;
        _windowHeight = WindowConfig.WindowHeight;
    }

    public Text CreateText()
    {
        Text endText = CreateActor<Text>();
        
        endText.Initialize(_font, 50, Color.Black, Color.White, 3, new(_windowWidth / 2f, _windowHeight / 2f));

        return endText;
    }

    public void CreateScoreText(Player mainPlayer)
    {
        Score newScore = CreateActor<Score>();
        
        newScore.Initialize(_font, 25, Color.Black, Color.White, 3, new(_windowWidth / 2f, 20), mainPlayer);
    }

    private string GetFontLocation(string fontName)
        => fontName;


}