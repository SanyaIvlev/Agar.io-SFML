namespace Agar.io_SFML;

public class AgarioController : Controller
{
    public Player PlayerPawn { get; private set; }

    protected AgarioController()
    {
        OnPawnUpdated += Pawn => PlayerPawn = Pawn as Player; 
    }
}