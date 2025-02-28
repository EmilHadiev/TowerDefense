using UnityEngine;
using Zenject;

public class BulletStorage : MonoBehaviour
{
    [SerializeField] private Bullet _bulletTemplate;
    [SerializeField] private PlayerViewStorage _playerViewStorage;
    [SerializeField] private int _poolSize;

    private IAttackable _attacker;
    private IPool<Bullet> _pool;
    private ISoundContainer _soundContainer;
    private BulletEffectSetter _effectSetter;

    private void OnEnable() => _attacker.Attacked += OnAttacked;
    private void OnDisable() => _attacker.Attacked -= OnAttacked;

    private void Start()
    {
        _pool = new BulletPool();
        _effectSetter = new BulletEffectSetter(_soundContainer);

        CreateBullets();
        SetParticleColor(_bulletTemplate.Color);  
    }

    [Inject]
    private void Constructor(IAttackable attacker, ISoundContainer soundContainer)
    {
        _attacker = attacker;
        _soundContainer = soundContainer;
    }

    private void SetParticleColor(Color color) => _playerViewStorage.SetParticleViewColor(color);

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
        _effectSetter.AddBullet(bullet);
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