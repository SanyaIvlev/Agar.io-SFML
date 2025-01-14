using Agar.io_SFML.Extensions;
using SFML.Graphics;
using SFML.System;

namespace Agar.io_SFML;

public class Player : Actor
{
    public Action<uint> OnBountyChanged;
    
    private Vector2f _targetPosition;
    
    private float _speed;
    private float _defaultSpeed;
    
    private IActionHandler _actionHandler;

    private uint _initialBounty;

    private bool _isHuman;

    private float _squaredStopDistance = 3f;
    
    private RenderWindow _window;
    
    public Player(IActionHandler humanActionHandler, bool isHuman, Vector2f startPosition, Color color, RenderWindow window) : base(startPosition)
    {
        _speed = _defaultSpeed = 100f;
        Bounty = _initialBounty = 10;
        
        _actionHandler = humanActionHandler;
        
        _isHuman = isHuman;

        shape = new CircleShape()
        {
            Position = startPosition,
            Radius = 20,
            FillColor = color,
            OutlineColor = Color.White,
            OutlineThickness = 2,
        };
        
        _window = window;
        
        shape.Origin = new(shape.Radius / 2, shape.Radius / 2);
        
        _targetPosition = startPosition;
    }

    public void CheckIntersectionWith(Actor actor)
    {
        var shapeBounds = shape.GetGlobalBounds();

        if (shapeBounds.Intersects(actor))
        {
            if (Bounty > actor.Bounty)
            {
                Eat(actor);
            }
        }
    }

    private void Eat(Actor actor)
    {
        Bounty += actor.Bounty;
        _speed /= 1 + 0.05f/actor.Bounty;
        
        shape.Radius += actor.Bounty / 2f;
        shape.Origin = new(shape.Radius / 2, shape.Radius / 2);
        
        OnDestroyed?.Invoke(actor);
        OnBountyChanged?.Invoke(Bounty);
    }

    public void ProcessAction()
    {
        _actionHandler.ProcessAction();
    }

    public override void Update()
    {
        if (_isHuman)
        {
            TryMove();
        }
        else
        {
            TryChangePositionAndMove();
        }
        
        base.Update();
    }

    private void TryMove()
    {
        Vector2f newPosition = _actionHandler.GetPosition();
        Vector2u windowSize = _window.Size;
        
        if (newPosition.X > windowSize.X || newPosition.X < 0 ||
            newPosition.Y > windowSize.Y || newPosition.Y < 0)
            return;
        
        _targetPosition = _actionHandler.GetPosition();
            
        Move();
    }

    private void Move()
    {
        _direction = new Vector2f(_targetPosition.X - Position.X, _targetPosition.Y - Position.Y);
        
        Vector2f normalizedDirection = _direction.Normalize();
        
        Position += normalizedDirection * _speed * Time.GetElapsedTimeAsSeconds();
    }
    
    private void TryChangePositionAndMove()
    {
        if (Position.GetSquaredDistanceTo(_targetPosition) <= _squaredStopDistance)
        {
            _targetPosition = _actionHandler.GetPosition();
        }

        Move();
    }
}