using Agar.io_SFML.Configs;
using Agar.io_SFML.Extensions;
using SFML.Graphics;
using SFML.System;

namespace Agar.io_SFML;

public class Player : EatableActor
{
    public Action<uint> OnBountyChanged;
    
    private float _speed;

    public void Initalize(Vector2f startPosition, Color color, Color outline)
    {
        Initialize(startPosition);
        
        _speed = 150f;
        Bounty = 10;

        shape = new()
        {
            Position = startPosition,
            Radius = 20,
            FillColor = color,
            OutlineColor = outline,
            OutlineThickness = 2,
        };
        
        shape.Origin = new(shape.Radius / 2, shape.Radius / 2);
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
        Vector2f nextPosition = Position + Direction * _speed * Time.GetElapsedTimeAsSeconds();

        if (nextPosition.X > WindowConfig.WindowWidth || nextPosition.X < 0 ||
            nextPosition.Y > WindowConfig.WindowHeight || nextPosition.Y < 0)
            return;
        
        Position = nextPosition;
    }
}