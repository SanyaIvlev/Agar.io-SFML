using Agar.io_SFML.Animations;
using SFML.Graphics;

namespace Agar.io_SFML.Factory;

public class AnimatorFactory : ActorFactory
{
    private TextureLoader _textureLoader;
    
    public AnimatorFactory(GameLoop gameLoop) : base(gameLoop)
    {
        _textureLoader = new TextureLoader();
    }

    public void CreatePlayerAnimator(Player player, bool isHuman)
    {
        ShapeAnimator animator = CreateActor<ShapeAnimator>();
        
        string actorTypeDirectory = isHuman ? "Human" : "AI";
        Texture[] idleFrames = _textureLoader.LoadTexturesFrom(actorTypeDirectory, "Idle");
        Texture[] walkingFrames = _textureLoader.LoadTexturesFrom(actorTypeDirectory, "Movement");

        State idleState = new(idleFrames, 200);
        State walkingState = new(walkingFrames, 100);
        
        animator.Initialize(player.shape, idleState);
        
        animator.AddTransition(idleState, walkingState, () => player.IsMoving());
        animator.AddTransition(walkingState, idleState, () => !player.IsMoving());
    }

    public void CreateFoodAnimator(Shape foodShape)
    {
        ShapeAnimator animator = CreateActor<ShapeAnimator>();

        Texture[] idleFrames = _textureLoader.LoadTexturesFrom("Food", "Idle");
        
        State idleState = new(idleFrames, 100);
        
        animator.Initialize(foodShape, idleState);
    }
}