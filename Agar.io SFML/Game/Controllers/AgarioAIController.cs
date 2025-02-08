using Agar.io_SFML.Extensions;
using SFML.Graphics;

namespace Agar.io_SFML;

public class AgarioAIController : AgarioController
{
    private readonly float _squaredStopDistance = 3f;
    
    public override void Initialize(Actor controlledPlayer, RenderWindow window)
    {
        base.Initialize(controlledPlayer, window);
        
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