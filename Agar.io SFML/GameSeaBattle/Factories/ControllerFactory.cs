using Agar.io_SFML.Engine;
using Agar.io_SFML.GameSeaBattle.Actors;

namespace Agar.io_SFML.GameSeaBattle;

public class ControllerFactory : ActorFactory
{
    private MapFactory _mapFactory;

    public ControllerFactory()
    {
        _mapFactory = new MapFactory();
    }
    
    public (SeaBattleController startingController, SeaBattleController opponent) CreateControllersByGameRules()
    {
        GameType typeOfGame = Service<GameType>.Get;
        return typeOfGame switch
        {
            GameType.PVP => (CreateController(true, true),
                CreateController(true, false)),
            
            GameType.PVE => (CreateController(true, true),
                CreateController(false, false)),
            
            GameType.EVE => (CreateController(false, true),
                CreateController(false, false)),
        };
    }
    
    private SeaBattleController CreateController(bool isHuman, bool isStartingController)
    {
        SeaBattleController controller;
        
        if (isHuman)
            controller = CreateActor<SeaBattleController>();
        else
            controller = CreateActor<SeaBattleAIController>();

        Actors.Player player = CreatePlayer(isHuman, isStartingController);
        
        controller.Initialize(player);

        return controller;
    }

    private Actors.Player CreatePlayer(bool isHuman, bool isStartingPlayer)
    {
        Actors.Player player = CreateActor<Actors.Player>();
        
        player.NeedsUpdate = !isHuman;
        player.field = _mapFactory.CreateField(isHuman, isStartingPlayer);
        
        return player;
    }
}