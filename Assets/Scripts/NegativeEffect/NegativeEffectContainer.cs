using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class NegativeEffectContainer : MonoBehaviour, INegativeEffectContainer
{
    private Property _speed;
    private EnemyRenderViewer _view;

    private Dictionary<Type, INegativeEffect> _negativeEffects;
    private ICoroutinePefrormer _pefrormer;
    private INegativeEffect _effect;
    private PlayerStat _playerStat;

    private IHealth _enemyHealth;

    private void Awake()
    {
        _speed = GetComponent<IMovable>().Speed;
        _view = GetComponent<EnemyRenderViewer>();
        _enemyHealth = GetComponent<IHealth>();
    }

    private void Start()
    {
        _negativeEffects = new Dictionary<Type, INegativeEffect>(10)
        {
            [typeof(FreezingEffect)] = new FreezingEffect(_pefrormer, _speed, _view),
            [typeof(PoisonEffect)] = new PoisonEffect(_pefrormer, _view, _playerStat, _enemyHealth),
        };
    }

    private void OnEnable()
    {
        if (_effect != null)
            _effect.StopEffect();
    }

    [Inject]
    private void Constructor(ICoroutinePefrormer pefrormer, PlayerStat playerStat)
    {
        _pefrormer = pefrormer;
        _playerStat = playerStat;
    }

    public void Activate<T>() where T : INegativeEffect
    {
        if (_negativeEffects.TryGetValue(typeof(T), out INegativeEffect effect))
        {
            effect.StartEffect();
            _effect = effect;
        }
        else
        {
            throw new ArgumentOutOfRangeException(nameof(T));
        }
    }
}