using UnityEngine;

[RequireComponent(typeof(TriggerObserver))]
[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private BulletData _data;
    [field: SerializeField] public BulletType Type { get; private set; }

    private TriggerObserver _observer;
    private IMover _mover;

    private float _tick;

    private void Awake() => _observer = GetComponent<TriggerObserver>();

    private void Start() => SetMover(new BulletMoverPattern(_data, transform));

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

    private void Update()
    {
        _mover.Update();
        UpdateLifeTime();
    }

    private void SetMover(IMover mover)
    {
        _mover?.StopMove();
        _mover = mover;
        _mover.StartMove();
    }

    private void OnEntered(Collider collider)
    {
        if (collider.TryGetComponent(out IHealth health))
        {
            health.TakeDamage(_data.Damage);
            gameObject.SetActive(false);
        }
    }

    private void OnExited(Collider collider)
    {
       
    }

    private void UpdateLifeTime()
    {
        _tick += Time.deltaTime;

        if (_tick >= _data.LifeTime)
            Hide();
    }

    private void Hide() => gameObject.SetActive(false);
}