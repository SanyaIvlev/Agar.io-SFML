using Microsoft.VisualBasic.CompilerServices;
using SFML.Graphics;
using SFML.System;

namespace Agar.io_SFML;

public class Game
{
    public const uint FIELD_WIDTH = 800;
    public const uint FIELD_HEIGHT = 600;
    
    public Action<IUpdatable> OnUpdatableSpawned;
    public Action<IDrawable> OnDrawableSpawned;
    
    public Action<IUpdatable> OnUpdatableDestroyed;
    public Action<IDrawable> OnDrawableDestroyed;
    
    private uint _playersOnStart;
    private uint _foodOnStart = 50;
    
    private List<Player> _players;
    private List<Food> _food;
    
    private InputHandler _inputHandler;
    
    private Random _random;

    private float _foodRespawnDelay;
    private float _passedTime;
    
    private List<Actor> _currentRemovingActors;

    public Game(InputHandler inputHandler)
    {
        _inputHandler = inputHandler;
        
        _random = new Random();
        
        _players = new List<Player>();

        _playersOnStart = 5;
        
        _passedTime = 0;
        _foodRespawnDelay = 3f;
    }
    
    
    public void Start()
    {
        _players = new ();
        _food = new();
        _currentRemovingActors = new();

        Player mainPlayer = SpawnPlayer(true, Color.Blue);
        _players.Add(mainPlayer);
        
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
        int x = _random.Next(0, (int)FIELD_WIDTH);
        int y = _random.Next(0, (int)FIELD_HEIGHT);
        
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
        
        if (isHuman)
        {
            startPosition = new (FIELD_WIDTH / 2f, FIELD_HEIGHT / 2f);
        }
        else
        {
            startPosition = GetRandomPosition();
            
        }

        Player newPlayer = new(_inputHandler, isHuman, startPosition, color);
        
        OnUpdatableSpawned?.Invoke(newPlayer);
        OnDrawableSpawned?.Invoke(newPlayer);

        newPlayer.OnDestroy += UpdateRemovingList;
        
        return newPlayer;
    }

    public void Update()
    {
        _passedTime += Time.GetElapsedTimeAsSeconds();
        
        if (_passedTime >= _foodRespawnDelay)
        {
            Food newFood = SpawnFood();
            _food.Add(newFood);
            
            _passedTime = 0;
        }

        CheckPlayersIntersections();
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
        
        CleanRemovingList();
    }

    private void UpdateRemovingList(Actor actor)
    {
        _currentRemovingActors.Add(actor);
    }

    private void CleanRemovingList()
    {
        foreach (var actor in _currentRemovingActors)
        {
            RemoveActor(actor);
        }
    }
    
    private void RemoveActor(Actor actor)
    {
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