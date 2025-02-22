using Agar.io_SFML.Animations;
using SFML.Graphics;

namespace Agar.io_SFML.Factory;

public enum HumanSkins
{
    Ghost,
    Monster,
}

public class AnimatorFactory : ActorFactory
{
    private TextureLoader _textureLoader;

    private Texture[] _humanIdleFrames;
    private Texture[] _humanWalkingFrames;
    
    private Texture[] _AIIdleFrames;
    private Texture[] _AIWalkingFrames;

    private string[] humanSkinDirectories;
    
    public AnimatorFactory()
    {
        _textureLoader = TextureLoader.Instance;
        
        _AIIdleFrames = _textureLoader.LoadTexturesFrom("AI", "Idle");
        _AIWalkingFrames = _textureLoader.LoadTexturesFrom("AI", "Movement");
    }

    public void SetPlayerSkin(HumanSkins skin)
    {
        string skinName = Enum.GetName(skin);
        
        _humanIdleFrames = _textureLoader.LoadTexturesFrom("Human", skinName,"Idle");
        _humanWalkingFrames = _textureLoader.LoadTexturesFrom("Human", skinName, "Walking");
    }


    public void CreatePlayerAnimator(Player player, bool isHuman)
    {
        ShapeAnimator animator = CreateActor<ShapeAnimator>();
        
        Texture[] idleFrames = isHuman? _humanIdleFrames : _AIIdleFrames;
        Texture[] walkingFrames = isHuman? _humanWalkingFrames : _AIWalkingFrames;

        State idleState = new(idleFrames, 200);
        State walkingState = new(walkingFrames, 100);
        
        animator.Initialize(player.shape, idleState);
        
        animator.AddTransition(idleState, walkingState, player.IsMoving);
        animator.AddTransition(walkingState, idleState, () => !player.IsMoving());
    }

    public void CreateFoodAnimator(Shape foodShape)
    {
        ShapeAnimator animator = CreateActor<ShapeAnimator>();

        Texture[] idleFrames = _textureLoader.LoadTexturesFrom("Food", "Idle");
        
        State idleState = new(idleFrames, 50);
        
        animator.Initialize(foodShape, idleState);
    }
}