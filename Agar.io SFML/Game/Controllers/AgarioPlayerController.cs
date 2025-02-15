using Agar.io_SFML.Audio;
using Agar.io_SFML.Configs;
using Agar.io_SFML.PauseControl;
using SFML.Graphics;
using SFML.Window;

namespace Agar.io_SFML;

public class AgarioPlayerController : AgarioController, IPauseHandler
{ 
    private readonly string _eatingSound;

    private bool _isPaused;
    
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
        if (_isPaused)
            return;
        
        ProcessMousePosition();
        base.Update();
    }
    
    public void SetPaused(bool isPaused)
    {
        _isPaused = isPaused;
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