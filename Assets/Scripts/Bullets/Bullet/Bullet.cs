using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(TriggerObserver))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BulletMover))]
[RequireComponent(typeof(LifetimeTimer))]
[RequireComponent(typeof(BulletEffectHandlerContainer))]
public class Bullet : MonoBehaviour, IBulletDefinition
{
    [SerializeField] private LifetimeTimer _timer;
    [SerializeField] private BulletEffectHandlerContainer _effectsContainer;

    [field: SerializeField] public BulletData Data { get; private set; }
    [field: SerializeField] public BulletType Type { get; private set; }

    private TriggerObserver _observer;
    private readonly ReflectDamageCalculator _reflectCalculator = new ReflectDamageCalculator();
    private BulletEffectView _effects;

    private IBulletMovable _movable;
    private IBulletEffectHandler _currentEffect;

    private PlayerStat _playerStat;

    private IReadOnlyDictionary<Type, IBulletEffectHandler> _bulletEffects;

    public Color Color => Data.Color;

    public IBulletData BulletData => Data;
    public IBulletDescription BulletDescription => Data;

    private void OnValidate()
    {
        _timer ??= GetComponent<LifetimeTimer>();
        _effectsContainer ??= GetComponent<BulletEffectHandlerContainer>();
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
        //_effects.PlaySound(Type);
    }

    private void OnDisable()
    {
        _observer.Entered -= OnTargetHit;
        _movable.Reflected -= OnReflected;
        ResetValues();
    }

    public void InitEffects(Action<int> setEffect)
    {
        _effectsContainer.SetEffects(setEffect);
        _bulletEffects = _effectsContainer.Effects;
    }

    [Inject]
    private void Constructor(PlayerStat stat, ISoundContainer soundContainer)
    {
        _playerStat = stat;
        _effects = new BulletEffectView(soundContainer);
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
            HandleCollision(collider);
            HideAfterCollided();
        }
    }

    private void HandleCollision(Collider collider)
    {
        _effects.ChangeTargetParticleColor(collider, Color);
        _currentEffect.HandleEffect(collider);
    }

    private float GetDamage()
    {
        if (Data.Damage == 0)
            return 0;

        return Data.Damage + _playerStat.Damage + GetReflectedDamage();
    }

    private void HideAfterCollided()
    {
        _observer.UnLock();
        gameObject.SetActive(false);
    }

    private void OnReflected() => _reflectCalculator.UpDamage();

    private float GetReflectedDamage() => (Data.Damage + _playerStat.Damage) * _reflectCalculator.Damage;

    private void ResetValues()
    {
        _timer.ResetTimer();
        _reflectCalculator.ResetDamage();
    }
}