namespace Agar.io_SFML.Engine.Scene;

public class Scene
{
    public Scene()
    {
        Service<Scene>.Set(this);
    }

    public virtual void Start() { }

    public virtual void Update() { }
}