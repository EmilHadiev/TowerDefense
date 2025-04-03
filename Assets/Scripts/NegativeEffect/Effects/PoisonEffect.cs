using System.Collections;
using UnityEngine;

public class PoisonEffect : INegativeEffect
{
    private const int PoisonDuration = 5;
    private const int Tick = 1;

    private const int DamageReducer = 2;

    private readonly ICoroutinePefrormer _performer;
    private readonly EnemyRenderViewer _view;
    private readonly WaitForSeconds _delay;
    private readonly PlayerStat _playerStat;
    private readonly IHealth _health;

    private readonly Color _poisonColor = Color.green;

    private Coroutine _poisonCoroutine;

    private Color _startColor;

    private int _currentTime;
    private int _currentDamageMultiplier;

    private bool _isOn;

    public PoisonEffect(ICoroutinePefrormer performer, EnemyRenderViewer view, PlayerStat playerStat, IHealth health)
    {
        _performer = performer;
        _view = view;
        _delay = new WaitForSeconds(Tick);
        _playerStat = playerStat;
        _health = health;

        _isOn = false;
    }

    public void StartEffect()
    {
        StartPoison();

        if (_isOn == false)
            _poisonCoroutine = _performer.StartPerform(PoisonCoroutine());

        _isOn = true;
    }

    public void StopEffect()
    {
        if (_poisonCoroutine != null)
            _performer.StopPerform(_poisonCoroutine);

        StopPoison();
    }

    private IEnumerator PoisonCoroutine()
    {
        while (_currentTime >= 0)
        {
            yield return _delay;
            _currentTime -= Tick;
            _health.TakeDamage((int)GetDamage());
        }

        StopPoison();
    }

    private float GetDamage() => _playerStat.Damage / DamageReducer * _currentDamageMultiplier;

    private void StartPoison()
    {
        _startColor = _view.Color;
        _view.SetColor(_poisonColor);
        _currentTime = PoisonDuration;
        _currentDamageMultiplier++;
    }

    private void StopPoison()
    {
        _view.SetColor(_startColor);
        _currentDamageMultiplier = 1;
        _isOn = false;
    }
}