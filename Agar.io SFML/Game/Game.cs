using Microsoft.VisualBasic.CompilerServices;
using SFML.Graphics;
using SFML.System;

namespace Agar.io_SFML;

public class Game
{
    private const uint FIELD_WIDTH = 800;
    private const uint FIELD_HEIGHT = 600;
    
    public Action<IUpdatable> OnUpdatableSpawned;
    public Action<IDrawable> OnDrawableSpawned;
    
    private uint _playersCount;
    private List<Player> _players;
    
    private InputHandler _inputHandler;
    
    private Random _random;

    public Game(InputHandler inputHandler)
    {
        _inputHandler = inputHandler;
        
        _random = new Random();
        
        _players = new List<Player>();

        _playersCount = 5;
    }
    
    
    public void Start()
    {
        _players = new ();

        Player mainPlayer = SpawnPlayer(true, Color.Blue);
        _players.Add(mainPlayer);
        
        foreach(var _ in Enumerable.Range(0, (int)_playersCount))
        {
            Player bot = SpawnPlayer(false, Color.Red);
            _players.Add(bot);
        }
    }
    
    public void Update()
    {
        
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

        Player newPlayer = new((FIELD_WIDTH, FIELD_HEIGHT), _inputHandler, isHuman, startPosition, color);
        
        OnUpdatableSpawned?.Invoke(newPlayer);
        OnDrawableSpawned?.Invoke(newPlayer);
        
        return newPlayer;
    }

    private Vector2f GetRandomPosition()
    {
        int x = _random.Next(0, (int)FIELD_WIDTH);
        int y = _random.Next(0, (int)FIELD_HEIGHT);
        
        return new (x, y);
    }
}