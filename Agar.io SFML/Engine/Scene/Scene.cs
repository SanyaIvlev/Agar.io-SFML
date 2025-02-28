namespace Agar.io_SFML.Engine.Scene;

public class Scene
{
    public Scene()
    {
        Dependency.Register(this);
    }

    public virtual void Start() { }

    public virtual void Update() { }

    public void CloseScene()
    {
        Dependency.Unregister(this);
    }
}