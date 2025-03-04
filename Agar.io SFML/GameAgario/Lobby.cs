using Agar.io_SFML.Animations;
using Agar.io_SFML.Engine;
using Agar.io_SFML.Engine.Scene;
using Agar.io_SFML.Factory;
using SFML.Graphics;
using SFML.System;

namespace Agar.io_SFML;

public class Lobby : Scene
{
    private ShapeFactory _shapeFactory;
    private AnimatorFactory _animatorFactory;
    
    private Button _playButton;
    private Button _leftArrowButton;
    private Button _rightArrowButton;
    
    private ShapeActor[] _allDisplayableSkins;
    private ShapeActor _currentDisplayableSkin;
    
    private HumanSkins _currentHumanSkin;
    
    private int _currentSkinIndex;

    public Lobby()
    {
       _shapeFactory = new ShapeFactory();
    }

    public override void Start()
    {
        _animatorFactory = new AnimatorFactory();
        
        _playButton = _shapeFactory.CreateButton("PlayButton");
        _leftArrowButton = _shapeFactory.CreateButton("LeftArrowButton");
        _rightArrowButton = _shapeFactory.CreateButton("RightArrowButton");
        
        LoadSkins();
        
        _rightArrowButton.AddCallback(() => SwitchSkin(+1));
        _leftArrowButton.AddCallback(() => SwitchSkin(-1));
        _playButton.AddCallback(StartGamePlay);
        
        
        RenderWindow renderWindow = Service<RenderWindow>.Get;

        _rightArrowButton.SetPosition(new Vector2f(renderWindow.Size.X / 2f + _playButton.GetWidth() / 2f + 200, 3f / 4 * renderWindow.Size.Y));
        _leftArrowButton.SetPosition(new Vector2f(renderWindow.Size.X / 2f - _playButton.GetWidth() / 2f - 200, 3f / 4 * renderWindow.Size.Y));
        _playButton.SetPosition(new Vector2f(renderWindow.Size.X / 2f, 3f / 4 * renderWindow.Size.Y));
    }

    private void StartGamePlay()
    {
        _animatorFactory.SetPlayerSkin(_currentHumanSkin);
        
        SceneHandler sceneHandler = Service<SceneHandler>.Get; sceneHandler.SelectScene<AgarioGame>();
    }

    private void LoadSkins()
    {
        TextureLoader textureLoader = TextureLoader.Instance;

        Texture[] _allSkinsTextures = textureLoader.FindAllTexturesInDirectory("HumanSkins");
        _allDisplayableSkins = new ShapeActor[_allSkinsTextures.Length];
        
        for(int i = 0; i < _allSkinsTextures.Length; i++)
        {
            _allDisplayableSkins[i] = _shapeFactory.CreateSkinShape(_allSkinsTextures[i]);
        }
        
        _currentDisplayableSkin = _allDisplayableSkins[0];
        
        _currentSkinIndex = 0;
        _currentHumanSkin = (HumanSkins)_currentSkinIndex;
        _currentDisplayableSkin.IsActive = true;
    }

    private void SwitchSkin(int nextOrPrevious)
    {
        _currentDisplayableSkin.IsActive = false;
        
        _currentSkinIndex += nextOrPrevious;
        
        if(_currentSkinIndex >= _allDisplayableSkins.Length)
            _currentSkinIndex = 0;
        
        if(_currentSkinIndex < 0)
            _currentSkinIndex = _allDisplayableSkins.Length - 1;
        
        _currentDisplayableSkin = _allDisplayableSkins[_currentSkinIndex];
        
        _currentHumanSkin = (HumanSkins)_currentSkinIndex;
        
        _currentDisplayableSkin.IsActive = true;
        
        _animatorFactory.SetPlayerSkin(_currentHumanSkin);
        
        SceneHandler sceneHandler = Service<SceneHandler>.Get; sceneHandler.SelectScene<AgarioGame>();
    }
}