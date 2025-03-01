using Agar.io_SFML.Configs;
using Agar.io_SFML.Engine;
using Agar.io_SFML.Engine.Scene;
using Agar.io_SFML.GameSeaBattle.Events;
using Agar.io_SFML.GameSeaBattle.Actors;
using SFML.System;

namespace Agar.io_SFML.GameSeaBattle;

public enum GameType
{
    PVP,
    PVE,
    EVE,
}

public class SeaBattleGame : Scene
{
    private SeaBattleController _controller1;
    private SeaBattleController _controller2;

    private SeaBattleController _currentController;
    private SeaBattleController _opponent;

    private long _timeAfterShot;
    private long _delayAfterShot;
    
    public override void Start()
    {
        Service<GameType>.Set(GameType.PVP);
        
        _timeAfterShot = SeaBattleGameConfig.TimeAfterShot;
        _delayAfterShot = 0;
        
        ControllerFactory controllerFactory = new ControllerFactory();

        (_controller1, _controller2) = controllerFactory.CreateControllersByGameRules();
        
        _currentController = _controller1;
        _opponent = _controller2;
        
        AdjustRightFieldPosition();
        
        TextFactory textFactory = new TextFactory();
        textFactory.CreateScore(_controller1.PlayerPawn);
        textFactory.CreateScore(_controller2.PlayerPawn);

        var currentPawnField = _currentController.PlayerPawn.field;
        var opponentField = _opponent.PlayerPawn.field;
        
        currentPawnField.TryUpdate();
        opponentField.TryUpdate();
        
        currentPawnField.SetCellsClickable(false);
        opponentField.SetCellsClickable(true);

        EventBus<OnShooted>.OnEvent += ChangeTurn;
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
        var currentPawn = _currentController.PlayerPawn;
        
        if (!currentPawn.NeedsUpdate)
            return;

        _delayAfterShot += Time.ElapsedTime;

        if (_delayAfterShot < _timeAfterShot)
            return;
        
        _delayAfterShot = 0;

        var opponentPawn = _opponent.PlayerPawn;
        
        currentPawn.Update(opponentPawn.field);
    }

    private void ChangeTurn(OnShooted onShootedEvent)
    {
        var currentPawn = _currentController.PlayerPawn;
        var opponentPawn = _opponent.PlayerPawn;
        
        if (onShootedEvent.ShootedCell.HasShip)
        {
            currentPawn.OnShipDestroyed();
        }
        
        currentPawn.field.TryUpdate();
        opponentPawn.field.TryUpdate();
        
        (_currentController, _opponent) = (_opponent, _currentController);
    }
}