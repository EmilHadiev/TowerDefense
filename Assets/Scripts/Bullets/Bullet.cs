using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TriggerObserver))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BulletMover))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private BulletData _data;
    [field: SerializeField] public BulletType Type { get; private set; }

    private TriggerObserver _observer;    
    private IBulletEffectHandler _bulletEffect;

    private Dictionary<Type, IBulletEffectHandler> _bulletEffects;

    private float _tick;

    public Color Color => _data.Color;

    private void Awake() => _observer = GetComponent<TriggerObserver>();

    private void OnEnable()
    {
        _observer.Entered += OnEntered;
        _observer.Exited += OnExited;
    }

    private void OnDisable()
    {
        _observer.Entered -= OnEntered;
        _observer.Exited -= OnExited;

        _tick = 0;
    }

    private void Start()
    {
        _bulletEffects = new Dictionary<Type, IBulletEffectHandler>(10)
        {
            [typeof(IceBulletEffect)] = new IceBulletEffect(),
            [typeof(FireballBulletEffect)] = new FireballBulletEffect(_data),
            [typeof(ElectricBulletEffect)] = new ElectricBulletEffect(transform, _data),
            [typeof(BulletPushingEffect)] = new BulletPushingEffect(),
            [typeof(BulletEmptyEffect)] = new BulletEmptyEffect(),
        };

        SetEffect<ElectricBulletEffect>();
    }

    private void Update() => UpdateLifeTime();

    public void SetEffect<T>() where T : IBulletEffectHandler
    {
        if (_bulletEffects.TryGetValue(typeof(T), out IBulletEffectHandler handler))
            _bulletEffect = handler;
        else
            throw new ArgumentOutOfRangeException(nameof(T));
    }

    public void SetSound(Action<BulletType> setBulletSound) => setBulletSound?.Invoke(Type);

    private void OnEntered(Collider collider)
    {
        if (collider.TryGetComponent(out IHealth health))
        {
            health.TakeDamage(_data.Damage);
            ChangeTargetParticleDamageColor(collider);
            _bulletEffect.HandleEffect(collider);
            HideAfterCollided();
        }
    }

    private void ChangeTargetParticleDamageColor(Collider collider)
    {
        if (collider.TryGetComponent(out IParticleViewContainer viewContainer))
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
}