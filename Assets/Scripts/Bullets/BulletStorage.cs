using System;
using UnityEngine;
using Zenject;

public class BulletStorage : MonoBehaviour
{
    [SerializeField] private Bullet _bulletTemplate;
    [SerializeField] private PlayerViewStorage _playerViewStorage;
    [SerializeField] private int _poolSize;
    [SerializeField] private int _additionalPoolSize;

    private IAttackable _attacker;
    private IPool<Bullet> _pool;
    private ISoundContainer _soundContainer;
    private IDesktopInput _desktopInput;
    private BulletEffectSetter _effectSetter;

    private void OnEnable()
    {
        _attacker.Attacked += OnAttacked;
        _desktopInput.SwitchBulletButtonClicked += OnBulletClicked;
    }

    private void OnDisable()
    {
        _attacker.Attacked -= OnAttacked;
        _desktopInput.SwitchBulletButtonClicked -= OnBulletClicked;
    }

    private void Start()
    {
        _pool = new BulletPool();
        _effectSetter = new BulletEffectSetter();

        CreateBullets(_poolSize);
        SetParticleColor(_bulletTemplate.Color);  
    }

    [Inject]
    private void Constructor(IAttackable attacker, ISoundContainer soundContainer, IDesktopInput desktopInput)
    {
        _attacker = attacker;
        _soundContainer = soundContainer;
        _desktopInput = desktopInput;
    }

    private void SetParticleColor(Color color) => _playerViewStorage.SetParticleViewColor(color);

    private void CreateBullets(int poolSize)
    {
        for (int i = 0; i < poolSize; i++)
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
            _soundContainer.Play(bullet.Type);
        }
        else
        {
            CreateBullets(_additionalPoolSize);
        }
    }

    private void OnBulletClicked(int bulletIndex)
    {
        _soundContainer.Play(BulletType.Switch);
    }
}