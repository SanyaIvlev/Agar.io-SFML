using SFML.System;

namespace Agar.io_SFML.Extensions;

public static class MathExtensions
{
    public static Vector2f Normalize(this Vector2f vector)
    {
        float vectorLength = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
        
        if (Math.Abs(vectorLength) <= 0.0001f)
        {
            return vector;
        }
        
        float inversedLength = 1 / vectorLength;
        
        vector *= inversedLength;

        return vector;
    }
    
    public static float GetSquaredDistanceTo(this Vector2f ownActor, Vector2f otherActor)
    {
        float distanceX = ownActor.X - otherActor.X;
        float distanceY = ownActor.Y - otherActor.Y;
        
        return distanceX * distanceX + distanceY * distanceY;
    }
}