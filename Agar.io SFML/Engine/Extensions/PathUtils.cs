namespace Agar.io_SFML;

public static class PathUtils
{
    public static string FontsDirectory => Path.GetFullPath(Directory.GetCurrentDirectory()) + @"\Resources\Fonts\";
    public static string AudioDirectory => Path.GetFullPath(Directory.GetCurrentDirectory()) + @"\Resources\Audio\";
    public static string ConfigurationDirectory => Path.GetFullPath(Directory.GetCurrentDirectory()) + @"\Resources\Configuration\";
    public static string AnimationsDirectory => Path.GetFullPath(Directory.GetCurrentDirectory()) + @"\Resources\Animations\";
    public static string TexturesDirectory => Path.GetFullPath(Directory.GetCurrentDirectory()) + @"\Resources\Textures\";
    public static string SavesDirectory => Path.GetFullPath(Directory.GetCurrentDirectory()) + @"\Resources\Saves\";
}