using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TriggerObserver))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BulletMover))]
public class Bullet : MonoBehaviour, IBullet
{
    [SerializeField] private BulletData _data;
    [field: SerializeField] public BulletType Type { get; private set; }

    private TriggerObserver _observer;

    private IBulletMovable _movable;
    private IBulletEffectHandler _bulletEffect;

    private PlayerStat _playerStat;

    private Dictionary<Type, IBulletEffectHandler> _bulletEffects;

    private float _tick;
    private float _sumReflectedСoefficients;

    public Color Color => _data.Color;

    public IBulletData BulletData => _data;
    public IBulletDescription BulletDescription => _data;

    private void Awake()
    {
        _observer = GetComponent<TriggerObserver>();
        _movable = GetComponent<BulletMover>();
    }

    private void OnEnable()
    {
        _observer.Entered += OnEntered;
        _observer.Exited += OnExited;

        _movable.Reflected += OnReflected;
    }

    private void OnDisable()
    {
        _observer.Entered -= OnEntered;
        _observer.Exited -= OnExited;

        _movable.Reflected -= OnReflected;
        ResetValues();
    }

    private void Update() => UpdateLifeTime();

    public void InitBullet(PlayerStat playerStat, IHealth playerHealth, ICoinStorage coinStorage, Action<int> setEffect)
    {
        _playerStat = playerStat;

        _bulletEffects = new Dictionary<Type, IBulletEffectHandler>(10)
        {
            [typeof(EmptyBulletEffect)] = new EmptyBulletEffect(),
            [typeof(SlowdownBulletEffect)] = new SlowdownBulletEffect(),
            [typeof(ExtraDamageBulletEffect)] = new ExtraDamageBulletEffect(_data, _playerStat),
            [typeof(SplashBulletEffect)] = new SplashBulletEffect(transform, _data, _playerStat),
            [typeof(PushingBulletEffect)] = new PushingBulletEffect(),
            [typeof(DeadlyBulletEffect)] = new DeadlyBulletEffect(),
            [typeof(PoisonBulletEffect)] = new PoisonBulletEffect(),
            [typeof(VampirismEffect)] = new VampirismEffect(playerHealth, _playerStat),
            [typeof(GoldenBulletEffect)] = new GoldenBulletEffect(coinStorage),
            [typeof(RandomBulletEffect)] = new RandomBulletEffect(setEffect)
        };
    }

    public void SetEffect<T>() where T : IBulletEffectHandler
    {
        if (_bulletEffects.TryGetValue(typeof(T), out IBulletEffectHandler handler))
            _bulletEffect = handler;
        else
            throw new ArgumentOutOfRangeException(nameof(T));
    }

    private void OnEntered(Collider collider)
    {
        if (collider.TryGetComponent(out IHealth health))
        {
            health.TakeDamage(GetDamage());
            ChangeTargetParticleDamageColor(collider);
            _bulletEffect.HandleEffect(collider);
            HideAfterCollided();
        }
    }

    private float GetDamage()
    {
        if (_data.Damage == 0)
            return 0;

        return _data.Damage + _playerStat.Damage + GetReflectedDamage();
    }

    private void ChangeTargetParticleDamageColor(Collider collider)
    {
        if (collider.TryGetComponent(out IParticleColorChangable viewContainer))
            viewContainer.SetDamageParticleColor(_data.Color);
    }

    private void HideAfterCollided()
    {
        _observer.UnLock();
        Hide();
    }

    private void OnExited(Collider collider)
    {

    }

    private void OnReflected() => _sumReflectedСoefficients += Constants.ReflectedCoefficient;

    private float GetReflectedDamage() => (_data.Damage + _playerStat.Damage) * _sumReflectedСoefficients;

    private void UpdateLifeTime()
    {
        _tick += Time.deltaTime;
        HideAfterDelay();
    }

    private void HideAfterDelay()
    {
        if (_tick >= _data.LifeTime)
            Hide();
    }

    private void Hide() => gameObject.SetActive(false);

    private void ResetValues()
    {
        _tick = 0;
        _sumReflectedСoefficients = 0;
    }
}