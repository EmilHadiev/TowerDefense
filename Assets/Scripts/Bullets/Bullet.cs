using UnityEngine;

[RequireComponent(typeof(TriggerObserver))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BulletMover))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private BulletData _data;
    [field: SerializeField] public BulletType Type { get; private set; }

    private TriggerObserver _observer;    
    private IBulletPolicy _bulletPolicy;

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
        _bulletPolicy = new FireballBullet();
    }

    private void Update() => UpdateLifeTime();

    private void OnEntered(Collider collider)
    {
        if (collider.TryGetComponent(out IHealth health))
        {
            health.TakeDamage(_data.Damage);
            ChangeTargetParticleDamageColor(collider);
            _bulletPolicy.Accept(collider);
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