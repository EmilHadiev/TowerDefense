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

    private void Awake()
    {
        _speed = GetComponent<IMovable>().Speed;
        _view = GetComponent<EnemyRenderViewer>();
    }

    private void Start()
    {
        _negativeEffects = new Dictionary<Type, INegativeEffect>(10)
        {
            [typeof(FreezingEffect)] = new FreezingEffect(_pefrormer, _speed, _view),
        };
    }

    private void OnEnable()
    {
        if (_effect != null)
            _effect.StopEffect();
    }

    [Inject]
    private void Constructor(ICoroutinePefrormer pefrormer) => _pefrormer = pefrormer;

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