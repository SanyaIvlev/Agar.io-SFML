using SFML.Graphics;

namespace Agar.io_SFML.Factory;

public class TextFactory
{
    private const string FontName = "Obelix Pro.ttf";

    public GameLoop _gameLoop;
    
    private int _maxWidth => (int)Boot.WindowWidth;
    private int _maxHeight => (int)Boot.WindowHeight;
    
    private Font _font;

    public TextFactory(GameLoop gameLoop)
    {
        _gameLoop = gameLoop;
        
        _font = new (GetFontLocation(FontName));
    }

    public Text CreateText()
    {
        Text endText = new(_font, 50, Color.Black, Color.White, 3, new(_maxWidth / 2f, _maxHeight / 2f));

        Register(endText);

        return endText;
    }

    public void CreateScoreText(Player mainPlayer)
    {
        Score score = new(_font, 25, Color.Black, Color.White, 3, new(0,0), mainPlayer);
        
        Register(score);
    }

    private void Register(Actor text)
    {
        _gameLoop.AddDrawable(text);
    }
    
    private string GetFontLocation(string fontName)
        => Path.GetFullPath(@"..\..\..\..\Resources\Fonts\" + fontName);
    
    
}