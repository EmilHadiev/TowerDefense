using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(TriggerObserver))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BulletMover))]
[RequireComponent(typeof(LifetimeTimer))]
[RequireComponent(typeof(BulletEffectHandlerContainer))]
[RequireComponent(typeof(BulletEffectView))]
public class Bullet : MonoBehaviour, IBulletDefinition
{
    [SerializeField] private LifetimeTimer _timer;
    [SerializeField] private BulletEffectHandlerContainer _effectsContainer;
    [SerializeField] private BulletEffectView _effectsView;

    [field: SerializeField] public BulletData Data { get; private set; }
    [field: SerializeField] public BulletType Type { get; private set; }

    private TriggerObserver _observer;
    private readonly ReflectDamageCoefficientCalculator _reflectCalculator = new ReflectDamageCoefficientCalculator();    

    private IBulletMovable _movable;
    private IBulletEffectHandler _currentEffect;

    private PlayerStat _playerStat;
    private IGunPlace _gunPlace;

    private IReadOnlyDictionary<Type, IBulletEffectHandler> _bulletEffects;

    public Color Color => Data.Color;

    public IBulletData BulletData => Data;
    public IBulletDescription BulletDescription => Data;

    private void OnValidate()
    {
        _timer ??= GetComponent<LifetimeTimer>();
        _effectsContainer ??= GetComponent<BulletEffectHandlerContainer>();
        _effectsView ??= GetComponent<BulletEffectView>();
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

    [Inject]
    private void Constructor(PlayerStat stat, IPlayer player)
    {
        _playerStat = stat;
        _gunPlace = player.GunPlace;
    }

    public void InitEffects(Action<int> setEffect)
    {
        _effectsContainer.SetEffects(setEffect);
        _bulletEffects = _effectsContainer.Effects;
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
        _effectsView.ChangeTargetParticleColor(collider, Color);
        _currentEffect.HandleEffect(collider);
    }

    private float GetDamage()
    {
        if (Data.Damage == 0)
            return 0;

        float totalDamage = Data.Damage + _playerStat.Damage + GetReflectedDamage();
        Debug.Log(_gunPlace == null);
        Debug.Log($"Было: {totalDamage} стало: {totalDamage + totalDamage * (_gunPlace.CurrentGun.DamagePercent / 100)}" );
        return totalDamage + totalDamage * (_gunPlace.CurrentGun.DamagePercent / 100);
    }

    private void HideAfterCollided()
    {
        _observer.UnLock();
        gameObject.SetActive(false);
    }

    private void OnReflected() => _reflectCalculator.UpCoefficient();

    private float GetReflectedDamage() => (Data.Damage + _playerStat.Damage) * _reflectCalculator.Coefficient;

    private void ResetValues()
    {
        _timer.ResetTimer();
        _reflectCalculator.ResetCoefficient();
    }
}