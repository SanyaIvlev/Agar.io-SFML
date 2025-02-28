using Agar.io_SFML.Audio;
using Agar.io_SFML.Configs;
using Agar.io_SFML.Engine;
using SFML.Graphics;
using SFML.Window;

namespace Agar.io_SFML;

public class AgarioPlayerController : AgarioController
{ 
    private readonly string _eatingSound;
    
    private readonly AgarioAudioSystem _agarioAudioSystem;

    private bool _isPaused;
    
    public AgarioPlayerController()
    {
        _eatingSound = AudioConfig.Eating;
        
        EventBus<PauseEvent>.OnEvent += SetPaused;
        
        _agarioAudioSystem =Service<AgarioAudioSystem>.Get;
    }
    
    public override void Initialize(Actor controlledPlayer)
    {
        base.Initialize(controlledPlayer);
        
        FollowPawnActions();
    }

    public override void Update()
    {
        if (_isPaused)
            return;
        
        ProcessMousePosition();
        base.Update();
    }
    
    private void SetPaused(PauseEvent pauseEvent)
    {
        _isPaused = pauseEvent.IsPaused;
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
        PlayerPawn.OnElimination += _agarioAudioSystem.PlayEliminationSound;
        PlayerPawn.OnFoodEaten += () => _agarioAudioSystem.PlaySoundOnce(_eatingSound);
    }
    
    private void UnfollowPawnActions()
    {
        PlayerPawn.OnElimination -= _agarioAudioSystem.PlayEliminationSound;
        PlayerPawn.OnFoodEaten -= () => _agarioAudioSystem.PlaySoundOnce(_eatingSound);
    }
}