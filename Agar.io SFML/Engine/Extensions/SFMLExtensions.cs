using SFML.Graphics;
using SFML.System;

namespace Agar.io_SFML.Extensions;

public static class SFMLExtensions
{
    public static bool Intersects(this FloatRect ownActor, Actor otherActor)
    {
        var otherActorShape = otherActor.shape;
        FloatRect otherActorBounds = otherActorShape.GetGlobalBounds();
        
        return ownActor.Intersects(otherActorBounds);
    }

    public static float GetSquaredDistanceTo(this Vector2f ownActor, Vector2f otherActor)
    {
        float distanceX = ownActor.X - otherActor.X;
        float distanceY = ownActor.Y - otherActor.Y;
        
        return distanceX * distanceX + distanceY * distanceY;
    }
        
}