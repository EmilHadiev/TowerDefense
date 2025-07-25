using DG.Tweening;
using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(ObstacleHealth))]
public class GoldenChest : MapEventObject
{
    [SerializeField] private ObstacleHealth _health;

    private const int X = -6;
    private const int Duration = 1;

    private ICoinStorage _coinStorage;
    private Tween _tween;

    [Inject]
    private void Constructor(ICoinStorage coinStorage)
    {
        _coinStorage = coinStorage;
    }

    private void OnValidate()
    {
        _health ??= GetComponent<ObstacleHealth>();
    }

    private void Awake()
    {
        _tween = transform.DOMoveX(X, Duration).SetLoops(-1, LoopType.Yoyo);
        _tween.Pause();
    }

    private void OnEnable()
    {
        _health.DamageApplied += DamageApplied;
    }

    private void OnDisable()
    {
        _health.DamageApplied -= DamageApplied;
        _tween.Pause();
    }

    public override void Activate()
    {
        _tween.Play();
    }

    private void DamageApplied(float damage)
    {
        _coinStorage.Add((int)damage);
    }
}