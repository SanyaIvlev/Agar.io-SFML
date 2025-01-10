using SFML.System;

namespace Agar.io_SFML.Actors;

public class Player : Actor
{
    private Vector2f _targetPosition;
    
    private float _speed;
    
    private InputHandler _inputHandler;

    private bool _isHuman;
    
    public Player((uint width, uint height) fieldSize, float speed, InputHandler inputHandler, bool isHuman)
    {
        _fieldSize = fieldSize;
        
        _speed = speed;
        
        _inputHandler = inputHandler;
        _inputHandler.newPosition += Move;
        
        _isHuman = isHuman;
    }

    private void Move(Vector2f newPosition)
    {
        if (_isHuman)
        {
            MoveToGivenPosition(newPosition);
        }
        else
        {
            MoveToRandomPosition();
        }
    }

    private void MoveToGivenPosition(Vector2f newPosition) //ПОТІМ ЗМІНИТИ
    {
        _targetPosition = newPosition;
        
        Move();
    }

    private void Move()
    {
        _velocity = new Vector2f(_targetPosition.X - _targetPosition.X, _targetPosition.Y - _targetPosition.Y);

        float distance = (float)Math.Sqrt(_velocity.X * _velocity.X + _velocity.Y * _velocity.Y);

        if (distance > 0)
        {
            _velocity /= distance;
            float playerSpeed = _speed * (distance / 100);

            playerSpeed = Math.Min(playerSpeed, _speed);
            playerSpeed = Math.Max(playerSpeed, 10f);
            _position += _velocity * playerSpeed * Time.ElapsedTime;
        }
    }
    
    private void MoveToRandomPosition()
    {
        if (_position == _targetPosition)
        {
            _targetPosition = GetRandomPosition();
        }

        Move();
    }
    
    

    private Vector2f GetRandomPosition()
    {
        int x = _random.Next(0, (int)_fieldSize.width);
        int y = _random.Next(0, (int)_fieldSize.height);

        return new(x, y);
    }

}