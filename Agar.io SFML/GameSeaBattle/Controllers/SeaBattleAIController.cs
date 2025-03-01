using Agar.io_SFML.Configs;

namespace Agar.io_SFML.GameSeaBattle.Actors;

public class SeaBattleAIController : SeaBattleController
{
    private int fieldWidth;
    private int fieldHeight;
    

    public SeaBattleAIController()
    {
        fieldWidth = SeaBattleFieldConfig.Width;
        fieldHeight = SeaBattleFieldConfig.Height;
    }
    
    public override void Update()
    {
        PlayerPawn.ShootingPosition = GetRandomPosition();
    }

    private (int x, int y) GetRandomPosition()
    {
        int x = MyRandom.Next(0, fieldWidth);
        int y = MyRandom.Next(0, fieldHeight);
        
        return (x,y);
    }
}