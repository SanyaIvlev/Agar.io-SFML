using Agar.io_SFML.Extensions;
using SFML.Graphics;
using SFML.System;

namespace Agar.io_SFML;

public class Player : Actor
{
    private Vector2f _targetPosition;
    
    private float _speed;
    private float _defaultSpeed;
    
    private InputHandler _inputHandler;

    private uint _initialBounty;

    private bool _isHuman;

    private float _radius => shape.Radius;

    private Vector2f _shapeCenter => new Vector2f(Position.X + _radius, Position.Y + _radius);
    
    public Player(InputHandler inputHandler, bool isHuman, Vector2f startPosition, Color color) : base(startPosition)
    {
        _speed = _defaultSpeed = 1.4f;
        Bounty = _initialBounty = 10;
        
        _inputHandler = inputHandler;
        _inputHandler.onPositionChanged += MoveToTarget;
        
        _isHuman = isHuman;

        shape = new CircleShape()
        {
            Position = startPosition,
            Radius = 20,
            FillColor = color
        };
        
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
        _speed /= 1 + 0.1f/actor.Bounty;
        shape.Radius += actor.Bounty;
        
        OnDestroy?.Invoke(actor);
    }

    private void MoveToTarget(Vector2f newPosition)
    {
        if (_isHuman)
        {
            _targetPosition = newPosition;
            
            Move();
        }
        else
        {
            MoveToRandomPosition();
        }
    }

    private void Move()
    {
        _velocity = new Vector2f(_targetPosition.X - Position.X, _targetPosition.Y - Position.Y);
        
        Vector2f normalizedVelocity = _velocity.Normalize();
        
        Position += normalizedVelocity * _speed * Time.GetElapsedTimeAsSeconds();
        Console.WriteLine();
    }
    
    private void MoveToRandomPosition()
    {
        if (Position == _targetPosition)
        {
            _targetPosition = GetRandomPosition();
        }

        Move();
    }
    
    private Vector2f Lerp(Vector2f firstVector, Vector2f secondVector, float by)
        => firstVector + (secondVector - firstVector) * by;
        

    private Vector2f GetRandomPosition()
    {
        int x = _random.Next(0, (int)_fieldSize.width);
        int y = _random.Next(0, (int)_fieldSize.height);

        return new(x, y);
    }
}