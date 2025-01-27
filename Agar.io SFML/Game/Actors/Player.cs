using Agar.io_SFML.Extensions;
using SFML.Graphics;
using SFML.System;

namespace Agar.io_SFML;

public class Player : EatableActor
{
    public Action<uint> OnBountyChanged;
    
    public Vector2f TargetPosition;
    
    private Vector2f _direction;
    
    private float _speed;
    private float _defaultSpeed;

    private uint _initialBounty;

    public void Initalize(Vector2f startPosition, Color color)
    {
        base.Initialize(startPosition);
        
        _speed = _defaultSpeed = 100f;
        Bounty = _initialBounty = 10;

        shape = new()
        {
            Position = startPosition,
            Radius = 20,
            FillColor = color,
            OutlineColor = Color.Black,
            OutlineThickness = 2,
        };
        
        shape.Origin = new(shape.Radius / 2, shape.Radius / 2);
        
        TargetPosition = startPosition;
    }

    public void CheckIntersectionWith(EatableActor actor)
    {
        var shapeBounds = shape.GetGlobalBounds();

        if (!shapeBounds.Intersects(actor))
            return;
        
        if (Bounty > actor.Bounty)
        {
            TryEat(actor);
        }
    }

    private void TryEat(EatableActor actor)
    {
        float collisionDepth = GetCollisionDepth(actor);
        
        if (collisionDepth < actor.shape.Radius)
            return;
        
        Eat(actor);
    }

    private float GetCollisionDepth(EatableActor actor)
    {
        var dx = actor.Center.X - Center.X;
        var dy = actor.Center.Y - Center.Y;
        
        float squaredDistance = dx * dx + dy * dy;
        float distance = MathF.Sqrt(squaredDistance);
        
        return shape.Radius + actor.shape.Radius - distance;
    }

    private void Eat(EatableActor actor)
    {
        Bounty += actor.Bounty;
        _speed /= 1 + 0.025f/actor.Bounty;
        
        shape.Radius += actor.Bounty / 2f;
        shape.Origin = new(shape.Radius / 2, shape.Radius / 2);
        
        OnDestroyed?.Invoke(actor);
        OnBountyChanged?.Invoke(Bounty);
    }

    public override void Update()
    {
        Move();
        
        base.Update();
    }

    private void Move()
    {
        _direction = new Vector2f(TargetPosition.X - Position.X, TargetPosition.Y - Position.Y);
        
        Vector2f normalizedDirection = _direction.Normalize();
        
        Position += normalizedDirection * _speed * Time.GetElapsedTimeAsSeconds();
    }
}