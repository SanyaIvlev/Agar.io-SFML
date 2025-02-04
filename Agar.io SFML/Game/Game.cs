using Agar.io_SFML.Configs;
using Agar.io_SFML.Extensions;
using Agar.io_SFML.Factory;
using SFML.Window;

#pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.

namespace Agar.io_SFML;

public class Game
{
    private Text _endText;

    private Controller _mainController;
    
    private List<Controller> _controllers;
    private List<Food> _food;
    
    private float _passedFoodTime;
    private float _passedPlayerTime;
    
    private List<EatableActor> _currentRemovingActors;

    private readonly GameMode _gameMode;
    
    private readonly EatableActorFactory _eatableActorFactory;
    private readonly TextFactory _textFactory;
    
    private KeyInputSet _keyInputs;

    private int _windowWidth;
    private int _windowHeight;
    
    public int _playersOnStart;
    public int _foodOnStart;
    
    public float _foodRespawnDelay;
    public float _playerRespawnDelay;
    public Game(GameMode gameMode, KeyInputSet keyInputSet, EatableActorFactory eatableActorFactory, TextFactory textFactory)
    {
        _gameMode = gameMode;
        
        _eatableActorFactory = eatableActorFactory;
        _textFactory = textFactory;
        
        _passedFoodTime = _passedPlayerTime = 0;
        
        _keyInputs = keyInputSet;
    }
    
    
    public void Start(GameLoop gameLoop)
    {
        gameLoop.OnGameUpdateNeeded += Update;
        
        _eatableActorFactory.SetPlayerDeathResponse(UpdateRemovingList);
        
        _windowWidth = WindowConfig.WindowWidth;
        _windowHeight = WindowConfig.WindowHeight;
        
        _playersOnStart = GameConfig.PlayersOnStart;
        _foodOnStart = GameConfig.FoodOnStart;
        
        _playerRespawnDelay = GameConfig.PlayerRespawnDelay;
        _foodRespawnDelay = GameConfig.FoodRespawnDelay;
        
        _controllers = [];
        _food = [];
        _currentRemovingActors = [];

        _mainController = SpawnController(true);
        
        InitializeKeyInputs();

        foreach(var _ in Enumerable.Range(0, _foodOnStart))
        {
            SpawnFood();
        }
        
        foreach(var _ in Enumerable.Range(0, _playersOnStart))
        {
            SpawnController(false);
        }

        if (_mainController.Pawn is Player player)
        {
            _textFactory.CreateScoreText(player);
        }

        _endText = _textFactory.CreateText();
    }

    private void InitializeKeyInputs()
    {
        KeyInput swapBind = _keyInputs.AddKeyBind(Keyboard.Key.F);

        swapBind.AddCallBackOnPressed(Swap);
    }

    private Controller SpawnController(bool isHuman)
    {
        Controller controller = _eatableActorFactory.CreateController(isHuman);
        
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
            Player currentPlayer = currentController.Pawn as Player;
            
            if(currentPlayer == null)
                continue;
            
            foreach (var anotherController in _controllers)
            {
                Player anotherPlayer = anotherController.Pawn as Player;
                
                if(anotherPlayer == null) 
                    continue;
                
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
                if (player == controller.Pawn)
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
        _endText.SetPosition(new (_windowWidth / 2f, _windowHeight / 2f));
        _gameMode.IsGameEnded = true;
    }

}