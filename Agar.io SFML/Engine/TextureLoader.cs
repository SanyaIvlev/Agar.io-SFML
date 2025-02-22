using SFML.Graphics;

namespace Agar.io_SFML.Animations;

public class TextureLoader
{
    public static TextureLoader Instance;
    
    private static string _animationsDirectory;

    public TextureLoader()
    {
        Instance = this;
        _animationsDirectory = PathUtils.AnimationsDirectory;
    }

    public Texture[] LoadTexturesFrom(string actorTypeDirectory, string animationTypeDirectory)
    {
        return LoadTexturesFrom(actorTypeDirectory, "", animationTypeDirectory);
    }

    public Texture[] LoadTexturesFrom(string actorTypeDirectory, string possibleSkinDirectory, string animationTypeDirectory)
    {
        string folder = _animationsDirectory + @"\" + actorTypeDirectory + @"\" + possibleSkinDirectory + @"\" + animationTypeDirectory;
        
        DirectoryInfo directoryInfo = new DirectoryInfo(folder);
        var sprites = directoryInfo.GetFiles("*.png");
        
        Texture[] textures = new Texture[sprites.Length];

        for (int i = 0; i < textures.Length; i++)
        {
            textures[i] = new Texture(folder + @"\" + sprites[i].Name);
        }
        
        return textures;
    }

    public Texture[] FindAllTexturesInDirectory(string directory)
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(PathUtils.TexturesDirectory + directory);
        var allFiles = directoryInfo.GetFiles();

        Texture[] allFilesTextures = new Texture[allFiles.Length];

        for(int i = 0; i < allFiles.Length; i++)
        {
            var fileInfo = allFiles[i];
            allFilesTextures[i] = new(fileInfo.FullName);
        }
        
        return allFilesTextures;
    }

    public Texture FindTextureByName(string name)
    {
        string[] allFiles = Directory.GetFiles(PathUtils.TexturesDirectory);

        foreach (var file in allFiles)
        {
            if (Path.GetFileNameWithoutExtension(file).Equals(name))
            {
                var fileInfo = new FileInfo(file);
        
                return new Texture(fileInfo.FullName);
            }
        }

        return null;
    }
}