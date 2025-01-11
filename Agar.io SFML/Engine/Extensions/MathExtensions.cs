using SFML.System;

namespace Agar.io_SFML.Extensions;

public static class MathExtensions
{
    public static Vector2f Normalize(this Vector2f vector)
    {
        float vectorLength = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
        float inversedLength = 1 / vectorLength;
        
        vector *= inversedLength;

        return vector;
    }
}