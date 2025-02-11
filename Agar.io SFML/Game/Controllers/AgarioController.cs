using Agar.io_SFML.Audio;
using SFML.Graphics;

namespace Agar.io_SFML;

public class AgarioController : Controller
{
    public Player PlayerPawn { get; private set; }
    protected AgarioAudioSystem audioSystem;

    protected AgarioController()
    {
        OnPawnUpdated += Pawn => PlayerPawn = Pawn as Player; 
    }

    public virtual void Initialize(Actor controlledPlayer, RenderWindow window, AgarioAudioSystem audioSystem)
    {
        base.Initialize(controlledPlayer, window);
        this.audioSystem = audioSystem;
    }

    public virtual void SwapWith(Controller anotherController)
    {
        var tempPawn = Pawn;
        
        SetPawn(anotherController.Pawn);
        anotherController.SetPawn(tempPawn);
    }
    
}