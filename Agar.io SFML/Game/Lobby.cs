using Agar.io_SFML.Animations;
using Agar.io_SFML.Factory;
using SFML.Graphics;
using SFML.System;

namespace Agar.io_SFML;

public class Lobby
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
    private RenderWindow _renderWindow;

    private List<Actor> _actorsToDestroyOnEnd;

    public Lobby(RenderWindow window)
    {
        _actorsToDestroyOnEnd = new List<Actor>();
        
        _renderWindow = window;
       _shapeFactory = new ShapeFactory(window);
       
       _playButton = _shapeFactory.CreateButton("PlayButton");
       _leftArrowButton = _shapeFactory.CreateButton("LeftArrowButton");
       _rightArrowButton = _shapeFactory.CreateButton("RightArrowButton");
    }

    public void Start(AnimatorFactory animatorFactory)
    {
        _animatorFactory = animatorFactory;
        
        LoadSkins();
        
        _rightArrowButton.AddCallback(() => SwitchSkin(+1));
        _leftArrowButton.AddCallback(() => SwitchSkin(-1));
        _playButton.AddCallback(StartGamePlay);

        _rightArrowButton.SetPosition(new Vector2f(_renderWindow.Size.X / 2f + _playButton.GetWidth() / 2f + 200, 3f / 4 * _renderWindow.Size.Y));
        _leftArrowButton.SetPosition(new Vector2f(_renderWindow.Size.X / 2f - _playButton.GetWidth() / 2f - 200, 3f / 4 * _renderWindow.Size.Y));
        _playButton.SetPosition(new Vector2f(_renderWindow.Size.X / 2f, 3f / 4 * _renderWindow.Size.Y));
    }

    public void Update()
    {
        if (_actorsToDestroyOnEnd.Count > 0)
        {
            foreach (var actorToDestroy in _actorsToDestroyOnEnd)
            {
                _shapeFactory.Destroy(actorToDestroy);
            }
            
            Boot.Instance.StartGameLoop();
        }
    }

    private void StartGamePlay()
    {
        _animatorFactory.SetPlayerSkin(_currentHumanSkin);
        
        _playButton.RemoveCallback(StartGamePlay);
        _leftArrowButton.RemoveCallback(() => SwitchSkin(-1));
        _rightArrowButton.RemoveCallback(() => SwitchSkin(+1));
        
        _actorsToDestroyOnEnd.AddRange(_allDisplayableSkins);
        _actorsToDestroyOnEnd.AddRange(new [] {_playButton, _leftArrowButton, _rightArrowButton});
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
        _currentDisplayableSkin.IsVisible = true;
    }

    private void SwitchSkin(int nextOrPrevious)
    {
        _currentDisplayableSkin.IsVisible = false;
        
        _currentSkinIndex += nextOrPrevious;
        
        if(_currentSkinIndex >= _allDisplayableSkins.Length)
            _currentSkinIndex = 0;
        
        if(_currentSkinIndex < 0)
            _currentSkinIndex = _allDisplayableSkins.Length - 1;
        
        _currentDisplayableSkin = _allDisplayableSkins[_currentSkinIndex];
        
        _currentHumanSkin = (HumanSkins)_currentSkinIndex;
        
        _currentDisplayableSkin.IsVisible = true;
    }
    
}