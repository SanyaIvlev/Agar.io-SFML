﻿using SFML.Graphics;

namespace Agar.io_SFML.Animations;

public class TextureLoader
{
    private string _animationsDirectory;

    public TextureLoader()
    {
        _animationsDirectory = Path.GetFullPath(Directory.GetCurrentDirectory()) + @"\Resources\Animations\";
    }

    public Texture[] LoadTexturesFrom(string actorTypeDirectory, string animationTypeDirectory)
    {
        string folder = _animationsDirectory + @"\" + actorTypeDirectory + @"\" + animationTypeDirectory;
        
        DirectoryInfo directoryInfo = new DirectoryInfo(folder);
        var sprites = directoryInfo.GetFiles("*.png");
        
        Texture[] textures = new Texture[sprites.Length];

        for (int i = 0; i < textures.Length; i++)
        {
            textures[i] = new Texture(folder + @"\" + sprites[i].Name);
        }
        
        return textures;
    } 
}