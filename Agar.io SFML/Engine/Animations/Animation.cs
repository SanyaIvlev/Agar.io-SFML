﻿using SFML.Graphics;

namespace Agar.io_SFML.Animations;

public class Animation
{
    public Texture CurrentFrame;
    
    private List<Texture> _frames;

    private int _currentIndex;
    
    private long _progress;
    private long _neededTime;

    public Animation(Texture[] frames, long neededTimeInMilliseconds)
    {
        _frames = new List<Texture>();
        _frames.AddRange(frames);
        
        _neededTime = neededTimeInMilliseconds;
    }

    public void Start()
    {
        CurrentFrame = _frames[0];
    }

    public void Update()
    {
        _progress += Time.ElapsedTime;
        
        if(_progress < _neededTime)
            return;

        _progress = 0;
        
        _currentIndex++;
        
        if(_currentIndex >= _frames.Count)
            _currentIndex = 0;
        
        CurrentFrame = _frames[_currentIndex];
    }
}