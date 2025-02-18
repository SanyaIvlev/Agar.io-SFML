using SFML.Graphics;

namespace Agar.io_SFML.Animations;

public class State
{
    public Texture CurrentFrame => _animation.CurrentFrame; 
        
    private Animation _animation;

    public State(Texture[] frames, long neededTimeInMilliseconds)
    {
        _animation = new Animation(frames, neededTimeInMilliseconds);
    }
    
    public void StartAnimation()
    {
        _animation.Start();
    }

    public void Update()
    {
        _animation.Update();
    }
}