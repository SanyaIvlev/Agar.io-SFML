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

        Actors.Player player = CreatePlayer(isHuman);
        
        controller.Initialize(player);

        return controller;
    }

    private Actors.Player CreatePlayer(bool isHuman)
    {
        Actors.Player player = CreateActor<Actors.Player>();
        
        player.NeedsUpdate = !isHuman;
        player.field = _mapFactory.CreateField(isHuman);
        
        return player;
    }
}