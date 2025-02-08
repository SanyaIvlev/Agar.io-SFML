using SFML.System;

namespace Agar.io_SFML.Extensions;

public static class MathExtensions
{
    private static Random _random = new();
    public static Vector2f Normalize(this Vector2f vector)
    {
        float vectorLength = MathF.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
        
        if (vectorLength <= 0.0001f)
        {
            return vector;
        }
        
        float inversedLength = 1 / vectorLength;
        
        vector *= inversedLength;

        return vector;
    }
    
    public static float GetSquaredDistanceTo(this Vector2f ownActor, Vector2f otherActor)
    {
        float dx = ownActor.X - otherActor.X;
        float dy = ownActor.Y - otherActor.Y;
        
        return dx * dx + dy * dy;
    }

    public static Vector2f GetRandomPosition(int maxX, int maxY)
    {
        int x = _random.Next(0, maxX);
        int y = _random.Next(0, maxY);
        
        return new (x, y);
    }
}