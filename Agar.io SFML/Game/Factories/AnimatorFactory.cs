using Agar.io_SFML.Animations;
using Agar.io_SFML.Engine;
using SFML.Graphics;

namespace Agar.io_SFML.Factory;

public enum HumanSkins
{
    Ghost,
    Mushroom,
}

public class AnimatorFactory : ActorFactory
{
    private TextureLoader _textureLoader;

    private Texture[] _humanIdleFrames;
    private Texture[] _humanMovementFrames;
    
    private Texture[] _AIIdleFrames;
    private Texture[] _AIMovementFrames;
    
    private Texture[] _foodIdleFrames;

    private string[] humanSkinDirectories;
    
    public AnimatorFactory()
    {
        Dependency.Register(this);
        
        _textureLoader = TextureLoader.Instance;
        
        _AIIdleFrames = _textureLoader.LoadTexturesFrom("AI", "Idle");
        _AIMovementFrames = _textureLoader.LoadTexturesFrom("AI", "Movement");
        _foodIdleFrames = _textureLoader.LoadTexturesFrom("Food", "Idle");
    }

    public void SetPlayerSkin(HumanSkins skin)
    {
        string skinName = Enum.GetName(skin);
        
        _humanIdleFrames = _textureLoader.LoadTexturesFrom("Human", skinName,"Idle");
        _humanMovementFrames = _textureLoader.LoadTexturesFrom("Human", skinName, "Movement");
    }


    public void CreatePlayerAnimator(Player player, bool isHuman)
    {
        ShapeAnimator animator = CreateActor<ShapeAnimator>();
        
        Texture[] idleFrames = isHuman? _humanIdleFrames : _AIIdleFrames;
        Texture[] walkingFrames = isHuman? _humanMovementFrames : _AIMovementFrames;

        State idleState = new(idleFrames, 200);
        State walkingState = new(walkingFrames, 100);

        animator
            .Initialize(player.shape)
            .AddInitialState(idleState)
            .AddTransition(idleState, walkingState, player.IsMoving)
            .AddTransition(walkingState, idleState, () => !player.IsMoving());
    }

    public void CreateFoodAnimator(Shape foodShape)
    {
        ShapeAnimator animator = CreateActor<ShapeAnimator>();
        
        State idleState = new(_foodIdleFrames, 50);

        animator
            .Initialize(foodShape)
            .AddInitialState(idleState);
    }
}