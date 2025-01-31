using Agar.io_SFML.Extensions;
using SFML.Graphics;
using SFML.System;

namespace Agar.io_SFML;

public class AgarioAIController : Controller
{
    private readonly float _squaredStopDistance = 3f;
    
    private Random _random;
    
    public override void Initialize(Actor controlledPlayer, RenderWindow window)
    {
        base.Initialize(controlledPlayer, window);
        
        _random = new Random();
        
        GenerateNewPosition();
        base.Update();
    }
    
    public override void Update()
    {
        if (NearNewPosition())
        {
            GenerateNewPosition();
        }
        base.Update();
    }

    private bool NearNewPosition()
    {
        var playerPosition = ControlledActor.Position;
        
        return playerPosition.GetSquaredDistanceTo(NewPosition) <= _squaredStopDistance;
    }

    private void GenerateNewPosition()
    {
        int x = _random.Next(0, (int)_window.Size.X);
        int y = _random.Next(0, (int)_window.Size.Y);

        NewPosition = new(x, y);
    }
}
