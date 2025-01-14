using Microsoft.VisualBasic.CompilerServices;
using SFML.Graphics;
using SFML.System;

namespace Agar.io_SFML;

public class Game
{
    public Action<IUpdatable> OnUpdatableSpawned;
    public Action<IDrawable> OnDrawableSpawned;
    
    public Action<IUpdatable> OnUpdatableDestroyed;
    public Action<IDrawable> OnDrawableDestroyed;
    
    private uint _playersOnStart;
    private uint _foodOnStart;

    private Text _endText;

    private Player _mainPlayer;
    
    private List<Player> _players;
    private List<Food> _food;
    
    private Random _random;

    private float _foodRespawnDelay;
    private float _playerRespawnDelay;
    
    private float _passedFoodTime;
    private float _passedPlayerTime;
    
    private List<Actor> _currentRemovingActors;

    private RenderWindow _window;

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
        
        _players = new ();
        _food = new();
        _currentRemovingActors = new();
        
        _random = new Random();
        

        _mainPlayer = SpawnPlayer(true, Color.Blue);
        _players.Add(_mainPlayer);
        
        foreach(var _ in Enumerable.Range(0, (int)_foodOnStart))
        {
            Food newFood = SpawnFood();
            _food.Add(newFood);
        }
        
        foreach(var _ in Enumerable.Range(0, (int)_playersOnStart))
        {
            Player bot = SpawnPlayer(false, Color.Red);
            _players.Add(bot);
        }
        
        CreateScore(_mainPlayer);
        _endText = CreateEndText();
    }

    public void ProcessAction()
    {
        foreach (var player in _players)
        {
            player.ProcessAction();
        }
    }

    private Food SpawnFood()
    {
        Vector2f initialPosition = GetRandomPosition();
        Color color = GetRandomColor();
        
        var newFood = new Food(initialPosition, color);
        
        OnUpdatableSpawned?.Invoke(newFood);
        OnDrawableSpawned?.Invoke(newFood);
        
        return newFood;
    }
    
    private Vector2f GetRandomPosition()
    {
        int x = _random.Next(0, (int)Boot.WINDOW_WIDTH);
        int y = _random.Next(0, (int)Boot.WINDOW_HEIGHT);
        
        return new (x, y);
    }

    private Color GetRandomColor()
    {
        int generatedColor = _random.Next(7);

        return generatedColor switch
        {
            0 => Color.Red,
            1 => Color.Blue,
            2 => Color.Yellow,
            3 => Color.Green,
            4 => Color.Magenta,
            5 => Color.Cyan,
            6 => Color.White
        };
    }

    private Player SpawnPlayer(bool isHuman, Color color)
    {
        Vector2f startPosition;
        IActionHandler actionHandler;
        
        if (isHuman)
        {
            actionHandler = new HumanActionHandler(_window);
            startPosition = new (Boot.WINDOW_WIDTH / 2f, Boot.WINDOW_HEIGHT / 2f);
        }
        else
        {
            actionHandler = new BotActionHandler(_window);
            startPosition = GetRandomPosition();
        }

        Player newPlayer = new(actionHandler, isHuman, startPosition, color, _window);
        
        OnUpdatableSpawned?.Invoke(newPlayer);
        OnDrawableSpawned?.Invoke(newPlayer);

        newPlayer.OnDestroyed += UpdateRemovingList;
        
        return newPlayer;
    }

    private void CreateScore(Player mainPlayer)
    {
        string fontName = "Obelix Pro.ttf";
        Font font = new (GetFontLocation(fontName));
        Score score = new(font, 25, Color.Black, Color.White, 3, new(0,0), mainPlayer);
        
        OnDrawableSpawned?.Invoke(score);
    }
    
    private Text CreateEndText()
    {
        string fontName = "Obelix Pro.ttf";
        Font font = new (GetFontLocation(fontName));
        Text endText = new(font, 50, Color.Black, Color.White, 3, new(Boot.WINDOW_WIDTH / 2f, Boot.WINDOW_HEIGHT / 2f));
        
        OnDrawableSpawned?.Invoke(endText);

        return endText;
    }

    private string GetFontLocation(string fontName)
        => Path.GetFullPath("..\\..\\..\\..\\Resources\\Fonts\\" + fontName);

    public void Update()
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
                _endText.Update("You Win!");
                _gameMode.IsGameEnded = true;
                return;
            }
            
            Player newPlayer = SpawnPlayer(false, Color.Red);
            _players.Add(newPlayer);
            
            _passedPlayerTime = 0;
        }

        CheckPlayersIntersections();
        CleanRemovingList();
    }

    private void CheckPlayersIntersections()
    {
        foreach (Player currentPlayer in _players)
        {
            foreach(Player anotherPlayer in _players)
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

    private void UpdateRemovingList(Actor actor)
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
        if (actor == _mainPlayer)
        {
            _endText.Update("You lose!");
            _gameMode.IsGameEnded = true;
        }
        if (_players.Contains(actor))
        {
            _players.Remove(actor as Player);
        }
        else if (_food.Contains(actor))
        {
            _food.Remove(actor as Food);
        }
        
        OnUpdatableDestroyed?.Invoke(actor);
        OnDrawableDestroyed?.Invoke(actor);
    }

}