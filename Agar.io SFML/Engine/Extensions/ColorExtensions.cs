using SFML.Graphics;

namespace Agar.io_SFML.Extensions;

public static class ColorExtensions
{
    private static Random _random = new();
    
    public static Color GetRandomColor(this Color color)
    {
        byte r = (byte)_random.Next(25, 255);
        byte g = (byte)_random.Next(25, 255);
        byte b = (byte)_random.Next(25, 255);

        return new Color(r, g, b);
    }

    public static Color GetDarkerShade(this Color color)
    {
        byte r = (byte)(color.R - 25);
        byte g = (byte)(color.G - 25);
        byte b = (byte)(color.B - 25);

        return new Color(r, g, b);
    }
}