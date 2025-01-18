using Agar.io_SFML.Extensions;
using SFML.Graphics;
using SFML.System;

namespace Agar.io_SFML;

public class Game
{
    public Action<IUpdatable> OnUpdatableSpawned;
    public Action<IDrawable> OnDrawableSpawned;
    
    public Action<IUpdatable> OnUpdatableDestroyed;
    public Action<IDrawable> OnDrawableDestroyed;
    
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

    private readonly RenderWindow _window;

    private GameMode _gameMode;

    public Game(RenderWindow window, GameMode gameMode)
    {
        _window = window;
        
        _gameMode = gameMode;

        _playersOnStart = 10;
        _foodOnStart = 100;
        
        _passedFoodTime = _passedPlayerTime = 0;
        
        _foodRespawnDelay = 0.5f;
        _playerRespawnDelay = 10f;
    }
    
    
    public void Start(GameLoop gameLoop)
    {
        gameLoop.OnInputProcessed += ProcessAction;
        gameLoop.OnGameUpdateNeeded += Update;
        
        _players = [];
        _food = [];
        _currentRemovingActors = [];
        
        _mainPlayer = SpawnPlayer(true);
        _players.Add(_mainPlayer);
        
        foreach(var _ in Enumerable.Range(0, (int)_foodOnStart))
        {
            Food newFood = SpawnFood();
            _food.Add(newFood);
        }
        
        foreach(var _ in Enumerable.Range(0, (int)_playersOnStart))
        {
            Player bot = SpawnPlayer(false);
            _players.Add(bot);
        }
        
        CreateScore(_mainPlayer);
        _endText = CreateEndText();
    }

    private void ProcessAction()
    {
        foreach (var player in _players)
        {
            player.ProcessAction();
        }
    }

    private Food SpawnFood()
    {
        Vector2f initialPosition = MathExtensions.GetRandomPosition((int)Boot.WindowWidth, (int)Boot.WindowHeight);
        
        Color foodColor = new();
        foodColor = foodColor.GetRandomColor();
        
        var newFood = new Food(initialPosition, foodColor);
        
        OnUpdatableSpawned?.Invoke(newFood);
        OnDrawableSpawned?.Invoke(newFood);
        
        return newFood;
    }

    private Player SpawnPlayer(bool isHuman)
    {
        Vector2f startPosition;
        IActionHandler actionHandler;
        
        if (isHuman)
        {
            actionHandler = new HumanActionHandler(_window);
            startPosition = new (Boot.WindowWidth / 2f, Boot.WindowHeight / 2f);
        }
        else
        {
            actionHandler = new BotActionHandler(_window);
            startPosition = MathExtensions.GetRandomPosition((int)Boot.WindowWidth, (int)Boot.WindowHeight);
        }

        Color playerColor = new();
        playerColor = playerColor.GetRandomColor();

        Player newPlayer = new(actionHandler, isHuman, startPosition, playerColor, _window);
        
        OnUpdatableSpawned?.Invoke(newPlayer);
        OnDrawableSpawned?.Invoke(newPlayer);

        newPlayer.OnDestroyed += UpdateRemovingList;
        
        return newPlayer;
    }

    private void CreateScore(Player mainPlayer)
    {
        const string fontName = "Obelix Pro.ttf";
        Font font = new (GetFontLocation(fontName));
        Score score = new(font, 25, Color.Black, Color.White, 3, new(0,0), mainPlayer);
        
        OnDrawableSpawned?.Invoke(score);
    }
    
    private Text CreateEndText()
    {
        const string fontName = "Obelix Pro.ttf";
        Font font = new (GetFontLocation(fontName));
        Text endText = new(font, 50, Color.Black, Color.White, 3, new(Boot.WindowWidth / 2f, Boot.WindowHeight / 2f));
        
        OnDrawableSpawned?.Invoke(endText);

        return endText;
    }

    private string GetFontLocation(string fontName)
        => Path.GetFullPath(@"..\..\..\..\Resources\Fonts\" + fontName);

    private void Update()
    {
        _passedFoodTime += Time.GetElapsedTimeAsSeconds();
        _passedPlayerTime += Time.GetElapsedTimeAsSeconds();
        
        if (_passedFoodTime >= _foodRespawnDelay)
        {
            Food newFood = SpawnFood();
            _food.Add(newFood);
            
            _passedFoodTime = 0;
        }
        
        if(_passedPlayerTime >= _playerRespawnDelay)
        {
            if (_players.Count == 1 && _players[0] == _mainPlayer)
            {
                _endText.SetText("You Win!");
                _gameMode.IsGameEnded = true;
                return;
            }
            
            Player newPlayer = SpawnPlayer(false);
            _players.Add(newPlayer);
            
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
            _endText.SetText("You lose!");
            _gameMode.IsGameEnded = true;
        }

        _players.SwapRemove(actor as Player);
        _food.SwapRemove(actor as Food);
        
        OnUpdatableDestroyed?.Invoke(actor);
        OnDrawableDestroyed?.Invoke(actor);
    }

}