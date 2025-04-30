using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TriggerObserver))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BulletMover))]
[RequireComponent(typeof(LifetimeTimer))]
public class Bullet : MonoBehaviour, IBulletDefinition
{
    [SerializeField] private LifetimeTimer _timer;
    [field: SerializeField] public BulletData Data { get; private set; }
    [field: SerializeField] public BulletType Type { get; private set; }

    private TriggerObserver _observer;
    private readonly ReflectDamageCalculator _reflectCalculator = new ReflectDamageCalculator();

    private IBulletMovable _movable;
    private IBulletEffectHandler _currentEffect;

    private PlayerStat _playerStat;

    private Dictionary<Type, IBulletEffectHandler> _bulletEffects;

    public Color Color => Data.Color;

    public IBulletData BulletData => Data;
    public IBulletDescription BulletDescription => Data;

    private void OnValidate()
    {
        _timer ??= GetComponent<LifetimeTimer>();
    }

    private void Awake()
    {
        _observer = GetComponent<TriggerObserver>();
        _movable = GetComponent<BulletMover>();
    }

    private void OnEnable()
    {
        _observer.Entered += OnTargetHit;
        _movable.Reflected += OnReflected;

        _timer.StartTimer(Data.LifeTime);
    }

    private void OnDisable()
    {
        _observer.Entered -= OnTargetHit;
        _movable.Reflected -= OnReflected;
        ResetValues();
    }

    public void InitBullet(PlayerStat stat, IReadOnlyDictionary<Type, IBulletEffectHandler> bulletEffects)
    {
        _playerStat = stat;
        _bulletEffects = new Dictionary<Type, IBulletEffectHandler>(bulletEffects);
    }

    public void SetEffect<T>() where T : IBulletEffectHandler
    {
        if (_bulletEffects.TryGetValue(typeof(T), out IBulletEffectHandler handler))
            _currentEffect = handler;
        else
            throw new ArgumentException(nameof(T));
    }

    private void OnTargetHit(Collider collider)
    {
        if (collider.TryGetComponent(out IHealth health))
        {
            health.TakeDamage(GetDamage());
            ChangeTargetParticleDamageColor(collider);
            _currentEffect.HandleEffect(collider);
            HideAfterCollided();
        }
    }

    private float GetDamage()
    {
        if (Data.Damage == 0)
            return 0;

        return Data.Damage + _playerStat.Damage + GetReflectedDamage();
    }

    private void ChangeTargetParticleDamageColor(Collider collider)
    {
        if (collider.TryGetComponent(out IParticleColorChangable viewContainer))
            viewContainer.SetDamageParticleColor(Data.Color);
    }

    private void HideAfterCollided() => _observer.UnLock();

    private void OnReflected() => _reflectCalculator.UpDamage();

    private float GetReflectedDamage() => (Data.Damage + _playerStat.Damage) * _reflectCalculator.Damage;

    private void ResetValues()
    {
        _timer.ResetTimer();
        _reflectCalculator.ResetDamage();
    }
}