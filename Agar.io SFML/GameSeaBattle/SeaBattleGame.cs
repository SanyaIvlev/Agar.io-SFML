using Agar.io_SFML.Configs;
using Agar.io_SFML.Engine;
using Agar.io_SFML.Engine.Scene;
using Agar.io_SFML.GameSeaBattle.Events;
using Agar.io_SFML.GameSeaBattle.Players;
using SFML.System;

namespace Agar.io_SFML.GameSeaBattle;

public class SeaBattleGame : Scene
{
    private SeaBattleController _controller1;
    private SeaBattleController _controller2;

    private SeaBattleController _currentController;
    private SeaBattleController _opponent;
    
    private ControllerFactory _controllerFactory;
    
    public override void Start()
    {
        _controllerFactory = new ControllerFactory();
        
        _currentController = _controller1 = _controllerFactory.CreateController(true);
        _opponent = _controller2 = _controllerFactory.CreateController(true);;
        
        CreateFields();
        
        _controller1.PlayerPawn.Initialize(true);
        _controller2.PlayerPawn.Initialize(true);
        
        _opponent.PlayerPawn.field.UpdateInteractable();

        AdjustRightFieldPosition();

        EventBus<OnShooted>.OnEvent += ChangeTurn;
    }

    private void CreateFields()
    {
        _controller1.PlayerPawn.field = new Field();
        _controller1.PlayerPawn.field.Initialize();
        
        _controller2.PlayerPawn.field = new Field();
        _controller2.PlayerPawn.field.Initialize();
    }
    
    private void AdjustRightFieldPosition()
    {
        int windowWidth = SeaBattleWindowConfig.WindowWidth;
        
        int fieldWidth = SeaBattleFieldConfig.Width;
        int fieldHeight = SeaBattleFieldConfig.Height;
        
        for (int y = 0; y < fieldHeight; y++)
        {
            for (int x = 0; x < fieldWidth; x++)
            {
                var opponentPlayerPawn = _controller2.PlayerPawn;
                var cell = opponentPlayerPawn.field.GetCell(x, y);
                Vector2f position = cell.GetPosition();
                Vector2f newPosition = new(position.X + windowWidth / 2f, position.Y);
                cell.SetPosition(newPosition);
            }
        }
    }

    public override void Update()
    {
        
    }

    private void ChangeTurn(OnShooted onShootedEvent)
    {
        _currentController.PlayerPawn.OpponentShipsDestroyed++;
        (_currentController, _opponent) = (_opponent, _currentController);
    }
}