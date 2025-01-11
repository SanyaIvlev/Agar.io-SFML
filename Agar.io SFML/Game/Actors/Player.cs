using Agar.io_SFML.Extensions;
using SFML.Graphics;
using SFML.System;

namespace Agar.io_SFML;

public class Player : Actor
{
    private Vector2f _targetPosition;
    
    private float _speed;
    
    private InputHandler _inputHandler;

    private bool _isHuman;
    
    public Player((uint width, uint height) fieldSize, InputHandler inputHandler, bool isHuman, Vector2f startPosition, Color color) : base(fieldSize, startPosition)
    {
        _speed = 0.4f;
        
        _inputHandler = inputHandler;
        _inputHandler.onPositionChanged += MoveToTarget;
        
        _isHuman = isHuman;

        _shape = new CircleShape()
        {
            Position = startPosition,
            Radius = 20,
            FillColor = color
        };
        
        _targetPosition = startPosition;
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
        _velocity = new Vector2f(_targetPosition.X - _position.X, _targetPosition.Y - _position.Y);
        
        Vector2f normalizedVelocity = _velocity.Normalize();
        
        _position += normalizedVelocity * _speed * Time.GetElapsedTimeAsSeconds();
        Console.WriteLine(_position);
    }
    
    private void MoveToRandomPosition()
    {
        if (_position == _targetPosition)
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