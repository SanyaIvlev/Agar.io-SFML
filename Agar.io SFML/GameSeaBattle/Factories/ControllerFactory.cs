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
    
    public (SeaBattleController, SeaBattleController) CreateControllersByGameRules()
    {
        GameType typeOfGame = Service<GameType>.Get;
        return typeOfGame switch
        {
            GameType.PVP => (CreateController(true), CreateController(true)),
            GameType.PVE => (CreateController(false), CreateController(true)),
            GameType.EVE => (CreateController(false), CreateController(false)),
        };
    }
    
    private SeaBattleController CreateController(bool isHuman)
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
        
        player.NeedsUpdate = !isHuman;
        player.field = _mapFactory.CreateField(isHuman);
        
        return player;
    }
}