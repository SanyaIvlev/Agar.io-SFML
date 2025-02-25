using Agar.io_SFML.Engine;
using Agar.io_SFML.Extensions;
using SFML.Graphics;

namespace Agar.io_SFML;

public class AgarioAIController : AgarioController
{
    private readonly float _squaredStopDistance = 3f;

    private bool _isPaused;
    
    public override void Initialize(Actor controlledPlayer)
    {
        base.Initialize(controlledPlayer);
        
        GenerateNewPosition();
        base.Update();

        EventBus<PauseEvent>.OnEvent += SetPaused;
    }
    
    public override void Update()
    {
        if (_isPaused)
            return;
        
        if (NearNewPosition())
        {
            GenerateNewPosition();
        }
        base.Update();
    }

    private void SetPaused(PauseEvent @event)
    {
        _isPaused = @event.IsPaused;
    }

    private bool NearNewPosition()
    {
        var playerPosition = PlayerPawn.Position;
        
        return playerPosition.GetSquaredDistanceTo(NewPosition) <= _squaredStopDistance;
    }

    private void GenerateNewPosition()
    {
        int x = MyRandom.Next(0, (int)_window.Size.X);
        int y = MyRandom.Next(0, (int)_window.Size.Y);

        NewPosition = new(x, y);
    }
}