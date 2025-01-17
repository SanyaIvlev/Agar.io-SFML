using SFML.Graphics;

namespace Agar.io_SFML.Extensions;

public static class SFMLExtensions
{
    public static bool Intersects(this FloatRect ownActor, Actor otherActor)
    {
        var otherActorShape = otherActor.shape;
        FloatRect otherActorBounds = otherActorShape.GetGlobalBounds();
        
        return ownActor.Intersects(otherActorBounds);
    }
        
}