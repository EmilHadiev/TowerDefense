using System;
using UnityEngine;
using Zenject;

public class Optimizator : ITickable, IDisposable, IInitializable
{
    private int _frameCount;
    private float _prevTime;

    private int _steps = 0;
    private float _fpsSum = 0;

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
            _steps++;
            _fpsSum += CurrentFPS;
        }
    }

    public void Dispose()
    {
        Debug.Log("avg fps = " + GetAverageFPS());
    }

    public void Initialize()
    {
        QualitySettings.SetQualityLevel(0);
        Debug.Log("Current quality level + " + QualitySettings.GetQualityLevel());
    }

    private float GetAverageFPS() => _fpsSum / _steps;
}