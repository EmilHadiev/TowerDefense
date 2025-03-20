using System;
using UnityEngine;
using Zenject;

public class FPSCounter : ITickable
{
    private int _frameCount;
    private float _prevTime;

    public int CurrentFPS { get; private set; }
    public bool IsEnoughFPS => CurrentFPS >= Constants.MinFPS;

    public void Tick()
    {
        _frameCount++;
        float timePassed = Time.realtimeSinceStartup - _prevTime;

        if (timePassed >= 1f)
        {
            CurrentFPS = Convert.ToInt32(_frameCount / timePassed);
            _frameCount = 0;
            _prevTime = Time.realtimeSinceStartup;
        }
    }
}