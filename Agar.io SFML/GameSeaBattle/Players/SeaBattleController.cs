namespace Agar.io_SFML.GameSeaBattle.Players;

public class SeaBattleController : Controller
{
    public Player PlayerPawn;

    public SeaBattleController()
    {
        OnPawnUpdated += Pawn => PlayerPawn = Pawn as Player;
    }
}