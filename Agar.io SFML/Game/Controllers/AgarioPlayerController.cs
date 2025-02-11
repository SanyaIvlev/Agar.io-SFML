using Agar.io_SFML.Audio;
using Agar.io_SFML.Configs;
using SFML.Graphics;
using SFML.Window;

namespace Agar.io_SFML;

public class AgarioPlayerController : AgarioController
{ 
    private readonly string _eatingSound;
    
    public AgarioPlayerController()
    {
        _eatingSound = AudioConfig.Eating;
    }
    
    public override void Initialize(Actor controlledPlayer, RenderWindow window, AgarioAudioSystem audioSystem)
    {
        base.Initialize(controlledPlayer, window, audioSystem);
        
        FollowPawnActions();
    }

    public override void Update()
    {
        ProcessMousePosition();
        base.Update();
    }

    public override void SwapWith(Controller anotherController)
    {
        UnfollowPawnActions();
        base.SwapWith(anotherController);
        FollowPawnActions();
    }
    
    private void ProcessMousePosition()
    {
        NewPosition = _window.MapPixelToCoords(Mouse.GetPosition(_window));
    }
    
    private void FollowPawnActions()
    {
        PlayerPawn.OnElimination += audioSystem.PlayEliminationSound;
        PlayerPawn.OnFoodEaten += () => audioSystem.PlaySoundOnce(_eatingSound);
    }
    
    private void UnfollowPawnActions()
    {
        PlayerPawn.OnElimination -= audioSystem.PlayEliminationSound;
        PlayerPawn.OnFoodEaten -= () => audioSystem.PlaySoundOnce(_eatingSound);
    }
}