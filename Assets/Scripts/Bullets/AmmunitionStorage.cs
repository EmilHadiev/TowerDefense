using UnityEngine;
using Zenject;

public class AmmunitionStorage : MonoBehaviour
{
    [SerializeField] private Bullet _bulletTemplate;
    [SerializeField] private Transform _container;
    [SerializeField] private int _poolSize;

    private IAttackable _attacker;
    private BulletPool _pool;

    private void OnEnable() => _attacker.Attacked += OnAttacked;
    private void OnDisable() => _attacker.Attacked -= OnAttacked;

    private void Start()
    {
        _pool = new BulletPool();

        for (int i = 0; i < _poolSize; i++)
            CreateTemplate();
    }

    [Inject]
    private void Constructor(IAttackable attacker) => _attacker = attacker;

    private void CreateTemplate()
    {
        Bullet bullet = Instantiate(_bulletTemplate, _container);
        bullet.gameObject.SetActive(false);
        _pool.Add(bullet);
    }

    private void OnAttacked()
    {
        if (_pool.TryGet(out Bullet bullet))
        {
            bullet.gameObject.SetActive(true);
            bullet.transform.parent = null;
            bullet.transform.position = transform.position;
        }
            
    }
}