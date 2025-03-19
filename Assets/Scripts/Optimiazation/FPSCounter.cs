using System;
using TMPro;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private int _frameCount;
    private float _prevTime;

    private int _currentFPS;

    private void Update()
    {
        _frameCount++;
        float timePassed = Time.realtimeSinceStartup - _prevTime;

        if (timePassed >= 1f)
        {
            _currentFPS = Convert.ToInt32(_frameCount / timePassed);
            _frameCount = 0;
            _prevTime = Time.realtimeSinceStartup;

            _text.text = _currentFPS.ToString();
        }
    }
}