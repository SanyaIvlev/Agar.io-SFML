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

    public static void Start()
    {
        ElapsedTime = 0;
        _timer = new();
        _timer.Restart();
    }

    public static void Update()
    {
        ElapsedTime = _timer.ElapsedTime.AsMilliseconds();
        _timer.Restart();
    }
    
    public static long GetElapsedTimeAsMicroseconds()
        => ElapsedTime * 1000;

    public static float GetElapsedTimeAsSeconds()
        => ElapsedTime / 1000f;
}