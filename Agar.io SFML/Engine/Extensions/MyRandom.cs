namespace Agar.io_SFML;

public class MyRandom
{
    private static Random _random = new();

    public static int Next(int minValue, int maxValue)
    {
        return _random.Next(minValue, maxValue);
    }
}