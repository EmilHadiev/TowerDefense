using System;
using System.Collections;
using UnityEngine;

public class FreezingEffect : INegativeEffect
{
    private const float PercentageSlowdown = 50;
    private const float SlowdownDuration = 2f;

    private readonly ICoroutinePefrormer _pefrormer;
    private readonly WaitForSeconds _delay;
    private readonly SpeedProperty _speed;

    private Coroutine _slowdownCoroutine;

    private float _defaultSpeed;

    public FreezingEffect(ICoroutinePefrormer pefrormer, SpeedProperty speed)
    {
        _delay = new WaitForSeconds(SlowdownDuration);
        _pefrormer = pefrormer;
        _speed = speed;
        _defaultSpeed = _speed.Speed;
    }

    public void StartEffect()
    {
        StopEffect();

        Debug.Log("Замедляю на 50%");
        _slowdownCoroutine = _pefrormer.StartPerform(SlowdownCoroutine());
    }

    public void StopEffect()
    {
        if (_slowdownCoroutine != null && _pefrormer != null)
            _pefrormer.StopPerform(_slowdownCoroutine);

        StopFreeze();
        Debug.Log("Перестаю замедлять на 50%");
    }

    private IEnumerator SlowdownCoroutine()
    {
        StartFreeze();
        yield return _delay;
        StopFreeze();
    }

    private void StartFreeze() => _speed.Speed = GetFreezeSpeed();

    private void StopFreeze() => _speed.Speed = _defaultSpeed;

    private float GetFreezeSpeed() => _speed.Speed / 100 * PercentageSlowdown;
}