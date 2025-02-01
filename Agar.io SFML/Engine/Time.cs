using SFML.System;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Agar.io_SFML;

public static class Time
{
    public static long ElapsedTime
    {
        get => _timer.ElapsedTime.AsMilliseconds();
        private set { }
    }

    private static Clock _timer;
    
    
    private static long totalTimeBeforeUpdate = 0;
    private static long previousTimeElapsed = 0;
    private static long deltaTime;

    public static void Start()
    {
        ElapsedTime = 0;
        _timer = new();
        _timer.Restart();
    }

    public static void UpdateTimer()
    {
        totalTimeBeforeUpdate = 0;
        
        ElapsedTime = _timer.ElapsedTime.AsMilliseconds();
        _timer.Restart();
    }

    public static long UpdateTimeBeforeUpdate()
    {
        long elapsedTime = GetElapsedTimeAsMicroseconds();
            
        deltaTime = Math.Abs(elapsedTime - previousTimeElapsed);
        previousTimeElapsed = elapsedTime;
            
        totalTimeBeforeUpdate += deltaTime;
        
        return totalTimeBeforeUpdate;
    }
    
    public static long GetElapsedTimeAsMicroseconds()
        => ElapsedTime * 1000;

    public static float GetElapsedTimeAsSeconds()
        => ElapsedTime / 1000f;
}