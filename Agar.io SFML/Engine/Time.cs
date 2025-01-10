using System.Diagnostics;
using SFML.System;

namespace Agar.io_SFML;

public static class Time
{
    public static long ElapsedTime { get; private set; }
    private static Clock timer = new();

    public static void Start()
    {
        ElapsedTime = 0;
        timer.Restart();
    }

    public static void Update()
    {
        ElapsedTime = timer.ElapsedTime.AsMilliseconds();
        timer.Restart();
    }
    
    public static long GetElapsedTimeAsMicroseconds()
        => ElapsedTime * 1000;
}