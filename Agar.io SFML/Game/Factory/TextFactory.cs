using SFML.Graphics;
using SFML.System;

namespace Agar.io_SFML.Factory;

public class TextFactory : ActorFactory
{
    private const string FontName = "Obelix Pro.ttf";
    
    private int _maxWidth => (int)Boot.WindowWidth;
    private int _maxHeight => (int)Boot.WindowHeight;
    
    private Font _font;

    public TextFactory(GameLoop gameLoop) : base (gameLoop)
    {
        _font = new (GetFontLocation(FontName));
    }

    public Text CreateText()
    {
        Text endText = CreateActor<Text>();
        
        endText.Initialize(_font, 50, Color.Black, Color.White, 3, new(_maxWidth / 2f, _maxHeight / 2f));

        return endText;
    }

    public void CreateScoreText(Player mainPlayer)
    {
        Score newScore = CreateActor<Score>();
        
        newScore.Initialize(_font, 25, Color.Black, Color.White, 3, new(Boot.WindowWidth / 2f, 20), mainPlayer);
    }
    
    private string GetFontLocation(string fontName)
        => Path.GetFullPath(@"..\..\..\..\Resources\Fonts\" + fontName);
    
    
}