using UnityEngine;
using Zenject;

public class AmmunitionStorage : MonoBehaviour
{
    [SerializeField] private Bullet _bulletTemplate;
    [SerializeField] private Transform _container;
    [SerializeField] private int _poolSize;

    private IAttackable _attacker;
    private IPool<Bullet> _pool;

    private void OnEnable() => _attacker.Attacked += OnAttacked;
    private void OnDisable() => _attacker.Attacked -= OnAttacked;

    private void Start()
    {
        _pool = new BulletPool();

        CreateBullets();
    }

    [Inject]
    private void Constructor(IAttackable attacker) => _attacker = attacker;

    private void CreateBullets()
    {
        for (int i = 0; i < _poolSize; i++)
            CreateTemplate();
    }

    private void CreateTemplate()
    {
        Bullet bullet = Instantiate(_bulletTemplate);
        bullet.gameObject.SetActive(false);
        _pool.Add(bullet);
    }

    private void OnAttacked()
    {
        if (_pool.TryGet(out Bullet bullet))
        {
            bullet.transform.parent = null;
            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.rotation;
            bullet.gameObject.SetActive(true);
        }
        else
        {
            CreateBullets();
        }
    }
}