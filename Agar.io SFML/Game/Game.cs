using Agar.io_SFML.Configs;
using Agar.io_SFML.Extensions;
using Agar.io_SFML.Factory;
using SFML.Graphics;
using SFML.Window;

#pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.

namespace Agar.io_SFML;

public class Game
{
    private Text _endText;

    private AgarioController _mainController;
    
    private List<AgarioController> _controllers;
    private List<Food> _food;
    
    private float _passedFoodTime;
    private float _passedPlayerTime;
    
    private List<EatableActor> _currentRemovingActors;

    private readonly GameMode _gameMode;
    
    private EatableActorFactory _eatableActorFactory;
    private TextFactory _textFactory;
    private ShapeFactory _shapeFactory;
    
    private KeyInputSet _keyInputs;
    
    private int _playersOnStart;
    private int _foodOnStart;
    
    private float _foodRespawnDelay;
    private float _playerRespawnDelay;

    private Camera _camera;
    
    
    public Game(GameMode gameMode, KeyInputSet keyInputSet, Camera camera)
    {
        _gameMode = gameMode;
        
        _passedFoodTime = _passedPlayerTime = 0;
        
        _keyInputs = keyInputSet;
        
        _camera = camera;
    }
    
    
    public void Start(GameLoop gameLoop, RenderWindow window)
    {
        gameLoop.OnGameUpdateNeeded += Update;
        
        _eatableActorFactory = new(window, gameLoop);
        _textFactory = new(gameLoop, _camera);
        _shapeFactory = new(window, gameLoop);
        
        _eatableActorFactory.SetPlayerDeathResponse(UpdateRemovingList);
        
        _playersOnStart = GameConfig.PlayersOnStart;
        _foodOnStart = GameConfig.FoodOnStart;
        
        _playerRespawnDelay = GameConfig.PlayerRespawnDelay;
        _foodRespawnDelay = GameConfig.FoodRespawnDelay;
        
        _controllers = [];
        _food = [];
        _currentRemovingActors = [];

        InitializeKeyInputs();
        
        SetActors();
    }

    private void SetActors()
    {
        _shapeFactory.CreateBackground();
        
        _mainController = SpawnController(true);
        
        _camera.FocusOn(_mainController.PlayerPawn);

        foreach(var _ in Enumerable.Range(0, _foodOnStart))
        {
            SpawnFood();
        }
        
        foreach(var _ in Enumerable.Range(0, _playersOnStart))
        {
            SpawnController(false);
        }

        _textFactory.CreateScoreText(_mainController.PlayerPawn);

        _endText = _textFactory.CreateText();
    }

    private void InitializeKeyInputs()
    {
        KeyInput swapBind = _keyInputs.AddKeyBind(Keyboard.Key.F);

        swapBind.AddCallBackOnPressed(Swap);
    }

    private AgarioController SpawnController(bool isHuman)
    {
        AgarioController controller = _eatableActorFactory.CreateController(isHuman);
        
        _controllers.Add(controller);

        return controller;
    }

    private void SpawnFood()
    {
        Food newFood = _eatableActorFactory.CreateFood();
        _food.Add(newFood);
    }

    private void Update()
    {
        if (_controllers.Count == 1 && _controllers[0] == _mainController)
        {
            EndGameWithText("You win!");
            return;
        }
        
        _passedFoodTime += Time.GetElapsedTimeAsSeconds();
        _passedPlayerTime += Time.GetElapsedTimeAsSeconds();
        
        if (_passedFoodTime >= _foodRespawnDelay)
        {
            SpawnFood();
            
            _passedFoodTime = 0;
        }
        
        if(_passedPlayerTime >= _playerRespawnDelay)
        {
           SpawnController(false);
            
            _passedPlayerTime = 0;
        }

        CheckPlayersIntersections();
        CleanRemovingList();
    }

    private void CheckPlayersIntersections()
    {
        
        foreach (var currentController in _controllers)
        {
            Player currentPlayer = currentController.PlayerPawn;
            
            foreach (var anotherController in _controllers)
            {
                Player anotherPlayer = anotherController.PlayerPawn;
                
                if (currentPlayer != anotherPlayer)
                {
                    currentPlayer.CheckIntersectionWith(anotherPlayer);
                }
            }

            foreach(var food in _food)
            {
                currentPlayer.CheckIntersectionWith(food);
            }
        }
    }

    private void Swap()
    {
        Controller closestController = _controllers.FindNearestController(_mainController);
        
        _mainController.SwapWith(closestController);
        
        _camera.FocusOn(_mainController.PlayerPawn);
    }

    private void UpdateRemovingList(EatableActor actor)
    {
        _currentRemovingActors.Add(actor);
    }

    private void CleanRemovingList()
    {
        for (int i = _currentRemovingActors.Count - 1; i >= 0; i--)
        {
            RemoveActor(_currentRemovingActors[i]);
        }
    }
    
    private void RemoveActor(Actor actor)
    {
        if (actor is Player player)
        {
            foreach (var controller in _controllers)
            {
                if (player == controller.PlayerPawn)
                {
                    _controllers.SwapRemove(controller);
                    
                    _eatableActorFactory.Destroy(controller);
                    
                    if (controller == _mainController)
                    {
                        EndGameWithText("You lose!");
                    }

                    return;
                }
            }
        }
        
        _food.SwapRemove(actor as Food);
        
        _eatableActorFactory.Destroy(actor);
    }

    private void EndGameWithText(string message)
    {
        _endText.UpdateText(message);
        _gameMode.IsGameEnded = true;
    }

}