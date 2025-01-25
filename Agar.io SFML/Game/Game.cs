using Agar.io_SFML.Extensions;
using Agar.io_SFML.Factory;
using SFML.Window;

#pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.

namespace Agar.io_SFML;

public class Game
{
    private readonly uint _playersOnStart;
    private readonly uint _foodOnStart;

    private Text _endText;

    private Player _mainPlayer;
    
    private List<Player> _players;
    private List<Food> _food;

    private readonly float _foodRespawnDelay;
    private readonly float _playerRespawnDelay;
    
    private float _passedFoodTime;
    private float _passedPlayerTime;
    
    private List<EatableActor> _currentRemovingActors;

    private readonly GameMode _gameMode;
    
    private readonly EatableActorFactory _eatableActorFactory;
    private readonly TextFactory _textFactory;
    
    private KeyInputSet _keyInputs;

    private string _swapInputName = "Swap";

    public Game(GameMode gameMode, KeyInputSet keyInputSet, EatableActorFactory eatableActorFactory, TextFactory textFactory)
    {
        _gameMode = gameMode;
        
        _eatableActorFactory = eatableActorFactory;
        _textFactory = textFactory;

        _playersOnStart = 10;
        _foodOnStart = 100;
        
        _passedFoodTime = _passedPlayerTime = 0;
        
        _foodRespawnDelay = 0.5f;
        _playerRespawnDelay = 10f;
        
        _keyInputs = keyInputSet;
    }
    
    
    public void Start(GameLoop gameLoop)
    {
        gameLoop.OnInputProcessed += ProcessAction;
        gameLoop.OnGameUpdateNeeded += Update;
        
        _players = [];
        _food = [];
        _currentRemovingActors = [];

        _mainPlayer = SpawnPlayer(true);
        
        _keyInputs.AddKeyBind(_swapInputName, Keyboard.Key.F);
        
        var swapBind = _keyInputs.GetKeyBindByName(_swapInputName);
        swapBind.OnPressed += Swap;
        
        foreach(var _ in Enumerable.Range(0, (int)_foodOnStart))
        {
            SpawnFood();
        }
        
        foreach(var _ in Enumerable.Range(0, (int)_playersOnStart))
        {
            SpawnPlayer(false);
        }
        
        _textFactory.CreateScoreText(_mainPlayer);
        _endText = _textFactory.CreateText();
    }

    private Player SpawnPlayer(bool isHuman)
    {
        Player player = _eatableActorFactory.CreatePlayer(isHuman);
        
        player.OnDestroyed += UpdateRemovingList;
        _players.Add(player);

        return player;
    }

    private void SpawnFood()
    {
        Food newFood = _eatableActorFactory.CreateFood();
        _food.Add(newFood);
    }

    private void ProcessAction()
    {
        foreach (var player in _players)
        {
            player.ProcessAction();
        }
    }

    private void Update()
    {
        if (_players.Count == 1 && _players[0] == _mainPlayer)
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
           SpawnPlayer(false);
            
            _passedPlayerTime = 0;
        }

        CheckPlayersIntersections();
        CleanRemovingList();
    }

    private void CheckPlayersIntersections()
    {
        foreach (var currentPlayer in _players)
        {
            foreach (var anotherPlayer in _players)
            {
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
        Player closestPlayer = _players.FindNearestPlayer(_mainPlayer);
        
        _mainPlayer.SwapWith(closestPlayer);
        
        _mainPlayer = closestPlayer;
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
    
    private void RemoveActor(EatableActor actor)
    {
        if (actor == _mainPlayer)
        {
            EndGameWithText("You lose!");
        }

        _players.SwapRemove(actor as Player);
        _food.SwapRemove(actor as Food);

        _eatableActorFactory.Unregister(actor);
    }

    private void EndGameWithText(string message)
    {
        _endText.UpdateText(message);
        _endText.SetPosition(new ((int)Boot.WindowWidth / 2f, (int)Boot.WindowHeight / 2f));
        _gameMode.IsGameEnded = true;
    }

}