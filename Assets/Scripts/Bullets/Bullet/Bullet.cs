using System;
using System.Collections.Generic;
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

    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Collider _collider;

    [field: SerializeField] public BulletData Data { get; private set; }
    [field: SerializeField] public BulletType Type { get; private set; }

    private TriggerObserver _observer;
    private readonly ReflectDamageCoefficientCalculator _reflectCalculator = new ReflectDamageCoefficientCalculator();    

    private IBulletMovable _movable;
    private IBulletEffectHandler _currentEffect;

    private IGunPlace _gunPlace;

    private IReadOnlyDictionary<Type, IBulletEffectHandler> _bulletEffects;

    public Color Color => Data.Color;

    public IBulletData BulletData => Data;
    public IBulletDescription BulletDescription => Data;

    private void OnValidate()
    {
        _rb ??= GetComponent<Rigidbody>();
        _collider ??= GetComponent<Collider>();
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
        PhysicsToggle(true);

        _observer.Entered += OnTargetHit;

        _timer.StartTimer(Data.LifeTime);
    }

    private void OnDisable()
    {
        PhysicsToggle(false);

        _observer.Entered -= OnTargetHit;
        ResetValues();
    }

    [Inject]
    private void Constructor(IPlayer player)
    {
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
            _gunPlace.CurrentGun.HandleAttack(collider);
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
        return _gunPlace.CurrentGun.Damage;
    }

    private void HideAfterCollided()
    {
        _observer.UnLock();
        gameObject.SetActive(false);
    }

    private void ResetValues()
    {
        _timer.ResetTimer();
        _reflectCalculator.ResetCoefficient();
    }

    private void PhysicsToggle(bool isOn)
    {
        if (isOn)
        {
            _rb.isKinematic = false;
            _collider.enabled = true;
        }
        else
        {
            _rb.isKinematic = true;
            _rb.velocity = Vector3.zero;
            _collider.enabled = false;
        }
    }
}