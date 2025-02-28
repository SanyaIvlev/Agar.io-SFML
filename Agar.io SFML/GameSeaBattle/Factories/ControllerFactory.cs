using Agar.io_SFML.Engine;
using Agar.io_SFML.GameSeaBattle.Players;

namespace Agar.io_SFML.GameSeaBattle;

public class ControllerFactory : ActorFactory
{
    private MapFactory _mapFactory;

    public ControllerFactory()
    {
        _mapFactory = new MapFactory();
        
    }
    public SeaBattleController CreateController(bool isHuman)
    {
        SeaBattleController controller;
        
        if (isHuman)
            controller = CreateActor<SeaBattleController>();
        else
            controller = CreateActor<SeaBattleAIController>();

        Players.Player player = CreatePlayer(isHuman);
        
        controller.Initialize(player);

        return controller;
    }

    private Players.Player CreatePlayer(bool isHuman)
    {
        Players.Player player = CreateActor<Players.Player>();
        
        player.SetUpdateNeed(!isHuman);
        player.field = _mapFactory.CreateField(isHuman);
        
        return player;
    }
}