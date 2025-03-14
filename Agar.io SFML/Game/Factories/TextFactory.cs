﻿using Agar.io_SFML.Configs;
using SFML.Graphics;
using SFML.System;

namespace Agar.io_SFML.Factory;

public class TextFactory : ActorFactory
{
    private const string FontName = @"\Obelix Pro.ttf";
    
    private Font _font;
    
    private readonly Camera _camera;
    
    private View _cameraView;

    public TextFactory(Camera camera)
    {
        _font = new (PathUtils.FontsDirectory + FontName);
        
        _camera = camera;
        _cameraView = _camera.view;
    }
    
    public Text CreateText()
    {
        Text endText = CreateActor<Text>();
        
        endText.Initialize(_font, 50, Color.Black, Color.White, 3, new(_cameraView.Size.X / 2f, _cameraView.Size.Y / 2f), _camera);

        return endText;
    }

    public Text CreateText(int width, int height)
    {
        Text endText = CreateActor<Text>();
        
        endText.Initialize(_font, 50, Color.Black, Color.White, 3, new(width, height), _camera);

        return endText;
    }

    public void CreateScoreText(Player mainPlayer)
    {
        Score newScore = CreateActor<Score>();
        
        newScore.Initialize(_font, 25, Color.Black, Color.White, 3, new(_cameraView.Size.X / 2f, 20), mainPlayer, _camera);
    }
}