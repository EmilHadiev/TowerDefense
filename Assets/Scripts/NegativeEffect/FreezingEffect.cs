using System.Collections;
using UnityEngine;

public class FreezingEffect : INegativeEffect
{
    private const float PercentageSlowdown = 50;
    private const float SlowdownDuration = 2f;

    private readonly ICoroutinePefrormer _pefrormer;
    private readonly WaitForSeconds _delay;
    private readonly Property _speed;
    private readonly EnemyRenderViewer _view;

    private readonly Color _freezeColor = Color.blue;

    private Color _startColor;

    private Coroutine _slowdownCoroutine;

    private float _defaultSpeed;

    public FreezingEffect(ICoroutinePefrormer pefrormer, Property speed, EnemyRenderViewer view)
    {
        _delay = new WaitForSeconds(SlowdownDuration);
        _pefrormer = pefrormer;
        _speed = speed;
        _defaultSpeed = _speed.Value;
        _view = view;
    }

    public void StartEffect()
    {
        StopEffect();

        _slowdownCoroutine = _pefrormer.StartPerform(SlowdownCoroutine());
    }

    public void StopEffect()
    {
        if (_slowdownCoroutine != null && _pefrormer != null)
            _pefrormer.StopPerform(_slowdownCoroutine);

        StopFreeze();
    }

    private IEnumerator SlowdownCoroutine()
    {
        StartFreeze();
        yield return _delay;
        StopFreeze();
    }

    private void StartFreeze()
    {
        _startColor = _view.Color;
        _view.SetColor(_freezeColor);
        _speed.Value = GetFreezeSpeed();
    }

    private void StopFreeze()
    {
        _view.SetColor(_startColor);
        _speed.Value = _defaultSpeed;
    }

    private float GetFreezeSpeed() => _speed.Value / 100 * PercentageSlowdown;
}