using Agar.io_SFML.Engine;
using Agar.io_SFML.Extensions;
using SFML.Graphics;
using SFML.System;

namespace Agar.io_SFML;

public class Player : EatableActor
{
    public Action<uint> OnBountyChanged;
    public Action<int> OnElimination;
    public Action OnFoodEaten;
    
    private float _speed;

    private Bounds _bounds;

    private int _eliminatedPlayers;
    private bool _isPaused;

    public void Initalize(Vector2f startPosition, Color color)
    {
        Initialize(startPosition);

        _bounds = new();
        
        _speed = 150f;
        Bounty = 10;

        shape = new()
        {
            Position = startPosition,
            Radius = 20,
            FillColor = color,
        };
        
        shape.Origin = new(shape.Radius / 2, shape.Radius / 2);

        EventBus<PauseEvent>.OnEvent += SetPaused;
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

    private void SetPaused(PauseEvent pauseEvent)
    {
        _isPaused = pauseEvent.IsPaused;
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

        if (actor is Player)
        {
            OnElimination?.Invoke(++_eliminatedPlayers);
        }

        if (actor is Food)
        {
            OnFoodEaten?.Invoke();
        }
        
        OnDestroyed?.Invoke(actor);
        OnBountyChanged?.Invoke(Bounty);
    }

    public bool IsMoving()
    {
        return !_isPaused;
    }

    public override void Update()
    {
        if (_isPaused)
            return;

        Move();
        base.Update();
    }

    private void Move()
    {
        Vector2f nextPosition = Position + Direction * _speed * Time.GetElapsedTimeAsSeconds();
        
        if (_bounds.IsInside(nextPosition))
            Position = nextPosition;
    }

}